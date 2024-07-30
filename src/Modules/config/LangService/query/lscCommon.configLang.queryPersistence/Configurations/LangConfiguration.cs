using lscCommon.configLang.queryDomain.Entities;
using lscCommon.configLang.queryPersistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lscCommon.configLang.queryPersistence.Configurations
{
	/// <summary>
	/// EF core configuration for lang entity
	/// </summary>
	public class LangConfiguration : IEntityTypeConfiguration<Lang>
	{
		/// <summary>
		/// Configures the 'Lang' entity.
		/// </summary>
		/// <param name="builder">The builder used to configure the entity type.</param>
		public void Configure(EntityTypeBuilder<Lang> builder)
		{
			// Sets the table name for the 'Lang' entity in the database
			builder.ToTable(TableNames.LangsTable);

			// Configures the primary key for the 'Lang' entity
			builder.HasKey(x => x.Id);

			// Configure the 'Id' property to map to the 'idx_key' column in the database
			builder.Property(x => x.Id).HasColumnName("idx_key");

			// Configure the 'Description' property to map to the 'description' column in the database
			builder.Property(x => x.Description).HasColumnName("description");

			// Configure the 'Vn' property to map to the 'vn' column in the database
			builder.Property(x => x.Vn).HasColumnName("vn");

			// Configure the 'En' property to map to the 'en' column in the database
			builder.Property(x => x.En).HasColumnName("en");

		}
	}
}
