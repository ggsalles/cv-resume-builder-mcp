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

                // 2️⃣ Verificar se o servidor está acessível
                await AtualizarMensagemAsync("Verificando acesso ao servidor...", 20);
                if (!HostDisponivel(ip))
                {
                    throw new Exception($"Servidor {ip} inacessível. Verifique a conexão ou VPN.");
                }

                // 3️⃣ Mapear unidade de rede
                await AtualizarMensagemAsync("Conectando ao servidor...", 25);
                string localDrive = "Z:";
                await Task.Run(() =>
                {
                    NetworkHelper.Unmap(localDrive, true);
                    int rc = NetworkHelper.MapNetworkDrive(localDrive, $@"\\{ip}\{share}", user, pass);
                    if (rc != 0)
                    {
                        throw new Exception($"Falha ao mapear unidade {share}. {TranslateError(rc)}");
                    }
                });

                // 4️⃣ Verificar e atualizar cliente
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

                // 5️⃣ Limpar mapeamento
                NetworkHelper.Unmap(localDrive, true);

                // 6️⃣ Verificar se já está em execução antes de abrir
                await AtualizarMensagemAsync("Finalizando...", 100);
                await Task.Delay(300);

                if (!IsProcessRunning("WEDLC"))
                {
                    Process.Start(exeLocal);
                    this.Close();
                }
                else
                {
                    await AtualizarMensagemAsync("WEDLC já está em execução. Fechando...", 100);
                    await Task.Delay(2000);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                await AtualizarMensagemAsync($"Erro: {ex.Message}", 100);
            }
        }

        /// <summary>
        /// Verifica se o host responde ao ping
        /// </summary>
        private bool HostDisponivel(string host, int timeout = 2000)
        {
            try
            {
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    var reply = ping.Send(host, timeout);
                    return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tradução dos códigos de erro mais comuns do mapeamento
        /// </summary>
        private static string TranslateError(int code)
        {
            switch (code)
            {
                case 53:
                    return "Caminho de rede não encontrado.";
                case 67:
                    return "Nome de rede inválido.";
                case 86:
                case 1326:
                    return "Usuário ou senha incorretos.";
                case 1219:
                    return "Conflito de credenciais (já existe conexão com outro usuário).";
                case 1909:
                    return "A conta está bloqueada.";
                case 1921:
                    return "O servidor não está disponível.";
                default:
                    return $"Erro desconhecido. Código {code}";
            }
        }



        // Método para verificar se o processo está em execução
        private bool IsProcessRunning(string processName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(processName);
                return processes.Length > 0;
            }
            catch
            {
                return false;
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
