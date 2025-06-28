using IntentManagementAPI.Models.Core;
using IntentManagementAPI.Models.Supporting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IntentManagementAPI.Data
{
    public class IntentManagementContext : DbContext
    {
        public IntentManagementContext(DbContextOptions<IntentManagementContext> options) : base(options)
        {
        }

        public DbSet<Intent> Intents { get; set; }
        public DbSet<ProbeIntent> ProbeIntents { get; set; }
        public DbSet<Expression> Expressions { get; set; }
        public DbSet<IntentReport> IntentReports { get; set; }
        public DbSet<IntentSpecification> IntentSpecifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Intent entity
            modelBuilder.Entity<Intent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.OwnsOne(e => e.ValidFor);
                entity.OwnsOne(e => e.Context);
                entity.HasOne(e => e.IntentSpecification).WithMany().HasForeignKey("IntentSpecificationId");
                entity.HasMany(e => e.IntentRelationship).WithOne(ir => ir.Intent).HasForeignKey(ir => ir.IntentId);
                entity.OwnsMany(e => e.RelatedParty);
                entity.OwnsMany(e => e.Characteristic, c => {
                    c.Property(p => p.Value).HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<object?>(v, (JsonSerializerOptions?)null));
                });
                entity.OwnsMany(e => e.Attachment, a => {
                    a.OwnsOne(e => e.ValidFor);
                });
                entity.HasOne(e => e.Expression).WithMany().HasForeignKey("ExpressionId").IsRequired(false);

                // TPH for Intent and ProbeIntent
                entity.HasDiscriminator<string>("IntentType")
                    .HasValue<Intent>("Intent")
                    .HasValue<ProbeIntent>("ProbeIntent");
            });

            // Configure Expression entity (TPH)
            modelBuilder.Entity<Expression>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasDiscriminator<string>("ExpressionType")
                    .HasValue<JsonLdExpression>("JsonLdExpression")
                    .HasValue<TurtleExpression>("TurtleExpression");
            });

            modelBuilder.Entity<IntentReport>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Intent);
                entity.OwnsOne(e => e.ValidFor);
            });
            modelBuilder.Entity<IntentSpecification>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
} 