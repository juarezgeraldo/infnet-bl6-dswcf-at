using AmigoAPI.DTO;
using Domain;

namespace AmigoAPI.Services
{
    public interface IAmigoService
    {
        List<Amigo> SelecionaAmigos();
        Amigo SelecionaAmigoId(int Id);

        void IncluiAmigoList (int Id, int amigoId);
        void ExcluiAmigoList (int id, int amigoId);
        List<Amigo> SelecionaAmigosAmigo(int Id);

        Amigo IncluiAmigo(IncluiAmigoDTO incluiAmigoDTO);
        Amigo AlteraAmigo(AlteraAmigoDTO alteraAmigoDTO);
        void ExcluiAmigo(int Id);
    }
}
