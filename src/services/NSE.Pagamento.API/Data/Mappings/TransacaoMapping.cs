﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pagamento.API.Models;

namespace NSE.Pagamento.API.Data.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Pagamento)
                .WithMany(p => p.Transacoes);

            builder.ToTable("Transacoes");
        }
    }
}
