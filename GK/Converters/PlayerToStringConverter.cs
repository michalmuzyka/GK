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

public class PlayerToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var converted = value as Player?;

        return converted switch
        {
            Player.Player1 => Consts.FirstPlayerName,
            Player.Player2 => Consts.SecondPlayerName,
            _ => string.Empty,
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}