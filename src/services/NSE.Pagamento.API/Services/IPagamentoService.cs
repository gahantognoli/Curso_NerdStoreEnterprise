using NSE.Core.Messages.Integration;
using System.Threading.Tasks;

namespace NSE.Pagamento.API.Services
{
    public interface IPagamentoService
    {
        Task<ResponseMessage> AutorizarPagamento(Models.Pagamento pagamento);
    }
}
