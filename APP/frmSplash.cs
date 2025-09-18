using APP.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP
{
    public partial class frmSplash : Form
    {
        private Label lblMessage;

        public frmSplash()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 400;
            this.Height = 150;

            lblMessage = new Label()
            {
                Text = "Iniciando...",
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Segoe UI", 12)
            };
            this.Controls.Add(lblMessage);

            this.Shown += async (s, e) => await StartUpdateProcess();
        }

        private async Task StartUpdateProcess()
        {
            string ipServidor = "192.168.1.100";
            string shareServidor = "Atualizacoes";
            string usuario = @"Servidor\Usuario";
            string senha = "senha123";
            string exeServidor = "MeuCliente.exe";
            string exeLocal = @"C:\MeuApp\MeuCliente.exe";

            try
            {
                // Executa a atualização em background
                await Task.Run(() =>
                {
                    UpdateWithStatus(ipServidor, shareServidor, usuario, senha, exeServidor, exeLocal);
                });

                lblMessage.Invoke((MethodInvoker)(() => lblMessage.Text = "Atualização concluída!"));
                await Task.Delay(500);

                // Chama o EXE de login
                Process.Start(@"C:\MeuApp\Login.exe");
            }
            catch (Exception ex)
            {
                lblMessage.Invoke((MethodInvoker)(() => lblMessage.Text = $"Erro: {ex.Message}"));
                await Task.Delay(3000);
            }
            finally
            {
                this.Close();
            }
        }

        // Wrapper que envia mensagens para o splash
        private void UpdateWithStatus(string ip, string share, string user, string password, string remoteExe, string localExe)
        {
            void SetMessage(string msg) => lblMessage.Invoke((MethodInvoker)(() => lblMessage.Text = msg));

            string remoteShare = $@"\\{ip}\{share}";
            string localDrive = "Z:";

            try
            {
                SetMessage("Desconectando unidade anterior...");
                NetworkHelper.Unmap(localDrive, true);

                SetMessage("Mapeando unidade de rede...");
                int rc = NetworkHelper.MapNetworkDrive(localDrive, remoteShare, user, password);
                if (rc != 0)
                {
                    SetMessage($"Falha ao mapear {remoteShare}. Código: {rc}");
                    return;
                }

                string serverExePath = System.IO.Path.Combine(localDrive + "\\", remoteExe);

                SetMessage("Verificando arquivo no servidor...");
                if (!System.IO.File.Exists(serverExePath))
                {
                    SetMessage($"Arquivo não encontrado: {serverExePath}");
                    return;
                }

                SetMessage("Verificando versões...");
                var serverVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(serverExePath);
                Version serverVersion = new Version(
                    serverVersionInfo.FileMajorPart,
                    serverVersionInfo.FileMinorPart,
                    serverVersionInfo.FileBuildPart,
                    serverVersionInfo.FilePrivatePart
                );

                Version localVersion = System.IO.File.Exists(localExe)
                    ? new Version(System.Diagnostics.FileVersionInfo.GetVersionInfo(localExe).FileMajorPart,
                                  System.Diagnostics.FileVersionInfo.GetVersionInfo(localExe).FileMinorPart,
                                  System.Diagnostics.FileVersionInfo.GetVersionInfo(localExe).FileBuildPart,
                                  System.Diagnostics.FileVersionInfo.GetVersionInfo(localExe).FilePrivatePart)
                    : new Version(0, 0, 0, 0);

                SetMessage($"Local: {localVersion} | Servidor: {serverVersion}");

                if (serverVersion > localVersion)
                {
                    SetMessage("Atualizando cliente...");
                    System.IO.File.Copy(serverExePath, localExe, true);
                    SetMessage("Cliente atualizado com sucesso!");
                }
                else
                {
                    SetMessage("Cliente já está atualizado.");
                }
            }
            catch (Exception ex)
            {
                SetMessage($"Erro ao atualizar cliente: {ex.Message}");
            }
            finally
            {
                SetMessage("Desconectando unidade de rede...");
                NetworkHelper.Unmap(localDrive, true);
            }
        }
    }

}
