using Forum.Challenger.Domain.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Challenger.Infra.Mapping
{
    public class PersonsMapping : IEntityTypeConfiguration<Persons>
    {
        public void Configure(EntityTypeBuilder<Persons> builder)
        {
            builder.ToTable("TB_PERSONS", "dbo");

            builder.HasKey(x => x.CdPerson);

            builder.Property(x => x.CdPerson).HasColumnName("CD_PERSON").HasColumnType("INT");
            builder.Property(x => x.DtPerson).HasColumnName("DT_PERSON").HasColumnType("DATETIME");
            builder.Property(x => x.NmPerson).HasColumnName("NM_PERSON").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.DsEmail).HasColumnName("DS_EMAIL").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.DsPassword).HasColumnName("DS_PASSWORD").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.FlActive).HasColumnName("FL_ACTIVE").HasColumnType("BIT");
        }
    }
}
