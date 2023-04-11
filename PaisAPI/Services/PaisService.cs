using Domain;
using Microsoft.Data.SqlClient;
using PaisAPI.DTO;

namespace PaisAPI.Services
{
    public class PaisService : IPaisService
    {
        public readonly BlobService _blobService;

        public PaisService(BlobService blobService)
        {
            _blobService = blobService;
        }

        private readonly string StringConexao = "Data Source=LAPTOP-JUNIOR;Initial Catalog=AZURE_AT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private readonly string tipoContainer = "BandeiraPais";
        public Pais AlteraPais(AlteraPaisDTO alteraPaisDTO)
        {
            var pais = new Pais();

            pais.BandeiraId = _blobService.CarregaBlob(alteraPaisDTO.BandeiraIdBase64, tipoContainer);
            pais.Nome = alteraPaisDTO.Nome;
            pais.Id = alteraPaisDTO.Id;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "AlteraPais";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", pais.Id);
                sqlComando.Parameters.AddWithValue("@Nome", pais.Nome);
                sqlComando.Parameters.AddWithValue("@BandeiraId", pais.BandeiraId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return pais;
        }

        public void ExcluiPais(int Id)
        {
            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "ExcluiPais";
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

        public Pais IncluiPais(IncluiPaisDTO incluiPaisDTO)
        {
            var pais = new Pais();

            pais.BandeiraId = _blobService.CarregaBlob(incluiPaisDTO.BandeiraIdBase64, tipoContainer);
            pais.Nome = incluiPaisDTO.Nome;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "IncluiPais";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Nome", pais.Nome);
                sqlComando.Parameters.AddWithValue("@BandeiraId", pais.BandeiraId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return pais;
        }

        public List<Pais> SelecionaPaises()
        {
            var paises = new List<Pais>();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaPaises";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            var pais = new Pais
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                BandeiraId = leitura["BandeiraId"].ToString(),
                            };
                            paises.Add(pais);
                        }
                    }
                }
                finally { connection.Close(); }
            }

            return paises;
        }

        public Pais SelecionaPaisId(int Id)
        {
            var pais = new Pais();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaPaisId";
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
                            pais.Id = (int)leitura["Id"];
                            pais.Nome = leitura["Nome"].ToString();
                            pais.BandeiraId = leitura["BandeiraId"].ToString();
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return pais;
        }

    }
}
