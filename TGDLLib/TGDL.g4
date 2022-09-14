grammar TGDL;

/* Parser Rules */
/* 
program :
        ;

state   :
        ;

attribute   :
            ;

action  :
        ;

require :
        ;

lambda  :
        ;

parameter   :
            ;

body    :
        ;

stat    :
        ;

*/

expression
    : primary
    | expression dot=DOT identifier // Make fail if there is expression. without identifier
    // expression . (identifier|methodCall)
    // | methodcall
    | prefix=(PLUS | MINUS) expression   
    | prefix=NOT expression     
    | <assoc=right> expression bop=POW expression  
    | expression bop=(MOL | DIV | MOD) expression   
    | expression bop=(PLUS | MINUS ) expression        
    | expression bop=(LESSEQUAL | GREATEREQUAL | LESS | GREATER) expression 
    | expression bop=(EQUAL | NOTEQUAL) expression   
    | expression bop=AND expression
    | expression bop=OR expression
    ;

primary
    :   '(' expression ')'
    |   literal
    |   identifier
    ;

literal
    : DECIMAL
    ;


identifier
    : IDENTIFIER
    ;

// conditinoalOR
// conditionalAnd

// chain does not work

// all operations possible


/* Lexer Rules */

// number


// operators
PLUS        : '+'   ;   
MINUS       : '-'   ;
NOT         : '!'   ;
POW         : '^'   ;
MOL         : '*'   ;
DIV         : '/'   ;
MOD         : '%'   ;
LESSEQUAL   : '<='  ;
GREATEREQUAL: '>='  ;
LESS        : '<'   ;
GREATER     : '>'   ;
EQUAL       : '=='  ;
NOTEQUAL    : '!='  ;
AND         : '&&'  ;
OR          : '||'  ;

DOT         : '.'   ;


// Keywords

// types
DECIMAL : NUMBER ('.' NUMBER)?;
IDENTIFIER: LETTER LETTERORDIGIT* ;

fragment NUMBER  :   DIGIT+  ;
fragment LETTERORDIGIT:  LETTER | DIGIT;
fragment DIGIT   :   [0-9]   ;
fragment LETTER  :   [a-zA-Z];

WHITESPACE : [ \t\r\n\u000C]+ -> channel(HIDDEN);

// fragments
