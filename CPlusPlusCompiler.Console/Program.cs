using CPlusPlusCompiler.Logic.LexerComponents;
using System;

namespace CPlusPlusCompiler.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var lex = new Lexer(@"#include <iostream> #if #desel 0125 0xa54
                                #include 'string.h' -- ++ += -= != ==
                                cont = cont + 1; ! != >> >>= >= << <= <<= & && &= | || |= ^ ^= ~ /= /
                                cont2 = 0; -> + 
                                int* miPtr = &cont2;
                                () [] -> . ++ - -  
                                = += -= *= /= %=>>= <<= &= ^= |=");
            var currentToken = lex.GetNextToken();
            while (currentToken.Type != TokenTypes.EOF)
            {
                System.Console.WriteLine(currentToken.ToString());
                currentToken = lex.GetNextToken();
            }
            System.Console.ReadKey();
        }
    }
}
