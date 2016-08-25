using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DriverLog.Converters
{
    /// <summary>
    /// Used to invert a Bindable property's of type <code>bool</code>
    /// 
    /// Syntax example:
    /// <code><Button Clicked="Play" IsVisible="{Binding IsPlaying, Converter={StaticResource: Inverter}}"/></code>
    ///
    /// Note you need to add Inverter as a resource in the page
    /// <code>
    ///     <ContentPage.Resources>
    ///         <ResourceDictionary>
    ///             <converters:InverseBoolConverter x:Key="Inverter" />
    ///         </ResourceDictionary>
    ///     </ContentPage.Resources>
    /// </code>
    /// 
    /// And add an XML namespace declaration for <code>converters</code> in your top level ContentPage attributes:
    /// <code>xmlns:converters="clr-namespace:DriverLog.Converters;assembly=DriverLog"</code>
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
