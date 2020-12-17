using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Clientes.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resultado = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Gabriel", "gabriel_antognoli@hotmail.com", "14573931074"));

            return CustomResponse(resultado);
        }
    }
}
