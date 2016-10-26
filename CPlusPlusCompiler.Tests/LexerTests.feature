Feature: LexerTests
	As a compiler student 
	In order to avoid mistakes in lexer
	I want to generate the necesary tokens

@Integers
Scenario: When receiving all types of integers
	Given the following code 0x2a5b 021 452
	When running the get tokens function
	Then the tokens returned should be
	| Type  | Lexeme |
	| HEX   | 0x2a5b |
	| OCTAL | 021    |
	| Digit | 452    |
	| EOF   |        |

Scenario Outline: When receiving any code
	Given the next string with one token < token >
	When running the get tokens function
	Then the tokens returned will have the type < Type > and lexeme < Lexeme >
	Examples: 
	| token     | Type               | Lexeme    |
	| '0x2a5b   | 'HEX               | '0x2a5b   |
	| '021      | 'OCTAL             | '021      |
	| '452      | 'Digit             | '452      |
	| 'int      | 'RESERVED_INT      | 'int      |
	| 'float    | 'RESERVED_FLOAT    | 'float    |
	| 'char     | 'RESERVED_CHAR     | 'char     |
	| 'bool     | 'RESERVED_BOOL     | 'bool     |
	| 'string   | 'RESERVED_STRING   | 'string   |
	| 'date     | 'RESERVED_DATE     | 'date     |
	| 'enum     | 'RESERVED_ENUM     | 'enum     |
	| 'struct   | 'RESERVED_STRUCT   | 'struct   |
	| 'const    | 'RESERVED_CONST    | 'const    |
	| 'if       | 'RESERVED_IF       | 'if       |
	| 'while    | 'RESERVED_WHILE    | 'while    |
	| 'do       | 'RESERVED_DO       | 'do       |
	| 'for      | 'RESERVED_FOR      | 'for      |
	| 'switch   | 'RESERVED_SWITCH   | 'switch   |
	| 'foreach  | 'RESERVED_FOREACH  | 'foreach  |
	| 'break    | 'RESERVED_BREAK    | 'break    |
	| 'continue | 'RESERVED_CONTINUE | 'continue |