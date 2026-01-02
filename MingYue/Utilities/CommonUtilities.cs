using System.Runtime.InteropServices;

namespace MingYue.Utilities
{
    public static class CommonUtilities
    {
        public static bool IsOSPlatformLinux(ILogger logger)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return true;
            }

            logger.LogWarning("Unsupported OS platform,Only support Linux paltform.");
            return false;
        }
    }
}
