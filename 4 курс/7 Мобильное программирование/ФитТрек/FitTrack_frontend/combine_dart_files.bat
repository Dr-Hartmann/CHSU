@echo off
setlocal enabledelayedexpansion
set output_file=all_dart_files.txt
if exist %output_file% del %output_file%
for /r lib %%f in (*.dart) do (
    set "filename=%%~nxf"
    if not "!filename:~-7!"==".g.dart" (
        echo Файл: %%~nxf >> %output_file%
        type %%f >> %output_file%
        echo. >> %output_file%
    )
)