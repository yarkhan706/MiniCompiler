namespace MiniCompiler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSourceCode = new TextBox();
            btnTokenize = new Button();
            dataGridViewTokens = new DataGridView();
            Type = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            Line = new DataGridViewTextBoxColumn();
            Column = new DataGridViewTextBoxColumn();
            btnParse = new Button();
            txtResult = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTokens).BeginInit();
            SuspendLayout();
            // 
            // txtSourceCode
            // 
            txtSourceCode.Location = new Point(21, 42);
            txtSourceCode.Multiline = true;
            txtSourceCode.Name = "txtSourceCode";
            txtSourceCode.ScrollBars = ScrollBars.Vertical;
            txtSourceCode.Size = new Size(443, 186);
            txtSourceCode.TabIndex = 0;
            // 
            // btnTokenize
            // 
            btnTokenize.Location = new Point(21, 245);
            btnTokenize.Name = "btnTokenize";
            btnTokenize.Size = new Size(75, 23);
            btnTokenize.TabIndex = 1;
            btnTokenize.Text = "Tokenize";
            btnTokenize.UseVisualStyleBackColor = true;
            btnTokenize.Click += btnTokenize_Click;
            // 
            // dataGridViewTokens
            // 
            dataGridViewTokens.AllowUserToAddRows = false;
            dataGridViewTokens.AllowUserToDeleteRows = false;
            dataGridViewTokens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTokens.Columns.AddRange(new DataGridViewColumn[] { Type, Value, Line, Column });
            dataGridViewTokens.Location = new Point(21, 288);
            dataGridViewTokens.Name = "dataGridViewTokens";
            dataGridViewTokens.ReadOnly = true;
            dataGridViewTokens.Size = new Size(443, 150);
            dataGridViewTokens.TabIndex = 2;
            // 
            // Type
            // 
            Type.HeaderText = "TokenType";
            Type.Name = "Type";
            Type.ReadOnly = true;
            // 
            // Value
            // 
            Value.HeaderText = "Value";
            Value.Name = "Value";
            Value.ReadOnly = true;
            // 
            // Line
            // 
            Line.HeaderText = "Line";
            Line.Name = "Line";
            Line.ReadOnly = true;
            // 
            // Column
            // 
            Column.HeaderText = "Column";
            Column.Name = "Column";
            Column.ReadOnly = true;
            // 
            // btnParse
            // 
            btnParse.Location = new Point(113, 245);
            btnParse.Name = "btnParse";
            btnParse.Size = new Size(75, 23);
            btnParse.TabIndex = 3;
            btnParse.Text = "Parse";
            btnParse.UseVisualStyleBackColor = true;
            btnParse.Click += btnParse_Click;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(492, 42);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(277, 186);
            txtResult.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtResult);
            Controls.Add(btnParse);
            Controls.Add(dataGridViewTokens);
            Controls.Add(btnTokenize);
            Controls.Add(txtSourceCode);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewTokens).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSourceCode;
        private Button btnTokenize;
        private DataGridView dataGridViewTokens;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn Line;
        private DataGridViewTextBoxColumn Column;
        private Button btnParse;
        private TextBox txtResult;
    }
}
