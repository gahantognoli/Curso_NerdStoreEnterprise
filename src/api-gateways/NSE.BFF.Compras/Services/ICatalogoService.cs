using NSE.BFF.Compras.Models;
using System;
using System.Threading.Tasks;

namespace NSE.BFF.Compras.Services
{
    public interface ICatalogoService
    {
        Task<ItemProdutoDTO> ObterPorId(Guid id);
    }
}
