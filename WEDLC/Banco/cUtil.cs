using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
