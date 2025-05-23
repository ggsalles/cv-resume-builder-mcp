namespace WEDLC.Forms
{
    partial class frmFolha
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
            this.grdDados = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.cboTipo = new System.Windows.Forms.ComboBox();
            this.txtSigla = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtEstudoPotencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grdEstudoPotenciais = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboAvalicaoMuscular = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.grdAvaliacaoMuscular = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboNeuroConducaoMotora = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.grdNeuroCondMotora = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboNeuroConducaoSensorial = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.grdNeuroCondSensorial = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoNSPD = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoReflexo = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoRBC = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoBlink = new System.Windows.Forms.ComboBox();
            this.grpBoxDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDados)).BeginInit();
            this.grpBotoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstudoPotenciais)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondMotora)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondSensorial)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxDados
            // 
            this.grpBoxDados.Controls.Add(this.grdDados);
            this.grpBoxDados.Controls.Add(this.label3);
            this.grpBoxDados.Controls.Add(this.label2);
            this.grpBoxDados.Controls.Add(this.cboGrupo);
            this.grpBoxDados.Controls.Add(this.cboTipo);
            this.grpBoxDados.Controls.Add(this.txtSigla);
            this.grpBoxDados.Controls.Add(this.label1);
            this.grpBoxDados.Controls.Add(this.txtNome);
            this.grpBoxDados.Controls.Add(this.lblNome);
            this.grpBoxDados.Controls.Add(this.txtCodigo);
            this.grpBoxDados.Controls.Add(this.lblCodigo);
            this.grpBoxDados.Location = new System.Drawing.Point(12, 12);
            this.grpBoxDados.Name = "grpBoxDados";
            this.grpBoxDados.Size = new System.Drawing.Size(1145, 242);
            this.grpBoxDados.TabIndex = 0;
            this.grpBoxDados.TabStop = false;
            // 
            // grdDados
            // 
            this.grdDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDados.Location = new System.Drawing.Point(6, 61);
            this.grdDados.Name = "grdDados";
            this.grdDados.Size = new System.Drawing.Size(1130, 168);
            this.grdDados.TabIndex = 6;
            this.grdDados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDados_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(566, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tipo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(718, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Grupo";
            // 
            // cboGrupo
            // 
            this.cboGrupo.Enabled = false;
            this.cboGrupo.FormattingEnabled = true;
            this.cboGrupo.Location = new System.Drawing.Point(718, 35);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(140, 21);
            this.cboGrupo.TabIndex = 5;
            this.cboGrupo.Validating += new System.ComponentModel.CancelEventHandler(this.cboGrupo_Validating);
            // 
            // cboTipo
            // 
            this.cboTipo.Enabled = false;
            this.cboTipo.FormattingEnabled = true;
            this.cboTipo.Location = new System.Drawing.Point(566, 35);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new System.Drawing.Size(146, 21);
            this.cboTipo.TabIndex = 4;
            this.cboTipo.SelectedIndexChanged += new System.EventHandler(this.cboTipo_SelectedIndexChanged);
            this.cboTipo.Validating += new System.ComponentModel.CancelEventHandler(this.cboTipo_Validating);
            this.cboTipo.Validated += new System.EventHandler(this.cboTipo_Validated);
            // 
            // txtSigla
            // 
            this.txtSigla.Location = new System.Drawing.Point(115, 35);
            this.txtSigla.MaxLength = 10;
            this.txtSigla.Name = "txtSigla";
            this.txtSigla.Size = new System.Drawing.Size(103, 20);
            this.txtSigla.TabIndex = 2;
            this.txtSigla.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSigla_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sigla";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(223, 35);
            this.txtNome.MaxLength = 50;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(337, 20);
            this.txtNome.TabIndex = 3;
            this.txtNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNome_KeyUp);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(220, 19);
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
            this.lblCodigo.Location = new System.Drawing.Point(3, 19);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Código";
            // 
            // grpBotoes
            // 
            this.grpBotoes.Controls.Add(this.btnAlterar);
            this.grpBotoes.Controls.Add(this.btnCancelar);
            this.grpBotoes.Controls.Add(this.btnExcluir);
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Controls.Add(this.btnNovo);
            this.grpBotoes.Location = new System.Drawing.Point(11, 542);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(1146, 46);
            this.grpBotoes.TabIndex = 1;
            this.grpBotoes.TabStop = false;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Enabled = false;
            this.btnAlterar.Image = global::WEDLC.Properties.Resources.edit;
            this.btnAlterar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlterar.Location = new System.Drawing.Point(100, 13);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(85, 27);
            this.btnAlterar.TabIndex = 28;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = global::WEDLC.Properties.Resources.cancelblue;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(373, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 27);
            this.btnCancelar.TabIndex = 31;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = global::WEDLC.Properties.Resources.trash;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(191, 13);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(85, 27);
            this.btnExcluir.TabIndex = 29;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(282, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 30;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1052, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 32;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Image = global::WEDLC.Properties.Resources.add;
            this.btnNovo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNovo.Location = new System.Drawing.Point(9, 13);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(85, 27);
            this.btnNovo.TabIndex = 27;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtEstudoPotencia);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.grdEstudoPotenciais);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(876, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 194);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estudo de Potenciais Evocados";
            // 
            // button2
            // 
            this.button2.Image = global::WEDLC.Properties.Resources.up;
            this.button2.Location = new System.Drawing.Point(246, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 24);
            this.button2.TabIndex = 21;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::WEDLC.Properties.Resources.down;
            this.button1.Location = new System.Drawing.Point(213, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 24);
            this.button1.TabIndex = 20;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtEstudoPotencia
            // 
            this.txtEstudoPotencia.Location = new System.Drawing.Point(9, 40);
            this.txtEstudoPotencia.MaxLength = 10;
            this.txtEstudoPotencia.Name = "txtEstudoPotencia";
            this.txtEstudoPotencia.Size = new System.Drawing.Size(198, 20);
            this.txtEstudoPotencia.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Código";
            // 
            // grdEstudoPotenciais
            // 
            this.grdEstudoPotenciais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEstudoPotenciais.Location = new System.Drawing.Point(9, 68);
            this.grdEstudoPotenciais.Name = "grdEstudoPotenciais";
            this.grdEstudoPotenciais.Size = new System.Drawing.Size(264, 111);
            this.grdEstudoPotenciais.TabIndex = 22;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboAvalicaoMuscular);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.grdAvaliacaoMuscular);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(282, 194);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Avaliação Muscular";
            // 
            // cboAvalicaoMuscular
            // 
            this.cboAvalicaoMuscular.FormattingEnabled = true;
            this.cboAvalicaoMuscular.Location = new System.Drawing.Point(9, 38);
            this.cboAvalicaoMuscular.Name = "cboAvalicaoMuscular";
            this.cboAvalicaoMuscular.Size = new System.Drawing.Size(198, 21);
            this.cboAvalicaoMuscular.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Image = global::WEDLC.Properties.Resources.up;
            this.button3.Location = new System.Drawing.Point(246, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 24);
            this.button3.TabIndex = 9;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::WEDLC.Properties.Resources.down;
            this.button4.Location = new System.Drawing.Point(213, 38);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(27, 24);
            this.button4.TabIndex = 8;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Código";
            // 
            // grdAvaliacaoMuscular
            // 
            this.grdAvaliacaoMuscular.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAvaliacaoMuscular.Location = new System.Drawing.Point(9, 68);
            this.grdAvaliacaoMuscular.Name = "grdAvaliacaoMuscular";
            this.grdAvaliacaoMuscular.Size = new System.Drawing.Size(264, 111);
            this.grdAvaliacaoMuscular.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboNeuroConducaoMotora);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.grdNeuroCondMotora);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(300, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 194);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Neuro Condução Motora";
            // 
            // cboNeuroConducaoMotora
            // 
            this.cboNeuroConducaoMotora.FormattingEnabled = true;
            this.cboNeuroConducaoMotora.Location = new System.Drawing.Point(9, 38);
            this.cboNeuroConducaoMotora.Name = "cboNeuroConducaoMotora";
            this.cboNeuroConducaoMotora.Size = new System.Drawing.Size(198, 21);
            this.cboNeuroConducaoMotora.TabIndex = 11;
            // 
            // button5
            // 
            this.button5.Image = global::WEDLC.Properties.Resources.up;
            this.button5.Location = new System.Drawing.Point(246, 38);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(27, 24);
            this.button5.TabIndex = 13;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Image = global::WEDLC.Properties.Resources.down;
            this.button6.Location = new System.Drawing.Point(213, 38);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(27, 24);
            this.button6.TabIndex = 12;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Código";
            // 
            // grdNeuroCondMotora
            // 
            this.grdNeuroCondMotora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroCondMotora.Location = new System.Drawing.Point(9, 68);
            this.grdNeuroCondMotora.Name = "grdNeuroCondMotora";
            this.grdNeuroCondMotora.Size = new System.Drawing.Size(264, 111);
            this.grdNeuroCondMotora.TabIndex = 14;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboNeuroConducaoSensorial);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.grdNeuroCondSensorial);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(588, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(282, 194);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Neuro Condução Sensorial";
            // 
            // cboNeuroConducaoSensorial
            // 
            this.cboNeuroConducaoSensorial.FormattingEnabled = true;
            this.cboNeuroConducaoSensorial.Location = new System.Drawing.Point(9, 38);
            this.cboNeuroConducaoSensorial.Name = "cboNeuroConducaoSensorial";
            this.cboNeuroConducaoSensorial.Size = new System.Drawing.Size(198, 21);
            this.cboNeuroConducaoSensorial.TabIndex = 15;
            // 
            // button7
            // 
            this.button7.Image = global::WEDLC.Properties.Resources.up;
            this.button7.Location = new System.Drawing.Point(246, 38);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(27, 24);
            this.button7.TabIndex = 17;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Image = global::WEDLC.Properties.Resources.down;
            this.button8.Location = new System.Drawing.Point(213, 38);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(27, 24);
            this.button8.TabIndex = 16;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Código";
            // 
            // grdNeuroCondSensorial
            // 
            this.grdNeuroCondSensorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNeuroCondSensorial.Location = new System.Drawing.Point(9, 68);
            this.grdNeuroCondSensorial.Name = "grdNeuroCondSensorial";
            this.grdNeuroCondSensorial.Size = new System.Drawing.Size(264, 111);
            this.grdNeuroCondSensorial.TabIndex = 18;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox9);
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(12, 460);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1146, 76);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Testes Especiais";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cboSimNaoNSPD);
            this.groupBox9.Location = new System.Drawing.Point(864, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(263, 45);
            this.groupBox9.TabIndex = 4;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "NSPD";
            // 
            // cboSimNaoNSPD
            // 
            this.cboSimNaoNSPD.FormattingEnabled = true;
            this.cboSimNaoNSPD.Location = new System.Drawing.Point(6, 18);
            this.cboSimNaoNSPD.Name = "cboSimNaoNSPD";
            this.cboSimNaoNSPD.Size = new System.Drawing.Size(201, 21);
            this.cboSimNaoNSPD.TabIndex = 26;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cboSimNaoReflexo);
            this.groupBox8.Location = new System.Drawing.Point(585, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(264, 45);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Reflexo H";
            // 
            // cboSimNaoReflexo
            // 
            this.cboSimNaoReflexo.FormattingEnabled = true;
            this.cboSimNaoReflexo.Location = new System.Drawing.Point(6, 18);
            this.cboSimNaoReflexo.Name = "cboSimNaoReflexo";
            this.cboSimNaoReflexo.Size = new System.Drawing.Size(192, 21);
            this.cboSimNaoReflexo.TabIndex = 25;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cboSimNaoRBC);
            this.groupBox7.Location = new System.Drawing.Point(297, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(263, 45);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "RBC";
            // 
            // cboSimNaoRBC
            // 
            this.cboSimNaoRBC.FormattingEnabled = true;
            this.cboSimNaoRBC.Location = new System.Drawing.Point(6, 18);
            this.cboSimNaoRBC.Name = "cboSimNaoRBC";
            this.cboSimNaoRBC.Size = new System.Drawing.Size(192, 21);
            this.cboSimNaoRBC.TabIndex = 24;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cboSimNaoBlink);
            this.groupBox6.Location = new System.Drawing.Point(9, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(264, 45);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Blink Reflex";
            // 
            // cboSimNaoBlink
            // 
            this.cboSimNaoBlink.FormattingEnabled = true;
            this.cboSimNaoBlink.Location = new System.Drawing.Point(6, 19);
            this.cboSimNaoBlink.Name = "cboSimNaoBlink";
            this.cboSimNaoBlink.Size = new System.Drawing.Size(192, 21);
            this.cboSimNaoBlink.TabIndex = 23;
            // 
            // frmFolha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 598);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpBoxDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFolha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folhas";
            this.Load += new System.EventHandler(this.frmFolha_Load);
            this.grpBoxDados.ResumeLayout(false);
            this.grpBoxDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDados)).EndInit();
            this.grpBotoes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstudoPotenciais)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondMotora)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondSensorial)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxDados;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtSigla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEstudoPotencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdEstudoPotenciais;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboAvalicaoMuscular;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdAvaliacaoMuscular;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboNeuroConducaoMotora;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grdNeuroCondMotora;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cboNeuroConducaoSensorial;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView grdNeuroCondSensorial;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView grdDados;
        private System.Windows.Forms.ComboBox cboSimNaoReflexo;
        private System.Windows.Forms.ComboBox cboSimNaoRBC;
        private System.Windows.Forms.ComboBox cboSimNaoBlink;
        private System.Windows.Forms.ComboBox cboSimNaoNSPD;
    }
}