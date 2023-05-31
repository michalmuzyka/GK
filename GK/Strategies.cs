using GK.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK
{
    public static class Strategies
    {
        private static IDictionary<Player, int> Distance = new Dictionary<Player, int>();
        private static IDictionary<Player, int> LastNumber = new Dictionary<Player, int>();
        private static IDictionary<Player, int> SecondLastNumber = new Dictionary<Player, int>();

        public static GameNumber Random(IEnumerable<GameNumber> numbers)
        {
            var availableNumbers = numbers.Where(n => n.Player == null).ToList();

            if (availableNumbers.Count == 0)
            {
                throw new InvalidOperationException("No available numbers.");
            }

            Random random = new Random((int)DateTime.Now.Ticks);
            int randomIndex = random.Next(0, availableNumbers.Count);
            return availableNumbers[randomIndex];
        }

        public static GameNumber TwoStep(IEnumerable<GameNumber> numbers, Player currentPlayer)
        {
            var availableNumbers = numbers.Where(n => n.Player == null).ToList();
            var currentPlayerNumbers = numbers.Where(n => n.Player == currentPlayer).ToList();

            if (!LastNumber.TryGetValue(currentPlayer, out _)
                && !SecondLastNumber.TryGetValue(currentPlayer, out _))
            {
                var gameNumber = availableNumbers[GetRandomGameNumber(availableNumbers)];
                LastNumber[currentPlayer] = gameNumber.Number;

                return gameNumber;
            }
            else if (!SecondLastNumber.TryGetValue(currentPlayer, out _))
            {
                var randomIndex = GetRandomGameNumber(availableNumbers);
                var gameNumber = availableNumbers[randomIndex];

                Distance[currentPlayer] = Math.Abs(
                    currentPlayerNumbers.First().Number - gameNumber.Number);

                SecondLastNumber[currentPlayer] = LastNumber[currentPlayer];
                LastNumber[currentPlayer] = gameNumber.Number;

                return gameNumber;
            }
            else
            {
                var distance = Distance[currentPlayer];

                var x = SecondLastNumber[currentPlayer];
                var y = LastNumber[currentPlayer];
                var d = Distance[currentPlayer];

                var nextNumber = availableNumbers
                    .Where(n => Math.Abs(x - n.Number) == d
                        || Math.Abs(y - n.Number) == d)
                    .FirstOrDefault();

                if (nextNumber == null)
                {
                    var randomIndex = GetRandomGameNumber(availableNumbers);
                    var gameNumber = availableNumbers[randomIndex];

                    SecondLastNumber[currentPlayer] = LastNumber[currentPlayer];
                    Distance[currentPlayer] = Math.Abs(LastNumber[currentPlayer] - gameNumber.Number);
                    LastNumber[currentPlayer] = gameNumber.Number;

                    return availableNumbers[randomIndex];
                }
                else
                {
                    if (Math.Abs(nextNumber.Number - SecondLastNumber[currentPlayer]) == Distance[currentPlayer])
                    {
                        LastNumber[currentPlayer] = nextNumber.Number;
                    }
                    else
                    {
                        SecondLastNumber[currentPlayer] = nextNumber.Number;
                    }

                    return nextNumber;
                }
            }
        }

        public static GameNumber Blocking(IEnumerable<GameNumber> numbers, Player currentPlayer)
        {
            var availableNumbers = numbers.Where(n => n.Player == null).ToList();
            var opponentNumbers = numbers.Where(n => n.Player != currentPlayer && n.Player != null).ToList();
            var opponentLongestSequence = new List<int>();

            for ( var i = 0; i < opponentNumbers.Count; i++)
            {
                if (BoardValidator.FindArithmeticalProgression(opponentNumbers, i, out var sequence))
                {
                    opponentLongestSequence = sequence;
                }
            }

            if (opponentLongestSequence.Count() <= 1)
            {
                return availableNumbers[GetRandomGameNumber(availableNumbers)];
            }
            else
            {
                var diff = Math.Abs(opponentLongestSequence[0] - opponentLongestSequence[1]);

                foreach(var o in opponentLongestSequence)
                {
                    foreach (var a in availableNumbers)
                    {
                        if (Math.Abs(o - a.Number) == diff)
                        {
                            return a;
                        }
                    }
                }

                return availableNumbers[GetRandomGameNumber(availableNumbers)];
            }
        }

        private static int GetRandomGameNumber(List<GameNumber> availableNumbers)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            return random.Next(0, availableNumbers.Count);
        }
    }
}
