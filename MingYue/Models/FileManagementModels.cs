using MingYue.Utilities;

namespace MingYue.Models
{
    public class FileItemInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
        public string Extension { get; set; } = string.Empty;
        public string? ThumbnailData { get; set; }

        public string SizeDisplay => IsDirectory ? "-" : FileUtilities.FormatSize(Size);
    }

    public class FavoriteFolder
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = "folder";
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }

        public FavoriteFolder()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

    public class DriveItemInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public long TotalSize { get; set; }
        public long AvailableSize { get; set; }
        public string DriveType { get; set; } = string.Empty;

        public string TotalSizeDisplay => FileUtilities.FormatSize(TotalSize);
        public string AvailableSizeDisplay => FileUtilities.FormatSize(AvailableSize);
        public string UsedSizeDisplay => FileUtilities.FormatSize(TotalSize - AvailableSize);
    }

    public class ShortcutItemInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // documents, downloads, gallery, media, backup
    }

    public class FileIndex
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string FileType { get; set; } = string.Empty;
        public DateTime IndexedAt { get; set; }

        public FileIndex()
        {
            IndexedAt = DateTime.UtcNow;
        }
    }

    public class Thumbnail
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public byte[] ThumbnailData { get; set; } = Array.Empty<byte>();
        public DateTime CreatedAt { get; set; }

        public Thumbnail()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

}
