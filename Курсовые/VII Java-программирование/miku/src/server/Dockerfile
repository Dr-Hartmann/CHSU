#FROM maven:3.9-eclipse-temurin-25-alpine AS builder
FROM dockerhub.timeweb.cloud/library/maven:3.9-eclipse-temurin-25-alpine AS builder
WORKDIR /build
COPY pom.xml .
COPY common/pom.xml common/
COPY server/pom.xml server/

RUN sed -i '/<module>client<\/module>/d' pom.xml

RUN --mount=type=cache,target=/root/.m2 \
    mvn dependency:go-offline -pl server -am -e -Dmaven.test.skip=true -Dmaven.compile.fork=false

COPY common/src common/src
COPY server/src server/src
RUN --mount=type=cache,target=/root/.m2 \
    mvn clean package -DskipTests

FROM dockerhub.timeweb.cloud/library/eclipse-temurin:25-jdk-alpine AS layers
WORKDIR /extract
COPY --from=builder /build/server/target/*.jar app.jar
RUN java -Djarmode=layertools -jar app.jar extract

FROM dockerhub.timeweb.cloud/library/eclipse-temurin:25-jre-alpine
WORKDIR /application

# Создание группы и пользователя только для запуска приложения
RUN addgroup -g 1001 -S springgroup && adduser -u 1001 -S springuser -G springgroup \
    && mkdir -p /application/logs && chown -R springuser:springgroup /application

# JVM настройки для оптимизации производительности
ENV JAVA_OPTS="-XX:+UseG1GC -XX:MaxGCPauseMillis=200 -XX:G1HeapRegionSize=16m -XX:G1ReservePercent=20 -XX:InitiatingHeapOccupancyPercent=15 -XX:MaxRAMPercentage=75.0 -XX:+ParallelRefProcEnabled -Dspring.jmx.enabled=false -Dcom.sun.management.jmxremote -Dcom.sun.management.jmxremote.authenticate=false -Dcom.sun.management.jmxremote.ssl=false"

USER springuser

COPY --from=layers /extract/dependencies/ ./
COPY --from=layers /extract/spring-boot-loader/ ./
COPY --from=layers /extract/snapshot-dependencies/ ./
COPY --from=layers /extract/application/ ./

ENTRYPOINT ["sh", "-c", "java $JAVA_OPTS org.springframework.boot.loader.launch.JarLauncher"]