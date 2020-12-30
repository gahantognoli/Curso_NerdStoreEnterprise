using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Vouchers;
using System.Threading.Tasks;

namespace NSE.Pedido.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> ObterVoucherPorCodigo(string codigo)
        {
            var voucher = await _voucherRepository.ObterVoucherPorCodigo(codigo);

            if (voucher == null) return null;

            if (!voucher.EstaValidoParaUtilizacao()) return null;

            return new VoucherDTO
            {
                Codigo = voucher.Codigo,
                TipoDescontoVoucher = (int)voucher.TipoDescontoVoucher,
                Percentual = voucher.Percentual,
                ValorDesconto = voucher.ValorDesconto
            };
        }
    }
}
