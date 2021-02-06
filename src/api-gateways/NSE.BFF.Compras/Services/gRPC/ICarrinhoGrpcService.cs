using NSE.BFF.Compras.Models;
using System.Threading.Tasks;

namespace NSE.BFF.Compras.Services.gRPC
{
    public interface ICarrinhoGrpcService
    {
        Task<CarrinhoDTO> ObterCarrinho();
    }
}