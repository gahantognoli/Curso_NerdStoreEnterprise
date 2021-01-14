using NSE.Core.DomainObjects;
using System;

namespace NSE.Pagamento.API.Models
{
    public class Transacao : Entity
    {
        public string CodigoAutorizacao { get; set; }
        public string BandeiraCartao { get; set; }
        public DateTime? DataTransacao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal CustoTransacao { get; set; }
        public StatusTransacao StatusTransacao { get; set; }
        public string TID { get; set; } // Id da Transacao
        public string NSU { get; set; } // Meio (paypal)

        public Guid PagamentoId { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}