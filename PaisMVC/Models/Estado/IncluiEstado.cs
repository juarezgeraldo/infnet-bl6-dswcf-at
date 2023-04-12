namespace PaisMVC.Models.Estado
{
    public class IncluiEstado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string BandeiraIdBase64 { get; set; }
        public IFormFile FormFile { get; set; }
        public string BandeiraId { get; set; }
        public int PaisId { get; set; }
    }
}
