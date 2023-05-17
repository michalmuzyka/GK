using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GK.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK;

public partial class Game : ObservableObject
{ 
    public GameMode GameMode { get; }
    public int X { get; }
    public int K { get; }

    [ObservableProperty()]
    public Player currentPlayer;

    [ObservableProperty()]
    public ObservableCollection<GameNumber> numbers;

    public Game(GameMode mode, int x, int k) 
    {
        GameMode = mode;
        X = x;
        K = k;

        var random = new Random();
        currentPlayer = random.NextEnum<Player>();
        numbers = new ObservableCollection<GameNumber>(Enumerable.Range(0, x).Select(n => new GameNumber() { Number = n }));
    }

    public void NextPlayer()
    {
        CurrentPlayer = (Player)(((int)CurrentPlayer + 1) % Enum.GetValues(typeof(Player)).Length);
    }

    [RelayCommand]
    public void SelectNumber(GameNumber gameNumber)
    {
        gameNumber.Player = CurrentPlayer;
        NextPlayer();
    }


}
