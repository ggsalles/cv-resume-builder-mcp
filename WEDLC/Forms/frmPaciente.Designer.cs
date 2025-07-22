namespace WEDLC.Forms
{
    partial class frmPaciente
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
            this.txtLocalidade = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboBeneficente = new System.Windows.Forms.ComboBox();
            this.cboFolha = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnExcluiFolha = new System.Windows.Forms.Button();
            this.cboMedico = new System.Windows.Forms.ComboBox();
            this.btnIncluiFolha = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grdFolha = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.cboSexo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboIndSec = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUf = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogradouro = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboIndPrinc = new System.Windows.Forms.ComboBox();
            this.cboConvenio = new System.Windows.Forms.ComboBox();
            this.txtCep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtCodigoProntuario = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.grpBotoes = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNascimento = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.grpPaciente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolha)).BeginInit();
            this.grpBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPaciente
            // 
            this.grpPaciente.Controls.Add(this.maskedTextBox1);
            this.grpPaciente.Controls.Add(this.txtNascimento);
            this.grpPaciente.Controls.Add(this.label17);
            this.grpPaciente.Controls.Add(this.txtTexto);
            this.grpPaciente.Controls.Add(this.txtLocalidade);
            this.grpPaciente.Controls.Add(this.label16);
            this.grpPaciente.Controls.Add(this.grdFolha);
            this.grpPaciente.Controls.Add(this.txtData);
            this.grpPaciente.Controls.Add(this.label15);
            this.grpPaciente.Controls.Add(this.label14);
            this.grpPaciente.Controls.Add(this.cboBeneficente);
            this.grpPaciente.Controls.Add(this.cboFolha);
            this.grpPaciente.Controls.Add(this.label13);
            this.grpPaciente.Controls.Add(this.btnExcluiFolha);
            this.grpPaciente.Controls.Add(this.cboMedico);
            this.grpPaciente.Controls.Add(this.btnIncluiFolha);
            this.grpPaciente.Controls.Add(this.label4);
            this.grpPaciente.Controls.Add(this.label12);
            this.grpPaciente.Controls.Add(this.label11);
            this.grpPaciente.Controls.Add(this.cboSexo);
            this.grpPaciente.Controls.Add(this.label10);
            this.grpPaciente.Controls.Add(this.cboIndSec);
            this.grpPaciente.Controls.Add(this.label9);
            this.grpPaciente.Controls.Add(this.txtUf);
            this.grpPaciente.Controls.Add(this.label8);
            this.grpPaciente.Controls.Add(this.txtBairro);
            this.grpPaciente.Controls.Add(this.label7);
            this.grpPaciente.Controls.Add(this.txtComplemento);
            this.grpPaciente.Controls.Add(this.label6);
            this.grpPaciente.Controls.Add(this.txtLogradouro);
            this.grpPaciente.Controls.Add(this.label5);
            this.grpPaciente.Controls.Add(this.label3);
            this.grpPaciente.Controls.Add(this.label2);
            this.grpPaciente.Controls.Add(this.cboIndPrinc);
            this.grpPaciente.Controls.Add(this.cboConvenio);
            this.grpPaciente.Controls.Add(this.txtCep);
            this.grpPaciente.Controls.Add(this.label1);
            this.grpPaciente.Controls.Add(this.txtNome);
            this.grpPaciente.Controls.Add(this.lblNome);
            this.grpPaciente.Controls.Add(this.txtCodigoProntuario);
            this.grpPaciente.Controls.Add(this.lblCodigo);
            this.grpPaciente.Location = new System.Drawing.Point(12, 12);
            this.grpPaciente.Name = "grpPaciente";
            this.grpPaciente.Size = new System.Drawing.Size(1161, 487);
            this.grpPaciente.TabIndex = 0;
            this.grpPaciente.TabStop = false;
            this.grpPaciente.Text = "Paciente";
            // 
            // txtLocalidade
            // 
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
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(657, 171);
            this.txtData.MaxLength = 8;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(103, 20);
            this.txtData.TabIndex = 17;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(657, 154);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Data";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(567, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Beneficente";
            // 
            // cboBeneficente
            // 
            this.cboBeneficente.FormattingEnabled = true;
            this.cboBeneficente.Location = new System.Drawing.Point(567, 171);
            this.cboBeneficente.Name = "cboBeneficente";
            this.cboBeneficente.Size = new System.Drawing.Size(81, 21);
            this.cboBeneficente.TabIndex = 16;
            // 
            // cboFolha
            // 
            this.cboFolha.FormattingEnabled = true;
            this.cboFolha.Location = new System.Drawing.Point(6, 218);
            this.cboFolha.Name = "cboFolha";
            this.cboFolha.Size = new System.Drawing.Size(489, 21);
            this.cboFolha.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 155);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Médico";
            // 
            // btnExcluiFolha
            // 
            this.btnExcluiFolha.Image = global::WEDLC.Properties.Resources.up;
            this.btnExcluiFolha.Location = new System.Drawing.Point(534, 216);
            this.btnExcluiFolha.Name = "btnExcluiFolha";
            this.btnExcluiFolha.Size = new System.Drawing.Size(27, 24);
            this.btnExcluiFolha.TabIndex = 20;
            this.btnExcluiFolha.UseVisualStyleBackColor = true;
            // 
            // cboMedico
            // 
            this.cboMedico.FormattingEnabled = true;
            this.cboMedico.Location = new System.Drawing.Point(6, 171);
            this.cboMedico.Name = "cboMedico";
            this.cboMedico.Size = new System.Drawing.Size(555, 21);
            this.cboMedico.TabIndex = 15;
            // 
            // btnIncluiFolha
            // 
            this.btnIncluiFolha.Image = global::WEDLC.Properties.Resources.down;
            this.btnIncluiFolha.Location = new System.Drawing.Point(501, 216);
            this.btnIncluiFolha.Name = "btnIncluiFolha";
            this.btnIncluiFolha.Size = new System.Drawing.Size(27, 24);
            this.btnIncluiFolha.TabIndex = 19;
            this.btnIncluiFolha.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Folhas";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(651, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Nascimento";
            // 
            // grdFolha
            // 
            this.grdFolha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFolha.Location = new System.Drawing.Point(6, 245);
            this.grdFolha.Name = "grdFolha";
            this.grdFolha.Size = new System.Drawing.Size(1149, 123);
            this.grdFolha.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(567, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Sexo";
            // 
            // cboSexo
            // 
            this.cboSexo.FormattingEnabled = true;
            this.cboSexo.Location = new System.Drawing.Point(567, 79);
            this.cboSexo.Name = "cboSexo";
            this.cboSexo.Size = new System.Drawing.Size(81, 21);
            this.cboSexo.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(567, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Ind Sec";
            // 
            // cboIndSec
            // 
            this.cboIndSec.FormattingEnabled = true;
            this.cboIndSec.Location = new System.Drawing.Point(567, 125);
            this.cboIndSec.Name = "cboIndSec";
            this.cboIndSec.Size = new System.Drawing.Size(588, 21);
            this.cboIndSec.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(455, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Telefone";
            // 
            // txtUf
            // 
            this.txtUf.Location = new System.Drawing.Point(375, 79);
            this.txtUf.MaxLength = 8;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(760, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Convênio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ind Princ";
            // 
            // cboIndPrinc
            // 
            this.cboIndPrinc.FormattingEnabled = true;
            this.cboIndPrinc.Location = new System.Drawing.Point(6, 125);
            this.cboIndPrinc.Name = "cboIndPrinc";
            this.cboIndPrinc.Size = new System.Drawing.Size(555, 21);
            this.cboIndPrinc.TabIndex = 13;
            // 
            // cboConvenio
            // 
            this.cboConvenio.FormattingEnabled = true;
            this.cboConvenio.Location = new System.Drawing.Point(763, 79);
            this.cboConvenio.Name = "cboConvenio";
            this.cboConvenio.Size = new System.Drawing.Size(395, 21);
            this.cboConvenio.TabIndex = 12;
            // 
            // txtCep
            // 
            this.txtCep.Location = new System.Drawing.Point(458, 35);
            this.txtCep.MaxLength = 8;
            this.txtCep.Name = "txtCep";
            this.txtCep.Size = new System.Drawing.Size(103, 20);
            this.txtCep.TabIndex = 3;
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
            this.txtCodigoProntuario.ForeColor = System.Drawing.Color.Red;
            this.txtCodigoProntuario.Location = new System.Drawing.Point(6, 35);
            this.txtCodigoProntuario.MaxLength = 10;
            this.txtCodigoProntuario.Name = "txtCodigoProntuario";
            this.txtCodigoProntuario.Size = new System.Drawing.Size(103, 20);
            this.txtCodigoProntuario.TabIndex = 1;
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
            this.grpBotoes.Controls.Add(this.btnCancelar);
            this.grpBotoes.Controls.Add(this.btnExcluir);
            this.grpBotoes.Controls.Add(this.btnGravar);
            this.grpBotoes.Controls.Add(this.btnSair);
            this.grpBotoes.Controls.Add(this.btnNovo);
            this.grpBotoes.Location = new System.Drawing.Point(11, 508);
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
            this.btnCancelar.TabIndex = 25;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = global::WEDLC.Properties.Resources.trash;
            this.btnExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluir.Location = new System.Drawing.Point(100, 13);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(85, 27);
            this.btnExcluir.TabIndex = 23;
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
            this.btnGravar.TabIndex = 24;
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
            this.btnSair.TabIndex = 26;
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
            this.btnNovo.TabIndex = 22;
            this.btnNovo.Text = "Novo";
            this.btnNovo.UseVisualStyleBackColor = true;
            // 
            // txtTexto
            // 
            this.txtTexto.AcceptsReturn = true;
            this.txtTexto.Location = new System.Drawing.Point(6, 390);
            this.txtTexto.MaxLength = 4000;
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTexto.Size = new System.Drawing.Size(1149, 87);
            this.txtTexto.TabIndex = 21;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 373);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "Observação";
            // 
            // txtNascimento
            // 
            this.txtNascimento.Location = new System.Drawing.Point(654, 80);
            this.txtNascimento.Mask = "00/00/0000";
            this.txtNascimento.Name = "txtNascimento";
            this.txtNascimento.Size = new System.Drawing.Size(100, 20);
            this.txtNascimento.TabIndex = 11;
            this.txtNascimento.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(458, 80);
            this.maskedTextBox1.Mask = "(99) 00000-0000";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(103, 20);
            this.maskedTextBox1.TabIndex = 9;
            // 
            // frmPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 564);
            this.ControlBox = false;
            this.Controls.Add(this.grpBotoes);
            this.Controls.Add(this.grpPaciente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaciente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Pacientes";
            this.Load += new System.EventHandler(this.frmPaciente_Load);
            this.grpPaciente.ResumeLayout(false);
            this.grpPaciente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFolha)).EndInit();
            this.grpBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPaciente;
        private System.Windows.Forms.GroupBox grpBotoes;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtCodigoProntuario;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtCep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboIndPrinc;
        private System.Windows.Forms.ComboBox cboConvenio;
        private System.Windows.Forms.ComboBox cboFolha;
        private System.Windows.Forms.Button btnExcluiFolha;
        private System.Windows.Forms.Button btnIncluiFolha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdFolha;
        private System.Windows.Forms.TextBox txtUf;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLogradouro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboIndSec;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboSexo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboMedico;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboBeneficente;
        private System.Windows.Forms.TextBox txtLocalidade;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.MaskedTextBox txtNascimento;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
    }
}