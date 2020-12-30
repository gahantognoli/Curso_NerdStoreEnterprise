using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.API.Application.Queries;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Pedido.API.Controllers
{
    public class VoucherController : MainController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("voucher/{codigo}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ObterPorCodigo(string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return NotFound();

            var voucher = await _voucherQueries.ObterVoucherPorCodigo(codigo);

            return voucher == null ? NotFound() : CustomResponse(voucher);
        }
    }
}
