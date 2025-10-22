package com.example.lab1_3;

import static org.junit.jupiter.api.Assertions.*;
import java.io.FileOutputStream;
import java.io.IOException;
import java.time.LocalDate;
import org.apache.poi.ss.usermodel.*;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.ActiveProfiles;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@SpringBootTest
@ActiveProfiles("test")
class DemoApplicationTests {

	@Test
	void writeIntoXLS() {
		var file = "test";
		try (var book = new HSSFWorkbook();) {
			var sheet = book.createSheet("Birthdays");
			var row = sheet.createRow(0);

			var name = row.createCell(0);
			name.setCellValue("John");

			var birthdate = row.createCell(1);
			var format = book.createDataFormat();
			var dateStyle = book.createCellStyle();
			dateStyle.setDataFormat(format.getFormat("dd.mm.yyyy"));
			birthdate.setCellStyle(dateStyle);
			var date = DateUtil.getExcelDate(LocalDate.of(2010, 12, 30), false);
			birthdate.setCellValue(date);

			sheet.autoSizeColumn(1);
			book.write(new FileOutputStream(file + ".xls"));
			assertEquals(1, sheet.getLastRowNum() + 1);
		} catch (IOException e) {
			log.error(e.toString());
		}
	}
}
