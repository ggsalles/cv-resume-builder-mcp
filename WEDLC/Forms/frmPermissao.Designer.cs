namespace WEDLC.Forms
{
    partial class frmPermissao
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
            this.grpBoxDados = new System.Windows.Forms.GroupBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.grpPermissao = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboPermissao = new System.Windows.Forms.ComboBox();
            this.txtModulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grdPermissao = new System.Windows.Forms.DataGridView();
            this.grdUsuario = new System.Windows.Forms.DataGridView();
            this.grpBoxDados.SuspendLayout();
            this.grpBotoes.SuspendLayout();
            this.grpPermissao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPermissao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBoxDados
            // 
            this.grpBoxDados.Controls.Add(this.txtNome);
            this.grpBoxDados.Controls.Add(this.lblNome);
            this.grpBoxDados.Controls.Add(this.txtCodigo);
            this.grpBoxDados.Controls.Add(this.lblCodigo);
            this.grpBoxDados.Location = new System.Drawing.Point(12, 5);
            this.grpBoxDados.Name = "grpBoxDados";
            this.grpBoxDados.Size = new System.Drawing.Size(501, 64);
            this.grpBoxDados.TabIndex = 0;
            this.grpBoxDados.TabStop = false;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(115, 35);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(364, 20);
            this.txtNome.TabIndex = 2;
            this.txtNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNome_KeyUp);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(115, 19);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 2;
            this.lblNome.Text = "Nome";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(6, 35);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(103, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(6, 19);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código";
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnLimpar);
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Location = new System.Drawing.Point(11, 483);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(502, 49);
            this.grpBotoes.TabIndex = 1;
            this.grpBotoes.TabStop = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Image = global::WEDLC.Properties.Resources.trash;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.Location = new System.Drawing.Point(101, 13);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(85, 27);
            this.btnLimpar.TabIndex = 26;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(10, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 6;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(407, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // grpPermissao
            // 
            this.grpPermissao.Controls.Add(this.label14);
            this.grpPermissao.Controls.Add(this.cboPermissao);
            this.grpPermissao.Controls.Add(this.txtModulo);
            this.grpPermissao.Controls.Add(this.label1);
            this.grpPermissao.Controls.Add(this.grdPermissao);
            this.grpPermissao.Controls.Add(this.grdUsuario);
            this.grpPermissao.Location = new System.Drawing.Point(12, 73);
            this.grpPermissao.Name = "grpPermissao";
            this.grpPermissao.Size = new System.Drawing.Size(501, 405);
            this.grpPermissao.TabIndex = 2;
            this.grpPermissao.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(304, 357);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 13);
            this.label14.TabIndex = 35;
            this.label14.Text = "Tipo da Permissão";
            // 
            // cboPermissao
            // 
            this.cboPermissao.FormattingEnabled = true;
            this.cboPermissao.Location = new System.Drawing.Point(304, 373);
            this.cboPermissao.Name = "cboPermissao";
            this.cboPermissao.Size = new System.Drawing.Size(187, 21);
            this.cboPermissao.TabIndex = 34;
            // 
            // txtModulo
            // 
            this.txtModulo.Enabled = false;
            this.txtModulo.Location = new System.Drawing.Point(12, 374);
            this.txtModulo.MaxLength = 10;
            this.txtModulo.Name = "txtModulo";
            this.txtModulo.Size = new System.Drawing.Size(286, 20);
            this.txtModulo.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Módulo";
            // 
            // grdPermissao
            // 
            this.grdPermissao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPermissao.Location = new System.Drawing.Point(11, 181);
            this.grdPermissao.Name = "grdPermissao";
            this.grdPermissao.Size = new System.Drawing.Size(480, 170);
            this.grdPermissao.TabIndex = 31;
            this.grdPermissao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPermissao_CellClick);
            // 
            // grdUsuario
            // 
            this.grdUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsuario.Location = new System.Drawing.Point(11, 18);
            this.grdUsuario.Name = "grdUsuario";
            this.grdUsuario.Size = new System.Drawing.Size(480, 154);
            this.grdUsuario.TabIndex = 30;
            this.grdUsuario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdUsuario_CellClick);
            // 
            // frmPermissao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 542);
            this.ControlBox = false;
            this.Controls.Add(this.grpPermissao);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpBoxDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPermissao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Permissao";
            this.Load += new System.EventHandler(this.frmPermissão_Load);
            this.grpBoxDados.ResumeLayout(false);
            this.grpBoxDados.PerformLayout();
            this.grpBotoes.ResumeLayout(false);
            this.grpPermissao.ResumeLayout(false);
            this.grpPermissao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPermissao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxDados;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.GroupBox grpPermissao;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboPermissao;
        private System.Windows.Forms.TextBox txtModulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdPermissao;
        private System.Windows.Forms.DataGridView grdUsuario;
    }
}