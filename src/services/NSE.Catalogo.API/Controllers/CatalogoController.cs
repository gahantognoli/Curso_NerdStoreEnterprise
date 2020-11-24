using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo")]
    [ApiController]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository) => _produtoRepository = produtoRepository;

        [HttpGet("produtos")]
        public async Task<IEnumerable<Produto>> Index() => await _produtoRepository.ObterTodos();

        [HttpGet("produtos/{id:guid}")]
        public async Task<Produto> ProdutoDetalhe(Guid id) => await _produtoRepository.ObterPorId(id);
    }
}
