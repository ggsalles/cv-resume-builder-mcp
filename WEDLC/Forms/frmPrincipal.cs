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
    }
}
