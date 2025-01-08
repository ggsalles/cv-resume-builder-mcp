using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WEDLC.Banco;



namespace WEDLC.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtAux = new DataTable();
                clLogin objclLogin = new clLogin();
                dtAux =  objclLogin.buscaUsuarioLogin(txtUsuario.Text.ToString());
                if (dtAux.Rows.Count == 0)
                {
                    MessageBox.Show("Usuário não cadastrado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Usuário conectado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
