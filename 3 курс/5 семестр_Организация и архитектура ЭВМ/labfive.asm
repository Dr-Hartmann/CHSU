RD #40 ;������ �������
WR 35 ;����� �����
RD #10 ;��������� �������
WR 36
RD #0
WR 37 ;��������� ��������
WR 50
M1: RD @35
WR 38
DIV #2
MUL #2
WR 39
RD 38
SUB 39
JZ CH ;������� ��� ������
RD @35
ADD 50
WR 50 ;������ ����������
JMP 22
CH: RD @35
ADD 51
WR 51 ;������ ����������
RD 35 ;�������
ADD #1
WR 35
RD 36
SUB #1
WR 36
JNZ M1 ;����
RD 50
SUB 51
OUT
HLT