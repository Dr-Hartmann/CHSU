@echo off
color 0F
title Lab1
chcp 1251
::�������� ���������
set path="%1"
if "%path%" EQU "" set path=%cd%
echo ���� ����� %path%
::�������� ����� �� ������� ���� ������ ������ � ��������� ����������
dir %path% /-p /o:gn /b > "NameList.txt"
echo ����� �������� � %cd%\NameList.txt