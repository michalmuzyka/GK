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
    }
}
