using MingYue.Utilities;

namespace MingYue.Models
{
    /// <summary>
    /// Represents a file or directory item with metadata information.
    /// </summary>
    public class FileItemInfo
    {
        /// <summary>
        /// Gets or sets the name of the file or directory.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path of the file or directory.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this item is a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the size of the file in bytes. For directories, this is typically 0.
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the last modified date and time of the file or directory.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Gets or sets the file extension (e.g., ".txt", ".pdf").
        /// </summary>
        public string Extension { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the thumbnail data as a base64 encoded string, if available.
        /// </summary>
        public string? ThumbnailData { get; set; }

        /// <summary>
        /// Gets a human-readable representation of the file size.
        /// </summary>
        public string SizeDisplay => IsDirectory ? "-" : FileUtilities.FormatSize(Size);
    }

    /// <summary>
    /// Represents a user's favorite folder for quick access.
    /// </summary>
    public class FavoriteFolder
    {
        /// <summary>
        /// Gets or sets the unique identifier of the favorite folder.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the display name of the favorite folder.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path of the favorite folder.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the icon name for the favorite folder.
        /// </summary>
        public string Icon { get; set; } = "folder";

        /// <summary>
        /// Gets or sets the display order of the favorite folder.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the favorite folder entry.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref=\"FavoriteFolder\"/> class.
        /// </summary>
        public FavoriteFolder()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Represents a drive or mount point with storage information.
    /// </summary>
    public class DriveItemInfo
    {
        /// <summary>
        /// Gets or sets the display name of the drive.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path or mount point of the drive.
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the icon name for the drive.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total size of the drive in bytes.
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// Gets or sets the available space on the drive in bytes.
        /// </summary>
        public long AvailableSize { get; set; }

        /// <summary>
        /// Gets or sets the type of the drive (e.g., "Fixed", "Removable", "Network").
        /// </summary>
        public string DriveType { get; set; } = string.Empty;

        /// <summary>
        /// Gets a human-readable representation of the total drive size.
        /// </summary>
        public string TotalSizeDisplay => FileUtilities.FormatSize(TotalSize);

        /// <summary>
        /// Gets a human-readable representation of the available drive space.
        /// </summary>
        public string AvailableSizeDisplay => FileUtilities.FormatSize(AvailableSize);

        /// <summary>
        /// Gets a human-readable representation of the used drive space.
        /// </summary>
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
