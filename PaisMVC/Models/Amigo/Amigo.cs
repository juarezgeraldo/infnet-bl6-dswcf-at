namespace PaisMVC.Models.Amigo
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Nascimento { get; set; }
        public string FotografiaId { get; set; }
        public int EstadoId { get; set; }
        public int PaisId { get; set; }
    }
}
