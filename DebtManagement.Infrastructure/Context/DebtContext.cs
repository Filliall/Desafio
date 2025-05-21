using DebtManagement.Domain.Entities;
using DebtManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace DebtManagement.Infrastructure.Context;
public class DebtContext : DbContext, IUnitOfWork
{
    public DbSet<Debt> Debts { get; set; }
    public DbSet<DebtInstallment> Installments { get; set; }

    public DebtContext(DbContextOptions<DebtContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Debt>(entity =>
        {
            entity.ToTable(t => t.HasComment("Tabela de dívidas")); // Novo formato
            entity.HasKey(d => d.Id).HasName("PK_Debts");

            entity.Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasComment("Identificador único da dívida");

            // Configuração usando novo padrão de chain methods
            entity.Property(d => d.DebtNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("Número único do título");

            // Configuração de relacionamento atualizada
            entity.HasMany(d => d.Installments)
                .WithOne()
                .HasForeignKey("DebtId")
                .HasPrincipalKey(d => d.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DebtInstallment>(entity =>
        {
            entity.HasKey(di => new { di.DebtId, di.InstallmentNumber });

            entity.HasOne<Debt>()
                .WithMany(d => d.Installments)
                .HasForeignKey(di => di.DebtId)
                .OnDelete(DeleteBehavior.Cascade);
        });


    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }
}