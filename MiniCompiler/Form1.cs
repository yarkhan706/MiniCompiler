using System.Diagnostics;
using System.Windows.Forms;

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

        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            string sourceCode = txtSourceCode.Text;  // TextBox for user to input code
            string tempFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".c");  // Temporary file with .c extension
            string exeFileName = Path.ChangeExtension(tempFileName, ".exe");  // Output .exe file

            try
            {
                // Save the source code into the temporary .c file
                File.WriteAllText(tempFileName, sourceCode);

                // Set up the ProcessStartInfo for running GCC
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "gcc",  // Assuming GCC is installed and in PATH
                    Arguments = $"-o \"{exeFileName}\" \"{tempFileName}\"",  // Compile the C code
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the GCC process
                Process process = new Process { StartInfo = processInfo };

                process.Start();

                // Capture the output and errors from the compilation process
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                // Display the output or errors in the result TextBox
                if (process.ExitCode == 0)
                {
                    txtOutput.Text = "Compilation succeeded: " + output;

                    // After successful compilation, run the compiled executable
                    RunExecutable(exeFileName);
                }
                else
                {
                    txtOutput.Text = "Compilation failed: " + errors;
                }

                // Clean up temporary files
                File.Delete(tempFileName);
            }
            catch (Exception ex)
            {
                txtOutput.Text = "Error: " + ex.Message;
            }
        }

        private void RunExecutable(string exeFileName)
        {
            try
            {
                // Set up ProcessStartInfo to run the compiled executable
                ProcessStartInfo runProcessInfo = new ProcessStartInfo
                {
                    FileName = exeFileName,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the executable
                Process runProcess = new Process { StartInfo = runProcessInfo };
                runProcess.Start();

                // Capture the output and errors from the running executable
                string runOutput = runProcess.StandardOutput.ReadToEnd();
                string runErrors = runProcess.StandardError.ReadToEnd();

                runProcess.WaitForExit();

                // Display the output or errors in the result TextBox
                if (runProcess.ExitCode == 0)
                {
                    txtOutput.AppendText("\nProgram Output:\n" + runOutput);
                }
                else
                {
                    txtOutput.AppendText("\nProgram Execution Failed:\n" + runErrors);
                }

                // Optionally, clean up the executable after running it
                File.Delete(exeFileName);
            }
            catch (Exception ex)
            {
                txtOutput.AppendText("\nError running the program: " + ex.Message);
            }
        }

        private void btnHelloWorld_Click(object sender, EventArgs e)
        {
            txtSourceCode.Text = "#include <stdio.h>\r\nint main() {\r\n   // printf() displays the string inside quotation\r\n   printf(\"Hello, World!\");\r\n   return 0;\r\n}\r\n";
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            txtSourceCode.Text = "#include <stdio.h>\r\n\r\nint main() {\r\n    int sum = 0;\r\n\r\n    // Calculate the sum of numbers from 1 to 10\r\n    for (int i = 1; i <= 10; i++) {\r\n        sum += i;\r\n    }\r\n\r\n    // Print the result\r\n    printf(\"The sum of numbers from 1 to 10 is: %d\\n\", sum);\r\n\r\n    return 0;\r\n}\r\n";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSourceCode.Text = "";
            txtOutput.Text = "";
            dataGridViewTokens.DataSource = null; // Removes the data source
            dataGridViewTokens.Rows.Clear();     // Clears any manually added rows
        }
    }
}
