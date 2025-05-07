namespace OrderProcessing.Infrastructure.Utils
{
    public static class ConfigHelper
    {
        public static string GetAppSettingsPath()
        {
            string currentDir = Directory.GetCurrentDirectory();

            string appSettingsPath = Path.Combine(currentDir, "appsettings.json");
            if (File.Exists(appSettingsPath))
            {
                return appSettingsPath;
            }

            string parentDir = Directory.GetParent(currentDir)?.FullName ?? "";
            appSettingsPath = Path.Combine(parentDir, "appsettings.json");
            if (parentDir != null && File.Exists(appSettingsPath))
            {
                return appSettingsPath;
            }

            return "appsettings.json";
        }
    }
}
