using System.Data.Entity.ModelConfiguration;
using SmartStore.Core.Domain.Media;

namespace SmartStore.Data.Mapping.Media
{
    public partial class MediaFileMap : EntityTypeConfiguration<MediaFile>
    {
        public MediaFileMap()
        {
            ToTable("MediaFile");
            HasKey(x => x.Id);

#pragma warning disable 612, 618
            Property(p => p.PictureBinary).IsMaxLength();
#pragma warning restore 612, 618

            Property(x => x.Name).HasMaxLength(300).HasColumnName("Name");
            Property(x => x.Extension).HasMaxLength(50);
            Property(x => x.MimeType).IsRequired().HasMaxLength(100);
            Property(x => x.MediaType).IsRequired().HasMaxLength(20);
            Property(x => x.Alt).HasMaxLength(400);
            Property(x => x.Title).HasMaxLength(400);
            Property(x => x.Metadata).IsMaxLength();

            HasIndex(x => x.Name).HasName("IX_Name");
            HasIndex(x => x.Alt).HasName("IX_Alt");
            HasIndex(x => x.Extension).HasName("IX_Extension");
            HasIndex(x => x.MimeType).HasName("IX_MimeType");
            HasIndex(x => x.MediaType).HasName("IX_MediaType");
            HasIndex(x => x.Size).HasName("IX_Size");
            HasIndex(x => x.PixelSize).HasName("IX_PixelSize");
            HasIndex(x => x.IsNew).HasName("IX_IsNew");
            HasIndex(x => x.Deleted).HasName("IX_Deleted");

            HasOptional(x => x.Folder)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.FolderId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.MediaStorage)
				.WithMany()
				.HasForeignKey(x => x.MediaStorageId)
				.WillCascadeOnDelete(false);

            HasMany(x => x.Tags)
                .WithMany(t => t.MediaFiles)
                .Map(m => m.ToTable("MediaFile_Tag_Mapping"));

            HasMany(x => x.Tracks)
                .WithRequired(x => x.MediaFile)
                .HasForeignKey(x => x.MediaFileId)
                .WillCascadeOnDelete(true);
        }
    }
}