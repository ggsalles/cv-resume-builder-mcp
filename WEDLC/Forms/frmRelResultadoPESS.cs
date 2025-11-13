using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WEDLC.Banco;

namespace WEDLC.Forms
{
    public partial class frmRelResultadoPESS : Form
    {
        public const int codModulo = 17; //Código do módulo

        public Int32 pIdPaciente { get; set; }
        public Int32 pIdFolha { get; set; }
        public string pSigla { get; set; }
        public string pNome { get; set; }
        public bool gerar { get; set; }
        public string idade { get; set; }

        public Int32 idResultado { get; set; }
        public frmRelResultadoPESS(int IdPaciente, Int32 IdFolha, Int32 CodGrupoFolha, string Sigla, string Nome, Int32 idResultado)
        {
            InitializeComponent();
            this.pIdPaciente = IdPaciente;
            this.pIdFolha = IdFolha;
            this.pSigla = Sigla;
            this.pNome = Nome;
            this.idResultado = idResultado;
        }

        private void frmRelResultadoPESS_Load(object sender, EventArgs e)
        {
            //Verifica permissão de acesso
            if (!cPermissao.PodeAcessarModulo(codModulo))
            {
                MessageBox.Show("Usuário sem acesso", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fecha de forma segura depois que o handle estiver pronto
                this.BeginInvoke(new Action(() => this.Close()));
                return;
            }

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

                DataTable dtTecnica = this.buscaRelTecnica(idResultado);

                DataTable dtPESS = this.buscaRelResultado(); 

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

                string path = Path.Combine(Application.StartupPath, "Relatorios", "relResultadoPESS.rdlc");
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
                    new ReportDataSource("dsTecnica", dtTecnica));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsPESS", dtPESS));

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("dsComentario", dtComentarios));

                // Renderiza
                reportViewer1.RefreshReport();

                // Restaura o cursor normal
                Cursor.Current = Cursors.Default;

                // GRAVA LOG
                clLog objcLog = new clLog();
                objcLog.IdLogDescricao = 5; // descrição na tabela LOGDESCRICAO 
                objcLog.IdUsuario = Sessao.IdUsuario;
                objcLog.Descricao = this.Name;
                objcLog.incluiLog();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Erro ao tentar carregar o relatório:\n\n" + ex.Message,
                                "Atenção",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                // GRAVA LOG
                clLog objcLog = new clLog();
                objcLog.IdLogDescricao = 3; // descrição na tabela LOGDESCRICAO 
                objcLog.IdUsuario = Sessao.IdUsuario;
                objcLog.Descricao = this.Name + " - " + ex.Message;
                objcLog.incluiLog();
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

        private DataTable buscaRelTecnica(Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPotenciaisEvocadosTecnica objPotenciaisEvocadosTecnica = new cPotenciaisEvocadosTecnica();

                objPotenciaisEvocadosTecnica.IdResultado = idResultado; //Código do resultado

                dtAux = objPotenciaisEvocadosTecnica.BuscaResultadoPotEvocadoTecnica();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelTecnica!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelResultado()
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPotenciaisPESS objPotenciaisPESS = new cPotenciaisPESS();

                objPotenciaisPESS.IdPaciente = pIdPaciente; //Código do resultado
                objPotenciaisPESS.IdFolha = pIdFolha; //Código do resultado

                dtAux = objPotenciaisPESS.BuscaResultadoPess();

                return dtAux;

            }
            catch (Exception)
            {
                MessageBox.Show("Erro na buscaRelResultado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable(); // Return an empty DataTable to fix CS0126  
            }
        }

        private DataTable buscaRelComentarios(Int32 idResultado)
        {
            try
            {
                DataTable dtAux = new DataTable();
                cPotenciaisPEV objPotenciaisPEV = new cPotenciaisPEV();

                objPotenciaisPEV.IdResultado = idResultado; //Código do resultado

                dtAux = objPotenciaisPEV.BuscaResultadoComentarioPev();

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
