using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WEDLC.Banco
{
    public class cUtil
    {
        public class DataNascimentoValidator
        {
            public static (bool valido, string mensagem) ValidarDataNascimento(string dataStr)
            {
                // Verifica se a string está vazia
                if (string.IsNullOrWhiteSpace(dataStr))
                {
                    return (false, "A data de nascimento não pode estar vazia.");
                }

                // Tenta converter a string para DateTime
                if (!DateTime.TryParse(dataStr, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dataNascimento))
                {
                    return (false, "Formato de data inválido!");
                }

                // Verifica se a data é no futuro
                if (dataNascimento > DateTime.Today)
                {
                    return (false, "Data de nascimento não pode ser no futuro.");
                }

                // Verifica se a data é muito antiga (ex: mais de 150 anos atrás)
                if (dataNascimento < DateTime.Today.AddYears(-150))
                {
                    return (false, "Data de nascimento inválida. Verifique o ano.");
                }

                return (true, "Data de nascimento válida.");
            }

            public static class IdadeCalculator
            {
                public static int CalcularIdade(DateTime dataNascimento)
                {
                    return CalcularIdade(dataNascimento, DateTime.Today);
                }

                public static int CalcularIdade(DateTime dataNascimento, DateTime dataReferencia)
                {
                    // Verifica se a data de nascimento é no futuro (em relação à data de referência)
                    if (dataNascimento > dataReferencia)
                    {
                        throw new ArgumentException("Data de nascimento não pode ser no futuro");
                    }

                    int idade = dataReferencia.Year - dataNascimento.Year;

                    // Se ainda não chegou o aniversário este ano, subtrai 1 da idade
                    if (dataNascimento.Date > dataReferencia.AddYears(-idade))
                    {
                        idade--;
                    }

                    return idade;
                }
            }
        }

        public class ValidaFormulario
        {
            public static bool FormularioEstaAberto<T>() where T : Form
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form is T)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public class CPFValidator
        {
            // Método para validar CPF
            public static bool ValidarCPF(string cpf)
            {
                // Remove caracteres não numéricos
                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                // Verifica se o tamanho é 11 ou se todos os dígitos são iguais
                if (cpf.Length != 11 || TodosDigitosIguais(cpf))
                {
                    return false;
                }

                // Calcula o primeiro dígito verificador
                int soma = 0;
                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(cpf[i].ToString()) * (10 - i);
                }
                int resto = soma % 11;
                int digito1 = resto < 2 ? 0 : 11 - resto;

                // Calcula o segundo dígito verificador
                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(cpf[i].ToString()) * (11 - i);
                }
                resto = soma % 11;
                int digito2 = resto < 2 ? 0 : 11 - resto;

                // Verifica se os dígitos calculados conferem com os informados
                return int.Parse(cpf[9].ToString()) == digito1 &&
                       int.Parse(cpf[10].ToString()) == digito2;
            }

            // Método auxiliar para verificar se todos os dígitos são iguais
            private static bool TodosDigitosIguais(string cpf)
            {
                for (int i = 1; i < cpf.Length; i++)
                {
                    if (cpf[i] != cpf[0])
                    {
                        return false;
                    }
                }
                return true;
            }

            // Método para formatar CPF (xxx.xxx.xxx-xx)
            public static string FormatarCPF(string cpf)
            {
                cpf = new string(cpf.Where(char.IsDigit).ToArray());
                if (cpf.Length != 11) return cpf;

                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }
        }

        public static class FormScaler
        {
            public static float ScaleFactor { get; set; } = 1.2f;

            public static void ApplyScaling(Form form)
            {
                form.Scale(new SizeF(ScaleFactor, ScaleFactor));
                foreach (Control ctrl in form.Controls)
                {
                    ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size * ScaleFactor, ctrl.Font.Style);
                }
            }
        }

        public static class ValidacaoTextBox
        {
            public static void PermitirDecimaisPositivosNegativos(TextBox textBox, KeyPressEventArgs e, int casasDecimais = 2)
            {
                // Teclas de controle permitidas (backspace, delete, tab, etc.)
                if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                    return;
                }

                // Permite sinal negativo apenas no início
                if (e.KeyChar == '-')
                {
                    // Só permite se estiver no início e não houver outro sinal negativo
                    if (textBox.SelectionStart == 0 && !textBox.Text.Contains("-"))
                    {
                        e.Handled = false;
                        return;
                    }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                }

                // Permite vírgula
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    // Converte ponto para vírgula (padrão brasileiro)
                    if (e.KeyChar == '.') e.KeyChar = ',';

                    // Verifica se já existe vírgula
                    if (textBox.Text.Contains(","))
                    {
                        e.Handled = true;
                        return;
                    }

                    // Não permite vírgula no início ou logo após o sinal negativo
                    if (textBox.Text.Length == 0 ||
                        (textBox.Text.Length == 1 && textBox.Text == "-"))
                    {
                        e.Handled = true;
                        return;
                    }

                    e.Handled = false;
                    return;
                }

                // Permite apenas números
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                // Validação de casas decimais (máximo 2)
                if (textBox.Text.Contains(","))
                {
                    int indexVirgula = textBox.Text.IndexOf(',');
                    string parteDecimal = textBox.Text.Substring(indexVirgula + 1);

                    // Remove texto selecionado do cálculo
                    if (textBox.SelectionLength > 0)
                    {
                        int startIndex = textBox.SelectionStart - indexVirgula - 1;
                        if (startIndex >= 0 && startIndex < parteDecimal.Length)
                        {
                            parteDecimal = parteDecimal.Remove(startIndex, textBox.SelectionLength);
                        }
                    }

                    // Verifica se atingiu o limite de casas decimais
                    if (parteDecimal.Length >= casasDecimais &&
                        textBox.SelectionStart > indexVirgula)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                e.Handled = false;
            }

            public static void FormatarAoPerderFocoUmaCasaDecimal(object sender, EventArgs e)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    FormatarUmaCasaDecimal(textBox);
                }
            }

            public static void FormatarAoPerderFocoDuasCasasDecimais(object sender, EventArgs e)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    FormatarDuasCasasDecimais(textBox);
                }
            }

            public static void SelecionaTextoTextBox(object sender, EventArgs e)
            {
                var textBox = sender as TextBox;
                textBox?.SelectAll();
            }

            public static void FormatarUmaCasaDecimal(TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    return;

                int cursorPosition = textBox.SelectionStart;

                try
                {
                    string textoLimpo = textBox.Text.Replace("R$", "").Replace(" ", "").Trim();

                    // Verifica se é um número inteiro (apenas dígitos)
                    bool ehNumeroInteiro = textoLimpo.All(char.IsDigit);

                    if (decimal.TryParse(textoLimpo.Replace(".", ","), NumberStyles.Any,
                                        CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                    {
                        // Se for número inteiro E tiver mais de 3 dígitos, assume que são centavos
                        if (ehNumeroInteiro && textoLimpo.Length > 3 && valor != 0)
                        {
                            valor = valor / 100m;
                        }

                        textBox.Text = valor.ToString("N1", CultureInfo.GetCultureInfo("pt-BR"));
                        textBox.SelectionStart = Math.Min(cursorPosition, textBox.Text.Length);
                    }
                }
                catch
                {
                    // Mantém o texto original em caso de erro
                }
            }

            public static void FormatarDuasCasasDecimais(TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    return;

                int cursorPosition = textBox.SelectionStart;

                try
                {
                    string textoLimpo = textBox.Text.Replace("R$", "").Replace(" ", "").Trim();

                    // Verifica se é um número inteiro (apenas dígitos)
                    bool ehNumeroInteiro = textoLimpo.All(char.IsDigit);

                    if (decimal.TryParse(textoLimpo.Replace(".", ","), NumberStyles.Any,
                                        CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                    {
                        // Se for número inteiro E tiver mais de 2 dígitos, assume que são centavos
                        if (ehNumeroInteiro && textoLimpo.Length > 2 && valor != 0)
                        {
                            valor = valor / 100m;
                        }

                        textBox.Text = valor.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
                        textBox.SelectionStart = Math.Min(cursorPosition, textBox.Text.Length);
                    }
                }
                catch
                {
                    // Mantém o texto original em caso de erro
                }
            }

            public static decimal? ObterValorDecimal(TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    return null;

                try
                {
                    string textoLimpo = textBox.Text.Replace("R$", "")
                                                   .Replace(" ", "")
                                                   .Replace(".", "")
                                                   .Trim();

                    if (decimal.TryParse(textoLimpo, NumberStyles.Any,
                                        CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                    {
                        //return Math.Round(valor, 2); // Garante 2 casas decimais
                        return Math.Truncate(valor * 100) / 100;
                    }

                    return null;
                }
                catch
                {
                    return null;
                }
            }

            public static bool ValidarDecimal(string texto)
            {
                if (string.IsNullOrWhiteSpace(texto))
                    return false;

                try
                {
                    string textoLimpo = texto.Replace(" ", "").Trim();
                    return decimal.TryParse(textoLimpo, NumberStyles.Any,
                                          CultureInfo.GetCultureInfo("pt-BR"), out _);
                }
                catch
                {
                    return false;
                }
            }

            public static bool IsValidEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                try
                {
                    // Usa a classe MailAddress do .NET para validação
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static class CalculosTextBox
        {
            public static decimal? CalcularDiferencaPositiva(TextBox textBoxA, TextBox textBoxB)
            {
                try
                {
                    decimal valorA = ObterValorDecimal(textBoxA);
                    decimal valorB = ObterValorDecimal(textBoxB);

                    // Calcula a diferença absoluta (sempre positiva)
                    decimal diferenca = Math.Abs(valorA - valorB);

                    return diferenca;
                }
                catch
                {
                    return null;
                }
            }

            public static decimal ObterValorDecimal(TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    return 0m;

                // Remove formatação e converte para decimal
                string textoLimpo = textBox.Text.Replace("R$", "")
                                               .Replace("%", "")
                                               .Replace(" ", "")
                                               .Trim();

                if (decimal.TryParse(textoLimpo, System.Globalization.NumberStyles.Any,
                                    System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                {
                    return valor;
                }

                return 0m;
            }

            public static void AtualizarDiferencaPositiva(TextBox textBoxA, TextBox textBoxB, TextBox textBoxResultado)
            {
                decimal? diferenca = CalcularDiferencaPositiva(textBoxA, textBoxB);

                if (diferenca.HasValue)
                {
                    textBoxResultado.Text = diferenca.Value.ToString("N2",
                        System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
                }
                else
                {
                    textBoxResultado.Text = "0,00";
                }
            }

        }

        public static decimal? CalcularDiferencaComNegativos(TextBox textBoxA, TextBox textBoxB, bool semprePositivo = true)
        {
            try
            {
                decimal? valorA = ObterValorDecimal(textBoxA);
                decimal? valorB = ObterValorDecimal(textBoxB);

                if (!valorA.HasValue) valorA = 0m;
                if (!valorB.HasValue) valorB = 0m;

                decimal diferenca = valorA.Value - valorB.Value;

                if (semprePositivo)
                {
                    return Math.Abs(diferenca);
                }
                else
                {
                    return diferenca;
                }
            }
            catch
            {
                return null;
            }
        }

        // Função para obter valor decimal considerando negativos
        public static decimal? ObterValorDecimal(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
                return null;

            try
            {
                string textoLimpo = textBox.Text.Replace("R$", "")
                                               .Replace("%", "")
                                               .Replace(" ", "")
                                               .Trim();

                if (decimal.TryParse(textoLimpo, NumberStyles.Any,
                                    CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                {
                    return valor;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public class AdvancedEmailValidator
        {
            public static EmailValidationResult ValidateEmail(string email)
            {
                // Resultado da validação
                var result = new EmailValidationResult();

                if (!string.IsNullOrWhiteSpace(email))
                {
                    // 1. Verifica se está vazio
                    //if (string.IsNullOrWhiteSpace(email))
                    //{
                    //    result.IsValid = false;
                    //    result.ErrorMessage = "Email não pode estar vazio";
                    //    return result;
                    //}

                    // 2. Verifica comprimento máximo
                    if (email.Length > 254)
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Email muito longo (máximo 254 caracteres)";
                        return result;
                    }

                    // 3. Verifica formato básico com regex
                    try
                    {
                        if (!Regex.IsMatch(email,
                            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"))
                        {
                            result.IsValid = false;
                            result.ErrorMessage = "Formato de email inválido";
                            return result;
                        }
                    }
                    catch
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Erro na validação do formato";
                        return result;
                    }

                    // 4. Tenta criar um MailAddress (validação do .NET)
                    try
                    {
                        var mailAddress = new MailAddress(email);
                        result.IsValid = true;
                        result.NormalizedEmail = mailAddress.Address;
                    }
                    catch
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Endereço de email inválido";
                        return result;
                    }

                    return result;
                }

                else
                {
                    result.IsValid = true;
                }

                return result;
            }
        }

        // Classe para armazenar o resultado da validação
        public class EmailValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }
            public string NormalizedEmail { get; set; }
        }
    }

    public class AssemblyInfoHelper
    {
        public static string GetAssemblyTitle()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            return attribute?.Title ?? Path.GetFileNameWithoutExtension(assembly.Location);
        }

        public static string GetAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
        }

        public static string GetAssemblyDescription()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            return attribute?.Description ?? string.Empty;
        }

        public static string GetAssemblyCompany()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            return attribute?.Company ?? string.Empty;
        }

        public static string GetAssemblyCopyright()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            return attribute?.Copyright ?? string.Empty;
        }

        public static string GetAssemblyProduct()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return attribute?.Product ?? string.Empty;
        }
    }
}
