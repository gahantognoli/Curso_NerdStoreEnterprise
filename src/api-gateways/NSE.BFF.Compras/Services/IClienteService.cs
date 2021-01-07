using NSE.BFF.Compras.Models;
using System.Threading.Tasks;

namespace NSE.BFF.Compras.Services
{
    public interface IClienteService
    {
        Task<EnderecoDTO> ObterEndereco();
    }
}
