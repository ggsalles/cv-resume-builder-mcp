using System;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            cConexao objcConexao = new cConexao();
            this.Text = this.Text + ": " + " Conectado no ambiente: " + objcConexao.cAmbiente.ToString();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?","Atenção!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void especialidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void especializacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Cria um objeto para o form de troca de senhas abrir
            frmEspecializacao objEspeciualizacao = new frmEspecializacao();

            //Abre o form de especialização não modal
            objEspeciualizacao.Show();
        }
    }
}
