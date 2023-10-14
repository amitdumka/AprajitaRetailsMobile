namespace AprajitaRetails.Mobile.Helpers
{
    public class DeviceData
    {
        public string ReadDeviceInfo()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
            sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
            sb.AppendLine($"Name: {DeviceInfo.Name}");
            sb.AppendLine($"OS Version: {DeviceInfo.VersionString}");
            sb.AppendLine($"Refresh Rate: {DeviceInfo.Current}");
            sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
            sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

            bool isVirtual = DeviceInfo.Current.DeviceType switch
            {
                DeviceType.Physical => false,
                DeviceType.Virtual => true,
                _ => false
            };

            sb.AppendLine($"Virtual device? {isVirtual}");

            return sb.ToString();
        }

        public bool IsAndroid() => DeviceInfo.Current.Platform == DevicePlatform.Android;

        public string PrintIdiom()
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
                return ("The current device is a desktop");
            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
                return ("The current device is a phone");
            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
                return ("The current device is a Tablet");
            return "Not Supported";
        }

        public bool isVirtual = DeviceInfo.Current.DeviceType switch
        {
            DeviceType.Physical => false,
            DeviceType.Virtual => true,
            _ => false
        };

        public string ThemeInfoLabel_Text = AppInfo.Current.RequestedTheme switch
        {
            AppTheme.Dark => "Dark theme",
            AppTheme.Light => "Light theme",
            _ => "Unknown"
        };
    }


    public static class TaskHelpers
    {
        public static void FireAndForget(this Task task, bool configureAwait = false)
        {
            task.ConfigureAwait(configureAwait);
        }
    }

}