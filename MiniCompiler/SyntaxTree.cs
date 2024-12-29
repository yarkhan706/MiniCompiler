using System.Collections.Generic;

namespace MiniCompiler
{
    // Base class for all nodes
    public abstract class SyntaxNode
    {
    }

    // Root node of the program
    public class ProgramNode : SyntaxNode
    {
        public List<SyntaxNode> Statements { get; set; } = new List<SyntaxNode>();
    }

    // Statement node (generic)
    public class StatementNode : SyntaxNode
    {
        public string StatementType { get; set; } // e.g., "Declaration", "Assignment"
        public SyntaxNode Expression { get; set; } // Optional
    }

    // Expression node
    public class ExpressionNode : SyntaxNode
    {
        public string Operator { get; set; }
        public SyntaxNode Left { get; set; }
        public SyntaxNode Right { get; set; }
        public string Value { get; set; } // For literals or identifiers
    }
}
