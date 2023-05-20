using GK.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK;

public static class BoardValidator
{
    private static IEnumerable<GameNumber> GetPlayerNumbers(this IEnumerable<GameNumber> numbers, Player player) 
        => numbers.Where(number => number.Player == player);

    private static void MarkWinningSequence(this List<GameNumber> numbers, List<int> sequence)
        => sequence.ForEach(id => numbers[id].InWinningSequence = true);


    public static (GameStatus result, Player? winner) Validate(IEnumerable<GameNumber> numbers, int k)
    {
        var firstPlayerTiles = numbers.GetPlayerNumbers(Player.Player1).ToList();
        var secondPlayerTiles = numbers.GetPlayerNumbers(Player.Player2).ToList();

        if (firstPlayerTiles.Count() >= k && FindArithmeticalProgression(firstPlayerTiles, k, out var sequence))
        {
            firstPlayerTiles.MarkWinningSequence(sequence);
            return (GameStatus.Win, Player.Player1);
        }

        if (secondPlayerTiles.Count() >= k && FindArithmeticalProgression(secondPlayerTiles, k, out sequence))
        {
            secondPlayerTiles.MarkWinningSequence(sequence);
            return (GameStatus.Win, Player.Player2);
        }

        if (numbers.FirstOrDefault(gameNumber => gameNumber.Player != null) == null)
            return (GameStatus.Draw, null);

        return (GameStatus.OnGoing, null);
    }

    public static bool FindArithmeticalProgression(List<GameNumber> list, int ap_length, out List<int> sequence)
    {
        var n = list.Count();
        var gapSize = n / (double)(ap_length - 1);
        var dMax = Math.Ceiling(gapSize) * (list[n - 1].Number - list[0].Number) / n;

        var p = n / (double)(ap_length + 1);

        for (var i0 = p; i0 < n; i0 += p)
        {
            var i0R = (int)i0;

            for (var i = i0R; i < n; ++i)
            {
                if (list[i].Number <= list[i0R].Number + dMax)
                {
                    for (var j = i + 1; j < n; ++j)
                    {
                        if (list[j].Number <= list[i].Number + dMax)
                        {
                            var diff = list[j].Number - list[i].Number;
                            var sequenceCount = 2;
                            sequence = new List<int>() { j, i };
                            var z = i;
                            for (var r = i - 1; r >= 0; r--)
                            {
                                if (list[z].Number - list[r].Number == diff)
                                {
                                    z = r;
                                    sequenceCount++;
                                    sequence.Add(r);
                                    if (sequenceCount >= ap_length)
                                        return true;
                                }
                            }
                            z = j;
                            for (var l = j + 1; l < n; l++)
                            {
                                if (list[l].Number - list[z].Number == diff)
                                {
                                    z = l;
                                    sequenceCount++;
                                    sequence.Add(l);
                                    if (sequenceCount >= ap_length)
                                        return true;
                                }
                            }

                            if (sequenceCount >= ap_length) 
                                return true;
                        }
                        else break;
                    }
                }
                else break;
            }

        }
        sequence = new List<int>();
        return false;
    }

}
