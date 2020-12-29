using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        protected bool ResponsePossuiErros(ResponseResult response)
        {
            if(response != null && response.Errors.Mensagens.Any())
            {
                foreach (var mensagem in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }

        protected void AdicionarErroValidacao(string mensagem) => ModelState.AddModelError(string.Empty, mensagem);

        protected bool OperacaoValida() => ModelState.ErrorCount == 0;
    }
}
