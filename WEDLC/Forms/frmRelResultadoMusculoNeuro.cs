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
        public frmRelResultadoMusculoNeuro(int IdPaciente, Int32 IdFolha, Int32 CodGrupoFolha, string Sigla, string Nome)
        {
            InitializeComponent();
            this.pIdPaciente = IdPaciente;
            this.pIdFolha = IdFolha;
            this.pSigla = Sigla;
            this.pNome = Nome;
        }

        private void Form1_Load(object sender, EventArgs e)
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

                DataTable dt = this.buscaRelResultadoPaciente(pIdPaciente);

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
                idade = cUtil.DataNascimentoValidator.IdadeCalculator.CalcularIdade(DateTime.Parse(dt.Rows[0]["nascimento"].ToString())).ToString();

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
                    new ReportDataSource("dsPaciente", dt));

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
    }
}
