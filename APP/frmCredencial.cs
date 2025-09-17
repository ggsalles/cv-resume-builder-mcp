using APP.Classes;
using System;
using System.Security.Principal;
using System.Windows.Forms;

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

        Label lblIP = new Label() { Left = 20, Top = 20, Text = "IP / Host:", Width = 100 };
        Label lblUser = new Label() { Left = 20, Top = 60, Text = "Usuário:", Width = 100 };
        Label lblPass = new Label() { Left = 20, Top = 100, Text = "Senha:", Width = 100 };
        Label lblShare = new Label() { Left = 20, Top = 140, Text = "Nome do Share:", Width = 100 };

        public frmCredencial()
        {
            this.Text = "Mapeamento de Pasta de Rede";
            this.ClientSize = new System.Drawing.Size(360, 230);

            Controls.AddRange(new Control[] { txtIP, txtUser, txtPass, txtShare, btnMap, btnUnmap, lblIP, lblUser, lblPass, lblShare });

            btnMap.Click += BtnMap_Click;
            btnUnmap.Click += BtnUnmap_Click;
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();
            string share = txtShare.Text.Trim();
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(share) ||
                string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Informe IP, usuário, senha e nome do compartilhamento.");
                return;
            }

            string remoteShare = $@"\\{ip}\{share}";
            string localDrive = "Z:"; // letra desejada

            int rc = NetworkHelper.MapNetworkDrive(localDrive, remoteShare, user, pass);

            if (rc == 0)
                MessageBox.Show($"Conectado a {remoteShare} com sucesso.");
            else
                MessageBox.Show($"Falha ao mapear {remoteShare}. Código: {rc}.");
        }

        private void BtnUnmap_Click(object sender, EventArgs e)
        {
            string localDrive = "Z:"; // mesma letra usada para mapear
            int rc = NetworkHelper.Unmap(localDrive);

            if (rc == 0)
                MessageBox.Show($"Desconectado {localDrive} com sucesso.");
            else
                MessageBox.Show($"Falha ao desconectar {localDrive}. Código: {rc}.");
        }
    }
}
