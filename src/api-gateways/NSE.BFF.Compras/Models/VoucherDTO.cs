﻿namespace NSE.BFF.Compras.Models
{
    public class VoucherDTO
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int TipoDescontoVoucher { get; set; }
    }
}
