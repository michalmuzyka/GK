using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GK.Logic;

public static class Consts
{
    public static int DefaultX = 10;
    public static int DefaultK = 3;    
    public static int DefaultA = 1;
    public static int DefaultB = DefaultX;

    public static Color FirstPlayerColor = Colors.LightGreen;
    public static Color FirstPlayerWinningColor = Colors.Green;
    public static Color SecondPlayerColor = Colors.LightPink;
    public static Color SecondPlayerWinningColor = Colors.Red;

    public static string FirstPlayerName = "Gracz 1";
    public static string SecondPlayerName = "Gracz 2";
}
