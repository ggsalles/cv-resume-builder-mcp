using Microsoft.Reporting.WinForms;
using System;
using System.Data;
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
            DataTable dt = this.buscaRelResultadoPaciente(pIdPaciente);

            reportViewer1.LocalReport.ReportPath = @"Relatorios\relResultadoMusculoNeuro.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            // Parâmetro
            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("pIdade", "56"),
                new ReportParameter("pTitulo", pSigla + " - " + pNome)
            };
            reportViewer1.LocalReport.SetParameters(parametros);

            // Dataset
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("dsPaciente", dt));

            // Renderiza
            reportViewer1.RefreshReport();
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
