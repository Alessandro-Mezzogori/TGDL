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
*/

lambda  
    : (parameter | parameter ',')* body
    ;

parameter: type IDENTIFIER  ;
    
type
    : token=BOOL_TYPE
    | token=DECIMAL_TYPE
    | token=STRING_TYPE
    | token=BOARD_TYPE
    | token=BOARDCELL_TYPE
    | token=PLAYER_TYPE
    | token=IDENTIFIER
    ;

body
    : '=>' (statement | LINEEND statement+)
    ;

statement
    : RETURN expression EOL             #returnStament
    | expression ASSIGN expression EOL  #assignmentStatement
    | expression EOL                    #expressionStatement
    ;

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
    | STRING
	| BOOL 
    ;


identifier
    : IDENTIFIER
    ;


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
ASSIGN      : '='   ;

// TOKENS

// Keywords

RETURN      : 'return'  ;
IF          : 'if'      ;


// types values
DECIMAL : NUMBER ('.' NUMBER)?;
STRING: '"' (~["\\\r\n] | EscapeSequence)* '"';
BOOL:	'true' | 'false';

// types
DECIMAL_TYPE:   'decimal'   ;
BOOL_TYPE   :   'bool'      ;
STRING_TYPE :   'string'    ;

// supplied types
BOARDCELL_TYPE  : 'cell'    ;
BOARD_TYPE      : 'board'   ;
PLAYER_TYPE     : 'player'  ;

// generic tokens
//EOL: ( LINEEND | EOF);
IDENTIFIER: LETTER LETTERORDIGIT* ;

EOL: ';'    ;
LINEEND
    : '\r'? '\n' 
    | '\r';

fragment NUMBER  :   DIGIT+  ;
fragment LETTERORDIGIT:  LETTER | DIGIT;
fragment DIGIT   :   [0-9]   ;
fragment LETTER  :   [a-zA-Z];

fragment EscapeSequence
    : '\\' [btnfr"'\\]
    | '\\' ([0-3]? [0-7])? [0-7]
    | '\\' 'u'+ HexDigit HexDigit HexDigit HexDigit
    ;

fragment HexDigit
    : [0-9a-fA-F]
    ;

WHITESPACE : [ \t\r\n\u000C]+ -> skip;//channel(HIDDEN);

// fragments
