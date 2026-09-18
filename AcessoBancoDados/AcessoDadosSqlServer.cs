using Microsoft.Data.SqlClient;
using System.Data;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //Cria a conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetros que vão para banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void Limparparametros()
        {
            sqlParameterCollection.Clear();
        }

        public void Adicionarparametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar, Excluir
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Cria a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir a conexão
                sqlConnection.Open();
                //Criar um comando que vai levar a informação ao banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Executar o comando, ou seja, mandar o comando ir ate o banco de dados.
                return sqlCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //Cria a conexão
                SqlConnection sqlConnection = CriarConexao();
                //Abrir a conexão
                sqlConnection.Open();
                //Criar um comando que vai levar a informação ao banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //Colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Em Segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criar um adpatador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //DataTable = Tabela de dados vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();
                //Mandar o comando ir ate o banco buscar os dados e o adaptador preencher o datable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
