using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
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
                CryptoHelper.Id = 2; // ID da credencial única
                DataTable dt = CryptoHelper.BuscaCriptografia();
                if (dt == null || dt.Rows.Count == 0)
                {
                    await AtualizarMensagemAsync("Erro: credenciais não encontradas!", 100);
                    return;
                }

                var row = dt.Rows[dt.Rows.Count - 1];
                string ip = row["ip_descriptografado"].ToString().Trim();
                string share = row["share_descriptografado"].ToString().Trim('/').Trim();
                string user = row["user_descriptografado"].ToString();
                string pass = row["pass_descriptografado"].ToString();

                // 2️⃣ Verificar se o servidor responde ao ping
                await AtualizarMensagemAsync("Verificando acesso ao servidor...", 20);
                if (!HostDisponivel(ip))
                {
                    throw new Exception($"Servidor {ip} inacessível. Verifique a conexão ou VPN.");
                }

                // 3️⃣ Montar URL do arquivo no IIS (HTTP)
                await AtualizarMensagemAsync("Montando URL do servidor...", 25);
                string exeServidor = "WEDLC.exe";
                string baseUrl = $"http://{ip}"; // se usar porta diferente, coloque :porta aqui
                //if (!string.IsNullOrWhiteSpace(share))
                //    baseUrl = $"{baseUrl}/{share}";
                string fileUrl = $"{baseUrl}/{exeServidor}";

                // 4️⃣ Verificar versão do EXE remoto (fazendo download para temp)
                await AtualizarMensagemAsync("Verificando versão do cliente no servidor...", 35);

                FileVersionInfo serverVer = null;
                string tempRemote = null;
                try
                {
                    tempRemote = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + "_" + exeServidor);
                    using (var http = CreateHttpClientBasic(user, pass))
                    {
                        bool ok = await DownloadFileWithProgressAsync(http, fileUrl, tempRemote, (p) =>
                        {
                            // mapear progresso do download dentro do passo 35..40 (pequeno feedback)
                            int prog = 35 + (int)(p * 5 / 100.0); // 35..40
                            AtualizarProgresso(prog);
                        });

                        if (!ok)
                            throw new Exception("Não foi possível baixar temporariamente o arquivo remoto para verificação de versão.");
                    }

                    serverVer = FileVersionInfo.GetVersionInfo(tempRemote);
                }
                catch
                {
                    // limpa o temp caso haja erro (será tratado abaixo)
                    if (tempRemote != null && File.Exists(tempRemote))
                        File.Delete(tempRemote);
                    throw;
                }

                // 5️⃣ Comparar versões e atualizar se necessário
                string exeLocal = @"C:\WEDLC\WEDLC.exe";
                FileVersionInfo localVer = File.Exists(exeLocal) ? FileVersionInfo.GetVersionInfo(exeLocal) : null;

                Version serverVersion = new Version(serverVer.FileMajorPart, serverVer.FileMinorPart, serverVer.FileBuildPart, serverVer.FilePrivatePart);
                Version localVersion = null;
                if (localVer != null)
                    localVersion = new Version(localVer.FileMajorPart, localVer.FileMinorPart, localVer.FileBuildPart, localVer.FilePrivatePart);

                if (localVer == null || serverVersion > localVersion)
                {
                    await AtualizarMensagemAsync("Atualizando cliente...", 40);

                    // Se já temos tempRemote (arquivo baixado), podemos usá-lo em vez de baixar novamente.
                    if (tempRemote != null && File.Exists(tempRemote))
                    {
                        // mover/replace de forma segura
                        string backup = exeLocal + ".bak";
                        try
                        {
                            // garante pasta existe
                            Directory.CreateDirectory(Path.GetDirectoryName(exeLocal));

                            // se o arquivo local está em uso, a substituição pode falhar — tratamos exceções
                            if (File.Exists(exeLocal))
                            {
                                // tenta substituir com File.Replace (mantendo backup)
                                File.Replace(tempRemote, exeLocal, backup, true);
                                // remove backup se quiser
                                if (File.Exists(backup)) File.Delete(backup);
                            }
                            else
                            {
                                File.Move(tempRemote, exeLocal);
                            }
                        }
                        catch (Exception)
                        {
                            // fallback: sobrescrever (pode lançar se arquivo em uso)
                            File.Copy(tempRemote, exeLocal, true);
                        }
                        finally
                        {
                            if (File.Exists(tempRemote)) File.Delete(tempRemote);
                        }

                        AtualizarProgresso(100);
                        await AtualizarMensagemAsync("Cliente atualizado com sucesso!", 100);
                    }
                    else
                    {
                        // caso não tenha o temp (por algum motivo), baixa diretamente com progresso
                        using (var http = CreateHttpClientBasic(user, pass))
                        {
                            await DownloadFileWithProgressAsync(http, fileUrl, exeLocal, (p) =>
                            {
                                // mapear progresso do download para 40..100
                                int prog = 40 + (int)((p * 60) / 100.0);
                                AtualizarProgresso(prog);
                            });
                        }

                        await AtualizarMensagemAsync("Cliente atualizado com sucesso!", 100);
                    }
                }
                else
                {
                    await AtualizarMensagemAsync("Cliente já está atualizado.", 100);
                    // apagar temp, se existir
                    if (tempRemote != null && File.Exists(tempRemote)) File.Delete(tempRemote);
                }

                // 6️⃣ Finalizar e executar
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

        // ==================== Métodos auxiliares ====================

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

        public static int? ExtractErrorCode(string message)
        {
            var match = System.Text.RegularExpressions.Regex.Match(message, @"Código:\s*(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int code))
                return code;
            return null;
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
                progressBar.Invoke(new Action(() => progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, valor))));
            else
                progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, valor));
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

        // ==================== HTTP Helpers: Basic Auth + Download com progresso ====================

        /// <summary>
        /// Cria um HttpClient configurado para Basic Authentication usando header Authorization.
        /// Caller deve descartar (dispose) o HttpClient.
        /// </summary>
        private HttpClient CreateHttpClientBasic(string user, string pass)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true
            };

            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(120)
            };

            if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
            {
                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", token);
            }

            return client;
        }

        /// <summary>
        /// Faz download do arquivo via HttpClient de forma streaming e reporta progresso (0..100).
        /// Retorna true se sucesso.
        /// </summary>
        private async Task<bool> DownloadFileWithProgressAsync(HttpClient client, string url, string destinationPath, Action<int> reportProgress)
        {
            try
            {
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Falha ao baixar arquivo. Status: {response.StatusCode}");

                    var contentLength = response.Content.Headers.ContentLength;

                    // garantir pasta destino existe
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        var buffer = new byte[1024 * 64];
                        long totalRead = 0;
                        int read;
                        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fs.WriteAsync(buffer, 0, read);
                            totalRead += read;

                            if (contentLength.HasValue && contentLength.Value > 0)
                            {
                                int pct = (int)((totalRead * 100) / contentLength.Value);
                                reportProgress?.Invoke(pct);
                            }
                            else
                            {
                                // se tamanho desconhecido, reporta - usa 0..100 estimado por chunks
                                reportProgress?.Invoke(0);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar {url}: {ex.Message}", ex);
            }
        }
    }
}
