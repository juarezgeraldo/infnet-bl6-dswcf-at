using Domain;
using EstadoAPI.Services;
using Microsoft.Data.SqlClient;
using PaisAPI.DTO;

namespace PaisAPI.Services
{
    public class EstadoService : IEstadoService
    {
        public readonly BlobService _blobService;

        public EstadoService(BlobService blobService)
        {
            _blobService = blobService;
        }

        private readonly string StringConexao = "Server=tcp:azure-at.database.windows.net,1433;Initial Catalog=AZURE_AT;Persist Security Info=False;User ID=juarez;Password=Galo2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private readonly string StringConexao = "Data Source=LAPTOP-JUNIOR;Initial Catalog=AZURE_AT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private readonly string tipoContainer = "BandeiraEstado";
        public Estado AlteraEstado(AlteraEstadoDTO alteraEstadoDTO)
        {
            var estado = new Estado();

            estado.BandeiraId = _blobService.CarregaBlob(alteraEstadoDTO.BandeiraIdBase64, tipoContainer);
            estado.Nome = alteraEstadoDTO.Nome;
            estado.Id = alteraEstadoDTO.Id;
            estado.PaisId = alteraEstadoDTO.PaisId;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "AlteraEstado";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", estado.Id);
                sqlComando.Parameters.AddWithValue("@Nome", estado.Nome);
                sqlComando.Parameters.AddWithValue("@BandeiraId", estado.BandeiraId);
                sqlComando.Parameters.AddWithValue("@PaisId", estado.PaisId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return estado;
        }

        public void ExcluiEstado(int Id)
        {
            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "ExcluiEstado";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", Id);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }
            };
        }

        public Estado IncluiEstado(IncluiEstadoDTO incluiEstadoDTO)
        {
            var estado = new Estado();

            estado.BandeiraId = _blobService.CarregaBlob(incluiEstadoDTO.BandeiraIdBase64, tipoContainer);
            estado.Nome = incluiEstadoDTO.Nome;
            estado.PaisId = (int) incluiEstadoDTO.PaisId;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "IncluiEstado";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Nome", estado.Nome);
                sqlComando.Parameters.AddWithValue("@BandeiraId", estado.BandeiraId);
                sqlComando.Parameters.AddWithValue("@PaisId", estado.PaisId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return estado;
        }

        public Estado SelecionaEstadoId(int Id)
        {
            var estado = new Estado();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaEstadoId";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComando.Parameters.AddWithValue("@Id", Id);

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            estado.Id = (int)leitura["Id"];
                            estado.Nome = leitura["Nome"].ToString();
                            estado.BandeiraId = leitura["BandeiraId"].ToString();
                            estado.PaisId = (int)leitura["PaisId"];
                            estado.PaisNome = leitura["PaisNome"].ToString();
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return estado;
        }

        public List<Estado> SelecionaEstados()
        {
            var estados = new List<Estado>();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaEstados";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            var estado = new Estado
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                BandeiraId = leitura["BandeiraId"].ToString(),
                                PaisId = (int)leitura["PaisId"],
                                PaisNome = leitura["PaisNome"].ToString(),
                            };
                            estados.Add(estado);
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return estados;
        }

        public List<Estado> SelecionEstadosPais(int Id)
        {
            var estados = new List<Estado>();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaEstadosPais";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;
                sqlComando.Parameters.AddWithValue("@PaisId", Id);

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            var estado = new Estado
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                BandeiraId = leitura["BandeiraId"].ToString(),
                                PaisId = (int)leitura["PaisId"],
                                PaisNome = leitura["PaisNome"].ToString(),
                            };
                            estados.Add(estado);
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return estados;
        }
    }
}
