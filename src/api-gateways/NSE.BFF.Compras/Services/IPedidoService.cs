using NSE.BFF.Compras.Models;
using NSE.Core.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.BFF.Compras.Services
{
    public interface IPedidoService
    {
        Task<ResponseResult> FinalizarPedido(PedidoDTO pedido);
        Task<PedidoDTO> ObterUltimoPedido();
        Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId();

        Task<VoucherDTO> ObterVoucherPorCodigo(string codigo);
    }
}
