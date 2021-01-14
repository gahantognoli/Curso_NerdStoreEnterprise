using NSE.Core.DomainObjects.Data;

namespace NSE.Pagamento.API.Models
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void AdicionarPagamento(Pagamento pagamento);
    }
}
