using FluentValidation;
using NSE.Core.Messages;
using NSE.Pedido.API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSE.Pedido.API.Application.Commands
{
    public class AdicionarPedidoCommand : Command
    {
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemDTO> PedidoItems { get; set; }

        public string VoucherCodigo { get; set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; set; }

        public EnderecoDTO Endereco { get; set; }

        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
        {
            public AdicionarPedidoValidation()
            {
                RuleFor(p => p.ClienteId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido");

                RuleFor(p => p.PedidoItems.Count())
                    .GreaterThan(0)
                    .WithMessage("O pedido precisa ter no mínimo 1 item");

                RuleFor(p => p.ValorTotal)
                    .GreaterThan(0)
                    .WithMessage("Valor do pedido inválido");

                RuleFor(p => p.NumeroCartao)
                    .CreditCard()
                    .WithMessage("Número de cartão inválido");

                RuleFor(p => p.NomeCartao)
                    .NotNull()
                    .WithMessage("Nome do portador do cartão requerido");

                RuleFor(p => p.CvvCartao.Length)
                    .GreaterThan(2)
                    .LessThan(5)
                    .WithMessage("O CVV do cartão precisa ter 3 ou 4 números");

                RuleFor(p => p.ExpiracaoCartao)
                    .NotNull()
                    .WithMessage("Data expiração do cartão requerida");
            }
        }
    }
}
