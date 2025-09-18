using System.Windows.Forms;

namespace APP

{
    partial class frmAtualizador
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtCaminhoLocal;
        private TextBox txtCaminhoRepositorio;
        private Button btnComparar;
        private Button btnProcurarLocal;
        private Button btnProcurarRepositorio;
        private Label lblStatus;
        private Label label1;
        private Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtCaminhoLocal = new System.Windows.Forms.TextBox();
            this.txtCaminhoRepositorio = new System.Windows.Forms.TextBox();
            this.btnComparar = new System.Windows.Forms.Button();
            this.btnProcurarLocal = new System.Windows.Forms.Button();
            this.btnProcurarRepositorio = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSplash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCaminhoLocal
            // 
            this.txtCaminhoLocal.Location = new System.Drawing.Point(120, 20);
            this.txtCaminhoLocal.Name = "txtCaminhoLocal";
            this.txtCaminhoLocal.Size = new System.Drawing.Size(300, 20);
            this.txtCaminhoLocal.TabIndex = 0;
            // 
            // txtCaminhoRepositorio
            // 
            this.txtCaminhoRepositorio.Location = new System.Drawing.Point(120, 60);
            this.txtCaminhoRepositorio.Name = "txtCaminhoRepositorio";
            this.txtCaminhoRepositorio.Size = new System.Drawing.Size(300, 20);
            this.txtCaminhoRepositorio.TabIndex = 1;
            // 
            // btnComparar
            // 
            this.btnComparar.Location = new System.Drawing.Point(180, 100);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(120, 30);
            this.btnComparar.TabIndex = 2;
            this.btnComparar.Text = "Comparar Versões";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // btnProcurarLocal
            // 
            this.btnProcurarLocal.Location = new System.Drawing.Point(430, 18);
            this.btnProcurarLocal.Name = "btnProcurarLocal";
            this.btnProcurarLocal.Size = new System.Drawing.Size(30, 23);
            this.btnProcurarLocal.TabIndex = 3;
            this.btnProcurarLocal.Text = "...";
            this.btnProcurarLocal.UseVisualStyleBackColor = true;
            this.btnProcurarLocal.Click += new System.EventHandler(this.btnProcurarLocal_Click);
            // 
            // btnProcurarRepositorio
            // 
            this.btnProcurarRepositorio.Location = new System.Drawing.Point(430, 58);
            this.btnProcurarRepositorio.Name = "btnProcurarRepositorio";
            this.btnProcurarRepositorio.Size = new System.Drawing.Size(30, 23);
            this.btnProcurarRepositorio.TabIndex = 4;
            this.btnProcurarRepositorio.Text = "...";
            this.btnProcurarRepositorio.UseVisualStyleBackColor = true;
            this.btnProcurarRepositorio.Click += new System.EventHandler(this.btnProcurarRepositorio_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(20, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(440, 23);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status: Aguardando comparação...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Arquivo Local:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Repositório:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSplash
            // 
            this.btnSplash.Location = new System.Drawing.Point(54, 100);
            this.btnSplash.Name = "btnSplash";
            this.btnSplash.Size = new System.Drawing.Size(120, 30);
            this.btnSplash.TabIndex = 8;
            this.btnSplash.Text = "Splash";
            this.btnSplash.UseVisualStyleBackColor = true;
            this.btnSplash.Click += new System.EventHandler(this.btnSplash_Click);
            // 
            // frmAtualizador
            // 
            this.ClientSize = new System.Drawing.Size(484, 191);
            this.Controls.Add(this.btnSplash);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnProcurarRepositorio);
            this.Controls.Add(this.btnProcurarLocal);
            this.Controls.Add(this.btnComparar);
            this.Controls.Add(this.txtCaminhoRepositorio);
            this.Controls.Add(this.txtCaminhoLocal);
            this.Name = "frmAtualizador";
            this.Text = "Atualizador de Aplicação";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button btnSplash;
    }
}