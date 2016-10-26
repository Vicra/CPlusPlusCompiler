using System;

namespace CPlusPlusCompiler.Logic.LexerComponents
{
    public partial class Lexer
    {
        public class SatanException : Exception
        {
            public SatanException(string nasty) : base(nasty)
            {

            }
        }
    }
}