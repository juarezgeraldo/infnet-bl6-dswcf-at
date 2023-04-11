namespace PaisMVC.Models.Pais
{
    public class IncluiPais
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string BandeiraId { get; set; } = string.Empty;
        public string BandeiraIdBase64 { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
