namespace Domain
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string BandeiraId { get; set; }
        public IEnumerable<Estado> Estados { get; set; }
    }
}
