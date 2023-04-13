namespace AmigoAPI.DTO
{
    public class AlteraAmigoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Nascimento { get; set; }
        public string FotografiaIdBase64 { get; set; }
        public int EstadoId { get; set; }
        public int PaisId { get; set; }
    }
}
