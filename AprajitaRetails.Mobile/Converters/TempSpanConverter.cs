using System.Globalization;

namespace AprajitaRetails.Mobil.Converters;

public class TempSpanConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // two values
        var minTemp = double.Parse(values[0].ToString()) * 3;
        var maxTemp = double.Parse(values[1].ToString()) * 3;

        var diff = maxTemp - minTemp;

        return diff;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

public class StringToDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return DateTime.Now;
        }
        else
        {
            DateTime dateTime;
            DateTime.TryParse((string)value, out dateTime);
            return dateTime;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}

public class StringToTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return DateTime.Now.TimeOfDay;
        }
        else
        {
            TimeSpan dateTime;
            TimeSpan.TryParse((string)value,out dateTime);
            return dateTime;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.ToString();
    }
}