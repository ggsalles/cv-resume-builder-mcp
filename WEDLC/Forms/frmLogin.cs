using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using WEDLC.Banco;
using WinFormsZoom;

namespace WEDLC.Forms
{
    public partial class frmLogin : Form
    {
        private FormZoomHelper zoomHelper;

        public enum NivelAcesso
        {
            NIVEL1_ADM = 1,
            NIVEL2_USUCOMPLETO = 2,
            NIVEL3_USULEITURA = 3,
            NIVEL4_SEMACESSO = 4
        }

        public frmLogin()
        {
            InitializeComponent();
            //EncryptConnectionString();
            //DecryptConnectionString();
            zoomHelper = new FormZoomHelper(this); // Inicializa o helper de zoom
            this.FormClosed += (s, e) => zoomHelper.Dispose(); // Descarta automaticamente quando o form for fechado

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Altera o cursor para "espera"
                Cursor.Current = Cursors.WaitCursor;

                if (validaDados() == false)
                {
                    // Retorna o cursor para "padrão"
                    Cursor.Current = Cursors.Default;

                    return;
                }

                DataTable dtAux = new DataTable();
                clLogin objclLogin = new clLogin();
                clLog objclLog = new clLog();

                string pCripto = "";
                byte[] pCifrado;

                // Rotina que critpografa a senha
                pCripto = objclLogin.critptografiaSenha(txtSenha.Text.ToString(), txtUsuario.Text.ToString(), out pCifrado);

                //pDescripto = objclLogin.descritptografiaSenha(txtSenha.Text.ToString(), pCifrado);

                dtAux = objclLogin.buscaUsuarioLogin(txtUsuario.Text.ToString(), 0); // 0 = modulo acesso

                // Se não econtrou ninguém...
                if (dtAux.Rows.Count == 0)
                {
                    // Retorna o cursor para "padrão"
                    Cursor.Current = Cursors.Default;

                    // Exibe mensagem de usuário não cadastrado
                    MessageBox.Show("Usuário não cadastrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Limpa os campos
                    txtUsuario.Text = "";

                    // Limpa a senha
                    txtSenha.Text = "";

                    // Foca no campo usuário
                    txtUsuario.Focus();

                    // Fecha o DataTable
                    dtAux.Dispose();

                    return;
                }

                // Se encontrou, valida a permissão de acesso
                if (dtAux.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtAux.Rows[0]["idnivel"].ToString()) == (Int32)NivelAcesso.NIVEL4_SEMACESSO)
                    {
                        MessageBox.Show("Você não tem permissão de acesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Se encontrou e for troca de senha...
                if (dtAux.Rows.Count == 1 && dtAux.Rows[0]["nome"].ToString() == txtUsuario.Text.ToString() && dtAux.Rows[0]["trocasenha"].ToString() == "1")
                {
                    // Retorna o cursor para "padrão"
                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Você será redirecionado para o formulário de troca de senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    objclLogin.Idusuario = Int16.Parse(dtAux.Rows[0]["idusuario"].ToString());
                    objclLogin.Nome = txtUsuario.Text.ToString();
                    objclLogin.Senha = txtSenha.Text.ToString();

                    // Deixa o form de senha invisivel
                    this.Visible = false;

                    // Cria um objeto para o form de troca de senhas abrir
                    frmTrocaSenha objTrocaSenha = new frmTrocaSenha(objclLogin);

                    //Abre o form de senha modal
                    objTrocaSenha.ShowDialog();

                    // Deixa o form da senha visivel novamente
                    this.Visible = true;

                    txtSenha.Text = "";
                    txtSenha.Focus();

                    return;

                }

                // Se for diferente...
                if (pCripto != dtAux.Rows[0]["password"].ToString())
                {
                    // Retorna o cursor para "padrão"
                    Cursor.Current = Cursors.Default;

                    // Exibe mensagem de senha inválida
                    MessageBox.Show("Senha Inválida!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Limpa a senha
                    txtSenha.Focus();

                    // Limpa o campo senha
                    txtSenha.Text = "";

                    // Fecha o DataTable
                    dtAux.Dispose();


                    return;
                }
                else
                {
                    // Retorna o cursor para "padrão"
                    Cursor.Current = Cursors.Default;

                    // Se chegou aqui, é porque o usuário e senha estão corretos
                    MessageBox.Show("Usuário " + txtUsuario.Text.ToString().ToUpper() + " conectado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // GRAVA LOG
                    objclLog.IdLogDescricao = 1; // descrição LOGIN na tabela LOGDESCRICAO
                    objclLog.IdUsuario = Int32.Parse(dtAux.Rows[0]["idusuario"].ToString());
                    objclLog.DescErro = "";

                    if (objclLog.incluiLogin() == false)
                    {
                        // Se não conseguiu gravar o log, exibe mensagem de erro
                        MessageBox.Show("Erro ao tentar gravar o log!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Fecha o form login
                        this.Hide();

                        // Cria um objeto para o form principal abrir
                        frmPrincipal objPrincipal = new frmPrincipal(txtUsuario.Text, null);
                        //objPrincipal.FindForm().Text = objPrincipal.FindForm().Text + ": " + txtUsuario.Text.ToString();

                        //Abre o form principal
                        objPrincipal.ShowDialog();

                    }

                    Close();
                }
            }
            catch (Exception ex)
            {
                // Retorna o cursor para "padrão"
                Cursor.Current = Cursors.Default;

                // Exibe a mensagem de erro
                MessageBox.Show(ex.Message);

                // GRAVA LOG
                clLog objclLog = new clLog();
                objclLog.IdLogDescricao = 3; // descrição GENÉRICO na tabela LOGDESCRICAO
                objclLog.IdUsuario = 9999;
                objclLog.DescErro = ex.Message.ToString();

                if (objclLog.incluiLogin() == false)
                {
                    MessageBox.Show("Erro ao tentar gravar o log!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool validaDados()
        {
            if (txtUsuario.Text.ToString().Length == 0 || txtSenha.Text.ToString().Length == 0)
            {
                MessageBox.Show("O preenchimento dos campos usuário e senha são obrigatórios.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static void EncryptConnectionString()
        {
            // Abre o arquivo de configuração do aplicativo
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection("connectionStrings");

            if (section != null && !section.SectionInformation.IsProtected)
            {
                // Criptografa a seção usando o provedor DPAPI
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
                Console.WriteLine("Connection string encrypted successfully.");
            }
        }

        public static void DecryptConnectionString()
        {
            // Abre o arquivo de configuração do aplicativo
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection("connectionStrings");
            if (section != null && section.SectionInformation.IsProtected)
            {
                // Descriptografa a seção
                section.SectionInformation.UnprotectSection();
                config.Save();
                Console.WriteLine("Connection string decrypted successfully.");
            }
        }
    }
}
