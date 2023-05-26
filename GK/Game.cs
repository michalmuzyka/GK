using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GK.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GK;

public partial class Game : ObservableObject
{
    public GameMode GameMode { get; }
    public int X { get; }
    public int K { get; }

    public Strategy FirstPlayerStrategy { get; }
    public Strategy SecondPlayerStrategy { get; }

    [ObservableProperty()]
    public Player? currentPlayer;

    [ObservableProperty()]
    public ObservableCollection<GameNumber> numbers;

    [ObservableProperty()]
    public GameStatus gameStatus;

    [ObservableProperty()]
    public Player? winner;

    public bool IsGameFinished => GameStatus != GameStatus.OnGoing;


    public Game(GameMode mode, int x, int k, int a, int b, Strategy player1, Strategy player2)
    {
        GameMode = mode;
        X = x;
        K = k;

        var random = new Random();
        currentPlayer = random.NextEnum<Player>();
        FirstPlayerStrategy = player1;
        SecondPlayerStrategy = player2;

        numbers = new ObservableCollection<GameNumber>(
            Enumerable.Range(a, b - a + 1)
            .OrderBy(n => random.Next())
            .Take(x)
            .Order()
            .Select(n => new GameNumber() { Number = n }));
    }

    public async Task WatchAiGameAsync()
    {
        while (!IsGameFinished)
        {
            PlayNextAIMove();
            await Task.Delay(1500);
        }
    }

    public void PlayWithAi()
    {
        if (GameMode == GameMode.PlayWithAi && CurrentPlayer == Player.Player2)
        {
            PlayNextAIMove();
        }
    }

    private void NextPlayer()
    {
        if (GameStatus == GameStatus.OnGoing && CurrentPlayer != null)
            CurrentPlayer = (Player)(((int)CurrentPlayer + 1) % 2);
        else
            CurrentPlayer = null;
    }

    private void PlayNextAIMove()
    {
        var number = Strategies.Random(Numbers);
        number.Player = CurrentPlayer;
        (GameStatus, Winner) = BoardValidator.Validate(Numbers, K);
        NextPlayer();
    }

    [RelayCommand]
    public void SelectNumber(GameNumber gameNumber)
    {
        gameNumber.Player = CurrentPlayer;
        (GameStatus, Winner) = BoardValidator.Validate(Numbers, K);

        NextPlayer();
        PlayNextAIMove();
    }
}
