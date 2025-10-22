CODE SEGMENT 
ASSUME CS:CODE
ORG 100h
    x DD 1.01
    minX DD 1.01
    maxX DD 1.1
    plus DD 0.01 ;calculation step
    two DD 2.0
    six DD 6.0
    result DD ?
    .386
    
    ;y=x*2^x - ln(x-1)-6
    
    START:
        mov AX, CS
        mov DS, AX

        ;checking the variable
        finit
        fld dword ptr [maxX]
        fld dword ptr [x]
        fcompp
        fstsw AX
        sahf
    ja exit
        fld dword ptr [x]
        fld dword ptr [minX]
        fcompp
        fstsw AX
        sahf
    ja exit
    jmp cycle
    
    pluss:
        fld dword ptr [x]   ;ST(0)=x
        fld dword ptr [plus]
        fadd
        fstp x ;ST(0)=x+0.01
        
        ;calculation cycle
    cycle:
        ;y=x*2^x
        fld dword ptr [x]   ;ST(0)=x
        fld dword ptr [two] ;ST(0)=2, ST(1)=x
        fyl2x   ;ST(0)=2*log2x
        fld1    ;ST(0)=1, ST(1)=2*log2x
        fscale  ;ST(0)=1*2^(2*log2x), ST(1)=2*log2x
        fxch    ;ST(0)=2*log2x, ST(1)=2^(2*log2x)
        fld1    ;ST(0)=1, ST(1)=2*log2x, ST(2)=2^(2*log2x)
        fxch    ;ST(0)=2*log2x, ST(1)=1, ST(2)=2^(2*log2x)
        fprem
        fmul
        f2xm1      
        fld1
        fadd  
        fmul    ;ST(0)=2^x
        fmul x  
        
        ;y=x*2^x - ln(x-1)
        fld dword ptr [x]
        fld1
        fsub
        fld1
        fxch
        fyl2x
        fldln2
        fmul
        fsub    
        
        ;y=x*2^x - ln(x-1)-6
        fld dword ptr [six]
        fsub    
        fstp result

        ;checking the condition
        fld dword ptr [x]
        fld dword ptr [maxX]
        fcompp
        fstsw AX
        sahf
    
    jnb pluss
    exit:

        mov   AX,4C00H
        int   21H
        ret
        
CODE ENDS
    END START