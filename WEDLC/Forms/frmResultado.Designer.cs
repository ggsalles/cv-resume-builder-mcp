namespace WEDLC.Forms
{
    partial class frmResultado
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
            this.grpPaciente = new System.Windows.Forms.GroupBox();
            this.grdDadosPessoais = new System.Windows.Forms.DataGridView();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigoProntuario = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.grpfolha = new System.Windows.Forms.GroupBox();
            this.grdFolhaPaciente = new System.Windows.Forms.DataGridView();
            this.grpPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDadosPessoais)).BeginInit();
            this.grpBotoes.SuspendLayout();
            this.grpfolha.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolhaPaciente)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPaciente
            // 
            this.grpPaciente.Controls.Add(this.grdDadosPessoais);
            this.grpPaciente.Controls.Add(this.txtNome);
            this.grpPaciente.Controls.Add(this.lblNome);
            this.grpPaciente.Controls.Add(this.txtCodigoProntuario);
            this.grpPaciente.Controls.Add(this.lblCodigo);
            this.grpPaciente.Location = new System.Drawing.Point(11, 7);
            this.grpPaciente.Name = "grpPaciente";
            this.grpPaciente.Size = new System.Drawing.Size(717, 170);
            this.grpPaciente.TabIndex = 0;
            this.grpPaciente.TabStop = false;
            this.grpPaciente.Text = "Paciente";
            // 
            // grdDadosPessoais
            // 
            this.grdDadosPessoais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDadosPessoais.Location = new System.Drawing.Point(6, 61);
            this.grdDadosPessoais.Name = "grdDadosPessoais";
            this.grdDadosPessoais.Size = new System.Drawing.Size(705, 99);
            this.grdDadosPessoais.TabIndex = 3;
            this.grdDadosPessoais.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDadosPessoais_CellClick);
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(115, 35);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(337, 20);
            this.txtNome.TabIndex = 2;
            this.txtNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNome_KeyUp);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(112, 19);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(35, 13);
            this.lblNome.TabIndex = 2;
            this.lblNome.Text = "Nome";
            // 
            // txtCodigoProntuario
            // 
            this.txtCodigoProntuario.ForeColor = System.Drawing.Color.Blue;
            this.txtCodigoProntuario.Location = new System.Drawing.Point(6, 35);
            this.txtCodigoProntuario.MaxLength = 10;
            this.txtCodigoProntuario.Name = "txtCodigoProntuario";
            this.txtCodigoProntuario.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoProntuario.TabIndex = 1;
            this.txtCodigoProntuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoProntuario_KeyPress);
            this.txtCodigoProntuario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoProntuario_KeyUp);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(3, 19);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(85, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Nº do Prontuário";
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Controls.Add(this.btnLimpar);
            this.grpBotoes.Location = new System.Drawing.Point(11, 340);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(717, 49);
            this.grpBotoes.TabIndex = 24;
            this.grpBotoes.TabStop = false;
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(626, 14);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 24;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Image = global::WEDLC.Properties.Resources.trash;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpar.Location = new System.Drawing.Point(7, 14);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(85, 27);
            this.btnLimpar.TabIndex = 25;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // grpfolha
            // 
            this.grpfolha.BackColor = System.Drawing.SystemColors.Control;
            this.grpfolha.Controls.Add(this.grdFolhaPaciente);
            this.grpfolha.Location = new System.Drawing.Point(11, 179);
            this.grpfolha.Name = "grpfolha";
            this.grpfolha.Size = new System.Drawing.Size(717, 159);
            this.grpfolha.TabIndex = 25;
            this.grpfolha.TabStop = false;
            this.grpfolha.Text = "Folha Paciente";
            // 
            // grdFolhaPaciente
            // 
            this.grdFolhaPaciente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFolhaPaciente.Location = new System.Drawing.Point(6, 16);
            this.grdFolhaPaciente.Name = "grdFolhaPaciente";
            this.grdFolhaPaciente.Size = new System.Drawing.Size(705, 133);
            this.grdFolhaPaciente.TabIndex = 5;
            this.grdFolhaPaciente.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFolhaPaciente_CellDoubleClick);
            // 
            // frmResultado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 398);
            this.ControlBox = false;
            this.Controls.Add(this.grpfolha);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpPaciente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResultado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resultados";
            this.Load += new System.EventHandler(this.frmResultado_Load);
            this.grpPaciente.ResumeLayout(false);
            this.grpPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDadosPessoais)).EndInit();
            this.grpBotoes.ResumeLayout(false);
            this.grpfolha.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFolhaPaciente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPaciente;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigoProntuario;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.DataGridView grdDadosPessoais;
        private System.Windows.Forms.GroupBox grpfolha;
        private System.Windows.Forms.DataGridView grdFolhaPaciente;
    }
}