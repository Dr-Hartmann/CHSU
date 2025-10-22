RD #40 ;начало буффера
WR 35 ;адрес числа
RD #10 ;элементов массива
WR 36
RD #0
WR 37 ;результат операций
WR 50
M1: RD @35
WR 38
DIV #2
MUL #2
WR 39
RD 38
SUB 39
JZ CH ;переход для чётных
RD @35
ADD 50
WR 50 ;запись результата
JMP 22
CH: RD @35
ADD 51
WR 51 ;запись результата
RD 35 ;счётчик
ADD #1
WR 35
RD 36
SUB #1
WR 36
JNZ M1 ;цикл
RD 50
SUB 51
OUT
HLT