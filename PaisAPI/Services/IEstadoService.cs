using Domain;

namespace EstadoAPI.Services
{
    public interface IEstadoService
    {
        List<Estado> SelecionaEstados();
        List<Estado> SelecionEstadosPais(int Id);
        Estado SelecionaEstadoId(int Id);

        Estado IncluiEstado(Estado estado);
        Estado AlteraEstado(Estado estado);
        void ExcluiEstado(int Id);
    }
}
