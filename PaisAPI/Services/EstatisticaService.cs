using Domain;
using Microsoft.Data.SqlClient;
using PaisAPI.DTO;

namespace PaisAPI.Services
{
    public class EstatisticaService : IEstatisticaService
    {
        private readonly string StringConexao = "Server=tcp:azure-at.database.windows.net,1433;Initial Catalog=AZURE_AT;Persist Security Info=False;User ID=juarez;Password=Galo2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public EstatisticaService()
        {
        }

        public EstatisticaDTO BuscaEstatisticas()
        {
            EstatisticaDTO estatistica = new EstatisticaDTO();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "QtdPaises";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            estatistica.QtdPaises = (int)leitura["QtdPaises"];
                        }
                    }
                }
                finally { connection.Close(); }
            }

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "QtdEstados";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            estatistica.QtdEstados = (int)leitura["QtdEstados"];
                        }
                    }
                }
                finally { connection.Close(); }
            }

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "QtdAmigos";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            estatistica.QtdAmigos = (int)leitura["QtdAmigos"];
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return estatistica;
        }
    }
}
