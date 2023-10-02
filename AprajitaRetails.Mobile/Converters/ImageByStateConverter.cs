using System.Globalization;
using System.Reflection;
using System.Xml;

namespace AprajitaRetails.Mobile.Converters;

public class ImageByStateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var target = (FlyoutItem)value;
        var allParams = ((string)parameter).Split((';')); // 0=normal, 1=selected

        if (target.IsChecked && allParams.Length > 1)
            return allParams[1];
        else
            return allParams[0];
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)value;
    }
}

[ContentProperty("Source")]
public class SfImageResourceExtension : IMarkupExtension<ImageSource>, IMarkupExtension
{
    public string? Source { get; set; }

    //
    // Parameters:
    //   serviceProvider:
    //
    // Exceptions:
    //   T:Microsoft.Maui.Controls.Xaml.XamlParseException:
    public ImageSource ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(Source))
        {
            IXmlLineInfoProvider xmlLineInfoProvider = serviceProvider.GetService(typeof(IXmlLineInfoProvider)) as IXmlLineInfoProvider;
            IXmlLineInfo xmlLineInfo2;
            if (xmlLineInfoProvider == null)
            {
                IXmlLineInfo xmlLineInfo = new XmlLineInfo();
                xmlLineInfo2 = xmlLineInfo;
            }
            else
            {
                xmlLineInfo2 = xmlLineInfoProvider.XmlLineInfo;
            }

            IXmlLineInfo xmlInfo = xmlLineInfo2;
            throw new XamlParseException("ImageResourceExtension requires Source property to be set", xmlInfo);
        }

        return ImageSource.FromResource(typeof(SfImageResourceExtension).GetTypeInfo().Assembly.GetName().Name + ".Resources.Images." + Source, typeof(SfImageResourceExtension).GetTypeInfo().Assembly);
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ((IMarkupExtension<ImageSource>)this).ProvideValue(serviceProvider);
    }
}