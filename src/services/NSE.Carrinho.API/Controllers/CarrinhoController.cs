using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.API.Data;
using NSE.Carrinho.API.Model;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Usuario;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CarrinhoContext _carrinhoContext;

        public CarrinhoController(IAspNetUser user, 
            CarrinhoContext carrinhoContext)
        {
            _user = user;
            _carrinhoContext = carrinhoContext;
        }

        [HttpGet("carrinho")]
        public async Task<CarrinhoCliente> ObterCarrinho()
        {
            return await ObterCarrinhoCliente() ?? new CarrinhoCliente();
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();
            
            if(carrinho == null)
                ManipularNovoCarrinho(item);
            else
                ManipularCarrinhoExistente(carrinho, item);

            if (!OperacaoValida()) return CustomResponse();

            await PersistirDados();

            return CustomResponse();
        }

        [HttpPut("carrinho/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoCliente();
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho, item);
            if (itemCarrinho == null) return CustomResponse();

            carrinho.AtualizarUnidades(itemCarrinho, item.Quantidade);

            ValidarCarrinho(carrinho);

            if (!OperacaoValida()) return CustomResponse();

            _carrinhoContext.CarrinhoItens.Update(itemCarrinho);
            _carrinhoContext.CarrinhoCliente.Update(carrinho);

            await PersistirDados();

            return CustomResponse();
        }

        [HttpDelete("carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var carrinho = await ObterCarrinhoCliente();
            
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho);
            
            if (itemCarrinho == null) return CustomResponse();

            ValidarCarrinho(carrinho);

            if (!OperacaoValida()) return CustomResponse();

            carrinho.RemoverItem(itemCarrinho);

            _carrinhoContext.CarrinhoItens.Remove(itemCarrinho);
            _carrinhoContext.CarrinhoCliente.Update(carrinho);

            await PersistirDados();

            return CustomResponse();
        }

        [HttpPost]
        [Route("carrinho/aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(Voucher voucher)
        {
            var carrinho = await ObterCarrinhoCliente();

            carrinho.AplicarVoucher(voucher);

            _carrinhoContext.CarrinhoCliente.Update(carrinho);

            await PersistirDados();

            return CustomResponse();
        }

        private async Task<CarrinhoCliente> ObterCarrinhoCliente()
        {
            return await _carrinhoContext.CarrinhoCliente
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(p => p.ClienteId == _user.ObterUserId());
        }

        private void ManipularNovoCarrinho(CarrinhoItem item)
        {
            var carrinho = new CarrinhoCliente(_user.ObterUserId());
            carrinho.AdicionarItem(item);

            ValidarCarrinho(carrinho);
            _carrinhoContext.CarrinhoCliente.Add(carrinho);
        }

        private void ManipularCarrinhoExistente(CarrinhoCliente carrinho, CarrinhoItem item)
        {
            var produtoItemExistente = carrinho.CarrinhoItemExistente(item);

            carrinho.AdicionarItem(item);
            ValidarCarrinho(carrinho);

            if (produtoItemExistente)
                _carrinhoContext.CarrinhoItens.Update(carrinho.ObterPorProdutoId(item.ProdutoId));
            else
                _carrinhoContext.CarrinhoItens.Add(item);
            
            _carrinhoContext.CarrinhoCliente.Update(carrinho);
        }

        private async Task<CarrinhoItem> ObterItemCarrinhoValidado(Guid produtoId, CarrinhoCliente carrinho, CarrinhoItem item = null)
        {
            if(item != null && produtoId != item.ProdutoId)
            {
                AdicionarErroProcessamento("O item não corresponde ao informado");
                return null;
            }

            if(carrinho == null)
            {
                AdicionarErroProcessamento("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await _carrinhoContext.CarrinhoItens
                .FirstOrDefaultAsync(p => p.CarrinhoId == carrinho.Id && p.ProdutoId == produtoId);

            if(itemCarrinho == null || !carrinho.CarrinhoItemExistente(itemCarrinho))
            {
                AdicionarErroProcessamento("O item não está no carrinho");
                return null;
            }

            return itemCarrinho;
        }

        private async Task PersistirDados()
        {
            var result = await _carrinhoContext.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persitir os dados no banco");
        }

        private bool ValidarCarrinho(CarrinhoCliente carrinho)
        {
            if (carrinho.EhValido()) return true;

            carrinho.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
    }
}
