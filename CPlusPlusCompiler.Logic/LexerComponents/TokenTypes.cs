﻿namespace CPlusPlusCompiler.Logic.LexerComponents
{
    public enum TokenTypes
    {
        EOF,
        ID,
        Digit,
        PR_PRINT,
        OP_SUM,
        OP_SUB,
        OP_MUL,
        OP_DIV,
        OP_EQU,
        FN_STM,
        PAR_IZQ,
        PAR_DER,
        DE_REF,
        INCLUDE,
        LS_THAN,
        GT_THAN,
        RETURN,
        COR_IZQ,
        COR_DER,
        RESERVED_ASM,
        RESERVED_AUTO,
        RESERVED_BOOL,
        RESERVED_BREAK,
        RESERVED_CASE,
        RESERVED_CATCH,
        RESERVED_CHAR,
        RESERVED_CLASS,
        RESERVED_CONST,
        RESERVED_CONST_CAST,
        RESERVED_CONTINUE,
        RESERVED_DATE,
        RESERVED_DEFAULT,
        RESERVED_DELETE,
        RESERVED_DO,
        RESERVED_DOUBLE,
        RESERVED_DYNAMIC_CAST,
        RESERVED_ELSE,
        RESERVED_ENUM,
        RESERVED_EXPLICIT,
        RESERVED_EXPORT,
        RESERVED_EXTERN,
        RESERVED_FALSE,
        RESERVED_FLOAT,
        RESERVED_FOR,
        RESERVED_FOREACH,
        RESERVED_FRIEND,
        RESERVED_GOTO,
        RESERVED_IF,
        RESERVED_INLINE,
        RESERVED_INT,
        RESERVED_LONG,
        RESERVED_MUTABLE,
        RESERVED_NAMESPACE,
        RESERVED_NEW,
        RESERVED_OPERATOR,
        RESERVED_PRIVATE,
        RESERVED_PROTECTED,
        RESERVED_PUBLIC,
        RESERVED_REGISTER,
        RESERVED_REINTERPRET_CAST,
        RESERVED_RETURN,
        RESERVED_SHORT,
        RESERVED_SIGNED,
        RESERVED_SIZEOF,
        RESERVED_STATIC,
        RESERVED_STATIC_CAST,
        RESERVED_STRING,
        RESERVED_STRUCT,
        RESERVED_SWITCH,
        RESERVED_TEMPLATE,
        RESERVED_THIS,
        RESERVED_THROW,
        RESERVED_TRUE,
        RESERVED_TRY,
        RESERVED_TYPEDEF,
        RESERVED_TYPEID,
        RESERVED_TYPENAME,
        RESERVED_UNION,
        RESERVED_UNSIGNED,
        RESERVED_USING,
        RESERVED_VIRTUAL,
        RESERVED_VOID,
        RESERVED_VOLATILE,
        RESERVED_WCHAR_T,
        RESERVED_WHILE,
        HASH_INCLUDE,
        HASH_DEFINE,
        HASH_IF,
        HASH_ISNDEF,
        HASH_ISDEF,
        COMILLA,
        PUNTO,
        HEX,
        OCTAL,
        OP_MOD,
        INCREMENT,
        ADD_AND_ASSIGNMENT,
        DECREMENT,
        SUBSTRACT_AND_ASSIGNMENT,
        EQUALS,
        DISTINCT,
        LOGICAL_NOT_OPERATOR,
        GT_THAN_OR_EQUAL,
        RIGHT_SHIFT,
        RIGHT_SHIFT_AND_ASSIGNMENT,
        LS_THAN_OR_EQUAL,
        LEFT_SHIFT_AND_ASSIGNMENT,
        LEFT_SHIFT,
        LOGICAL_AND,
        BITWISE_AND_ASSIGNMENT,
        BINARY_AND_OPERATOR,
        LOGICAL_OR,
        BITWISER_INCLUSIVE_OR_AND_ASSIGNMENT,
        BINARY_OR_OPERATOR,
        BITWISER_EXCLUSIVE_OR_AND_ASSIGNMENT,
        BITWISE_XOR,
        COMPLEMENT,
        MUL_AND_ASSIGN,
        DIV_AND_ASSIGN,
        MOD_AND_ASSIGN,
        ARROW,
        LLAVE_IZQ,
        LLAVE_DER
    }
}