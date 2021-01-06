using NSE.Core.DomainObjects.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NSE.Pedido.Domain.Pedidos
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        DbConnection ObterConexao();

        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObterItemPedido(Guid pedidoId, Guid produtoId);
    }
}
