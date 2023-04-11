using Domain;
using PaisAPI.DTO;

namespace EstadoAPI.Services
{
    public interface IEstadoService
    {
        List<Estado> SelecionaEstados();
        List<Estado> SelecionEstadosPais(int Id);
        Estado SelecionaEstadoId(int Id);

        Estado IncluiEstado(IncluiEstadoDTO incluiEstadoDTO);
        Estado AlteraEstado(AlteraEstadoDTO alteraEstadoDTO);
        void ExcluiEstado(int Id);
    }
}
