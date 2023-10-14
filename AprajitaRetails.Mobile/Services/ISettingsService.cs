namespace AprajitaRetails.Mobile.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Assigns a value to a settings key.
        /// </summary>
        /// <typeparam name="T">The type of the object bound to the key.</typeparam>
        /// <param name="key">The key to check.</param>
        /// <param name="value">The value to assign to the setting key.</param>
        void SetValue<T>(string key, T? value);

        /// <summary>
        /// Reads a value from the current <see cref="IServiceProvider"/> instance and returns its casting in the right type.
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        /// <param name="key">The key associated to the requested object.</param>
        T? GetValue<T>(string key);
    }
    //public sealed class SettingsService : ISettingsService
    //{
    //    private readonly IDictionary<string, object> _properties = Application.Current.Properties;// Application.Current.Properties;

    //    public T GetValue<T>(string key)
    //    {
    //        if (_properties.TryGetValue(key, out var value))
    //        {
    //            return (T)value;
    //        }

    //        return default;
    //    }

    //    public void SetValue<T>(string key, T value)
    //    {
    //        if (!_properties.ContainsKey(key))
    //        {
    //            _properties.Add(key, value);
    //        }
    //        else
    //        {
    //            _properties[key] = value;
    //        }
    //    }
    //}
}