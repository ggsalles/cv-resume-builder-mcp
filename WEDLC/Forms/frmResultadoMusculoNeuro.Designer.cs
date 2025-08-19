namespace WEDLC.Forms
{
    partial class frmResultadoMusculoNeuro
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
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdAvaliacaoMuscular = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAtividadeInsercao = new System.Windows.Forms.Button();
            this.txtSiglaAtividade = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNomeAtividade = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCodAtividade = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTextoAtividade = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPotenciaisUnidade = new System.Windows.Forms.Button();
            this.txtSiglaPotencial = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNomePotencial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCodigoPotencial = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTextoPotencial = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdNeuroConducaoMotora = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.grdNeuroConducaoSensorial = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnComentario = new System.Windows.Forms.Button();
            this.txtTextoComentario = new System.Windows.Forms.TextBox();
            this.txtSiglaComentario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNomeComentario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigoComentario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBotoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoMotora)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoSensorial)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Location = new System.Drawing.Point(12, 758);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(1289, 47);
            this.grpBotoes.TabIndex = 24;
            this.grpBotoes.TabStop = false;
            // 
            // btnGravar
            // 
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(6, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 25;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1196, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 24;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.grdAvaliacaoMuscular);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 249);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Músculos Examinados";
            // 
            // grdAvaliacaoMuscular
            // 
            this.grdAvaliacaoMuscular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAvaliacaoMuscular.Location = new System.Drawing.Point(6, 16);
            this.grdAvaliacaoMuscular.Name = "grdAvaliacaoMuscular";
            this.grdAvaliacaoMuscular.Size = new System.Drawing.Size(659, 226);
            this.grdAvaliacaoMuscular.TabIndex = 7;
            this.grdAvaliacaoMuscular.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdAvaliacaoMuscular_CellValidating);
            this.grdAvaliacaoMuscular.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdAvaliacaoMuscular_EditingControlShowing);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btnAtividadeInsercao);
            this.groupBox2.Controls.Add(this.txtSiglaAtividade);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNomeAtividade);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCodAtividade);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTextoAtividade);
            this.groupBox2.Location = new System.Drawing.Point(690, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 249);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Atividade de Inserção e Pós Inserção";
            // 
            // btnAtividadeInsercao
            // 
            this.btnAtividadeInsercao.Image = global::WEDLC.Properties.Resources.add1;
            this.btnAtividadeInsercao.Location = new System.Drawing.Point(578, 33);
            this.btnAtividadeInsercao.Name = "btnAtividadeInsercao";
            this.btnAtividadeInsercao.Size = new System.Drawing.Size(27, 24);
            this.btnAtividadeInsercao.TabIndex = 27;
            this.btnAtividadeInsercao.UseVisualStyleBackColor = true;
            this.btnAtividadeInsercao.Click += new System.EventHandler(this.btnAtividadeInsercao_Click);
            // 
            // txtSiglaAtividade
            // 
            this.txtSiglaAtividade.Location = new System.Drawing.Point(117, 35);
            this.txtSiglaAtividade.MaxLength = 50;
            this.txtSiglaAtividade.Name = "txtSiglaAtividade";
            this.txtSiglaAtividade.ReadOnly = true;
            this.txtSiglaAtividade.Size = new System.Drawing.Size(95, 20);
            this.txtSiglaAtividade.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Sigla";
            // 
            // txtNomeAtividade
            // 
            this.txtNomeAtividade.Location = new System.Drawing.Point(218, 35);
            this.txtNomeAtividade.MaxLength = 50;
            this.txtNomeAtividade.Name = "txtNomeAtividade";
            this.txtNomeAtividade.ReadOnly = true;
            this.txtNomeAtividade.Size = new System.Drawing.Size(354, 20);
            this.txtNomeAtividade.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Nome";
            // 
            // txtCodAtividade
            // 
            this.txtCodAtividade.ForeColor = System.Drawing.Color.Blue;
            this.txtCodAtividade.Location = new System.Drawing.Point(8, 35);
            this.txtCodAtividade.MaxLength = 10;
            this.txtCodAtividade.Name = "txtCodAtividade";
            this.txtCodAtividade.ReadOnly = true;
            this.txtCodAtividade.Size = new System.Drawing.Size(103, 20);
            this.txtCodAtividade.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Código";
            // 
            // txtTextoAtividade
            // 
            this.txtTextoAtividade.AcceptsReturn = true;
            this.txtTextoAtividade.Location = new System.Drawing.Point(8, 63);
            this.txtTextoAtividade.MaxLength = 4000;
            this.txtTextoAtividade.Multiline = true;
            this.txtTextoAtividade.Name = "txtTextoAtividade";
            this.txtTextoAtividade.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextoAtividade.Size = new System.Drawing.Size(597, 179);
            this.txtTextoAtividade.TabIndex = 19;
            this.txtTextoAtividade.WordWrap = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.btnPotenciaisUnidade);
            this.groupBox3.Controls.Add(this.txtSiglaPotencial);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtNomePotencial);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtCodigoPotencial);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtTextoPotencial);
            this.groupBox3.Location = new System.Drawing.Point(690, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(610, 249);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Potenciais de Unidade Motora";
            // 
            // btnPotenciaisUnidade
            // 
            this.btnPotenciaisUnidade.Image = global::WEDLC.Properties.Resources.add1;
            this.btnPotenciaisUnidade.Location = new System.Drawing.Point(578, 36);
            this.btnPotenciaisUnidade.Name = "btnPotenciaisUnidade";
            this.btnPotenciaisUnidade.Size = new System.Drawing.Size(27, 24);
            this.btnPotenciaisUnidade.TabIndex = 33;
            this.btnPotenciaisUnidade.UseVisualStyleBackColor = true;
            this.btnPotenciaisUnidade.Click += new System.EventHandler(this.btnPotenciaisUnidade_Click);
            // 
            // txtSiglaPotencial
            // 
            this.txtSiglaPotencial.Location = new System.Drawing.Point(117, 38);
            this.txtSiglaPotencial.MaxLength = 50;
            this.txtSiglaPotencial.Name = "txtSiglaPotencial";
            this.txtSiglaPotencial.ReadOnly = true;
            this.txtSiglaPotencial.Size = new System.Drawing.Size(95, 20);
            this.txtSiglaPotencial.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Sigla";
            // 
            // txtNomePotencial
            // 
            this.txtNomePotencial.Location = new System.Drawing.Point(218, 38);
            this.txtNomePotencial.MaxLength = 50;
            this.txtNomePotencial.Name = "txtNomePotencial";
            this.txtNomePotencial.ReadOnly = true;
            this.txtNomePotencial.Size = new System.Drawing.Size(354, 20);
            this.txtNomePotencial.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Nome";
            // 
            // txtCodigoPotencial
            // 
            this.txtCodigoPotencial.ForeColor = System.Drawing.Color.Blue;
            this.txtCodigoPotencial.Location = new System.Drawing.Point(8, 38);
            this.txtCodigoPotencial.MaxLength = 10;
            this.txtCodigoPotencial.Name = "txtCodigoPotencial";
            this.txtCodigoPotencial.ReadOnly = true;
            this.txtCodigoPotencial.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoPotencial.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Código";
            // 
            // txtTextoPotencial
            // 
            this.txtTextoPotencial.AcceptsReturn = true;
            this.txtTextoPotencial.Location = new System.Drawing.Point(6, 64);
            this.txtTextoPotencial.MaxLength = 4000;
            this.txtTextoPotencial.Multiline = true;
            this.txtTextoPotencial.Name = "txtTextoPotencial";
            this.txtTextoPotencial.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextoPotencial.Size = new System.Drawing.Size(597, 179);
            this.txtTextoPotencial.TabIndex = 21;
            this.txtTextoPotencial.WordWrap = false;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.grdNeuroConducaoMotora);
            this.groupBox4.Location = new System.Drawing.Point(12, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(671, 249);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Velocidade de Neurocondução Motora";
            // 
            // grdNeuroConducaoMotora
            // 
            this.grdNeuroConducaoMotora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroConducaoMotora.Location = new System.Drawing.Point(6, 16);
            this.grdNeuroConducaoMotora.Name = "grdNeuroConducaoMotora";
            this.grdNeuroConducaoMotora.Size = new System.Drawing.Size(659, 226);
            this.grdNeuroConducaoMotora.TabIndex = 9;
            this.grdNeuroConducaoMotora.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNeuroConducaoMotora_CellEndEdit);
            this.grdNeuroConducaoMotora.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdNeuroConducaoMotora_EditingControlShowing);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add(this.grdNeuroConducaoSensorial);
            this.groupBox5.Location = new System.Drawing.Point(12, 508);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(671, 249);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Neurocondução Sensorial";
            // 
            // grdNeuroConducaoSensorial
            // 
            this.grdNeuroConducaoSensorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroConducaoSensorial.Location = new System.Drawing.Point(6, 16);
            this.grdNeuroConducaoSensorial.Name = "grdNeuroConducaoSensorial";
            this.grdNeuroConducaoSensorial.Size = new System.Drawing.Size(659, 226);
            this.grdNeuroConducaoSensorial.TabIndex = 11;
            this.grdNeuroConducaoSensorial.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNeuroConducaoSensorial_CellEndEdit);
            this.grdNeuroConducaoSensorial.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdNeuroConducaoSensorial_EditingControlShowing);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnComentario);
            this.groupBox6.Controls.Add(this.txtTextoComentario);
            this.groupBox6.Controls.Add(this.txtSiglaComentario);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.txtNomeComentario);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.txtCodigoComentario);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Location = new System.Drawing.Point(690, 508);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(610, 249);
            this.groupBox6.TabIndex = 25;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Comentários";
            // 
            // btnComentario
            // 
            this.btnComentario.Image = global::WEDLC.Properties.Resources.add1;
            this.btnComentario.Location = new System.Drawing.Point(578, 33);
            this.btnComentario.Name = "btnComentario";
            this.btnComentario.Size = new System.Drawing.Size(27, 24);
            this.btnComentario.TabIndex = 34;
            this.btnComentario.UseVisualStyleBackColor = true;
            this.btnComentario.Click += new System.EventHandler(this.btnComentario_Click);
            // 
            // txtTextoComentario
            // 
            this.txtTextoComentario.AcceptsReturn = true;
            this.txtTextoComentario.Location = new System.Drawing.Point(8, 63);
            this.txtTextoComentario.MaxLength = 4000;
            this.txtTextoComentario.Multiline = true;
            this.txtTextoComentario.Name = "txtTextoComentario";
            this.txtTextoComentario.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextoComentario.Size = new System.Drawing.Size(597, 179);
            this.txtTextoComentario.TabIndex = 17;
            this.txtTextoComentario.WordWrap = false;
            // 
            // txtSiglaComentario
            // 
            this.txtSiglaComentario.Location = new System.Drawing.Point(115, 35);
            this.txtSiglaComentario.MaxLength = 50;
            this.txtSiglaComentario.Name = "txtSiglaComentario";
            this.txtSiglaComentario.ReadOnly = true;
            this.txtSiglaComentario.Size = new System.Drawing.Size(95, 20);
            this.txtSiglaComentario.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Sigla";
            // 
            // txtNomeComentario
            // 
            this.txtNomeComentario.Location = new System.Drawing.Point(216, 35);
            this.txtNomeComentario.MaxLength = 50;
            this.txtNomeComentario.Name = "txtNomeComentario";
            this.txtNomeComentario.ReadOnly = true;
            this.txtNomeComentario.Size = new System.Drawing.Size(356, 20);
            this.txtNomeComentario.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome";
            // 
            // txtCodigoComentario
            // 
            this.txtCodigoComentario.ForeColor = System.Drawing.Color.Blue;
            this.txtCodigoComentario.Location = new System.Drawing.Point(6, 35);
            this.txtCodigoComentario.MaxLength = 10;
            this.txtCodigoComentario.Name = "txtCodigoComentario";
            this.txtCodigoComentario.ReadOnly = true;
            this.txtCodigoComentario.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoComentario.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Código";
            // 
            // frmResultadoMusculoNeuro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 814);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBotoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResultadoMusculoNeuro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inormações do Resultado";
            this.Load += new System.EventHandler(this.frmResultadoMusculoNeuro_Load);
            this.grpBotoes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoMotora)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoSensorial)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grdAvaliacaoMuscular;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTextoAtividade;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTextoPotencial;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView grdNeuroConducaoMotora;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView grdNeuroConducaoSensorial;
        private System.Windows.Forms.TextBox txtSiglaAtividade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNomeAtividade;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCodAtividade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSiglaPotencial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNomePotencial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCodigoPotencial;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtTextoComentario;
        private System.Windows.Forms.TextBox txtSiglaComentario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNomeComentario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigoComentario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAtividadeInsercao;
        private System.Windows.Forms.Button btnPotenciaisUnidade;
        private System.Windows.Forms.Button btnComentario;
        private System.Windows.Forms.Button btnGravar;
    }
}