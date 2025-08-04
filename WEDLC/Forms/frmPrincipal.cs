using System;
using System.Net;
using System.Windows.Forms;
using WEDLC.Banco;
using System.Net.NetworkInformation;


namespace WEDLC.Forms
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal(string pUsuario, string pSenha)

        {
            InitializeComponent();

            this.DoubleBuffered = true;
            string ip = ObterIPExterno();
            string mac = ObterMACAddress();
            GerenciadorConexaoMySQL objcConexao = new GerenciadorConexaoMySQL();
            this.Text = "Usuário: " + pUsuario + " || Conectado no ambiente: " + objcConexao._ambiente.ToString() + " || Servidor: " + ip + " || Endereço MAC: " + mac;
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

            // Cria um objeto para o form de troca de senhas abrir
            frmEspecializacao objEspecializacao = new frmEspecializacao();

            // Define o form pai como o form principal
            objEspecializacao.MdiParent = this;

            //Abre o form de especialização não modal
            objEspecializacao.Show();
        }

        private void nervoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmNervo objNervo = new frmNervo();

            // Define o form pai como o form principal
            objNervo.MdiParent = this;

            //Abre o form de especialização não modal
            objNervo.Show();
        }

        private void músculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmMusculo objMusculo = new frmMusculo();

            // Define o form pai como o form principal
            objMusculo.MdiParent = this;

            //Abre o form de especialização não modal
            objMusculo.Show();
        }

        public string ObterIPExterno()
        {
            string enderecoIP = new WebClient().DownloadString("http://icanhazip.com").Trim();
            return enderecoIP;
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
            // Cria um objeto para o form de troca de senhas abrir
            frmFolha objFolha = new frmFolha();

            // Define o form pai como o form principal
            objFolha.MdiParent = this;

            //Abre o form de especialização não modal
            objFolha.Show();
        }

        private void comentárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmComentarios objComentario = new frmComentarios();

            // Define o form pai como o form principal
            objComentario.MdiParent = this;

            //Abre o form de especialização não modal
            objComentario.Show();
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmPaciente objPaciente= new frmPaciente();

            // Define o form pai como o form principal
            objPaciente.MdiParent = this;

            //Abre o form de especialização não modal
            objPaciente.Show();
        }

        private void médicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmMedico objMedico = new frmMedico();
            // Define o form pai como o form principal
            objMedico.MdiParent = this;
            //Abre o form de especialização não modal
            objMedico.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            frmLogin objLogin = new frmLogin();
            objLogin.Close();
        }

        private void convênioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cria um objeto para o form de troca de senhas abrir
            frmConvenio objConvenio = new frmConvenio();
            // Define o form pai como o form principal
            objConvenio.MdiParent = this;
            //Abre o form de especialização não modal
            objConvenio.Show(); 
        }
    }
}
