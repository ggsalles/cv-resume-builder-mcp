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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grdAvaliacaoMuscular = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAtividadeInsercao = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPotenciaisUnidade = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grdNeuroConducaoMotora = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.grdNeuroConducaoSensorial = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grdComentario = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBotoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoMotora)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoSensorial)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComentario)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnCancelar);
            this.grpBotoes.Controls.Add(this.btnExcluir);
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Controls.Add(this.btnNovo);
            this.grpBotoes.Location = new System.Drawing.Point(12, 763);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(1228, 47);
            this.grpBotoes.TabIndex = 24;
            this.grpBotoes.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = global::WEDLC.Properties.Resources.cancelblue;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(99, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 27);
            this.btnCancelar.TabIndex = 23;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = global::WEDLC.Properties.Resources.trash;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(484, 14);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(85, 27);
            this.btnExcluir.TabIndex = 26;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Visible = false;
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(8, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 22;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1133, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 24;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Image = global::WEDLC.Properties.Resources.add;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(391, 14);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(85, 27);
            this.btnNovo.TabIndex = 25;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.grdAvaliacaoMuscular);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 249);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Músculos Examinados";
            // 
            // grdAvaliacaoMuscular
            // 
            this.grdAvaliacaoMuscular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAvaliacaoMuscular.Location = new System.Drawing.Point(6, 16);
            this.grdAvaliacaoMuscular.Name = "grdAvaliacaoMuscular";
            this.grdAvaliacaoMuscular.Size = new System.Drawing.Size(597, 226);
            this.grdAvaliacaoMuscular.TabIndex = 7;
            this.grdAvaliacaoMuscular.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdAvaliacaoMuscular_CellValidating);
            this.grdAvaliacaoMuscular.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdAvaliacaoMuscular_EditingControlShowing);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtAtividadeInsercao);
            this.groupBox2.Location = new System.Drawing.Point(630, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(610, 249);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Atividade de Inserção e Pós Inserção";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 35);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 20);
            this.textBox1.TabIndex = 24;
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(597, 83);
            this.dataGridView1.TabIndex = 26;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(218, 35);
            this.textBox5.MaxLength = 50;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(387, 20);
            this.textBox5.TabIndex = 25;
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
            // textBox6
            // 
            this.textBox6.ForeColor = System.Drawing.Color.Blue;
            this.textBox6.Location = new System.Drawing.Point(8, 35);
            this.textBox6.MaxLength = 10;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(103, 20);
            this.textBox6.TabIndex = 23;
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
            // txtAtividadeInsercao
            // 
            this.txtAtividadeInsercao.AcceptsReturn = true;
            this.txtAtividadeInsercao.Location = new System.Drawing.Point(6, 151);
            this.txtAtividadeInsercao.MaxLength = 4000;
            this.txtAtividadeInsercao.Multiline = true;
            this.txtAtividadeInsercao.Name = "txtAtividadeInsercao";
            this.txtAtividadeInsercao.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAtividadeInsercao.Size = new System.Drawing.Size(597, 91);
            this.txtAtividadeInsercao.TabIndex = 19;
            this.txtAtividadeInsercao.WordWrap = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtPotenciaisUnidade);
            this.groupBox3.Location = new System.Drawing.Point(629, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(610, 256);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Potenciais de Unidade Motora";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(117, 38);
            this.textBox7.MaxLength = 50;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(95, 20);
            this.textBox7.TabIndex = 31;
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
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(8, 64);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(597, 83);
            this.dataGridView2.TabIndex = 33;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(218, 38);
            this.textBox8.MaxLength = 50;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(387, 20);
            this.textBox8.TabIndex = 32;
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
            // textBox9
            // 
            this.textBox9.ForeColor = System.Drawing.Color.Blue;
            this.textBox9.Location = new System.Drawing.Point(8, 38);
            this.textBox9.MaxLength = 10;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(103, 20);
            this.textBox9.TabIndex = 30;
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
            // txtPotenciaisUnidade
            // 
            this.txtPotenciaisUnidade.AcceptsReturn = true;
            this.txtPotenciaisUnidade.Location = new System.Drawing.Point(6, 154);
            this.txtPotenciaisUnidade.MaxLength = 4000;
            this.txtPotenciaisUnidade.Multiline = true;
            this.txtPotenciaisUnidade.Name = "txtPotenciaisUnidade";
            this.txtPotenciaisUnidade.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPotenciaisUnidade.Size = new System.Drawing.Size(597, 92);
            this.txtPotenciaisUnidade.TabIndex = 21;
            this.txtPotenciaisUnidade.WordWrap = false;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.grdNeuroConducaoMotora);
            this.groupBox4.Location = new System.Drawing.Point(12, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(610, 256);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Velocidade de Neurocondução Motora";
            // 
            // grdNeuroConducaoMotora
            // 
            this.grdNeuroConducaoMotora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroConducaoMotora.Location = new System.Drawing.Point(6, 16);
            this.grdNeuroConducaoMotora.Name = "grdNeuroConducaoMotora";
            this.grdNeuroConducaoMotora.Size = new System.Drawing.Size(597, 230);
            this.grdNeuroConducaoMotora.TabIndex = 9;
            this.grdNeuroConducaoMotora.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdNeuroConducaoMotora_CellEndEdit);
            this.grdNeuroConducaoMotora.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdNeuroConducaoMotora_EditingControlShowing);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add(this.grdNeuroConducaoSensorial);
            this.groupBox5.Location = new System.Drawing.Point(12, 516);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(610, 248);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Neurocondução Sensorial";
            // 
            // grdNeuroConducaoSensorial
            // 
            this.grdNeuroConducaoSensorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroConducaoSensorial.Location = new System.Drawing.Point(6, 16);
            this.grdNeuroConducaoSensorial.Name = "grdNeuroConducaoSensorial";
            this.grdNeuroConducaoSensorial.Size = new System.Drawing.Size(597, 225);
            this.grdNeuroConducaoSensorial.TabIndex = 11;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtComentario);
            this.groupBox6.Controls.Add(this.textBox4);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.grdComentario);
            this.groupBox6.Controls.Add(this.textBox2);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Location = new System.Drawing.Point(630, 516);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(610, 248);
            this.groupBox6.TabIndex = 25;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Comentários";
            // 
            // txtComentario
            // 
            this.txtComentario.AcceptsReturn = true;
            this.txtComentario.Location = new System.Drawing.Point(6, 150);
            this.txtComentario.MaxLength = 4000;
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComentario.Size = new System.Drawing.Size(597, 91);
            this.txtComentario.TabIndex = 17;
            this.txtComentario.WordWrap = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(115, 35);
            this.textBox4.MaxLength = 50;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(95, 20);
            this.textBox4.TabIndex = 14;
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
            // grdComentario
            // 
            this.grdComentario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdComentario.Location = new System.Drawing.Point(6, 61);
            this.grdComentario.Name = "grdComentario";
            this.grdComentario.Size = new System.Drawing.Size(597, 83);
            this.grdComentario.TabIndex = 16;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(216, 35);
            this.textBox2.MaxLength = 50;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(387, 20);
            this.textBox2.TabIndex = 15;
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
            // textBox3
            // 
            this.textBox3.ForeColor = System.Drawing.Color.Blue;
            this.textBox3.Location = new System.Drawing.Point(6, 35);
            this.textBox3.MaxLength = 10;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(103, 20);
            this.textBox3.TabIndex = 13;
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
            this.ClientSize = new System.Drawing.Size(1252, 820);
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
            this.Load += new System.EventHandler(this.frmPaciente_Load);
            this.grpBotoes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoMotora)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroConducaoSensorial)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComentario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grdAvaliacaoMuscular;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAtividadeInsercao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPotenciaisUnidade;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView grdNeuroConducaoMotora;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView grdNeuroConducaoSensorial;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView grdComentario;
    }
}