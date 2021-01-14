using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NSE.Pagamento.API.Data.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Models.Pagamento>
    {
        public void Configure(EntityTypeBuilder<Models.Pagamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Ignore(p => p.CartaoCredito);

            builder.HasMany(p => p.Transacoes)
                .WithOne(p => p.Pagamento)
                .HasForeignKey(p => p.PagamentoId);

            builder.ToTable("Pagamentos");
        }
    }
}
