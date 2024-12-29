namespace MiniCompiler
{
    partial class ParseTreeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTree = new Panel();
            SuspendLayout();
            // 
            // panelTree
            // 
            panelTree.AutoScroll = true;
            panelTree.Dock = DockStyle.Fill;
            panelTree.Location = new Point(0, 0);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(769, 450);
            panelTree.TabIndex = 0;
            // 
            // ParseTreeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(769, 450);
            Controls.Add(panelTree);
            Name = "ParseTreeForm";
            Text = "ParseTreeForm";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTree;
    }
}