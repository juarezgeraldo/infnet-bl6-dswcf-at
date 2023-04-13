using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
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
        public IEnumerable<Amigo> AmigoList { get; set; }
    }
}
