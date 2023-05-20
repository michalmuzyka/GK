using GK.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GK;

public class WinningTileToColorConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var convertedStatus = (bool)(values[0]);
        var convertedPlayer = (Player?)values[1];

        return new SolidColorBrush(convertedStatus switch
        {
            true => convertedPlayer == Player.Player1 ? Consts.FirstPlayerWinningColor : Consts.SecondPlayerWinningColor,
            _ => Colors.Gray,
        }) ;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class WinningTileToBorderSizeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var converted = (bool)value;

        return converted switch
        {
            true => 5,
            _ => 1,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
