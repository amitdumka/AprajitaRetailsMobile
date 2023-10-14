//using System.Diagnostics;
//using System.Text;
//using Android.App;
//using Android.Bluetooth;
//using eStore_MauiLib.Services;
//using Java.Util;

//[assembly: Dependency(typeof(PrintService))]
//[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
//[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
//[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
//[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]
//[assembly: UsesFeature("android.hardware.location", Required = false)]
//[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
//[assembly: UsesFeature("android.hardware.location.network", Required = false)]
//namespace eStore_MauiLib.Services
//{

//    public class PrintService : IPrintService
//    {
//        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

//        public IList<string> GetDeviceList()
//        {

//            var btdevice = bluetoothAdapter?.BondedDevices
//            .Select(i => i.Name).ToList();
//            return btdevice;

//        }

//        public async Task Print(string deviceName, string text)
//        {

//            BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
//                                      where bd?.Name == deviceName
//                                      select bd).FirstOrDefault();
//            try
//            {
//                BluetoothSocket bluetoothSocket = device?.
//                    CreateRfcommSocketToServiceRecord(
//                    UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));

//                bluetoothSocket?.Connect();
//                byte[] buffer = Encoding.UTF8.GetBytes(text);
//                await bluetoothSocket?.OutputStream.WriteAsync(buffer, 0, buffer.Length);
//                bluetoothSocket.Close();

//            }
//            catch (Exception exp)
//            {
//                Debug.WriteLine(exp.Message);

//                throw exp;
//            }
//        }
//        public Task PrintFile(string device, string path)
//        {
//            throw new NotImplementedException();
//        }
//    }

//}



//using System;
//namespace eStore_MauiLib.Helpers
//{

//    public class FileHelper
//    {
//        public async Task<FileResult> PickAndShow(PickOptions options)
//        {
//            try
//            {
//                var result = await FilePicker.Default.PickAsync(options);
//                if (result != null)
//                {
//                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
//                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
//                    {
//                        using var stream = await result.OpenReadAsync();
//                        var image = ImageSource.FromStream(() => stream);
//                    }
//                }

//                return result;
//            }
//            catch (Exception ex)
//            {
//                // The user canceled or something went wrong
//            }

//            return null;
//        }

//        public FilePickerFileType customFileType = new FilePickerFileType(
//                  new Dictionary<DevicePlatform, IEnumerable<string>>
//                  {
//                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
//                    { DevicePlatform.Android, new[] { "application/comics" } },
//                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } },
//                    { DevicePlatform.Tizen, new[] { "*/*" } },
//                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
//                  });

//        public PickOptions options = new()
//        {
//            PickerTitle = "Please select a comic file",
//            FileTypes = new FilePickerFileType(
//                 new Dictionary<DevicePlatform, IEnumerable<string>>
//                 {
//                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
//                    { DevicePlatform.Android, new[] { "application/comics" } },
//                    { DevicePlatform.WinUI, new[] { ".cbr", ".cbz" } },
//                    { DevicePlatform.Tizen, new[] { "*/*" } },
//                    { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
//                 }),
//        };

//        public async Task<string> ReadTextFile(string filePath)
//        {
//            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(filePath);
//            using StreamReader reader = new StreamReader(fileStream);

//            return await reader.ReadToEndAsync();
//        }
//        public async Task ConvertFileToUpperCase(string sourceFile, string targetFileName)
//        {
//            // Read the source file
//            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(sourceFile);
//            using StreamReader reader = new StreamReader(fileStream);

//            string content = await reader.ReadToEndAsync();

//            // Transform file content to upper case text
//            content = content.ToUpperInvariant();

//            // Write the file content to the app data directory
//            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, targetFileName);

//            using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
//            using StreamWriter streamWriter = new StreamWriter(outputStream);

//            await streamWriter.WriteAsync(content);
//        }
//    }
//    public class DeviceData
//    {
//        public string ReadDeviceInfo()
//        {
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();

//            sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
//            sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
//            sb.AppendLine($"Name: {DeviceInfo.Name}");
//            sb.AppendLine($"OS Version: {DeviceInfo.VersionString}");
//            sb.AppendLine($"Refresh Rate: {DeviceInfo.Current}");
//            sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
//            sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

//            bool isVirtual = DeviceInfo.Current.DeviceType switch
//            {
//                DeviceType.Physical => false,
//                DeviceType.Virtual => true,
//                _ => false
//            };

//            sb.AppendLine($"Virtual device? {isVirtual}");

//            return sb.ToString();
//        }

//        public bool IsAndroid() => DeviceInfo.Current.Platform == DevicePlatform.Android;

//        public string PrintIdiom()
//        {
//            if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
//                return ("The current device is a desktop");
//            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
//                return ("The current device is a phone");
//            else if (DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
//                return ("The current device is a Tablet");
//            return "Not Supported";
//        }

//        public bool isVirtual = DeviceInfo.Current.DeviceType switch
//        {
//            DeviceType.Physical => false,
//            DeviceType.Virtual => true,
//            _ => false
//        };

//        public string ThemeInfoLabel_Text = AppInfo.Current.RequestedTheme switch
//        {
//            AppTheme.Dark => "Dark theme",
//            AppTheme.Light => "Light theme",
//            _ => "Unknown"
//        };
//    }
//}

//using System;
//namespace eStore_MauiLib.Services
//{

//    public interface IPrintService
//    {
//        IList<string> GetDeviceList();
//        Task Print(string devicename, string text);
//        Task PrintFile(string device, string path);
//    }


//}
//public class PrintPageViewModel
//{
//    private readonly IPrintService _blueToothService;

//    private IList<string> _deviceList;
//    public IList<string> DeviceList
//    {
//        get
//        {
//            if (_deviceList == null)
//                _deviceList = new ObservableCollection<string>();
//            return _deviceList;
//        }
//        set
//        {
//            _deviceList = value;
//        }
//    }

//    private string _printMessage;
//    public string PrintMessage
//    {
//        get
//        {
//            return _printMessage;
//        }
//        set
//        {
//            _printMessage = value;
//        }
//    }

//    private string _selectedDevice;
//    public string SelectedDevice
//    {
//        get
//        {
//            return _selectedDevice;
//        }
//        set
//        {
//            _selectedDevice = value;
//        }
//    }

//    public ICommand PrintCommand => new Command(async () =>
//    {
//        PrintMessage += " Xamarin Forms is awesome!";
//        await _blueToothService.Print(SelectedDevice, PrintMessage);
//    });

//    public PrintPageViewModel()
//    {
//        _blueToothService = DependencyService.Get<IPrintService>();
//        if (_blueToothService == null)
//        {
//            _blueToothService = ServiceHelper.GetService<IPrintService>();
//        }


//        var list = _blueToothService.GetDeviceList();
//        DeviceList.Clear();
//        foreach (var item in list)
//        {
//            DeviceList.Add(item);
//        }
//    }

//}

//public static class ServiceHelper
//{

//    public static T GetService<T>() => Current.GetService<T>();

//    public static IServiceProvider Current =>
//#if WINDOWS10_0_17763_0_OR_GREATER
//    MauiWinUIApplication.Current.Services;
//#elif ANDROID
//        MauiApplication.Current.Services;
//#elif IOS || MACCATALYST
//    MauiUIApplicationDelegate.Current.Services;
//#else
//    null;
//#endif
//}

//BindingContext = new PrintPageViewModel();


//var services = builder.Services;
//#if ANDROID
//            builder.Services.AddTransient<AndroidTestPage>();
//            builder.Services.AddTransient<PrintPageViewModel>();
//            services.AddTransient<IPrintService, eStore_MauiLib.Services.PrintService>();
//            services.AddSingleton<IPrintService, eStore_MauiLib.Services.PrintService>();
//#endif


// < !--Phone-- >
//            < TabBar x: Name = "PhoneTabs" >
//                < Tab Title = "Home" Icon = "tab_home.png" >
//                    < ShellContent ContentTemplate = "{DataTemplate page:DashboardPage}" />
//                </ Tab >
//                < Tab Title = "Favorites" Icon = "tab_favorites.png" >
//                    < ShellContent ContentTemplate = "{DataTemplate page:VoucherPage}" />
//                </ Tab >
//                < Tab Title = "Test" Icon = "tab_map.png" >
//                    < ShellContent ContentTemplate = "{DataTemplate page:AndroidTestPage}" />
//                </ Tab >
//                < Tab Title = "Login" Icon = "tab_settings.png" >
//                    < ShellContent ContentTemplate = "{DataTemplate page:LoginPage}" />
//                </ Tab >
//                < Tab Title = "Settings" Icon = "tab_settings.png" >
//                    < ShellContent ContentTemplate = "{DataTemplate page:MainPage}" />
//                </ Tab >
//            </ TabBar >



//            < uses - permission android: name = "android.permission.ACCESS_NETWORK_STATE" />

//    < uses - permission android: name = "android.permission.INTERNET" />

//    < uses - permission android: name = "android.permission.VIBRATE" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_SCAN" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_ADVERTISE" />

//    < uses - permission android: name = "android.permission.ACCESS_FINE_LOCATION" />

//    < uses - permission android: name = "android.permission.BLUETOOTH" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_ADMIN" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_PRIVILEGED" />

//    < uses - permission android: name = "android.permission.READ_EXTERNAL_STORAGE" />

//    < uses - permission android: name = "android.permission.ACCESS_COARSE_LOCATION" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_CONNECT" />

//    < uses - permission android: name = "android.permission.ACCESS_FINE_LOCATION" />

//    < uses - permission android: name = "android.permission.BLUETOOTH_CONNECT" />
//    < uses - permission android: name = "android.permission.BLUETOOTH_SCAN" />
//    < uses - permission android: name = "android.permission.ACCESS_COARSE_LOCATION" />
//    < uses - permission android: name = "android.permission.ACCESS_FINE_LOCATION" />
//    < uses - permission android: name = "android.permission.ACCESS_LOCATION_EXTRA_COMMANDS" />
//    < uses - permission android: name = "android.permission.BLUETOOTH_PRIVILEGED"
//        tools: ignore = "ProtectedPermissions" />

//    < uses - feature android: name = "android.hardware.location" android: required = "false" />

//    < uses - feature android: name = "android.hardware.location.gps" android: required = "false" />

//    < uses - feature android: name = "android.hardware.location.network" android: required = "false" />

//    < uses - permission android: name = "android.permission.ACCESS_BACKGROUND_LOCATION" />


//    < uses - feature android: name = "android.hardware.bluetooth" android: required = "true" />

//    < uses - feature android: name = "android.hardware.bluetooth_le" android: required = "true" />

//    [assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
//[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
//[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
//[assembly: UsesPermission(Android.Manifest.Permission.BluetoothConnect)]
//[assembly: UsesFeature("android.hardware.location", Required = false)]
//[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
//[assembly: UsesFeature("android.hardware.location.network", Required = false)]