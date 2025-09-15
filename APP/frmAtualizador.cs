using System.Windows.Forms;
using System;
using System.IO;
using System.Diagnostics;

namespace APP
{
    public partial class frmAtualizador : Form
    {
        public frmAtualizador()
        {
            InitializeComponent();
        }

        private void btnComparar_Click(object sender, EventArgs e)
        {
            try
            {
                string caminhoLocal = txtCaminhoLocal.Text;
                string caminhoRepositorio = txtCaminhoRepositorio.Text;

                if (string.IsNullOrEmpty(caminhoLocal) || string.IsNullOrEmpty(caminhoRepositorio))
                {
                    MessageBox.Show("Por favor, preencha ambos os caminhos.");
                    return;
                }

                if (!File.Exists(caminhoLocal) || !File.Exists(caminhoRepositorio))
                {
                    MessageBox.Show("Um ou ambos os arquivos não existem.");
                    return;
                }

                // Comparar versões
                if (ArquivosSaoIguais(caminhoLocal, caminhoRepositorio))
                {
                    lblStatus.Text = "Status: Versões idênticas - Não é necessário atualizar";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "Status: Versões diferentes - Atualização necessária";
                    lblStatus.ForeColor = System.Drawing.Color.Red;

                    // Perguntar se deseja atualizar
                    DialogResult resultado = MessageBox.Show(
                        "Versão diferente encontrada. Deseja atualizar o arquivo local?",
                        "Atualização Disponível",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        AtualizarArquivo(caminhoLocal, caminhoRepositorio);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private bool ArquivosSaoIguais(string caminho1, string caminho2)
        {
            //// Método 1: Comparar por hash MD5 (mais preciso)
            //using (var md5 = System.Security.Cryptography.MD5.Create())
            //{
            //    using (var stream1 = File.OpenRead(caminho1))
            //    using (var stream2 = File.OpenRead(caminho2))
            //    {
            //        byte[] hash1 = md5.ComputeHash(stream1);
            //        byte[] hash2 = md5.ComputeHash(stream2);

            //        for (int i = 0; i < hash1.Length; i++)
            //        {
            //            if (hash1[i] != hash2[i])
            //                return false;
            //        }
            //        return true;
            //    }
            //}

            // Método alternativo: Comparar por versão do arquivo (se for .exe)
            // return GetFileVersion(caminho1) == GetFileVersion(caminho2);

            if (GetFileVersion(caminho1) == GetFileVersion(caminho2))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private string GetFileVersion(string caminho)
        {
            try
            {
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(caminho);
                return versionInfo.FileVersion ?? "0.0.0.0";
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        private void AtualizarArquivo(string caminhoLocal, string caminhoRepositorio)
        {
            try
            {
                // Fazer backup do arquivo antigo
                string backupPath = caminhoLocal + ".backup";
                if (File.Exists(caminhoLocal))
                {
                    File.Copy(caminhoLocal, backupPath, true);
                }

                // Copiar novo arquivo
                File.Copy(caminhoRepositorio, caminhoLocal, true);

                lblStatus.Text = "Status: Arquivo atualizado com sucesso!";
                lblStatus.ForeColor = System.Drawing.Color.Blue;

                MessageBox.Show("Arquivo atualizado com sucesso! Backup salvo como: " + backupPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar arquivo: {ex.Message}");
            }
        }

        private void btnProcurarLocal_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executáveis (*.exe)|*.exe|Todos os arquivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCaminhoLocal.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnProcurarRepositorio_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Executáveis (*.exe)|*.exe|Todos os arquivos (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCaminhoRepositorio.Text = openFileDialog.FileName;
                }
            }
        }
    }
}