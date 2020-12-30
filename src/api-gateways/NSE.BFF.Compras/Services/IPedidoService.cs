using NSE.BFF.Compras.Models;
using System.Threading.Tasks;

namespace NSE.BFF.Compras.Services
{
    public interface IPedidoService
    {
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }
}
