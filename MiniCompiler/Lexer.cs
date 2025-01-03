using System;
using System.Collections.Generic;

namespace MiniCompiler
{
    public enum TokenType
    {
        Keyword,
        Identifier,
        Number,
        Operator,
        Separator,
        Unknown
    }

    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }

        public Token(TokenType type, string value, int line, int column)
        {
            Type = type;
            Value = value;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return $"[{Type}] {Value} (Line: {Line}, Column: {Column})";
        }
    }

    public class Lexer
    {
        private readonly string _sourceCode;
        private int _currentIndex;
        private int _line;
        private int _column;

        private readonly HashSet<string> _keywords = new HashSet<string>
        {
            "int", "float", "if", "else", "while", "for", "return"
        };

        private readonly HashSet<string> _multiCharOperators = new HashSet<string>
        {
            "==", "!=", "<=", ">=", "&&", "||"
        };

        public Lexer(string sourceCode)
        {
            _sourceCode = sourceCode;
            _currentIndex = 0;
            _line = 1;
            _column = 1;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (_currentIndex < _sourceCode.Length)
            {
                char currentChar = _sourceCode[_currentIndex];

                if (char.IsWhiteSpace(currentChar))
                {
                    HandleWhitespace(currentChar);
                }
                else if (char.IsLetter(currentChar))
                {
                    tokens.Add(HandleKeywordOrIdentifier());
                }
                else if (char.IsDigit(currentChar))
                {
                    tokens.Add(HandleNumber());
                }
                else if ("+-*/=<>!&|".Contains(currentChar))
                {
                    tokens.Add(HandleOperator());
                }
                else if (";,(){}".Contains(currentChar))
                {
                    tokens.Add(new Token(TokenType.Separator, currentChar.ToString(), _line, _column));
                    Advance();
                }
                else
                {
                    tokens.Add(new Token(TokenType.Unknown, currentChar.ToString(), _line, _column));
                    Advance();
                }
            }

            return tokens;
        }

        private void HandleWhitespace(char currentChar)
        {
            if (currentChar == '\n')
            {
                _line++;
                _column = 1;
            }
            else
            {
                _column++;
            }

            _currentIndex++;
        }

        private Token HandleKeywordOrIdentifier()
        {
            int startColumn = _column;
            int startIndex = _currentIndex;

            while (_currentIndex < _sourceCode.Length && char.IsLetterOrDigit(_sourceCode[_currentIndex]))
            {
                Advance();
            }

            string value = _sourceCode.Substring(startIndex, _currentIndex - startIndex);

            TokenType type = _keywords.Contains(value) ? TokenType.Keyword : TokenType.Identifier;

            return new Token(type, value, _line, startColumn);
        }

        private Token HandleNumber()
        {
            int startColumn = _column;
            int startIndex = _currentIndex;
            bool isFloatingPoint = false;

            while (_currentIndex < _sourceCode.Length && (char.IsDigit(_sourceCode[_currentIndex]) || _sourceCode[_currentIndex] == '.'))
            {
                if (_sourceCode[_currentIndex] == '.')
                {
                    if (isFloatingPoint)
                        throw new Exception($"Unexpected '.' in number at Line {_line}, Column {_column}");
                    isFloatingPoint = true;
                }

                Advance();
            }

            string value = _sourceCode.Substring(startIndex, _currentIndex - startIndex);

            return new Token(TokenType.Number, value, _line, startColumn);
        }

        private Token HandleOperator()
        {
            int startColumn = _column;
            int startIndex = _currentIndex;

            // Handle multi-character operators (e.g., ==, !=, <=, >=)
            if (_currentIndex + 1 < _sourceCode.Length)
            {
                string potentialOperator = _sourceCode.Substring(_currentIndex, 2);
                if (_multiCharOperators.Contains(potentialOperator))
                {
                    Advance();
                    Advance();
                    return new Token(TokenType.Operator, potentialOperator, _line, startColumn);
                }
            }

            // Handle single-character operators
            char currentChar = _sourceCode[_currentIndex];
            Advance();
            return new Token(TokenType.Operator, currentChar.ToString(), _line, startColumn);
        }

        private void Advance()
        {
            _currentIndex++;
            _column++;
        }
    }
}
