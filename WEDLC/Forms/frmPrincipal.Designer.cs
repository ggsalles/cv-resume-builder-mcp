namespace WEDLC.Forms
{
    partial class frmPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manutençãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.especializacaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folhaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.músculoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nervoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comentárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.manutençãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // manutençãoToolStripMenuItem
            // 
            this.manutençãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabelasToolStripMenuItem});
            this.manutençãoToolStripMenuItem.Name = "manutençãoToolStripMenuItem";
            this.manutençãoToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.manutençãoToolStripMenuItem.Text = "Manutenção";
            // 
            // tabelasToolStripMenuItem
            // 
            this.tabelasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comentárioToolStripMenuItem,
            this.especializacaoToolStripMenuItem,
            this.folhaToolStripMenuItem,
            this.músculoToolStripMenuItem,
            this.nervoToolStripMenuItem});
            this.tabelasToolStripMenuItem.Name = "tabelasToolStripMenuItem";
            this.tabelasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tabelasToolStripMenuItem.Text = "Tabelas";
            // 
            // especializacaoToolStripMenuItem
            // 
            this.especializacaoToolStripMenuItem.Name = "especializacaoToolStripMenuItem";
            this.especializacaoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.especializacaoToolStripMenuItem.Text = "Especialização";
            this.especializacaoToolStripMenuItem.Click += new System.EventHandler(this.especializacaoToolStripMenuItem_Click);
            // 
            // folhaToolStripMenuItem
            // 
            this.folhaToolStripMenuItem.Name = "folhaToolStripMenuItem";
            this.folhaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.folhaToolStripMenuItem.Text = "Folha";
            this.folhaToolStripMenuItem.Click += new System.EventHandler(this.folhaToolStripMenuItem_Click);
            // 
            // músculoToolStripMenuItem
            // 
            this.músculoToolStripMenuItem.Name = "músculoToolStripMenuItem";
            this.músculoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.músculoToolStripMenuItem.Text = "Músculo";
            this.músculoToolStripMenuItem.Click += new System.EventHandler(this.músculoToolStripMenuItem_Click);
            // 
            // nervoToolStripMenuItem
            // 
            this.nervoToolStripMenuItem.Name = "nervoToolStripMenuItem";
            this.nervoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nervoToolStripMenuItem.Text = "Nervo";
            this.nervoToolStripMenuItem.Click += new System.EventHandler(this.nervoToolStripMenuItem_Click);
            // 
            // comentárioToolStripMenuItem
            // 
            this.comentárioToolStripMenuItem.Name = "comentárioToolStripMenuItem";
            this.comentárioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.comentárioToolStripMenuItem.Text = "Comentário";
            this.comentárioToolStripMenuItem.Click += new System.EventHandler(this.comentárioToolStripMenuItem_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manutençãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tabelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem especializacaoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nervoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem músculoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folhaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comentárioToolStripMenuItem;
    }
}