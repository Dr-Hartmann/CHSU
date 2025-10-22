package com.example.lab3;

import java.io.FileInputStream;
import java.io.IOException;
import java.math.BigDecimal;
import java.sql.*;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.StreamSupport;
import org.apache.poi.ss.usermodel.*;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@Component
public class Lab3 {
    @Value("${spring.datasource.url}")
    private String url;

    @Value("${spring.datasource.username}")
    private String username;

    @Value("${spring.datasource.password}")
    private String password;

    public void prepareTable(String file) {
        try (var conn = getConnection();
                var stmt = conn.createStatement();
                var in = new FileInputStream(file);
                var book = file.endsWith(".xls") ? new HSSFWorkbook(in) : new XSSFWorkbook(in)) {

            conn.setAutoCommit(false);
            stmt.execute(lab3sql);
            checkTables(conn.getMetaData());

            var formatter = new DataFormatter();

            int[] sheets = { 2, 1, 0 };
            for (int s : sheets) {
                var sheet = book.getSheetAt(s);
                var sheetName = sheet.getSheetName().strip().replace(" ", "_");
                var headers = getHeaders(sheet.getRow(0));

                for (var r = 1; r <= sheet.getLastRowNum(); ++r) {
                    var row = sheet.getRow(r);
                    if (row == null)
                        continue;
                    var values = getValues(row, formatter);
                    insertRow(sheetName, conn, values, headers);
                }
                checkTable(sheetName, stmt);
            }
            conn.commit();
            giveAnswer(stmt);
        } catch (SQLException | IOException e) {
            log.error(e.toString());
        }
    }

    private List<String> getValues(Row row, DataFormatter formatter) {
        return StreamSupport.stream(row.spliterator(), false)
                .map(c -> {
                    if (c.getCellType() == CellType.NUMERIC && DateUtil.isCellDateFormatted(c)) {
                        var d = c.getLocalDateTimeCellValue().toLocalDate();
                        return "'" + d.format(DateTimeFormatter.ofPattern("yyyy-MM-dd")) + "'";
                    } else if (c.getCellType() == CellType.NUMERIC) {
                        var val = formatter.formatCellValue(c).strip().replace(',', '.');
                        return "'" + new BigDecimal(val) + "'";
                    }
                    return "'" + formatter.formatCellValue(c).strip() + "'";
                })
                .toList();
    }

    private void insertRow(String sheetName, Connection conn, List<String> values, List<String> headers)
            throws SQLException {
        try (var pstmt = conn.prepareStatement("INSERT INTO " + "\"" + sheetName + "\""
                + "(" + String.join(",", headers) + ") "
                + "VALUES (" + String.join(",", values) + ");")) {
            pstmt.execute();
        }
    }

    private void checkTable(String sheetName, Statement stmt) throws SQLException {
        var out = new StringBuilder();
        try (var resultSet = stmt.executeQuery("SELECT * FROM \"" + sheetName + "\" LIMIT 50;")) {
            var columnCount = resultSet.getMetaData().getColumnCount();
            while (resultSet.next()) {
                out.append("\n");
                for (var i = 1; i <= columnCount; i++) {
                    out.append(resultSet.getObject(i).toString()).append(" ");
                }
            }
        }
        var msg = out.toString();
        log.info("Лист: {} -- {}", sheetName, msg);
    }

    private void giveAnswer(Statement stmt) throws SQLException {
        var out = new StringBuilder();
        try (var resultSet = stmt.executeQuery(marselSQL)) {
            var columnCount = resultSet.getMetaData().getColumnCount();
            while (resultSet.next()) {
                for (var i = 1; i <= columnCount; i++) {
                    out.append(resultSet.getObject(i).toString()).append(" ");
                }
            }
        }
        var msg = out.toString();
        log.info("Ответ на вопрос: {}", msg);
    }

    private List<String> getHeaders(Row row) {
        return StreamSupport.stream(row.spliterator(), false)
                .filter(c -> c.getCellType() != CellType.BLANK)
                .map(Cell::getStringCellValue)
                .filter(cValue -> cValue != null && !cValue.isBlank())
                .map(c -> "\"".concat(
                        c.strip().replace(" ", "_"))
                        .concat("\""))
                .toList();
    }

    private void checkTables(DatabaseMetaData meta) throws SQLException {
        var names = new ArrayList<String>();
        names.add("Магазин");
        names.add("Товар");
        names.add("Движение_товаров");

        for (var name : names) {
            if (!tableExist(meta, name)) {
                throw new IllegalStateException("Таблица не найдена: " + name);
            }
        }
    }

    private String lab3sql = """
            CREATE TABLE IF NOT EXISTS "Магазин" (
                "ID_магазина" VARCHAR(50) PRIMARY KEY,
                "Район" VARCHAR(100) NOT NULL,
                "Адрес" VARCHAR(100) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS "Товар" (
                "Артикул" INT PRIMARY KEY,
                "Отдел" VARCHAR(100) NOT NULL,
                "Наименование_товара" VARCHAR(200) NOT NULL,
                "Ед._изм" VARCHAR(50) NOT NULL,
                "Количество_в_упаковке" DECIMAL(10,2) NOT NULL,
                "Поставщик" VARCHAR(100) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS "Движение_товаров" (
                "ID_операции" INT PRIMARY KEY,
                "Дата" DATE NOT NULL,
                "ID_магазина" VARCHAR(100) NOT NULL,
                "Артикул" INT NOT NULL,
                "Количество_упаковок,_шт." INT NOT NULL,
                "Тип_операции" VARCHAR(50) NOT NULL,
                "Цена_руб./шт." INT NOT NULL,
                FOREIGN KEY ("ID_магазина") REFERENCES "Магазин"("ID_магазина"),
                FOREIGN KEY ("Артикул") REFERENCES "Товар"("Артикул")
            );
            """;

    private String marselSQL = """
            SELECT SUM(
                CASE
                    WHEN dt."Тип_операции" = 'Поступление' THEN t."Количество_в_упаковке" * dt."Количество_упаковок,_шт."
                    WHEN dt."Тип_операции" = 'Продажа' THEN - t."Количество_в_упаковке" * dt."Количество_упаковок,_шт."
                    ELSE 0
                END
            ) AS Изменение_запаса_кг
            FROM "Движение_товаров" dt
            JOIN "Товар" t ON dt."Артикул" = t."Артикул"
            JOIN "Магазин" m ON dt."ID_магазина" = m."ID_магазина"
            WHERE t."Наименование_товара" = 'Творог 9% жирности'
                AND m."Район" = 'Заречный';
            """;

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection(url, username, password);
    }

    private boolean tableExist(DatabaseMetaData meta, String table) throws SQLException {
        try (var rs = meta.getTables(null, "PUBLIC", table, new String[] { "TABLE" })) {
            return rs.next();
        }
    }
}
