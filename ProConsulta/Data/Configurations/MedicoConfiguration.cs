using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProConsulta.Models;

namespace ProConsulta.Data.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("Medicos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Documento)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(11)");

            builder.Property(x => x.Crm)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(8)");

            builder.Property(x => x.Celular)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(11)");

            builder.HasIndex(x => x.Nome)
                .IsUnique(true);

            builder.HasOne(m => m.Especialidade)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.EspecialidadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.Agendamentos)
                .WithOne(m => m.Medico)
                .HasForeignKey(a => a.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
