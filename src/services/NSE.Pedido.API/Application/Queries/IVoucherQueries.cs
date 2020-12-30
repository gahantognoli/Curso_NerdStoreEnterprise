using NSE.Pedido.API.Application.DTO;
using System.Threading.Tasks;

namespace NSE.Pedido.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }
}
