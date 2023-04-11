using Domain;
using PaisAPI.DTO;

namespace PaisAPI.Services
{
    public interface IPaisService
    {
        List<Pais> SelecionaPaises();
        Pais SelecionaPaisId(int Id);
        Pais IncluiPais(IncluiPaisDTO incluiPaisDTO);
        Pais AlteraPais(AlteraPaisDTO alteraPaisDTO);
        void ExcluiPais(int Id);
    }
}
