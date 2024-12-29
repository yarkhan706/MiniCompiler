namespace MiniCompiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTokenize_Click(object sender, EventArgs e)
        {
            string sourceCode = txtSourceCode.Text;

            Lexer lexer = new Lexer(sourceCode);
            List<Token> tokens = lexer.Tokenize();

            dataGridViewTokens.Rows.Clear();

            foreach (var token in tokens)
            {
                dataGridViewTokens.Rows.Add(token.Type, token.Value, token.Line, token.Column);
            }
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            string sourceCode = txtSourceCode.Text;

            Lexer lexer = new Lexer(sourceCode);
            List<Token> tokens = lexer.Tokenize();

            Parser parser = new Parser(tokens);

            try
            {
                ProgramNode programNode = parser.Parse();

                // Open the Parse Tree form and pass the root node
                ParseTreeForm parseTreeForm = new ParseTreeForm(programNode);
                parseTreeForm.ShowDialog(); // Open the form as a modal dialog
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
