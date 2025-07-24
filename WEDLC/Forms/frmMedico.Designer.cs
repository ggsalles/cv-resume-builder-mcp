namespace WEDLC.Forms
{
    partial class frmMedico
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
            this.txtPais = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mskTelefone = new System.Windows.Forms.MaskedTextBox();
            this.txtLocalidade = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUf = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigoMedico = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMediaConsultorio = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboClasseConsultorio = new System.Windows.Forms.ComboBox();
            this.grdEspecialidade = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnIncluiEspecialidade = new System.Windows.Forms.Button();
            this.btnExcluiEspecialidade = new System.Windows.Forms.Button();
            this.cboEspecialConsultorio = new System.Windows.Forms.ComboBox();
            this.mskTelefoneConsultorio = new System.Windows.Forms.MaskedTextBox();
            this.txtLocalidadeConsultorio = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUfConsultorio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBairroConsultorio = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtComplementoConsultorio = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLogradouroConsultorio = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCepConsultorio = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtConsultorio = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.grpPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDadosPessoais)).BeginInit();
            this.grpBotoes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEspecialidade)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPaciente
            // 
            this.grpPaciente.Controls.Add(this.grdDadosPessoais);
            this.grpPaciente.Controls.Add(this.txtPais);
            this.grpPaciente.Controls.Add(this.label2);
            this.grpPaciente.Controls.Add(this.mskTelefone);
            this.grpPaciente.Controls.Add(this.txtLocalidade);
            this.grpPaciente.Controls.Add(this.label16);
            this.grpPaciente.Controls.Add(this.label9);
            this.grpPaciente.Controls.Add(this.txtUf);
            this.grpPaciente.Controls.Add(this.label8);
            this.grpPaciente.Controls.Add(this.txtBairro);
            this.grpPaciente.Controls.Add(this.label7);
            this.grpPaciente.Controls.Add(this.txtComplemento);
            this.grpPaciente.Controls.Add(this.label6);
            this.grpPaciente.Controls.Add(this.txtLogradouro);
            this.grpPaciente.Controls.Add(this.label5);
            this.grpPaciente.Controls.Add(this.txtCep);
            this.grpPaciente.Controls.Add(this.label1);
            this.grpPaciente.Controls.Add(this.txtNome);
            this.grpPaciente.Controls.Add(this.lblNome);
            this.grpPaciente.Controls.Add(this.txtCodigoMedico);
            this.grpPaciente.Controls.Add(this.lblCodigo);
            this.grpPaciente.Location = new System.Drawing.Point(12, 12);
            this.grpPaciente.Name = "grpPaciente";
            this.grpPaciente.Size = new System.Drawing.Size(1161, 277);
            this.grpPaciente.TabIndex = 0;
            this.grpPaciente.TabStop = false;
            this.grpPaciente.Text = "Dados Pessoais";
            // 
            // grdDadosPessoais
            // 
            this.grdDadosPessoais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDadosPessoais.Location = new System.Drawing.Point(6, 109);
            this.grdDadosPessoais.Name = "grdDadosPessoais";
            this.grdDadosPessoais.Size = new System.Drawing.Size(1147, 168);
            this.grdDadosPessoais.TabIndex = 34;
            this.grdDadosPessoais.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDadosPessoais_CellClick);
            // 
            // txtPais
            // 
            this.txtPais.Enabled = false;
            this.txtPais.Location = new System.Drawing.Point(458, 79);
            this.txtPais.MaxLength = 8;
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(300, 20);
            this.txtPais.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "País";
            // 
            // mskTelefone
            // 
            this.mskTelefone.Enabled = false;
            this.mskTelefone.Location = new System.Drawing.Point(764, 79);
            this.mskTelefone.Mask = "(99) 00000-0000";
            this.mskTelefone.Name = "mskTelefone";
            this.mskTelefone.Size = new System.Drawing.Size(103, 20);
            this.mskTelefone.TabIndex = 10;
            // 
            // txtLocalidade
            // 
            this.txtLocalidade.Enabled = false;
            this.txtLocalidade.Location = new System.Drawing.Point(153, 79);
            this.txtLocalidade.MaxLength = 8;
            this.txtLocalidade.Name = "txtLocalidade";
            this.txtLocalidade.Size = new System.Drawing.Size(216, 20);
            this.txtLocalidade.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(150, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Localidade";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(761, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Telefone";
            // 
            // txtUf
            // 
            this.txtUf.Enabled = false;
            this.txtUf.Location = new System.Drawing.Point(375, 79);
            this.txtUf.MaxLength = 2;
            this.txtUf.Name = "txtUf";
            this.txtUf.Size = new System.Drawing.Size(77, 20);
            this.txtUf.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(375, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "UF";
            // 
            // txtBairro
            // 
            this.txtBairro.Enabled = false;
            this.txtBairro.Location = new System.Drawing.Point(6, 79);
            this.txtBairro.MaxLength = 8;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(141, 20);
            this.txtBairro.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Bairro";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Enabled = false;
            this.txtComplemento.Location = new System.Drawing.Point(873, 35);
            this.txtComplemento.MaxLength = 8;
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(282, 20);
            this.txtComplemento.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(870, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Complemento";
            // 
            // txtLogradouro
            // 
            this.txtLogradouro.Enabled = false;
            this.txtLogradouro.Location = new System.Drawing.Point(567, 35);
            this.txtLogradouro.MaxLength = 8;
            this.txtLogradouro.Name = "txtLogradouro";
            this.txtLogradouro.Size = new System.Drawing.Size(300, 20);
            this.txtLogradouro.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Logradouro";
            // 
            // txtCep
            // 
            this.txtCep.Enabled = false;
            this.txtCep.Location = new System.Drawing.Point(458, 35);
            this.txtCep.MaxLength = 8;
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(103, 20);
            this.txtCep.TabIndex = 3;
            this.txtCep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCep_KeyPress);
            this.txtCep.Validating += new System.ComponentModel.CancelEventHandler(this.txtCep_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(455, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "CEP";
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
            // txtCodigoMedico
            // 
            this.txtCodigoMedico.ForeColor = System.Drawing.Color.Blue;
            this.txtCodigoMedico.Location = new System.Drawing.Point(6, 35);
            this.txtCodigoMedico.MaxLength = 10;
            this.txtCodigoMedico.Name = "txtCodigoMedico";
            this.txtCodigoMedico.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoMedico.TabIndex = 1;
            this.txtCodigoMedico.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoMedico_KeyPress);
            this.txtCodigoMedico.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoMedico_KeyUp);
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
            this.grpBotoes.Location = new System.Drawing.Point(12, 586);
            this.grpBotoes.Name = "grpBotoes";
            this.grpBotoes.Size = new System.Drawing.Size(1162, 46);
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
            this.btnCancelar.TabIndex = 27;
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
            this.btnExcluir.TabIndex = 25;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Image = global::WEDLC.Properties.Resources.save;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.Location = new System.Drawing.Point(191, 13);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(85, 27);
            this.btnGravar.TabIndex = 26;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            this.btnSair.Image = global::WEDLC.Properties.Resources.exit;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(1071, 13);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 27);
            this.btnSair.TabIndex = 28;
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
            this.btnNovo.TabIndex = 24;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMediaConsultorio);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboClasseConsultorio);
            this.groupBox1.Controls.Add(this.grdEspecialidade);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnIncluiEspecialidade);
            this.groupBox1.Controls.Add(this.btnExcluiEspecialidade);
            this.groupBox1.Controls.Add(this.cboEspecialConsultorio);
            this.groupBox1.Controls.Add(this.mskTelefoneConsultorio);
            this.groupBox1.Controls.Add(this.txtLocalidadeConsultorio);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtUfConsultorio);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtBairroConsultorio);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtComplementoConsultorio);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtLogradouroConsultorio);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtCepConsultorio);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtConsultorio);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(12, 295);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1161, 285);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do Consultório";
            // 
            // txtMediaConsultorio
            // 
            this.txtMediaConsultorio.Enabled = false;
            this.txtMediaConsultorio.ForeColor = System.Drawing.Color.Red;
            this.txtMediaConsultorio.Location = new System.Drawing.Point(619, 80);
            this.txtMediaConsultorio.MaxLength = 10;
            this.txtMediaConsultorio.Name = "txtMediaConsultorio";
            this.txtMediaConsultorio.Size = new System.Drawing.Size(90, 20);
            this.txtMediaConsultorio.TabIndex = 20;
            this.txtMediaConsultorio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            this.txtMediaConsultorio.Leave += new System.EventHandler(this.txtValor_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(616, 63);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Média";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(564, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Classe";
            // 
            // cboClasseConsultorio
            // 
            this.cboClasseConsultorio.Enabled = false;
            this.cboClasseConsultorio.FormattingEnabled = true;
            this.cboClasseConsultorio.Location = new System.Drawing.Point(567, 79);
            this.cboClasseConsultorio.Name = "cboClasseConsultorio";
            this.cboClasseConsultorio.Size = new System.Drawing.Size(46, 21);
            this.cboClasseConsultorio.TabIndex = 19;
            // 
            // grdEspecialidade
            // 
            this.grdEspecialidade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEspecialidade.Enabled = false;
            this.grdEspecialidade.Location = new System.Drawing.Point(6, 150);
            this.grdEspecialidade.Name = "grdEspecialidade";
            this.grdEspecialidade.Size = new System.Drawing.Size(1149, 123);
            this.grdEspecialidade.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Especialidades";
            // 
            // btnIncluiEspecialidade
            // 
            this.btnIncluiEspecialidade.Enabled = false;
            this.btnIncluiEspecialidade.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiEspecialidade.Location = new System.Drawing.Point(501, 121);
            this.btnIncluiEspecialidade.Name = "btnIncluiEspecialidade";
            this.btnIncluiEspecialidade.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiEspecialidade.TabIndex = 22;
            this.btnIncluiEspecialidade.UseVisualStyleBackColor = true;
            // 
            // btnExcluiEspecialidade
            // 
            this.btnExcluiEspecialidade.Enabled = false;
            this.btnExcluiEspecialidade.Image = global::WEDLC.Properties.Resources.up;
            this.btnExcluiEspecialidade.Location = new System.Drawing.Point(534, 121);
            this.btnExcluiEspecialidade.Name = "btnExcluiEspecialidade";
            this.btnExcluiEspecialidade.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiEspecialidade.TabIndex = 23;
            this.btnExcluiEspecialidade.UseVisualStyleBackColor = true;
            // 
            // cboEspecialConsultorio
            // 
            this.cboEspecialConsultorio.Enabled = false;
            this.cboEspecialConsultorio.FormattingEnabled = true;
            this.cboEspecialConsultorio.Location = new System.Drawing.Point(6, 123);
            this.cboEspecialConsultorio.Name = "cboEspecialConsultorio";
            this.cboEspecialConsultorio.Size = new System.Drawing.Size(489, 21);
            this.cboEspecialConsultorio.TabIndex = 21;
            // 
            // mskTelefoneConsultorio
            // 
            this.mskTelefoneConsultorio.Enabled = false;
            this.mskTelefoneConsultorio.Location = new System.Drawing.Point(458, 79);
            this.mskTelefoneConsultorio.Mask = "(99) 00000-0000";
            this.mskTelefoneConsultorio.Name = "mskTelefoneConsultorio";
            this.mskTelefoneConsultorio.Size = new System.Drawing.Size(103, 20);
            this.mskTelefoneConsultorio.TabIndex = 18;
            // 
            // txtLocalidadeConsultorio
            // 
            this.txtLocalidadeConsultorio.Enabled = false;
            this.txtLocalidadeConsultorio.Location = new System.Drawing.Point(153, 79);
            this.txtLocalidadeConsultorio.MaxLength = 8;
            this.txtLocalidadeConsultorio.Name = "txtLocalidadeConsultorio";
            this.txtLocalidadeConsultorio.Size = new System.Drawing.Size(216, 20);
            this.txtLocalidadeConsultorio.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(150, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Localidade";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(455, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Telefone";
            // 
            // txtUfConsultorio
            // 
            this.txtUfConsultorio.Enabled = false;
            this.txtUfConsultorio.Location = new System.Drawing.Point(375, 79);
            this.txtUfConsultorio.MaxLength = 8;
            this.txtUfConsultorio.Name = "txtUfConsultorio";
            this.txtUfConsultorio.Size = new System.Drawing.Size(77, 20);
            this.txtUfConsultorio.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(375, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "UF";
            // 
            // txtBairroConsultorio
            // 
            this.txtBairroConsultorio.Enabled = false;
            this.txtBairroConsultorio.Location = new System.Drawing.Point(6, 79);
            this.txtBairroConsultorio.MaxLength = 8;
            this.txtBairroConsultorio.Name = "txtBairroConsultorio";
            this.txtBairroConsultorio.Size = new System.Drawing.Size(141, 20);
            this.txtBairroConsultorio.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Bairro";
            // 
            // txtComplementoConsultorio
            // 
            this.txtComplementoConsultorio.Enabled = false;
            this.txtComplementoConsultorio.Location = new System.Drawing.Point(873, 35);
            this.txtComplementoConsultorio.MaxLength = 8;
            this.txtComplementoConsultorio.Name = "txtComplementoConsultorio";
            this.txtComplementoConsultorio.Size = new System.Drawing.Size(282, 20);
            this.txtComplementoConsultorio.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(870, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Complemento";
            // 
            // txtLogradouroConsultorio
            // 
            this.txtLogradouroConsultorio.Enabled = false;
            this.txtLogradouroConsultorio.Location = new System.Drawing.Point(567, 35);
            this.txtLogradouroConsultorio.MaxLength = 8;
            this.txtLogradouroConsultorio.Name = "txtLogradouroConsultorio";
            this.txtLogradouroConsultorio.Size = new System.Drawing.Size(300, 20);
            this.txtLogradouroConsultorio.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(564, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Logradouro";
            // 
            // txtCepConsultorio
            // 
            this.txtCepConsultorio.Enabled = false;
            this.txtCepConsultorio.Location = new System.Drawing.Point(458, 35);
            this.txtCepConsultorio.MaxLength = 8;
            this.txtCepConsultorio.Name = "txtCepConsultorio";
            this.txtCepConsultorio.Size = new System.Drawing.Size(103, 20);
            this.txtCepConsultorio.TabIndex = 12;
            this.txtCepConsultorio.Validating += new System.ComponentModel.CancelEventHandler(this.txtCepConsultorio_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(455, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "CEP";
            // 
            // txtConsultorio
            // 
            this.txtConsultorio.Enabled = false;
            this.txtConsultorio.Location = new System.Drawing.Point(6, 35);
            this.txtConsultorio.MaxLength = 50;
            this.txtConsultorio.Name = "txtConsultorio";
            this.txtConsultorio.Size = new System.Drawing.Size(446, 20);
            this.txtConsultorio.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Consultório";
            // 
            // frmMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 640);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpPaciente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMedico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Médicos";
            this.Load += new System.EventHandler(this.frmPaciente_Load);
            this.grpPaciente.ResumeLayout(false);
            this.grpPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDadosPessoais)).EndInit();
            this.grpBotoes.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEspecialidade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPaciente;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigoMedico;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtCep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUf;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLocalidade;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.MaskedTextBox mskTelefone;
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView grdEspecialidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIncluiEspecialidade;
        private System.Windows.Forms.Button btnExcluiEspecialidade;
        private System.Windows.Forms.ComboBox cboEspecialConsultorio;
        private System.Windows.Forms.MaskedTextBox mskTelefoneConsultorio;
        private System.Windows.Forms.TextBox txtLocalidadeConsultorio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUfConsultorio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBairroConsultorio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtComplementoConsultorio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtLogradouroConsultorio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCepConsultorio;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtConsultorio;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboClasseConsultorio;
        private System.Windows.Forms.TextBox txtMediaConsultorio;
        private System.Windows.Forms.DataGridView grdDadosPessoais;
    }
}