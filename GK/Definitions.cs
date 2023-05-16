using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK;

public static class Consts
{
    public static int DefaultX = 10;
    public static int DefaultK = 3;
}

public enum GameMode
{
    PlayWithAi,
    WatchAi,
}

public enum Player
{
    Player1,
    Player2
}

public class GameNumber
{
    public int Number { get; set; }

    public Player Player { get; set; }
}