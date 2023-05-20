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
    public Player? currentPlayer;

    [ObservableProperty()]
    public ObservableCollection<GameNumber> numbers;

    [ObservableProperty()]
    public GameStatus gameStatus;

    [ObservableProperty()]
    public Player? winner;

    public bool IsGameFinished => GameStatus != GameStatus.OnGoing;


    public Game(GameMode mode, int x, int k, int a, int b)
    {
        GameMode = mode;
        X = x;
        K = k;

        var random = new Random();
        currentPlayer = random.NextEnum<Player>();
        numbers = new ObservableCollection<GameNumber>(
            Enumerable.Range(a, b - a + 1)
            .OrderBy(n => random.Next())
            .Take(x)
            .Order()
            .Select(n => new GameNumber() { Number = n }));
    }

    public void NextPlayer()
    {
        if (GameStatus == GameStatus.OnGoing)
            CurrentPlayer = (Player)(((int)CurrentPlayer + 1) % 2);
        else
            CurrentPlayer = null;
    }

    [RelayCommand]
    public void SelectNumber(GameNumber gameNumber)
    {
        gameNumber.Player = CurrentPlayer;
        (GameStatus, Winner) = BoardValidator.Validate(Numbers, K);

        NextPlayer();
    }

}
