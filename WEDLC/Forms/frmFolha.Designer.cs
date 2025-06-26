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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.grpComplemento = new System.Windows.Forms.GroupBox();
            this.grpTestesEspeciais = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoNSPD = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoReflexo = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoRBC = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cboSimNaoBlink = new System.Windows.Forms.ComboBox();
            this.grpNeuroCondSensorial = new System.Windows.Forms.GroupBox();
            this.cboNeuroConducaoSensorial = new System.Windows.Forms.ComboBox();
            this.btnExcluiNeuroCondSensorial = new System.Windows.Forms.Button();
            this.btnIncluiNeuroCondSensorial = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.grdNeuroCondSensorial = new System.Windows.Forms.DataGridView();
            this.grpNeuroCondMotora = new System.Windows.Forms.GroupBox();
            this.cboNeuroConducaoMotora = new System.Windows.Forms.ComboBox();
            this.btnExcluiNeuroCondMotora = new System.Windows.Forms.Button();
            this.btnIncluiNeuroCondMotora = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.grdNeuroCondMotora = new System.Windows.Forms.DataGridView();
            this.grpAvaliacaoMuscular = new System.Windows.Forms.GroupBox();
            this.cboAvaliacaoMuscular = new System.Windows.Forms.ComboBox();
            this.btnExcluiAvaliacaoMuscular = new System.Windows.Forms.Button();
            this.btnIncluiAvaliacaoMuscular = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.grdAvaliacaoMuscular = new System.Windows.Forms.DataGridView();
            this.grpEstudoPotenEvocado = new System.Windows.Forms.GroupBox();
            this.btnExcluiEstudoPotencial = new System.Windows.Forms.Button();
            this.btnIncluiEstudoPotencial = new System.Windows.Forms.Button();
            this.txtEstudoPotencial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grdEstudoPotencial = new System.Windows.Forms.DataGridView();
            this.btnComplemento = new System.Windows.Forms.Button();
            this.grpBoxDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDados)).BeginInit();
            this.grpBotoes.SuspendLayout();
            this.grpComplemento.SuspendLayout();
            this.grpTestesEspeciais.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.grpNeuroCondSensorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondSensorial)).BeginInit();
            this.grpNeuroCondMotora.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondMotora)).BeginInit();
            this.grpAvaliacaoMuscular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).BeginInit();
            this.grpEstudoPotenEvocado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstudoPotencial)).BeginInit();
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
            this.grpBoxDados.Size = new System.Drawing.Size(1167, 242);
            this.grpBoxDados.TabIndex = 0;
            this.grpBoxDados.TabStop = false;
            this.grpBoxDados.Text = "Dados da Folha";
            // 
            // grdDados
            // 
            this.grdDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDados.Location = new System.Drawing.Point(6, 61);
            this.grdDados.Name = "grdDados";
            this.grdDados.Size = new System.Drawing.Size(1155, 168);
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
            this.cboTipo.Validating += new System.ComponentModel.CancelEventHandler(this.cboTipo_Validating);
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
            this.txtCodigo.ForeColor = System.Drawing.Color.Red;
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
            this.grpBotoes.Controls.Add(this.btnCancelar);
            this.grpBotoes.Controls.Add(this.btnExcluir);
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Controls.Add(this.btnNovo);
            this.grpBotoes.Location = new System.Drawing.Point(11, 260);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(1168, 46);
            this.grpBotoes.TabIndex = 1;
            this.grpBotoes.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Image = global::WEDLC.Properties.Resources.cancelblue;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(282, 13);
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
            this.btnExcluir.Location = new System.Drawing.Point(100, 13);
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
            this.btnGravar.Location = new System.Drawing.Point(191, 13);
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
            this.btnSair.Location = new System.Drawing.Point(1077, 13);
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
            // grpComplemento
            // 
            this.grpComplemento.Controls.Add(this.grpTestesEspeciais);
            this.grpComplemento.Controls.Add(this.grpNeuroCondSensorial);
            this.grpComplemento.Controls.Add(this.grpNeuroCondMotora);
            this.grpComplemento.Controls.Add(this.grpAvaliacaoMuscular);
            this.grpComplemento.Controls.Add(this.grpEstudoPotenEvocado);
            this.grpComplemento.Enabled = false;
            this.grpComplemento.Location = new System.Drawing.Point(12, 343);
            this.grpComplemento.Name = "grpComplemento";
            this.grpComplemento.Size = new System.Drawing.Size(1167, 307);
            this.grpComplemento.TabIndex = 10;
            this.grpComplemento.TabStop = false;
            this.grpComplemento.Text = "Complemento";
            // 
            // grpTestesEspeciais
            // 
            this.grpTestesEspeciais.Controls.Add(this.groupBox9);
            this.grpTestesEspeciais.Controls.Add(this.groupBox8);
            this.grpTestesEspeciais.Controls.Add(this.groupBox7);
            this.grpTestesEspeciais.Controls.Add(this.groupBox6);
            this.grpTestesEspeciais.Enabled = false;
            this.grpTestesEspeciais.Location = new System.Drawing.Point(10, 221);
            this.grpTestesEspeciais.Name = "grpTestesEspeciais";
            this.grpTestesEspeciais.Size = new System.Drawing.Size(1146, 76);
            this.grpTestesEspeciais.TabIndex = 14;
            this.grpTestesEspeciais.TabStop = false;
            this.grpTestesEspeciais.Text = "Testes Especiais";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cboSimNaoNSPD);
            this.groupBox9.Location = new System.Drawing.Point(864, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(272, 45);
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
            this.cboSimNaoNSPD.SelectedIndexChanged += new System.EventHandler(this.cboSimNaoNSPD_SelectedIndexChanged);
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
            this.cboSimNaoReflexo.SelectedIndexChanged += new System.EventHandler(this.cboSimNaoReflexo_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cboSimNaoRBC);
            this.groupBox7.Location = new System.Drawing.Point(297, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(264, 45);
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
            this.cboSimNaoRBC.SelectedIndexChanged += new System.EventHandler(this.cboSimNaoRBC_SelectedIndexChanged);
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
            this.cboSimNaoBlink.SelectedIndexChanged += new System.EventHandler(this.cboSimNaoBlink_SelectedIndexChanged);
            // 
            // grpNeuroCondSensorial
            // 
            this.grpNeuroCondSensorial.Controls.Add(this.cboNeuroConducaoSensorial);
            this.grpNeuroCondSensorial.Controls.Add(this.btnExcluiNeuroCondSensorial);
            this.grpNeuroCondSensorial.Controls.Add(this.btnIncluiNeuroCondSensorial);
            this.grpNeuroCondSensorial.Controls.Add(this.label7);
            this.grpNeuroCondSensorial.Controls.Add(this.grdNeuroCondSensorial);
            this.grpNeuroCondSensorial.Enabled = false;
            this.grpNeuroCondSensorial.Location = new System.Drawing.Point(586, 21);
            this.grpNeuroCondSensorial.Name = "grpNeuroCondSensorial";
            this.grpNeuroCondSensorial.Size = new System.Drawing.Size(282, 194);
            this.grpNeuroCondSensorial.TabIndex = 13;
            this.grpNeuroCondSensorial.TabStop = false;
            this.grpNeuroCondSensorial.Text = "Neuro Condução Sensorial";
            // 
            // cboNeuroConducaoSensorial
            // 
            this.cboNeuroConducaoSensorial.FormattingEnabled = true;
            this.cboNeuroConducaoSensorial.Location = new System.Drawing.Point(9, 38);
            this.cboNeuroConducaoSensorial.Name = "cboNeuroConducaoSensorial";
            this.cboNeuroConducaoSensorial.Size = new System.Drawing.Size(198, 21);
            this.cboNeuroConducaoSensorial.TabIndex = 15;
            // 
            // btnExcluiNeuroCondSensorial
            // 
            this.btnExcluiNeuroCondSensorial.Image = global::WEDLC.Properties.Resources.up;
            this.btnExcluiNeuroCondSensorial.Location = new System.Drawing.Point(246, 38);
            this.btnExcluiNeuroCondSensorial.Name = "btnExcluiNeuroCondSensorial";
            this.btnExcluiNeuroCondSensorial.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiNeuroCondSensorial.TabIndex = 17;
            this.btnExcluiNeuroCondSensorial.UseVisualStyleBackColor = true;
            this.btnExcluiNeuroCondSensorial.Click += new System.EventHandler(this.btnExcluiNeuroCondSensorial_Click);
            // 
            // btnIncluiNeuroCondSensorial
            // 
            this.btnIncluiNeuroCondSensorial.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiNeuroCondSensorial.Location = new System.Drawing.Point(213, 38);
            this.btnIncluiNeuroCondSensorial.Name = "btnIncluiNeuroCondSensorial";
            this.btnIncluiNeuroCondSensorial.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiNeuroCondSensorial.TabIndex = 16;
            this.btnIncluiNeuroCondSensorial.UseVisualStyleBackColor = true;
            this.btnIncluiNeuroCondSensorial.Click += new System.EventHandler(this.btnIncluiNeuroCondSensorial_Click);
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
            // grpNeuroCondMotora
            // 
            this.grpNeuroCondMotora.Controls.Add(this.cboNeuroConducaoMotora);
            this.grpNeuroCondMotora.Controls.Add(this.btnExcluiNeuroCondMotora);
            this.grpNeuroCondMotora.Controls.Add(this.btnIncluiNeuroCondMotora);
            this.grpNeuroCondMotora.Controls.Add(this.label6);
            this.grpNeuroCondMotora.Controls.Add(this.grdNeuroCondMotora);
            this.grpNeuroCondMotora.Enabled = false;
            this.grpNeuroCondMotora.Location = new System.Drawing.Point(298, 21);
            this.grpNeuroCondMotora.Name = "grpNeuroCondMotora";
            this.grpNeuroCondMotora.Size = new System.Drawing.Size(282, 194);
            this.grpNeuroCondMotora.TabIndex = 12;
            this.grpNeuroCondMotora.TabStop = false;
            this.grpNeuroCondMotora.Text = "Neuro Condução Motora";
            // 
            // cboNeuroConducaoMotora
            // 
            this.cboNeuroConducaoMotora.FormattingEnabled = true;
            this.cboNeuroConducaoMotora.Location = new System.Drawing.Point(9, 38);
            this.cboNeuroConducaoMotora.Name = "cboNeuroConducaoMotora";
            this.cboNeuroConducaoMotora.Size = new System.Drawing.Size(198, 21);
            this.cboNeuroConducaoMotora.TabIndex = 11;
            // 
            // btnExcluiNeuroCondMotora
            // 
            this.btnExcluiNeuroCondMotora.Image = global::WEDLC.Properties.Resources.up;
            this.btnExcluiNeuroCondMotora.Location = new System.Drawing.Point(246, 38);
            this.btnExcluiNeuroCondMotora.Name = "btnExcluiNeuroCondMotora";
            this.btnExcluiNeuroCondMotora.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiNeuroCondMotora.TabIndex = 13;
            this.btnExcluiNeuroCondMotora.UseVisualStyleBackColor = true;
            this.btnExcluiNeuroCondMotora.Click += new System.EventHandler(this.btnExcluiNeuroCondMotora_Click);
            // 
            // btnIncluiNeuroCondMotora
            // 
            this.btnIncluiNeuroCondMotora.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiNeuroCondMotora.Location = new System.Drawing.Point(213, 38);
            this.btnIncluiNeuroCondMotora.Name = "btnIncluiNeuroCondMotora";
            this.btnIncluiNeuroCondMotora.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiNeuroCondMotora.TabIndex = 12;
            this.btnIncluiNeuroCondMotora.UseVisualStyleBackColor = true;
            this.btnIncluiNeuroCondMotora.Click += new System.EventHandler(this.btnIncluiNeuroCondMotora_Click);
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
            // grpAvaliacaoMuscular
            // 
            this.grpAvaliacaoMuscular.Controls.Add(this.cboAvaliacaoMuscular);
            this.grpAvaliacaoMuscular.Controls.Add(this.btnExcluiAvaliacaoMuscular);
            this.grpAvaliacaoMuscular.Controls.Add(this.btnIncluiAvaliacaoMuscular);
            this.grpAvaliacaoMuscular.Controls.Add(this.label4);
            this.grpAvaliacaoMuscular.Controls.Add(this.grdAvaliacaoMuscular);
            this.grpAvaliacaoMuscular.Enabled = false;
            this.grpAvaliacaoMuscular.Location = new System.Drawing.Point(10, 21);
            this.grpAvaliacaoMuscular.Name = "grpAvaliacaoMuscular";
            this.grpAvaliacaoMuscular.Size = new System.Drawing.Size(282, 194);
            this.grpAvaliacaoMuscular.TabIndex = 11;
            this.grpAvaliacaoMuscular.TabStop = false;
            this.grpAvaliacaoMuscular.Text = "Avaliação Muscular";
            // 
            // cboAvaliacaoMuscular
            // 
            this.cboAvaliacaoMuscular.FormattingEnabled = true;
            this.cboAvaliacaoMuscular.Location = new System.Drawing.Point(9, 38);
            this.cboAvaliacaoMuscular.Name = "cboAvaliacaoMuscular";
            this.cboAvaliacaoMuscular.Size = new System.Drawing.Size(198, 21);
            this.cboAvaliacaoMuscular.TabIndex = 7;
            // 
            // btnExcluiAvaliacaoMuscular
            // 
            this.btnExcluiAvaliacaoMuscular.Image = global::WEDLC.Properties.Resources.up;
            this.btnExcluiAvaliacaoMuscular.Location = new System.Drawing.Point(246, 38);
            this.btnExcluiAvaliacaoMuscular.Name = "btnExcluiAvaliacaoMuscular";
            this.btnExcluiAvaliacaoMuscular.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiAvaliacaoMuscular.TabIndex = 9;
            this.btnExcluiAvaliacaoMuscular.UseVisualStyleBackColor = true;
            this.btnExcluiAvaliacaoMuscular.Click += new System.EventHandler(this.btnExcluiAvaliacaoMuscular_Click);
            // 
            // btnIncluiAvaliacaoMuscular
            // 
            this.btnIncluiAvaliacaoMuscular.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiAvaliacaoMuscular.Location = new System.Drawing.Point(213, 38);
            this.btnIncluiAvaliacaoMuscular.Name = "btnIncluiAvaliacaoMuscular";
            this.btnIncluiAvaliacaoMuscular.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiAvaliacaoMuscular.TabIndex = 8;
            this.btnIncluiAvaliacaoMuscular.UseVisualStyleBackColor = true;
            this.btnIncluiAvaliacaoMuscular.Click += new System.EventHandler(this.btnIncluiAvaliacaoMuscular_Click);
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
            // grpEstudoPotenEvocado
            // 
            this.grpEstudoPotenEvocado.Controls.Add(this.btnExcluiEstudoPotencial);
            this.grpEstudoPotenEvocado.Controls.Add(this.btnIncluiEstudoPotencial);
            this.grpEstudoPotenEvocado.Controls.Add(this.txtEstudoPotencial);
            this.grpEstudoPotenEvocado.Controls.Add(this.label5);
            this.grpEstudoPotenEvocado.Controls.Add(this.grdEstudoPotencial);
            this.grpEstudoPotenEvocado.Enabled = false;
            this.grpEstudoPotenEvocado.Location = new System.Drawing.Point(874, 21);
            this.grpEstudoPotenEvocado.Name = "grpEstudoPotenEvocado";
            this.grpEstudoPotenEvocado.Size = new System.Drawing.Size(282, 194);
            this.grpEstudoPotenEvocado.TabIndex = 10;
            this.grpEstudoPotenEvocado.TabStop = false;
            this.grpEstudoPotenEvocado.Text = "Estudo de Potenciais Evocados";
            // 
            // btnExcluiEstudoPotencial
            // 
            this.btnExcluiEstudoPotencial.Image = global::WEDLC.Properties.Resources.delete;
            this.btnExcluiEstudoPotencial.Location = new System.Drawing.Point(246, 38);
            this.btnExcluiEstudoPotencial.Name = "btnExcluiEstudoPotencial";
            this.btnExcluiEstudoPotencial.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiEstudoPotencial.TabIndex = 21;
            this.btnExcluiEstudoPotencial.UseVisualStyleBackColor = true;
            this.btnExcluiEstudoPotencial.Click += new System.EventHandler(this.btnExcluiEstudoPotencial_Click);
            // 
            // btnIncluiEstudoPotencial
            // 
            this.btnIncluiEstudoPotencial.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiEstudoPotencial.Location = new System.Drawing.Point(213, 38);
            this.btnIncluiEstudoPotencial.Name = "btnIncluiEstudoPotencial";
            this.btnIncluiEstudoPotencial.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiEstudoPotencial.TabIndex = 20;
            this.btnIncluiEstudoPotencial.UseVisualStyleBackColor = true;
            this.btnIncluiEstudoPotencial.Click += new System.EventHandler(this.btnIncluiEstudoPotencial_Click);
            // 
            // txtEstudoPotencial
            // 
            this.txtEstudoPotencial.Location = new System.Drawing.Point(9, 40);
            this.txtEstudoPotencial.MaxLength = 30;
            this.txtEstudoPotencial.Name = "txtEstudoPotencial";
            this.txtEstudoPotencial.Size = new System.Drawing.Size(198, 20);
            this.txtEstudoPotencial.TabIndex = 19;
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
            // grdEstudoPotencial
            // 
            this.grdEstudoPotencial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEstudoPotencial.Location = new System.Drawing.Point(9, 68);
            this.grdEstudoPotencial.Name = "grdEstudoPotencial";
            this.grdEstudoPotencial.Size = new System.Drawing.Size(264, 111);
            this.grdEstudoPotencial.TabIndex = 22;
            // 
            // btnComplemento
            // 
            this.btnComplemento.Enabled = false;
            this.btnComplemento.Image = global::WEDLC.Properties.Resources.add1;
            this.btnComplemento.Location = new System.Drawing.Point(14, 313);
            this.btnComplemento.Name = "btnComplemento";
            this.btnComplemento.Size = new System.Drawing.Size(27, 24);
            this.btnComplemento.TabIndex = 13;
            this.btnComplemento.UseVisualStyleBackColor = true;
            this.btnComplemento.Click += new System.EventHandler(this.btnComplemento_Click);
            // 
            // frmFolha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 661);
            this.ControlBox = false;
            this.Controls.Add(this.btnComplemento);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpBoxDados);
            this.Controls.Add(this.grpComplemento);
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
            this.grpComplemento.ResumeLayout(false);
            this.grpTestesEspeciais.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.grpNeuroCondSensorial.ResumeLayout(false);
            this.grpNeuroCondSensorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondSensorial)).EndInit();
            this.grpNeuroCondMotora.ResumeLayout(false);
            this.grpNeuroCondMotora.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNeuroCondMotora)).EndInit();
            this.grpAvaliacaoMuscular.ResumeLayout(false);
            this.grpAvaliacaoMuscular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAvaliacaoMuscular)).EndInit();
            this.grpEstudoPotenEvocado.ResumeLayout(false);
            this.grpEstudoPotenEvocado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEstudoPotencial)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.ComboBox cboTipo;
        private System.Windows.Forms.DataGridView grdDados;
        private System.Windows.Forms.GroupBox grpComplemento;
        private System.Windows.Forms.GroupBox grpTestesEspeciais;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cboSimNaoNSPD;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cboSimNaoReflexo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cboSimNaoRBC;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cboSimNaoBlink;
        private System.Windows.Forms.GroupBox grpNeuroCondSensorial;
        private System.Windows.Forms.ComboBox cboNeuroConducaoSensorial;
        private System.Windows.Forms.Button btnExcluiNeuroCondSensorial;
        private System.Windows.Forms.Button btnIncluiNeuroCondSensorial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView grdNeuroCondSensorial;
        private System.Windows.Forms.GroupBox grpNeuroCondMotora;
        private System.Windows.Forms.ComboBox cboNeuroConducaoMotora;
        private System.Windows.Forms.Button btnExcluiNeuroCondMotora;
        private System.Windows.Forms.Button btnIncluiNeuroCondMotora;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView grdNeuroCondMotora;
        private System.Windows.Forms.GroupBox grpAvaliacaoMuscular;
        private System.Windows.Forms.ComboBox cboAvaliacaoMuscular;
        private System.Windows.Forms.Button btnExcluiAvaliacaoMuscular;
        private System.Windows.Forms.Button btnIncluiAvaliacaoMuscular;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdAvaliacaoMuscular;
        private System.Windows.Forms.GroupBox grpEstudoPotenEvocado;
        private System.Windows.Forms.Button btnExcluiEstudoPotencial;
        private System.Windows.Forms.Button btnIncluiEstudoPotencial;
        private System.Windows.Forms.TextBox txtEstudoPotencial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdEstudoPotencial;
        private System.Windows.Forms.Button btnComplemento;
    }
}