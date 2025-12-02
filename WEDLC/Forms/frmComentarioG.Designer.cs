namespace WEDLC.Forms
{
    partial class frmComentarioG
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
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnComentario = new System.Windows.Forms.Button();
            this.txtTextoComentario = new System.Windows.Forms.TextBox();
            this.txtSiglaComentario = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNomeComentario = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCodigoComentario = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grpBoxDados.SuspendLayout();
            this.grpBotoes.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxDados
            // 
            this.grpBoxDados.Controls.Add(this.txtCodigo);
            this.grpBoxDados.Controls.Add(this.lblCodigo);
            this.grpBoxDados.Location = new System.Drawing.Point(12, 7);
            this.grpBoxDados.Name = "grpBoxDados";
            this.grpBoxDados.Size = new System.Drawing.Size(699, 70);
            this.grpBoxDados.TabIndex = 0;
            this.grpBoxDados.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(6, 38);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(103, 20);
            this.txtCodigo.TabIndex = 1;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(6, 22);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código";
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Location = new System.Drawing.Point(12, 301);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(700, 49);
            this.grpBotoes.TabIndex = 12;
            this.grpBotoes.TabStop = false;
            // 
            // btnGravar
            // 
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(14, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 13;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(609, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 14;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnComentario);
            this.groupBox3.Controls.Add(this.txtTextoComentario);
            this.groupBox3.Controls.Add(this.txtSiglaComentario);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtNomeComentario);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.txtCodigoComentario);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Location = new System.Drawing.Point(11, 84);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(699, 210);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Comentários";
            // 
            // btnComentario
            // 
            this.btnComentario.Image = global::WEDLC.Properties.Resources.add1;
            this.btnComentario.Location = new System.Drawing.Point(662, 32);
            this.btnComentario.Name = "btnComentario";
            this.btnComentario.Size = new System.Drawing.Size(27, 24);
            this.btnComentario.TabIndex = 10;
            this.btnComentario.UseVisualStyleBackColor = true;
            this.btnComentario.Click += new System.EventHandler(this.btnComentario_Click);
            // 
            // txtTextoComentario
            // 
            this.txtTextoComentario.AcceptsReturn = true;
            this.txtTextoComentario.Location = new System.Drawing.Point(6, 61);
            this.txtTextoComentario.MaxLength = 4000;
            this.txtTextoComentario.Multiline = true;
            this.txtTextoComentario.Name = "txtTextoComentario";
            this.txtTextoComentario.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextoComentario.Size = new System.Drawing.Size(683, 141);
            this.txtTextoComentario.TabIndex = 12;
            this.txtTextoComentario.WordWrap = false;
            // 
            // txtSiglaComentario
            // 
            this.txtSiglaComentario.Location = new System.Drawing.Point(115, 35);
            this.txtSiglaComentario.MaxLength = 50;
            this.txtSiglaComentario.Name = "txtSiglaComentario";
            this.txtSiglaComentario.ReadOnly = true;
            this.txtSiglaComentario.Size = new System.Drawing.Size(95, 20);
            this.txtSiglaComentario.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(112, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Sigla";
            // 
            // txtNomeComentario
            // 
            this.txtNomeComentario.Location = new System.Drawing.Point(216, 35);
            this.txtNomeComentario.MaxLength = 50;
            this.txtNomeComentario.Name = "txtNomeComentario";
            this.txtNomeComentario.ReadOnly = true;
            this.txtNomeComentario.Size = new System.Drawing.Size(436, 20);
            this.txtNomeComentario.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(213, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Nome";
            // 
            // txtCodigoComentario
            // 
            this.txtCodigoComentario.ForeColor = System.Drawing.Color.Blue;
            this.txtCodigoComentario.Location = new System.Drawing.Point(6, 35);
            this.txtCodigoComentario.MaxLength = 10;
            this.txtCodigoComentario.Name = "txtCodigoComentario";
            this.txtCodigoComentario.ReadOnly = true;
            this.txtCodigoComentario.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoComentario.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Código";
            // 
            // frmComentarioG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 362);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpBoxDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComentarioG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComentarioG";
            this.Load += new System.EventHandler(this.frmComentarioG_Load);
            this.grpBoxDados.ResumeLayout(false);
            this.grpBoxDados.PerformLayout();
            this.grpBotoes.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxDados;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnComentario;
        private System.Windows.Forms.TextBox txtTextoComentario;
        private System.Windows.Forms.TextBox txtSiglaComentario;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNomeComentario;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtCodigoComentario;
        private System.Windows.Forms.Label label19;
    }
}