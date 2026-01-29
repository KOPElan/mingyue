namespace MingYue.Utilities
{
    /// <summary>
    /// Helper class for resolving application paths from configuration and environment variables
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the cache directory path from environment variable, configuration, or default.
        /// Priority: MINGYUE_CACHE_DIR env var > Configuration > Default path
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        /// <returns>Resolved cache directory path</returns>
        public static string GetCacheDirectory(IConfiguration configuration)
        {
            // Get cache directory from environment variable first, then configuration, then default
            var cacheDir = Environment.GetEnvironmentVariable("MINGYUE_CACHE_DIR");
            if (string.IsNullOrWhiteSpace(cacheDir))
            {
                cacheDir = configuration["Storage:CacheDirectory"];
            }
            
            // If still not set, use a default path
            if (string.IsNullOrWhiteSpace(cacheDir))
            {
                // Default to /srv/mingyue/cache on Linux, or .mingyue-cache in user profile on Windows
                if (OperatingSystem.IsLinux())
                {
                    cacheDir = "/srv/mingyue/cache";
                }
                else
                {
                    var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    cacheDir = Path.Combine(homeDir, ".mingyue-cache");
                }
            }
            
            return cacheDir;
        }
        
        /// <summary>
        /// Ensures a directory exists, creating it if necessary
        /// </summary>
        /// <param name="path">Directory path to ensure exists</param>
        /// <exception cref="IOException">Thrown if directory creation fails</exception>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
