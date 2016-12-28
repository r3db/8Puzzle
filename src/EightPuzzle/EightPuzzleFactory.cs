using System;
using System.Collections.Generic;
using Contracts;

namespace EightPuzzleR
{
    // Done!
    public static class EightPuzzleFactory
    {
        #region Factory

        // Done!
        public static EightPuzzle Create(string map)
        {
            map.LengthEquals(9);

            return new EightPuzzle(new []
            {
                new [] {Parse(map, 0), Parse(map, 1), Parse(map, 2)},
                new [] {Parse(map, 3), Parse(map, 4), Parse(map, 5)},
                new [] {Parse(map, 6), Parse(map, 7), Parse(map, 8)},
            });
        }

        // Done!
        public static EightPuzzle Create(IList<int> map)
        {
            map.LengthEquals(9);

            return new EightPuzzle(new[]
            {
                new [] {map[0], map[1], map[2]},
                new [] {map[3], map[4], map[5]},
                new [] {map[6], map[7], map[8]},
            });
        }

        // Done!
        private static int Parse(string map, int index)
        {
            int value = int.Parse(map[index].ToString(EightPuzzle.Culture), EightPuzzle.Culture);

            value.BiggerOrEqualThan(EightPuzzle.Min);
            value.LessThan(EightPuzzle.Max);

            return value;
        }

        #endregion
    }
}