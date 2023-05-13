using ChallengeB3.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeB3.Infra.Data.Mappings
{
    public class RegisterMap : IEntityTypeConfiguration<Register>
    {
        public void Configure( EntityTypeBuilder<Register> builder)
        {
            builder.Property(c => c.RegisterId)
                .HasColumnName("RegisterId").
                ValueGeneratedOnAdd();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Status)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.Date)
                .HasColumnType("DateTime");

        }
    }
}
