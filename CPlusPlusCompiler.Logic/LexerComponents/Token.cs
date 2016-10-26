namespace CPlusPlusCompiler.Logic.LexerComponents
{
    public class Token
    {
        public TokenTypes Type { get; set; }
        public string Lexeme { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return "Lexeme: " + Lexeme + " Type: " + Type;
        }
    }
}
