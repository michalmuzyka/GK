using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK;

public class Game : ObservableObject
{
    public GameMode GameMode { get; }
    public int X { get; }
    public int K { get; }


    public Game(GameMode mode, int x, int k) 
    {
        GameMode = mode;
        X = x;
        K = k;
    }






}
