using System;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WEDLC.Banco;


namespace WEDLC.Forms
{
    public partial class frmPrincipal : Form
    {
        // Step 2: Update the constructor to await ObterIPExterno and set ip
        public frmPrincipal(string pUsuario, string pSenha)
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            string ip = "";
            string mac = ObterMACAddress();
            string version = GetAssemblyFileVersion();
            GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();

            //cUtil.FormScaler.ApplyScaling(this);

            // Use a local async function to await the IP and set the form text
            async void SetFormTextAsync()
            {
                ip = await ObterIPExterno();
                this.Text = "Usuário: " + pUsuario + " || Conectado no ambiente: " + objcConexao._ambiente.ToString() + " || Servidor: " + ip + " || Endereço MAC: " + mac + " || Versão: " + version;
            }
            SetFormTextAsync();
        }

        static string GetAssemblyFileVersion()
        {
            var assembly = Assembly.LoadFrom(@"C:\WEDLC\WEDLC.exe"); // ou Assembly.LoadFrom("Caminho/MeuExe.exe")
            var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            return attribute?.Version ?? "Versão não encontrada";
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?","Atenção!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }
        private void especializacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmEspecializacao>() == true)
            {
                MessageBox.Show("O formulário de Especialização já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmEspecializacao objEspecializacao = new frmEspecializacao();

            // Define o form pai como o form principal
            objEspecializacao.MdiParent = this;

            //Abre o form de especialização não modal
            objEspecializacao.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;

        }

        private void nervoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmNervo>() == true)
            {
                MessageBox.Show("O formulário de Nervo já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmNervo objNervo = new frmNervo();

            // Define o form pai como o form principal
            objNervo.MdiParent = this;

            //Abre o form de especialização não modal
            objNervo.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void músculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmMusculo>() == true)
            {
                MessageBox.Show("O formulário de Músculo já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmMusculo objMusculo = new frmMusculo();

            // Define o form pai como o form principal
            objMusculo.MdiParent = this;

            //Abre o form de especialização não modal
            objMusculo.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        // Step 1: Change ObterIPExterno to return Task<string> instead of void
        private async Task<string> ObterIPExterno()
        {
            try
            {
                try
                {
                    // Call the static method directly
                    string publicIP = await cIp.GetPublicIPAsync();
                    return publicIP;
                }
                catch (Exception ex)
                {
                    return $"Erro ao obter IP: {ex.Message}";
                }
            }
            catch
            {
                return "IP não encontrado";
            }
        }

        public string ObterMACAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var nic in nics)
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    return BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                }
            }
            return "MAC Address não encontrado";
        }

        private void folhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmFolha>() == true)
            {
                MessageBox.Show("O formulário de Folha já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmFolha objFolha = new frmFolha();

            // Define o form pai como o form principal
            objFolha.MdiParent = this;

            //Abre o form de especialização não modal
            objFolha.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void comentárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmComentarios>() == true)
            {
                MessageBox.Show("O formulário de Comentário já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmComentarios objComentario = new frmComentarios();

            // Define o form pai como o form principal
            objComentario.MdiParent = this;

            //Abre o form de especialização não modal
            objComentario.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmPaciente>() == true)
            {
                MessageBox.Show("O formulário de Paciente já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmPaciente objPaciente= new frmPaciente();

            // Define o form pai como o form principal
            objPaciente.MdiParent = this;

            //Abre o form de especialização não modal
            objPaciente.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void médicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmMedico>() == true)
            {
                MessageBox.Show("O formulário de Médico já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmMedico objMedico = new frmMedico();

            // Define o form pai como o form principal
            objMedico.MdiParent = this;

            //Abre o form de especialização não modal
            objMedico.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            frmLogin objLogin = new frmLogin();
            objLogin.Close();
        }

        private void convênioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmConvenio>() == true)
            {
                MessageBox.Show("O formulário de Convênio já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmConvenio objConvenio = new frmConvenio();

            // Define o form pai como o form principal
            objConvenio.MdiParent = this;

            //Abre o form de especialização não modal
            objConvenio.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void indicaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmIndicacao>() == true)
            {
                MessageBox.Show("O formulário de Indicação já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmIndicacao objIndicacao = new frmIndicacao();

            // Define o form pai como o form principal
            objIndicacao.MdiParent = this;

            //Abre o form de especialização não modal
            objIndicacao.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void exameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmExame objExame = new frmExame();

            // Define o form pai como o form principal
            objExame.MdiParent = this;

            //Abre o form de especialização não modal
            objExame.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void resultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmResultado>() == true)
            {
                MessageBox.Show("O formulário de Resultados já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmResultado objResultado = new frmResultado();

            // Define o form pai como o form principal
            objResultado.MdiParent = this;

            //Abre o form de especialização não modal
            objResultado.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }

        private void atividadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmAtividadeInsercao>() == true)
            {
                MessageBox.Show("O formulário de Resultados já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de troca de senhas abrir
            frmAtividadeInsercao objAtividade = new frmAtividadeInsercao();

            // Define o form pai como o form principal
            objAtividade.MdiParent = this;

            //Abre o form de especialização não modal
            objAtividade.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;

        }

        private void potenciaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cUtil.ValidaFormulario.FormularioEstaAberto<frmPotenciais>() == true)
            {
                MessageBox.Show("O formulário de Potenciais já está aberto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Altera o cursor para "espera"
            Cursor.Current = Cursors.WaitCursor;

            // Cria um objeto para o form de potenciais abrir
            frmPotenciais objPotenciais = new frmPotenciais();

            // Define o form pai como o form principal
            objPotenciais.MdiParent = this;

            // Abre o form de potenciais não modal
            objPotenciais.Show();

            // Restaura o cursor normal
            Cursor.Current = Cursors.Default;
        }
    }
}
