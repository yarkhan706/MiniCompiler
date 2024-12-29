using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniCompiler
{
    public partial class ParseTreeForm : Form
    {
        private SyntaxNode _rootNode;
        private int _nodeSpacingX = 150; // Horizontal spacing between nodes
        private int _nodeSpacingY = 80;  // Vertical spacing between levels
        private int _nodeRadius = 30;    // Radius of node circles

        public ParseTreeForm(SyntaxNode rootNode)
        {
            InitializeComponent();
            _rootNode = rootNode;

            // Attach Paint event for the panel
            panelTree.Paint += PanelTree_Paint;
        }

        // Panel's Paint event to draw the parse tree
        private void PanelTree_Paint(object sender, PaintEventArgs e)
        {
            if (_rootNode == null) return;

            using (Graphics g = e.Graphics)
            {
                g.Clear(Color.White); // Clear the panel's background

                // Start drawing the tree at the center-top of the panel
                int startX = panelTree.Width / 2;
                int startY = 20;

                // Track the maximum width and height needed for the drawing
                var maxBounds = new Size();

                // Draw the tree and calculate its bounds
                DrawSyntaxTree(g, _rootNode, startX, startY, ref maxBounds);

                // Update the panel's AutoScrollMinSize based on the tree bounds
                panelTree.AutoScrollMinSize = new Size(maxBounds.Width, maxBounds.Height);
            }
        }

        // Method to recursively draw the syntax tree and track bounds
        private void DrawSyntaxTree(Graphics g, SyntaxNode node, int x, int y, ref Size maxBounds)
        {
            if (node == null) return;

            // Apply auto-scroll offset
            Point scrollOffset = panelTree.AutoScrollPosition;
            x += scrollOffset.X;
            y += scrollOffset.Y;

            // Update the maximum bounds for scrolling
            maxBounds.Width = Math.Max(maxBounds.Width, x + _nodeRadius * 2);
            maxBounds.Height = Math.Max(maxBounds.Height, y + _nodeRadius * 2);

            // Draw the current node as a circle
            Brush brush = Brushes.LightBlue;
            Pen pen = Pens.Black;

            g.FillEllipse(brush, x - _nodeRadius, y - _nodeRadius, _nodeRadius * 2, _nodeRadius * 2);
            g.DrawEllipse(pen, x - _nodeRadius, y - _nodeRadius, _nodeRadius * 2, _nodeRadius * 2);

            // Add label inside the circle
            string label = node is ProgramNode ? "Program" :
                           node is StatementNode stmtNode ? stmtNode.StatementType.ToString() :
                           node is ExpressionNode exprNode ? exprNode.Value ?? exprNode.Operator :
                           "Unknown";

            // Center the label inside the node
            SizeF labelSize = g.MeasureString(label, SystemFonts.DefaultFont);
            g.DrawString(label, SystemFonts.DefaultFont, Brushes.Black,
                x - labelSize.Width / 2, y - labelSize.Height / 2);

            // Recursively draw children
            if (node is ProgramNode programNode)
            {
                int childX = x - _nodeSpacingX * (programNode.Statements.Count - 1) / 2; // Center children
                foreach (var statement in programNode.Statements)
                {
                    // Draw line to the child node
                    g.DrawLine(pen, x, y + _nodeRadius, childX, y + _nodeSpacingY - _nodeRadius);

                    // Recursively draw the child node
                    DrawSyntaxTree(g, statement, childX, y + _nodeSpacingY, ref maxBounds);

                    // Update the child position
                    childX += _nodeSpacingX;
                }
            }
            else if (node is StatementNode statementNode && statementNode.Expression != null)
            {
                // Draw line to the expression
                g.DrawLine(pen, x, y + _nodeRadius, x, y + _nodeSpacingY - _nodeRadius);

                // Recursively draw the expression
                DrawSyntaxTree(g, statementNode.Expression, x, y + _nodeSpacingY, ref maxBounds);
            }
            else if (node is ExpressionNode expressionNode)
            {
                if (expressionNode.Left != null)
                {
                    int leftX = x - _nodeSpacingX;
                    g.DrawLine(pen, x, y + _nodeRadius, leftX, y + _nodeSpacingY - _nodeRadius);
                    DrawSyntaxTree(g, expressionNode.Left, leftX, y + _nodeSpacingY, ref maxBounds);
                }

                if (expressionNode.Right != null)
                {
                    int rightX = x + _nodeSpacingX;
                    g.DrawLine(pen, x, y + _nodeRadius, rightX, y + _nodeSpacingY - _nodeRadius);
                    DrawSyntaxTree(g, expressionNode.Right, rightX, y + _nodeSpacingY, ref maxBounds);
                }
            }
        }
    }
}
