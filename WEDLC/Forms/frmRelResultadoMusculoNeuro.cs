using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmRelResultadoMusculoNeuro : Form
    {
        public Int32 pIdPaciente { get; set; }
        public Int32 pIdFolha { get; set; }
        public string pSigla { get; set; }
        public string pNome { get; set; }
        public bool gerar { get; set; }
        public string idade { get; set; }

        public Int32 idResultado { get; set; }
        public frmRelResultadoMusculoNeuro(int IdPaciente, Int32 IdFolha, Int32 CodGrupoFolha, string Sigla, string Nome, Int32 idResultado)
        {
            InitializeComponent();
            this.pIdPaciente = IdPaciente;
            this.pIdFolha = IdFolha;
            this.pSigla = Sigla;
            this.pNome = Nome;
            this.idResultado = idResultado;
        }

        private void frmRelResultadoMusculoNeuro_Load(object sender, EventArgs e)
        {
            try
            {
                // Pergunta ao usuário
                var resposta = MessageBox.Show("Deseja gerar uma página em branco após a impressão?",
                                               "Confirmação",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);

                // Altera o cursor para "espera"
                Cursor.Current = Cursors.WaitCursor;

                DataTable dtPaciente = this.buscaRelResultadoPaciente(pIdPaciente);
                DataTable dtAvaliacaoMuscularD = this.buscaRelResultadoAvaliacaoMuscular(pIdPaciente, pIdFolha, "D");
                DataTable dtAvaliacaoMuscularE = this.buscaRelResultadoAvaliacaoMuscular(pIdPaciente, pIdFolha, "E");
                DataTable dtAtividadeInsercao = this.buscaRelResultadoAtividadeInsercao(idResultado);
                DataTable dtPotenciaisUnidade = this.buscaRelResultadoPotenciaisUnidade(idResultado);
                DataTable dtNeuroConducaoMotoraLatencia = this.buscaRelNeuroCondMotoraLatencia(pIdFolha, idResultado);
                DataTable dtNeuroConducaoSensorial = this.buscaRelNeuroCondSensorial(pIdFolha, idResultado);
                DataTable dtComentarios = this.buscaRelComentarios(idResultado);

                // Define a variável com base na resposta do usuário
                if (resposta == DialogResult.Yes)
                {
                    gerar = true;
                }
                else
                {
                    gerar = false;
                }

                // Cálculo da idade
                idade = cUtil.DataNascimentoValidator.IdadeCalculator.CalcularIdade(DateTime.Parse(dtPaciente.Rows[0]["nascimento"].ToString())).ToString();

                string path = Path.Combine(Application.StartupPath, "Relatorios", "relResultadoMusculoNeuro.rdlc");
                reportViewer1.LocalReport.ReportPath = path;
                reportViewer1.LocalReport.DataSources.Clear();

                // Parâmetro
                ReportParameter[] parametros = new ReportParameter[]
                {
                new ReportParameter("pIdade", idade.ToString()),
                new ReportParameter("pTitulo", pSigla + " - " + pNome),
                new ReportParameter("PaginaEmBranco", gerar.ToString().ToLower()) // ou "false"
                };
                reportViewer1.LocalReport.SetParameters(parametros);

                // Dataset
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsPaciente", dtPaciente));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsAvalMuscD", dtAvaliacaoMuscularD));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsAvalMuscE", dtAvaliacaoMuscularE));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsAtividadeInsercao", dtAtividadeInsercao));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsPotenciaisUnidadeMotora", dtPotenciaisUnidade));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsVelNeuroCondMotora", dtNeuroConducaoMotoraLatencia));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsNeuroCondSensorial", dtNeuroConducaoSensorial));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsComentario", dtComentarios));

                // Renderiza
                reportViewer1.RefreshReport();

                // Restaura o cursor normal
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Erro ao tentar carregar o relatório:\n\n" + ex.Message,
                                "Atenção",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private DataTable buscaRelResultadoPaciente(Int32 idPaciente)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultado objcResultado = new cResultado();

                objcResultado.Paciente.IdPaciente = idPaciente; //Código da especialização

                dtAux = objcResultado.BuscaRelResultadoPaciente();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao tentar buscar o paciente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelResultadoAvaliacaoMuscular(Int32 idPaciente, Int32 idFolha, string lado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoAvaliacaoMuscular objResultadoAvaliacaoMuscular = new cResultadoAvaliacaoMuscular();

                objResultadoAvaliacaoMuscular.IdPaciente = idPaciente; //Código da especialização
                objResultadoAvaliacaoMuscular.IdFolha = idFolha; //Código da especialização
                objResultadoAvaliacaoMuscular.Lado = lado; //Código da especialização   

                dtAux = objResultadoAvaliacaoMuscular.buscaRelAvaliacaoMuscular();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelResultadoAvaliacaoMuscular!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelResultadoAtividadeInsercao(Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoAtividadeInsercao objResultadoAtividadeInsercao = new cResultadoAtividadeInsercao();

                objResultadoAtividadeInsercao.IdResultado = idResultado; //Código do resultado

                dtAux = objResultadoAtividadeInsercao.buscaResultadoAtividadeInsercao();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelResultadoAtividadeInsercao!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelResultadoPotenciaisUnidade(Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoPotenciaisUnidadeMotora objResultadoPotenciaisUnidadeMotora = new cResultadoPotenciaisUnidadeMotora();

                objResultadoPotenciaisUnidadeMotora.IdResultado = idResultado; //Código do resultado

                dtAux = objResultadoPotenciaisUnidadeMotora.buscaResultadoUnidadePotencial();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelResultadoPotenciaisUnidade!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelNeuroCondMotoraLatencia(Int32 idFolha, Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoNeuroCondMotora objResultadoNeuroCondMotora = new cResultadoNeuroCondMotora();

                objResultadoNeuroCondMotora.IdResultado = idResultado; //Código do resultado
                objResultadoNeuroCondMotora.IdFolha = idFolha; //Código do resultado

                dtAux = objResultadoNeuroCondMotora.buscaRelNeuroCondMotoraLatencia();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelNeuroCondMotoraLatencia!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelNeuroCondSensorial(Int32 idFolha, Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoNeuroCondSensorial objResultadoNeuroCondSensorial = new cResultadoNeuroCondSensorial();

                objResultadoNeuroCondSensorial.IdResultado = idResultado; //Código do resultado
                objResultadoNeuroCondSensorial.IdFolha = idFolha; //Código do resultado

                dtAux = objResultadoNeuroCondSensorial.buscaRelNeuroCondSensorial();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelNeuroCondSensorial!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelComentarios(Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cResultadoComentario objResultadoComentario = new cResultadoComentario();

                objResultadoComentario.IdResultado = idResultado; //Código do resultado

                dtAux = objResultadoComentario.buscaResultadoComentario();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelComentarios!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

    }
}
