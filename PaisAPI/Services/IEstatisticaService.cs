using Domain;
using PaisAPI.DTO;

namespace PaisAPI.Services
{
    public interface IEstatisticaService
    {
        EstatisticaDTO BuscaEstatisticas();
    }
}
