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

    public IDictionary<Player, Strategy> PlayerStrategies = new Dictionary<Player, Strategy>();

    [ObservableProperty()]
    public Player? currentPlayer;

    [ObservableProperty()]
    public Strategy? currentStrategy;

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
        PlayerStrategies[Player.Player1] = player1;
        PlayerStrategies[Player.Player2] = player2;

        currentStrategy = mode switch
        {
            GameMode.PlayWithAi => PlayerStrategies[Player.Player2],
            GameMode.WatchAi => PlayerStrategies[currentPlayer!.Value],
            _ => null
        };

        var nums = new List<GameNumber>();
        for (int i = 0; i < x; i++)
        {
            nums.Add(new GameNumber() { Number = random.Next(a, b + 1)});
        }

        numbers = new(nums.OrderBy(n => n.Number));
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
        {
            CurrentPlayer = (Player)(((int)CurrentPlayer + 1) % 2);
            if (GameMode == GameMode.WatchAi)
            {
                CurrentStrategy = PlayerStrategies[CurrentPlayer!.Value];
            }
        }
        else
        {
            CurrentPlayer = null;
            CurrentStrategy = null;
        }
    }

    private void PlayNextAIMove()
    {
        var number = PlayerStrategies[CurrentPlayer!.Value] switch
        {
            Strategy.Random => Strategies.Random(Numbers),
            Strategy.TwoStep => Strategies.TwoStep(Numbers, CurrentPlayer.Value),
            Strategy.Blocking => Strategies.Blocking(Numbers, CurrentPlayer.Value),
            _ => throw new ArgumentOutOfRangeException(nameof(Strategy)),
        };

        number.Player = CurrentPlayer;
        (GameStatus, Winner) = BoardValidator.Validate(Numbers, K);
        NextPlayer();
    }

    [RelayCommand]
    public void SelectNumber(GameNumber gameNumber)
    {
        gameNumber.Player = CurrentPlayer;
        (GameStatus, Winner) = BoardValidator.Validate(Numbers, K);

        if (GameStatus == GameStatus.OnGoing)
        {
            NextPlayer();
            PlayNextAIMove();
        }
    }
}
