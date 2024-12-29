using System;
using System.Collections.Generic;

namespace MiniCompiler
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _currentTokenIndex;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _currentTokenIndex = 0;
        }

        public ProgramNode Parse()
        {
            var program = new ProgramNode();

            while (!IsAtEnd())
            {
                program.Statements.Add(ParseStatement());
            }

            return program;
        }

        private SyntaxNode ParseStatement()
        {
            Token current = Peek();

            if (current.Type == TokenType.Keyword && current.Value == "int")
            {
                return ParseDeclaration();
            }
            else if (current.Type == TokenType.Identifier)
            {
                return ParseAssignment();
            }
            else if (current.Type == TokenType.Keyword && current.Value == "if")
            {
                return ParseIfStatement();
            }
            else if (current.Type == TokenType.Keyword && current.Value == "while")
            {
                return ParseWhileStatement();
            }
            else if (current.Value == "{")
            {
                return ParseBlock();
            }
            else
            {
                throw new Exception($"Unexpected token: {current.Value}");
            }
        }

        private StatementNode ParseDeclaration()
        {
            Consume("int"); // Match "int"
            Token identifier = Consume(TokenType.Identifier); // Match variable name
            Consume(";"); // Match ";"

            return new StatementNode
            {
                StatementType = "Declaration",
                Expression = new ExpressionNode
                {
                    Value = identifier.Value
                }
            };
        }

        private StatementNode ParseAssignment()
        {
            Token identifier = Consume(TokenType.Identifier); // Match variable name
            Consume("="); // Match "="
            var expression = ParseExpression();
            Consume(";"); // Match ";"

            return new StatementNode
            {
                StatementType = "Assignment",
                Expression = expression
            };
        }

        private StatementNode ParseIfStatement()
        {
            Consume("if");
            Consume("(");
            var condition = ParseExpression();
            Consume(")");

            var thenBlock = ParseBlock();

            SyntaxNode elseBlock = null;
            if (Match("else"))
            {
                elseBlock = ParseBlock();
            }

            return new StatementNode
            {
                StatementType = "If",
                Expression = new ExpressionNode
                {
                    Left = condition,
                    Right = new ExpressionNode
                    {
                        Left = thenBlock,
                        Right = elseBlock
                    }
                }
            };
        }

        private StatementNode ParseWhileStatement()
        {
            Consume("while");
            Consume("(");
            var condition = ParseExpression();
            Consume(")");

            var body = ParseBlock();

            return new StatementNode
            {
                StatementType = "While",
                Expression = new ExpressionNode
                {
                    Left = condition,
                    Right = body
                }
            };
        }

        private SyntaxNode ParseBlock()
        {
            Consume("{");

            var statements = new List<SyntaxNode>();
            while (!Check("}"))
            {
                statements.Add(ParseStatement());
            }

            Consume("}");

            var blockNode = new ProgramNode();
            blockNode.Statements.AddRange(statements);
            return blockNode;
        }

        private ExpressionNode ParseExpression()
        {
            // Parse the first AdditiveExpression
            var left = ParseAdditiveExpression();

            // Handle relational operators
            while (Match("<", ">", "<=", ">=", "==", "!="))
            {
                string op = Previous().Value; // Get the relational operator
                var right = ParseAdditiveExpression(); // Parse the right-hand expression
                left = new ExpressionNode
                {
                    Operator = op,
                    Left = left,
                    Right = right
                };
            }

            return left;
        }

        private ExpressionNode ParseAdditiveExpression()
        {
            var left = ParseMultiplicativeExpression();

            while (Match("+", "-"))
            {
                string op = Previous().Value; // Get the operator
                var right = ParseMultiplicativeExpression();
                left = new ExpressionNode
                {
                    Operator = op,
                    Left = left,
                    Right = right
                };
            }

            return left;
        }

        private ExpressionNode ParseMultiplicativeExpression()
        {
            var left = ParseFactor();

            while (Match("*", "/"))
            {
                string op = Previous().Value; // Get the operator
                var right = ParseFactor();
                left = new ExpressionNode
                {
                    Operator = op,
                    Left = left,
                    Right = right
                };
            }

            return left;
        }

        private ExpressionNode ParseFactor()
        {
            if (Match(TokenType.Number))
            {
                return new ExpressionNode { Value = Previous().Value };
            }
            else if (Match(TokenType.Identifier))
            {
                return new ExpressionNode { Value = Previous().Value };
            }
            else if (Match("("))
            {
                var expr = ParseExpression();
                Consume(")"); // Ensure closing parenthesis is consumed
                return expr;
            }
            else
            {
                throw new Exception("Expected a number, identifier, or '('");
            }
        }

        // Helper methods
        private Token Consume(string value)
        {
            if (Check(value)) return Advance();
            throw new Exception($"Expected '{value}' but found '{Peek().Value}'");
        }

        private Token Consume(TokenType type)
        {
            if (Check(type)) return Advance();
            throw new Exception($"Expected '{type}' but found '{Peek().Type}'");
        }

        private bool Match(params string[] values)
        {
            foreach (var value in values)
            {
                if (Check(value))
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private bool Match(TokenType type)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
            return false;
        }

        private bool Check(string value)
        {
            return !IsAtEnd() && Peek().Value == value;
        }

        private bool Check(TokenType type)
        {
            return !IsAtEnd() && Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _currentTokenIndex++;
            return Previous();
        }

        private Token Peek()
        {
            return _tokens[_currentTokenIndex];
        }

        private Token Previous()
        {
            return _tokens[_currentTokenIndex - 1];
        }

        private bool IsAtEnd()
        {
            return _currentTokenIndex >= _tokens.Count;
        }
    }
}
