namespace NSE.Carrinho.API.Model
{
    public class Voucher
    {
        public string Codigo { get; set; }
        public decimal? ValorDesconto { get; set; }
        public decimal? Percentual { get; set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; set; }
    }

    public enum TipoDescontoVoucher
    {
        Porcentagem = 0,
        Valor = 1
    }
}
