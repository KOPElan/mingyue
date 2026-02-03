using MingYue.Models;

namespace MingYue.Services
{
    /// <summary>
    /// Provides file indexing services for fast file search across directories.
    /// </summary>
    public interface IFileIndexService
    {
        /// <summary>
        /// Indexes a single file for search.
        /// </summary>
        /// <param name="filePath">The full path to the file to index.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task IndexFileAsync(string filePath);

        /// <summary>
        /// Indexes all files in a directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory to index.</param>
        /// <param name="recursive">If true, recursively indexes all subdirectories.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task IndexDirectoryAsync(string directoryPath, bool recursive = false);

        /// <summary>
        /// Searches for files matching the specified pattern.
        /// </summary>
        /// <param name="searchPattern">The search pattern to match against file names.</param>
        /// <returns>A list of matching file indexes.</returns>
        Task<List<FileIndex>> SearchFilesAsync(string searchPattern);

        /// <summary>
        /// Searches for files within a specific directory.
        /// </summary>
        /// <param name="directoryPath">The directory path to search within, or null to search all indexed files.</param>
        /// <param name="searchPattern">The search pattern to match against file names.</param>
        /// <returns>A list of matching file indexes.</returns>
        Task<List<FileIndex>> SearchFilesInDirectoryAsync(string? directoryPath, string searchPattern);

        /// <summary>
        /// Performs an advanced search with additional filtering options.
        /// </summary>
        /// <param name="startDirectory">The directory to start the search from.</param>
        /// <param name="searchPattern">The search pattern to match against file names.</param>
        /// <param name="includeSubdirectories">If true, searches in subdirectories.</param>
        /// <param name="includeDirectories">If true, includes directories in the results.</param>
        /// <returns>A list of matching file indexes.</returns>
        Task<List<FileIndex>> AdvancedSearchAsync(string startDirectory, string searchPattern, bool includeSubdirectories = true, bool includeDirectories = true);

        /// <summary>
        /// Checks if a directory has been indexed.
        /// </summary>
        /// <param name="directoryPath">The directory path to check.</param>
        /// <returns>True if the directory has been indexed; otherwise, false.</returns>
        Task<bool> HasIndexAsync(string directoryPath);

        /// <summary>
        /// Removes a file from the index.
        /// </summary>
        /// <param name="filePath">The file path to remove from the index.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveFromIndexAsync(string filePath);

        /// <summary>
        /// Clears the entire file index.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ClearIndexAsync();
    }
}
