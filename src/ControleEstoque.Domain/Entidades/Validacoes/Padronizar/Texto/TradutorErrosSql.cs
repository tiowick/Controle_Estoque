using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Entidades.Validacoes.Padronizar.Texto
{
    [DebuggerStepThrough]
    public static class TradutorErrosSql
    {
        public static string Traduzir(this string erroMensagem, bool erroOut = true)
        {
            if (!erroOut)
                return erroMensagem;

            var mensagem = PreprocessarErro(erroMensagem);
            if (string.IsNullOrEmpty(mensagem))
                return "";

            return TraduzirErro(mensagem);
        }

        private static string PreprocessarErro(string erroMensagem)
        {
            return erroMensagem
                ?.ToLower(culture: CultureInfo.CurrentCulture)
                .Replace('\r', ' ')
                .Replace('\n', ' ')
                .Replace('\t', ' ')
                .Replace("the statement has been terminated.", "")
                ?? string.Empty;
        }

        private static string TraduzirErro(string erroMensagem)
        {
            switch (erroMensagem)
            {
                case var msg when msg.Contains("timeout waiting for response to originate"):
                    return "Tempo limite para atender a chamada expirou";

                case var msg when msg.Contains("one or more errors occurred. (erro na quantidade de parametros passados para o procedimento)"):
                    return "Um ou mais erros ocorreram. (erro na quantidade de parametros passados para o procedimento)";

                case var msg when msg.Contains("has too many arguments specified."):
                    return "Erro na quantidade de parametros passados para o procedimento";

                case var msg when msg.Contains("originate failed"):
                    return "Originação da chamada falhou";

                case var msg when msg.Contains("timeout waiting for protocol identifier"):
                    return "Tempo limite de conexão com o discador";

                case var msg when msg.Contains("index (zero based) must be greater than or equal to zero and less than the size of the argument list."):
                    return "Índice baseado em 0 (Zero), precisa ser maior ou igual a zero e precisar o mesmo número de argumentos da lista";

                case var msg when msg.Contains("the insert statement conflicted with the check constraint"):
                    return "Ocorreu um conflito no momento da inclusão/alteração de registro. Conflito de identidade check";

                case var msg when msg.Contains("field is required."):
                    return msg.Replace("the ", "").Trim().Replace("field is required.", ": Este campo é obrigatório").Trim();

                case var msg when msg.Contains("procedure or function"):
                    return "Erro de parâmetros no procedimento";

                case var msg when msg.Contains("the insert statement conflicted with the foreign key constraint"):
                    return "Erro de chave estrangeira, não existe referência de código nas tabelas para insert/update";

                case var msg when msg.Contains("could not find stored procedure"):
                    return $"Não foi possível localizar o procedimento: {msg.Replace("could not find stored procedure", "").Trim()}";

                case var msg when msg.Contains("a transport-level error has occurred when receiving results from the server."):
                    return "Erro de conexão, verifique se a Internet está conectada corretamente ou não foi possível estabelecer conexão com servidor de dados";

                case var msg when msg.Contains("invalid column name"):
                    return $"Erro versão DB => Coluna: {msg.Replace("invalid column name", "")}";

                case var msg when msg.Contains("index and length must refer to a location within the string. (parameter 'length')"):
                    return "Erro versão Layout => O tamanho ou índice precisa estar dentro do tamanho total do texto";

                case var msg when msg.Contains("the conversion of the varchar value"):
                    return "Erro de conversão de dados: Campo Texto para Numérico";

                case var msg when msg.Contains("passwords must be at least 6 characters"):
                    return "Senha precisa ter no mínimo 6 caracteres";

                case var msg when msg.Contains("cannot insert duplicate key row in object"):
                    var pos = msg.IndexOf("the duplicate key value is");
                    return msg.Substring(pos).Replace("the duplicate key value is", "Não é permitido inserir registro duplicado");

                case var msg when msg.Contains("unexpected character encountered while parsing value:"):
                    return "Erro na conversão dos dados. Layout alterado! Avise ao administrador";

                case var msg when msg.Contains("there are fewer columns in the insert statement than values specified in the values clause."):
                    return "O número de colunas para inclusão na tabela é diferente! Avise ao administrador";

                case var msg when msg.Contains("conflicted with the foreign key same table"):
                    return "Existe um conflito de tabelas, não foi possível concluir a operação";

                case var msg when msg.Contains("the delete statement conflicted with the reference constraint") || msg.Contains("the delete statement conflicted with the same table reference"):
                    return "Não é possível excluir o registro, pois está sendo usado em outros cadastros.";

                case var msg when msg.Contains("invalid object name '"):
                    return msg.Replace("invalid object name", "Não é possível localizar:");

                case var msg when msg.Contains("the duplicate key value is"):
                    var duplicatePos = msg.ToLower().IndexOf("the duplicate key value is");
                    var duplicateMsg = msg.Substring(duplicatePos).Replace("the duplicate key value is", "");
                    return $"Não é permitido inserir valores duplicados na tabela: Valor Duplicado: {duplicateMsg}";

                case var msg when msg.Contains("cannot insert the value null into column "):
                    var nullPos = msg.ToLower().IndexOf(", table");
                    var nullMsg = "Campo: " + msg.Substring(0, nullPos - 1).Replace("cannot insert the value null into column ", "");
                    return $"Não é permitido inserir valores vazios na tabela: {nullMsg}";

                case var msg when msg.Contains("não foi possível conectar ao discador para criação da campanha"):
                    return "Não foi possível conectar ao discador para criação da campanha";

                case var msg when msg.Contains("make sure that the name is entered correctly."):
                    return "Confira se o nome informado para o procedimento, base de dados ou function está escrito de forma correta";

                default:
                    return erroMensagem ?? "Erro desconhecido, avise ao administrador do sistema";
            }
        }
    }

}
