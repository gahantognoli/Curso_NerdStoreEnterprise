using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NSE.Pedido.Infra.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Domain.Pedidos.Pedido>
    {
        public void Configure(EntityTypeBuilder<Domain.Pedidos.Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Endereco, e =>
            {
                e.Property(pe => pe.Logradouro)
                    .HasColumnName("Logradouro");

                e.Property(pe => pe.Numero)
                    .HasColumnName("Numero");

                e.Property(pe => pe.Complemento)
                    .HasColumnName("Complemento");

                e.Property(pe => pe.Bairro)
                    .HasColumnName("Bairro");

                e.Property(pe => pe.Cep)
                    .HasColumnName("Cep");

                e.Property(pe => pe.Cidade)
                    .HasColumnName("Cidade");

                e.Property(pe => pe.Estado)
                    .HasColumnName("Estado");
            });

            builder.Property(p => p.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasMany(p => p.PedidoItems)
                .WithOne(p => p.Pedido)
                .HasForeignKey(p => p.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}
