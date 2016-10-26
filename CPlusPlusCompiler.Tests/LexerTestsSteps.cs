using System;
using System.Collections.Generic;
using System.Linq;
using CPlusPlusCompiler.Logic.LexerComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CPlusPlusCompiler.Tests
{
    [Binding]
    public class LexerTestsSteps
    {
        public Lexer LexerObject;
        public List<Token> TokensList;
        [Given]
        public void Given_the_following_code_P0(string code)
        {
            LexerObject = new Lexer(code);
        }

        [Given]
        public void Given_the_next_string_with_one_token_P0(string code)
        {
            LexerObject = new Lexer(code);
        }

        [When]
        public void When_running_the_get_tokens_function()
        {
            TokensList = LexerObject.GetAllTokens();
        }

        [Then]
        public void Then_the_tokens_returned_will_have_the_type_P0_and_lexeme_P1(string type, string lexeme)
        {
            var tokenExpected = new Token()
            {
                Type = (TokenTypes)Enum.Parse(typeof(TokenTypes), type),
                Lexeme = lexeme
            };
            Assert.AreEqual(tokenExpected.Type, TokensList[0].Type);
            Assert.AreEqual(tokenExpected.Lexeme, TokensList[0].Lexeme);
        }


        [Then]
        public void Then_the_tokens_returned_should_be(Table table)
        {
            var tokensExpected = table.CreateSet<Token>().ToList();

            for (int i = 0; i < TokensList.Count; i++)
            {
                Assert.AreEqual(tokensExpected[i].Lexeme, TokensList[i].Lexeme);
                Assert.AreEqual(tokensExpected[i].Type, TokensList[i].Type);
            }
        }
    }
}
