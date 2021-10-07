using Forum.Challenger.Domain.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Challenger.Infra.Mapping
{
    public class TopicsMapping : IEntityTypeConfiguration<Topics>
    {
        public void Configure(EntityTypeBuilder<Topics> builder)
        {
            builder.ToTable("TB_TOPICS", "dbo");

            builder.HasKey(x => x.CdTopic);

            builder.Property(x => x.CdTopic).HasColumnName("CD_TOPIC").HasColumnType("INT");
            builder.Property(x => x.DtTopic).HasColumnName("DT_TOPIC").HasColumnType("DATETIME");
            builder.Property(x => x.DtTopicLastEdition).HasColumnName("DT_TOPIC_LAST_EDITION").HasColumnType("DATETIME");
            builder.Property(x => x.DsTitle).HasColumnName("DS_TITLE").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.DsText).HasColumnName("DS_TEXT").HasColumnType("VARCHAR(50)");
            builder.Property(x => x.CdPerson).HasColumnName("CD_PERSON").HasColumnType("INT");
            builder.Property(x => x.FlActive).HasColumnName("FL_ACTIVE").HasColumnType("BIT");
        }
    }
}
