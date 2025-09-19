using APP.Classes;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WEDLC.Banco;

namespace APP
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            EncryptConnectionString();
            //DecryptConnectionString();
            lblMensagem.Text = "Buscando por atualização...";
        }

        private async Task AtualizarClienteAsync()
        {
            try
            {
                Application.DoEvents();

                await AtualizarMensagemAsync("Iniciando atualização...", 5);

                // 1️⃣ Buscar credenciais
                await AtualizarMensagemAsync("Buscando credenciais no banco...", 15);
                CryptoHelper.Id = 2; // ID da credencial unica
                DataTable dt = CryptoHelper.BuscaCriptografia();
                if (dt == null || dt.Rows.Count == 0)
                {
                    await AtualizarMensagemAsync("Erro: credenciais não encontradas!", 100);
                    return;
                }

                var row = dt.Rows[dt.Rows.Count - 1];
                string ip = row["ip_descriptografado"].ToString();
                string share = row["share_descriptografado"].ToString();
                string user = row["user_descriptografado"].ToString();
                string pass = row["pass_descriptografado"].ToString();

                // 2️⃣ Mapear unidade de rede
                await AtualizarMensagemAsync("Conectando ao servidor...", 25);
                string localDrive = "Z:";
                await Task.Run(() =>
                {
                    NetworkHelper.Unmap(localDrive, true);
                    int rc = NetworkHelper.MapNetworkDrive(localDrive, $@"\\{ip}\{share}", user, pass);
                    if (rc != 0) throw new Exception($"Falha ao mapear unidade {share}. Código: {rc}");
                });

                // 3️⃣ Verificar e atualizar cliente
                string exeServidor = "WEDLC.exe";
                string exeLocal = @"C:\WEDLC\WEDLC.exe";
                string serverExePath = Path.Combine(localDrive + "\\", exeServidor);

                if (!File.Exists(serverExePath))
                    throw new FileNotFoundException("Arquivo do servidor não encontrado.");

                await AtualizarMensagemAsync("Verificando versão do cliente...", 35);

                FileVersionInfo serverVer = FileVersionInfo.GetVersionInfo(serverExePath);
                FileVersionInfo localVer = File.Exists(exeLocal) ? FileVersionInfo.GetVersionInfo(exeLocal) : null;

                if (localVer == null || new Version(serverVer.FileMajorPart, serverVer.FileMinorPart, serverVer.FileBuildPart, serverVer.FilePrivatePart) >
                    new Version(localVer.FileMajorPart, localVer.FileMinorPart, localVer.FileBuildPart, localVer.FilePrivatePart))
                {
                    await AtualizarMensagemAsync("Atualizando cliente...", 40);
                    await CopiarArquivoComProgressoAsync(serverExePath, exeLocal);
                    await AtualizarMensagemAsync("Cliente atualizado com sucesso!", 100);
                }
                else
                {
                    await AtualizarMensagemAsync("Cliente já está atualizado.", 100);
                }

                // 4️⃣ Limpar mapeamento
                NetworkHelper.Unmap(localDrive, true);

                // 5️⃣ Abrir login e fechar splash
                await AtualizarMensagemAsync("Finalizando...", 100);
                await Task.Delay(300);
                Process.Start(@"C:\WEDLC\WEDLC.exe");
                this.Close();
            }
            catch (Exception ex)
            {
                await AtualizarMensagemAsync($"Erro: {ex.Message}", 100);
            }
        }

        // Atualiza texto e progresso juntos
        private async Task AtualizarMensagemAsync(string msg, int progresso = -1)
        {
            if (lblMensagem.InvokeRequired)
                lblMensagem.Invoke(new Action(() => lblMensagem.Text = msg));
            else
                lblMensagem.Text = msg;

            if (progresso >= 0)
                AtualizarProgresso(progresso);

            await Task.Yield(); // força UI a atualizar
        }

        private void AtualizarProgresso(int valor)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke(new Action(() => progressBar.Value = valor));
            else
                progressBar.Value = valor;
        }

        // Copia arquivo atualizando a barra suavemente
        private async Task CopiarArquivoComProgressoAsync(string origem, string destino)
        {
            const int bufferSize = 1024 * 256; // 256 KB
            using (FileStream fsOrigem = new FileStream(origem, FileMode.Open, FileAccess.Read))
            using (FileStream fsDestino = new FileStream(destino, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[bufferSize];
                long totalBytes = fsOrigem.Length;
                long bytesCopiados = 0;
                int bytesLidos;

                while ((bytesLidos = await fsOrigem.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fsDestino.WriteAsync(buffer, 0, bytesLidos);
                    bytesCopiados += bytesLidos;

                    int progresso = 40 + (int)((bytesCopiados * 60) / totalBytes); // da etapa 40 até 100
                    AtualizarProgresso(progresso);
                }
            }
        }

        private async void SplashForm_Load(object sender, EventArgs e)
        {
            await AtualizarClienteAsync();
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
