using AmigoAPI.DTO;
using Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AmigoAPI.Services
{
    public class AmigoService : IAmigoService
    {
        public readonly BlobService _blobService;

        public AmigoService(BlobService blobService)
        {
            _blobService = blobService;
        }

        private readonly string StringConexao = "Server=tcp:azure-at.database.windows.net,1433;Initial Catalog=AZURE_AT;Persist Security Info=False;User ID=juarez;Password=Galo2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private readonly string StringConexao = "Data Source=LAPTOP-JUNIOR;Initial Catalog=AZURE_AT;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private readonly string tipoContainer = "FotografiaAmigo";

        public List<Amigo> SelecionaAmigos()
        {
            var amigos = new List<Amigo>();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaAmigos";
                var sqlComando = new SqlCommand(procedure, connection);

                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var leitura = sqlComando.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (leitura.Read())
                        {
                            var amigo = new Amigo
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                Sobrenome = leitura["Sobrenome"].ToString(),
                                Email = leitura["Email"].ToString(),
                                Telefone = leitura["Telefone"].ToString(),
                                Nascimento = Convert.ToDateTime(leitura["Nascimento"]),
                                FotografiaId = leitura["FotografiaId"].ToString(),
                                EstadoId = (int)leitura["EstadoId"],
                                PaisId = (int)leitura["PaisId"],
                            };
                            amigos.Add(amigo);
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return amigos;
        }

        public Amigo SelecionaAmigoId(int Id)
        {
            var amigo = new Amigo();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaAmigoId";
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
                            amigo = new Amigo
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                Sobrenome = leitura["Sobrenome"].ToString(),
                                Email = leitura["Email"].ToString(),
                                Telefone = leitura["Telefone"].ToString(),
                                Nascimento = Convert.ToDateTime(leitura["Nascimento"]),
                                FotografiaId = leitura["FotografiaId"].ToString(),
                                EstadoId = (int)leitura["EstadoId"],
                                PaisId = (int)leitura["PaisId"],
                            };
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return amigo;
        }

        public Amigo IncluiAmigo(IncluiAmigoDTO incluiAmigoDTO)
        {
            var amigo = new Amigo();

            amigo.Nome = incluiAmigoDTO.Nome;
            amigo.Sobrenome = incluiAmigoDTO.Sobrenome;
            amigo.Email = incluiAmigoDTO.Email;
            amigo.Telefone = incluiAmigoDTO.Telefone;
            amigo.Nascimento = incluiAmigoDTO.Nascimento;
            amigo.FotografiaId = _blobService.CarregaBlob(incluiAmigoDTO.FotografiaIdBase64, tipoContainer);
            amigo.EstadoId = incluiAmigoDTO.EstadoId;
            amigo.PaisId = incluiAmigoDTO.PaisId;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "IncluiAmigo";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Nome", amigo.Nome);
                sqlComando.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                sqlComando.Parameters.AddWithValue("@Email", amigo.Email);
                sqlComando.Parameters.AddWithValue("@Telefone", amigo.Telefone);
                sqlComando.Parameters.AddWithValue("@Nascimento", amigo.Nascimento);
                sqlComando.Parameters.AddWithValue("@FotografiaId", amigo.FotografiaId);
                sqlComando.Parameters.AddWithValue("@EstadoId", amigo.EstadoId);
                sqlComando.Parameters.AddWithValue("@PaisId", amigo.PaisId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return amigo;
        }

        public Amigo AlteraAmigo(AlteraAmigoDTO alteraAmigoDTO)
        {
            var amigo = new Amigo();

            amigo.Id = alteraAmigoDTO.Id;
            amigo.Nome = alteraAmigoDTO.Nome;
            amigo.Sobrenome = alteraAmigoDTO.Sobrenome;
            amigo.Email = alteraAmigoDTO.Email;
            amigo.Telefone = alteraAmigoDTO.Telefone;
            amigo.Nascimento = alteraAmigoDTO.Nascimento;
            amigo.FotografiaId = _blobService.CarregaBlob(alteraAmigoDTO.FotografiaIdBase64, tipoContainer);
            amigo.EstadoId = alteraAmigoDTO.EstadoId;
            amigo.PaisId = alteraAmigoDTO.PaisId;

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "AlteraAmigo";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", amigo.Id);
                sqlComando.Parameters.AddWithValue("@Nome", amigo.Nome);
                sqlComando.Parameters.AddWithValue("@Sobrenome", amigo.Sobrenome);
                sqlComando.Parameters.AddWithValue("@Email", amigo.Email);
                sqlComando.Parameters.AddWithValue("@Telefone", amigo.Telefone);
                sqlComando.Parameters.AddWithValue("@Nascimento", amigo.Nascimento);
                sqlComando.Parameters.AddWithValue("@FotografiaId", amigo.FotografiaId);
                sqlComando.Parameters.AddWithValue("@EstadoId", amigo.EstadoId);
                sqlComando.Parameters.AddWithValue("@PaisId", amigo.PaisId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }

            };
            return amigo;
        }

        public void ExcluiAmigo(int Id)
        {
            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "ExcluiAmigo";
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
        public List<Amigo> SelecionaAmigosAmigo(int Id)
        {
            var amigos = new List<Amigo>();

            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "SelecionaAmigosAmigo";
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
                            var amigo = new Amigo
                            {
                                Id = (int)leitura["Id"],
                                Nome = leitura["Nome"].ToString(),
                                Sobrenome = leitura["Sobrenome"].ToString(),
                                Email = leitura["Email"].ToString(),
                                Telefone = leitura["Telefone"].ToString(),
                                Nascimento = Convert.ToDateTime(leitura["Nascimento"]),
                                FotografiaId = leitura["FotografiaId"].ToString(),
                                EstadoId = (int)leitura["EstadoId"],
                                PaisId = (int)leitura["PaisId"],
                            };
                            amigos.Add(amigo);
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return amigos;
        }

        public void IncluiAmigoList(int Id, int amigoId)
        {
            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "IncluiAmigoList";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", Id);
                sqlComando.Parameters.AddWithValue("@AmigoId", amigoId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }
            }
        }

        public void ExcluiAmigoList(int id, int amigoId)
        {
            using (var connection = new SqlConnection(StringConexao))
            {
                var procedure = "ExcluiAmigoList";
                var sqlComando = new SqlCommand(procedure, connection);
                sqlComando.CommandType = System.Data.CommandType.StoredProcedure;

                sqlComando.Parameters.AddWithValue("@Id", id);
                sqlComando.Parameters.AddWithValue("@AmigoId", amigoId);

                try
                {
                    connection.Open();
                    sqlComando.ExecuteNonQuery();
                }
                finally { connection.Close(); }
            };
        }

    }
}
