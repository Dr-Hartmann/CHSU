@echo off
color 0F
title Lab1
chcp 1251
::Передача параметра
set path="%1"
if "%path%" EQU "" set path=%cd%
echo Путь равен %path%
::Создание файла со списком всех файлов только в указанной директории
dir %path% /-p /o:gn /b > "NameList.txt"
echo Файлы записаны в %cd%\NameList.txt