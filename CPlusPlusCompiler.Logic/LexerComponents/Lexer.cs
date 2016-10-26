using System.Collections.Generic;
using System.Linq;

namespace CPlusPlusCompiler.Logic.LexerComponents
{
    public partial class Lexer
    {
        private int _row = 0;
        private int _column = 0;

        private readonly string _sourceCode;
        private int _currentPointer;
        private readonly Dictionary<string, TokenTypes> _reserveWords;
        private readonly Dictionary<string, TokenTypes> _symbols;
        private readonly Dictionary<string, TokenTypes> _hashWords;
        private readonly char[] _octals = { '0', '1', '2', '3', '4', '5', '6', '7' };
        private readonly char[] _hexadecimals = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' };

        public Lexer(string sourceCode)
        {

            _sourceCode = sourceCode;
            _currentPointer = 0;

            _hashWords = new Dictionary<string, TokenTypes>
            {
                {"#include", TokenTypes.HASH_INCLUDE},
                {"#define", TokenTypes.HASH_DEFINE},
                {"#if", TokenTypes.HASH_IF},
                {"#idndef", TokenTypes.HASH_ISNDEF},
                {"#ifdef", TokenTypes.HASH_ISDEF}
            };

            _reserveWords = new Dictionary<string, TokenTypes>
            {
                {"asm", TokenTypes.RESERVED_ASM},
                {"auto", TokenTypes.RESERVED_AUTO},
                {"bool", TokenTypes.RESERVED_BOOL},
                {"break", TokenTypes.RESERVED_BREAK},
                {"case", TokenTypes.RESERVED_CASE},
                {"catch", TokenTypes.RESERVED_CATCH},
                {"char", TokenTypes.RESERVED_CHAR},
                {"class", TokenTypes.RESERVED_CLASS},
                {"const", TokenTypes.RESERVED_CONST},
                {"const_cast", TokenTypes.RESERVED_CONST_CAST},
                {"continue", TokenTypes.RESERVED_CONTINUE},
                {"date", TokenTypes.RESERVED_DATE},
                {"default", TokenTypes.RESERVED_DEFAULT},
                {"delete", TokenTypes.RESERVED_DELETE},
                {"do", TokenTypes.RESERVED_DO},
                {"double", TokenTypes.RESERVED_DOUBLE},
                {"dynamic_cast", TokenTypes.RESERVED_DYNAMIC_CAST},
                {"else", TokenTypes.RESERVED_ELSE},
                {"enum", TokenTypes.RESERVED_ENUM},
                {"explicit", TokenTypes.RESERVED_EXPLICIT},
                {"export", TokenTypes.RESERVED_EXPORT},
                {"extern", TokenTypes.RESERVED_EXTERN},
                {"false", TokenTypes.RESERVED_FALSE},
                {"float", TokenTypes.RESERVED_FLOAT},
                {"for", TokenTypes.RESERVED_FOR},
                {"foreach", TokenTypes.RESERVED_FOREACH},
                {"friend", TokenTypes.RESERVED_FRIEND},
                {"goto", TokenTypes.RESERVED_GOTO},
                {"if", TokenTypes.RESERVED_IF},
                {"inline", TokenTypes.RESERVED_INLINE},
                {"int", TokenTypes.RESERVED_INT},
                {"long", TokenTypes.RESERVED_LONG},
                {"mutable", TokenTypes.RESERVED_MUTABLE},
                {"namespace", TokenTypes.RESERVED_NAMESPACE},
                {"new", TokenTypes.RESERVED_NEW},
                {"operator", TokenTypes.RESERVED_OPERATOR},
                {"private", TokenTypes.RESERVED_PRIVATE},
                {"protected", TokenTypes.RESERVED_PROTECTED},
                {"public", TokenTypes.RESERVED_PUBLIC},
                {"register", TokenTypes.RESERVED_REGISTER},
                {"reinterpret_cast", TokenTypes.RESERVED_REINTERPRET_CAST},
                {"return", TokenTypes.RESERVED_RETURN},
                {"short", TokenTypes.RESERVED_SHORT},
                {"signed", TokenTypes.RESERVED_SIGNED},
                {"sizeof", TokenTypes.RESERVED_SIZEOF},
                {"static", TokenTypes.RESERVED_STATIC},
                {"static_cast", TokenTypes.RESERVED_STATIC_CAST},
                {"string", TokenTypes.RESERVED_STRING},
                {"struct", TokenTypes.RESERVED_STRUCT},
                {"switch", TokenTypes.RESERVED_SWITCH},
                {"template", TokenTypes.RESERVED_TEMPLATE},
                {"this", TokenTypes.RESERVED_THIS},
                {"throw", TokenTypes.RESERVED_THROW},
                {"true", TokenTypes.RESERVED_TRUE},
                {"try", TokenTypes.RESERVED_TRY},
                {"typedef", TokenTypes.RESERVED_TYPEDEF},
                {"typeid", TokenTypes.RESERVED_TYPEID},
                {"typename", TokenTypes.RESERVED_TYPENAME},
                {"union", TokenTypes.RESERVED_UNION},
                {"unsigned", TokenTypes.RESERVED_UNSIGNED},
                {"using", TokenTypes.RESERVED_USING},
                {"virtual", TokenTypes.RESERVED_VIRTUAL},
                {"void", TokenTypes.RESERVED_VOID},
                {"volatile", TokenTypes.RESERVED_VOLATILE},
                {"wchar_t", TokenTypes.RESERVED_WCHAR_T},
                {"while", TokenTypes.RESERVED_WHILE},
                {"#include", TokenTypes.INCLUDE},

            };
            _symbols = new Dictionary<string, TokenTypes>
            {
                {"+", TokenTypes.OP_SUM},
                {"-", TokenTypes.OP_SUB},
                {"*", TokenTypes.OP_MUL},
                {"/", TokenTypes.OP_DIV},
                {"=", TokenTypes.OP_EQU},
                {"(", TokenTypes.PAR_IZQ},
                {")", TokenTypes.PAR_DER},
                {"&", TokenTypes.DE_REF},
                {"<", TokenTypes.LS_THAN},
                {">", TokenTypes.GT_THAN},
                {";", TokenTypes.FN_STM},
                {"{", TokenTypes.LLAVE_IZQ},
                {"}", TokenTypes.LLAVE_DER},
                {"[", TokenTypes.COR_IZQ},
                {"]", TokenTypes.COR_DER},
                {"'", TokenTypes.COMILLA},
                {".", TokenTypes.PUNTO},
                {"%", TokenTypes.OP_MOD},
                {"~", TokenTypes.COMPLEMENT}
            };
        }

        public Token GetNextToken()
        {
            var tmp = GetCurrentSymbol();
            var lexeme = "";

            while (char.IsWhiteSpace(tmp))
            {
                _currentPointer++;
                tmp = GetCurrentSymbol();
            }

            if (tmp == '\0')
            {
                return new Token { Type = TokenTypes.EOF, Lexeme = "" };
            }

            if (tmp.Equals('#'))
            {
                lexeme += tmp;
                _currentPointer++;
                return GetHashWord(lexeme);
            }

            if (char.IsLetter(tmp) || tmp.Equals('_'))
            {
                lexeme += tmp;
                _currentPointer++;
                return GetId(lexeme);

            }

            if (char.IsDigit(tmp))
            {
                lexeme += tmp;
                _currentPointer++;
                if (tmp.Equals('0'))
                {
                    return GetNonDecimal(lexeme);
                }
                return GetDecimal(lexeme);
            }
            if (tmp.Equals('+'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '+')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "++", Type = TokenTypes.INCREMENT };
                }
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "+=", Type = TokenTypes.ADD_AND_ASSIGNMENT };
                }
            }
            else if (tmp.Equals('-'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '-')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "--", Type = TokenTypes.DECREMENT };
                }
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "-=", Type = TokenTypes.SUBSTRACT_AND_ASSIGNMENT };
                }
                if (_sourceCode[_currentPointer] == '>')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "->", Type = TokenTypes.ARROW };
                }
                _currentPointer++;
                return new Token { Lexeme = "-", Type = TokenTypes.OP_SUB };
            }
            else if (tmp.Equals('='))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token {Lexeme = "==", Type = TokenTypes.EQUALS};
                }
                _currentPointer++;
                return new Token { Lexeme = "=", Type = TokenTypes.OP_EQU };
            }
            else if (tmp.Equals('!'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "!=", Type = TokenTypes.DISTINCT };
                }
                _currentPointer++;
                return new Token { Lexeme = "!", Type = TokenTypes.LOGICAL_NOT_OPERATOR };
            }
            else if (tmp.Equals('>'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = ">=", Type = TokenTypes.GT_THAN_OR_EQUAL };
                }
                if (_sourceCode[_currentPointer] == '>')
                {
                    _currentPointer++;
                    if (_sourceCode[_currentPointer] == '=')
                    {
                        _currentPointer++;
                        return new Token { Lexeme = ">>=", Type = TokenTypes.RIGHT_SHIFT_AND_ASSIGNMENT };
                    }
                    return new Token { Lexeme = ">>", Type = TokenTypes.RIGHT_SHIFT };
                }
                _currentPointer++;
                return new Token { Lexeme = ">", Type = TokenTypes.GT_THAN };
            }
            else if (tmp.Equals('<'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "<=", Type = TokenTypes.LS_THAN_OR_EQUAL };
                }
                if (_sourceCode[_currentPointer] == '<')
                {
                    _currentPointer++;
                    if (_sourceCode[_currentPointer] == '=')
                    {
                        _currentPointer++;
                        return new Token { Lexeme = "<<=", Type = TokenTypes.LEFT_SHIFT_AND_ASSIGNMENT };
                    }
                    return new Token { Lexeme = "<<", Type = TokenTypes.LEFT_SHIFT };
                }
                _currentPointer++;
                return new Token { Lexeme = "<", Type = TokenTypes.LS_THAN };
            }
            else if (tmp.Equals('&'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '&')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "&&", Type = TokenTypes.LOGICAL_AND };
                }
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "&=", Type = TokenTypes.BITWISE_AND_ASSIGNMENT };
                }
                _currentPointer++;
                return new Token { Lexeme = "&", Type = TokenTypes.BINARY_AND_OPERATOR };
            }
            else if (tmp.Equals('|'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '|')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "||", Type = TokenTypes.LOGICAL_OR };
                }
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "|=", Type = TokenTypes.BITWISER_INCLUSIVE_OR_AND_ASSIGNMENT };
                }
                _currentPointer++;
                return new Token { Lexeme = "|", Type = TokenTypes.BINARY_OR_OPERATOR };
            }
            else if (tmp.Equals('^'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "^=", Type = TokenTypes.BITWISER_EXCLUSIVE_OR_AND_ASSIGNMENT };
                }
                _currentPointer++;
                return new Token { Lexeme = "^", Type = TokenTypes.BITWISE_XOR };
            }
            else if (tmp.Equals('*'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "*=", Type = TokenTypes.MUL_AND_ASSIGN };
                }
                _currentPointer++;
                return new Token { Lexeme = "*", Type = TokenTypes.OP_MUL };
            }
            else if (tmp.Equals('/'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "/=", Type = TokenTypes.DIV_AND_ASSIGN };
                }
                _currentPointer++;
                return new Token { Lexeme = "/", Type = TokenTypes.OP_DIV };
            }
            else if (tmp.Equals('%'))
            {
                _currentPointer++;
                if (_sourceCode[_currentPointer] == '=')
                {
                    _currentPointer++;
                    return new Token { Lexeme = "%=", Type = TokenTypes.MOD_AND_ASSIGN };
                }
                _currentPointer++;
                return new Token { Lexeme = "%", Type = TokenTypes.OP_MOD };
            }
            if (_symbols.ContainsKey(tmp.ToString()))
            {
                _currentPointer++;
                return new Token { Type = _symbols[tmp.ToString()], Lexeme = tmp.ToString() };
            }

            throw new SatanException("Nasty");
        }

        private Token GetNonDecimal(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            if (tmp1.ToString().ToLower().Equals("x"))
            {
                lexeme += tmp1;
                _currentPointer++;
                return GetHexadecimalValue(lexeme);
            }
            if (_octals.Contains(tmp1))
            {
                return GetOctalValue(lexeme);
            }
            return GetDecimal(lexeme);
        }

        private Token GetOctalValue(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            while (_octals.Contains(tmp1))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }
            return new Token { Type = TokenTypes.OCTAL, Lexeme = lexeme };
        }

        private Token GetHexadecimalValue(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            while (_hexadecimals.Contains(tmp1))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }
            return new Token { Type = TokenTypes.HEX, Lexeme = lexeme };
        }

        private Token GetDecimal(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            while (char.IsDigit(tmp1))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }
            return new Token { Type = TokenTypes.Digit, Lexeme = lexeme };
        }

        private Token GetId(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            while (char.IsLetterOrDigit(tmp1) || tmp1.Equals('_'))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }

            return new Token
            {
                Type = _reserveWords.ContainsKey(lexeme) ? _reserveWords[lexeme] : TokenTypes.ID,
                Lexeme = lexeme
            };
        }

        private Token GetHashWord(string lexeme)
        {
            var tmp1 = GetCurrentSymbol();
            while (char.IsLetter(tmp1))
            {
                lexeme += tmp1;
                _currentPointer++;
                tmp1 = GetCurrentSymbol();
            }

            return new Token
            {
                Type = _hashWords.ContainsKey(lexeme) ? _hashWords[lexeme] : TokenTypes.ID,
                Lexeme = lexeme
            };
        }

        private char GetCurrentSymbol()
        {
            if (_currentPointer < _sourceCode.Length)
            {
                return _sourceCode[_currentPointer];
            }
            return '\0';
        }

        public List<Token> GetAllTokens()
        {
            List<Token> returnTokens = new List<Token>();
            var currentToken = GetNextToken();
            returnTokens.Add(currentToken);
            while (currentToken.Type != TokenTypes.EOF)
            {
                currentToken = GetNextToken();
                returnTokens.Add(currentToken);
            }
            return returnTokens;
        }
    }
}
