using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
                    await Task.Delay(4000);
                    this.Close();
                    return;
                }

                var row = dt.Rows[dt.Rows.Count - 1];
                string ip = row["ip_descriptografado"].ToString().Trim();
                string user = row["user_descriptografado"].ToString();
                string pass = row["pass_descriptografado"].ToString();
                string share = row["share_descriptografado"].ToString().ToLower();

                // 2️⃣ Verificar acesso ao servidor
                await AtualizarMensagemAsync("Verificando acesso ao servidor...", 20);
                if (!HostDisponivel(ip))
                    throw new Exception($"Servidor {ip} inacessível. Verifique a conexão ou VPN.");

                // 3️⃣ Montar URL
                string baseUrl = $"https://{ip}/{share}";
                string exeServidor = "WEDLC.exe";
                string zipServidor = "update.zip"; // pacote com todos arquivos
                string fileUrlExe = $"{baseUrl}/{exeServidor}";
                string fileUrlZip = $"{baseUrl}/{zipServidor}";

                // 4️⃣ Verificar versão do EXE remoto
                await AtualizarMensagemAsync("Verificando versão do cliente no servidor...", 35);

                FileVersionInfo serverVer = null;
                string tempExe = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_" + exeServidor);
                using (var http = CreateHttpClientBasic(user, pass))
                {
                    bool ok = await DownloadFileWithProgressAsync(http, fileUrlExe, tempExe, (p, msg) =>
                    {
                        int prog = 35 + (int)(p * 5 / 100.0); // 35..40
                        AtualizarProgresso(prog);
                    });
                    if (!ok)
                        throw new Exception("Não foi possível baixar o EXE remoto para verificação de versão.");
                }

                serverVer = FileVersionInfo.GetVersionInfo(tempExe);
                string exeLocal = @"C:\WEDLC\WEDLC.exe";
                FileVersionInfo localVer = File.Exists(exeLocal) ? FileVersionInfo.GetVersionInfo(exeLocal) : null;

                Version serverVersion = new Version(serverVer.FileMajorPart, serverVer.FileMinorPart, serverVer.FileBuildPart, serverVer.FilePrivatePart);
                Version localVersion = localVer != null
                    ? new Version(localVer.FileMajorPart, localVer.FileMinorPart, localVer.FileBuildPart, localVer.FilePrivatePart)
                    : null;

                if (localVer == null || serverVersion > localVersion)
                {
                    await AtualizarMensagemAsync("Atualizando cliente...", 45);

                    // 5️⃣ Baixar pacote ZIP com todos arquivos
                    string tempZip = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "_update.zip");
                    using (var http = CreateHttpClientBasic(user, pass))
                    {
                        await DownloadFileWithProgressAsync(http, fileUrlZip, tempZip, (p, msg) =>
                        {
                            int prog = 45 + (int)((p * 50) / 100.0); // 45..95
                            AtualizarProgresso(prog);
                        });
                    }

                    // 6️⃣ Extrair com segurança (sobrescrevendo)
                    string destino = @"C:\WEDLC";
                    await AtualizarMensagemAsync("Extraindo atualização...", 95);
                    await ExtractZipToDirectorySafeAsync(tempZip, destino, "WEDLC");

                    if (File.Exists(tempZip)) File.Delete(tempZip);
                    if (File.Exists(tempExe)) File.Delete(tempExe);

                    AtualizarProgresso(100);
                    await AtualizarMensagemAsync("Cliente atualizado com sucesso!", 100);
                }
                else
                {
                    await AtualizarMensagemAsync("Cliente já está atualizado.", 100);
                    if (File.Exists(tempExe)) File.Delete(tempExe);
                }

                // 7️⃣ Executar cliente
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
                await Task.Delay(4000); // aguarda alguns segundos antes de fechar
                this.Close();
            }
        }

        // 🔹 Extração segura de ZIP
        private async Task ExtractZipToDirectorySafeAsync(string zipPath, string destino, string exeName)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                int total = archive.Entries.Count;
                int atual = 0;

                foreach (var entry in archive.Entries)
                {
                    atual++;
                    string msg = $"Extraindo {entry.FullName} ({atual}/{total})";
                    await AtualizarMensagemAsync(msg, 70 + (int)((atual * 30.0) / total));

                    // 🔹 Se for diretório (não tem arquivo)
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        string dirPath = Path.Combine(destino, entry.FullName);
                        Directory.CreateDirectory(dirPath);
                        continue;
                    }

                    // 🔹 Caminho final
                    string destinoPath = Path.Combine(destino, entry.FullName);

                    // 🔹 Garante que diretório existe
                    string dir = Path.GetDirectoryName(destinoPath);
                    if (!string.IsNullOrEmpty(dir))
                        Directory.CreateDirectory(dir);

                    // 🔹 Extrai arquivo (sobrescrevendo se existir)
                    entry.ExtractToFile(destinoPath, true);

                    await Task.Yield(); // força UI atualizar
                }
            }
        }


        // ================== AUXILIARES ==================

        private HttpClient CreateHttpClientBasic(string user, string pass)
        {
            var handler = new HttpClientHandler
            {
                PreAuthenticate = true,
                Credentials = new System.Net.NetworkCredential(user, pass),
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            return new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        private async Task<bool> DownloadFileWithProgressAsync(HttpClient client, string url, string destino, Action<int, string> reportProgress)
        {
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!response.IsSuccessStatusCode) return false;

                var total = response.Content.Headers.ContentLength ?? -1L;
                var canReportProgress = total != -1;

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fs = new FileStream(destino, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var buffer = new byte[81920];
                    long totalRead = 0;
                    int read;
                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fs.WriteAsync(buffer, 0, read);
                        totalRead += read;

                        if (canReportProgress)
                        {
                            int percent = (int)((totalRead * 100L) / total);
                            string msg = $"{(totalRead / 1024.0 / 1024.0):F2} MB / {(total / 1024.0 / 1024.0):F2} MB";
                            reportProgress?.Invoke(percent, msg);
                        }
                    }
                }
            }
            return true;
        }

        private bool IsProcessRunning(string processName)
        {
            try
            {
                return Process.GetProcessesByName(processName).Length > 0;
            }
            catch
            {
                return false;
            }
        }

        private async Task AtualizarMensagemAsync(string msg, int progresso = -1)
        {
            if (lblMensagem.InvokeRequired)
                lblMensagem.Invoke(new Action(() => lblMensagem.Text = msg));
            else
                lblMensagem.Text = msg;

            if (progresso >= 0)
                AtualizarProgresso(progresso);

            await Task.Yield();
        }

        private void AtualizarProgresso(int valor)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke(new Action(() => progressBar.Value = Math.Min(progressBar.Maximum, valor)));
            else
                progressBar.Value = Math.Min(progressBar.Maximum, valor);
        }

        private async void SplashForm_Load(object sender, EventArgs e)
        {
            await AtualizarClienteAsync();
        }

        private bool HostDisponivel(string host)
        {
            try
            {
                using (var ping = new System.Net.NetworkInformation.Ping())
                {
                    var reply = ping.Send(host, 2000); // timeout 2 segundos
                    return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
