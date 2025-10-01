using System;
using System.Data;
using System.Windows.Forms;
using WEDLC.Banco;

namespace APP
{
    public partial class frmCredencial : Form
    {

        TextBox txtIP = new TextBox() { Left = 120, Top = 20, Width = 200 };
        TextBox txtUser = new TextBox() { Left = 120, Top = 60, Width = 200 };
        TextBox txtPass = new TextBox() { Left = 120, Top = 100, Width = 200, UseSystemPasswordChar = true };
        TextBox txtShare = new TextBox() { Left = 120, Top = 140, Width = 200, Text = "Compartilhamento" };

        Button btnMap = new Button() { Left = 20, Top = 180, Text = "Mapear Pasta", Width = 140 };
        Button btnUnmap = new Button() { Left = 180, Top = 180, Text = "Desmapear Pasta", Width = 140 };
        Button btnCripto = new Button() { Left = 20, Top = 200, Text = "Cripto", Width = 140 };

        Label lblIP = new Label() { Left = 20, Top = 20, Text = "IP / Host:", Width = 100 };
        Label lblUser = new Label() { Left = 20, Top = 60, Text = "Usuário:", Width = 100 };
        Label lblPass = new Label() { Left = 20, Top = 100, Text = "Senha:", Width = 100 };
        Label lblShare = new Label() { Left = 20, Top = 140, Text = "Nome do Share:", Width = 100 };

        public frmCredencial()
        {
            this.Text = "Mapeamento de Pasta de Rede";
            this.ClientSize = new System.Drawing.Size(360, 230);

            Controls.AddRange(new Control[] { txtIP, txtUser, txtPass, txtShare, btnMap, btnUnmap, btnCripto, lblIP, lblUser, lblPass, lblShare });

            btnMap.Click += BtnMap_Click;
            btnUnmap.Click += BtnUnmap_Click;
            btnCripto.Click += BtnCripto_Click;
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnUnmap_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCripto_Click(object sender, EventArgs e)
        {

            // Setar credenciais
            CryptoHelper.SetCredenciais("191.252.156.57", "WEDLC", "Usuario", "A!wop_");
            CryptoHelper.Id = 1;

            // Inserir no banco
            if (CryptoHelper.IncluiCriptografia())
                Console.WriteLine("Credenciais inseridas com sucesso!");

            // Buscar do banco
            DataTable dt = CryptoHelper.BuscaCriptografia();
            if (dt != null && dt.Rows.Count > 0)
                Console.WriteLine("IP descriptografado: " + dt.Rows[0]["ip"]);

        }

    }
}
