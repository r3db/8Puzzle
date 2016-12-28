using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using ICloneable;
using StateSearch;
using System.Globalization;

namespace EightPuzzleR
{
    public sealed class EightPuzzle : ILayout<EightPuzzle>, ICloneable<EightPuzzle>
    {
        #region Internal Static Data

        private const int MoveCost = 1;

        internal static readonly CultureInfo Culture = CultureInfo.GetCultureInfo(string.Empty);
        internal const int Min = 0;
        internal const int Max = 9;

        #endregion

        #region Internal Data

        private readonly int[][] board;
        private EightPuzzleCell blank;

        #endregion

        #region .Ctor

        public EightPuzzle(int[][] map)
        {
            board = map;
            blank = FindBlankCell(board);
        }

        private EightPuzzle(int[][] map, EightPuzzleCell blank)
        {
            board = map;
            CtorContract(blank);
            this.blank = blank;
        }

        private static void CtorContract(EightPuzzleCell blank)
        {
            blank.Line.BiggerOrEqualThanDebug(0);
            blank.Line.LessThanDebug(9);

            blank.Column.BiggerOrEqualThanDebug(0);
            blank.Column.LessThanDebug(9);
        }

        private static EightPuzzleCell FindBlankCell(int[][] map)
        {
            for (int i = 0; i < map.Length; ++i)
            {
                map[i].Length.NotEquals(3);

                for (int k = 0; k < map[i].Length; ++k)
                {
                    int current = map[i][k];
                    if (current != 0) continue;
                    return new EightPuzzleCell(i, k);
                }
            }
            throw new ArgumentException("No blank cell");
        }

        #endregion

        #region Base Methods

        public override bool Equals(object obj)
        {
            return obj != null && GetHashCode().CompareTo(obj.GetHashCode()) == 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < board.Length; ++i)
            {
                for (int k = 0; k < board[i].Length; ++k)
                {
                    sb.Append(board[i][k]);
                }
            }
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            //return ToString().GetHashCode();

            int result = 0;
            int count = 1;

            for (int i = 0; i < board.Length; ++i)
            {
                for (int k = 0; k < board[i].Length; ++k)
                {
                    result += count*board[i][k];
                    count *= 10;
                }
            }

            return result;

        }

        #endregion

        #region Methods

        private static bool IsInRange(int line, int column)
        {
            return (line >= Min && line < 3) && (column >= 0 && column < 3);
        }

        private static IList<EightPuzzleCell> Neighbors(int line, int column)
        {
            if (IsInRange(line, column) == false)
            {
                return new List<EightPuzzleCell>(0);
            }

            IList<EightPuzzleCell> moves = new List<EightPuzzleCell>(4);

            for (int i = line - 1; i <= line + 1; ++i)
            {
                if (line == i) continue;
                if (IsInRange(i, column) == false) continue;
                moves.Add(new EightPuzzleCell(i, column));
            }

            for (int i = column - 1; i <= column + 1; ++i)
            {
                if (column == i) continue;
                if (IsInRange(line, i) == false) continue;
                moves.Add(new EightPuzzleCell(line, i));
            }

            return moves;
        }

        private static EightPuzzle PerformMove(EightPuzzle map, EightPuzzleCell piece)
        {
            int value = map.board[piece.Line][piece.Column];
            map.board[map.blank.Line][map.blank.Column] = value;
            map.board[piece.Line][piece.Column] = 0;

            map.blank = new EightPuzzleCell(piece.Line, piece.Column);

            return map;
        }

        private static int ManhattanDistance(EightPuzzle p1, EightPuzzle p2)
        {
            //MethodContract(p1, p2);

            // =S

            int md = 0;
            for (int i = 0; i < p1.board.Length; ++i)
            {
                for (int k = 0; k < p1.board[i].Length; ++k)
                {
                    int value1 = p1.board[i][k];
                    if (value1 == 0) continue;
                    //-->Start Brutalidade do ciclo interior!!!
                    for (int x = 0; x < p2.board.Length; ++x)
                    {
                        for (int y = 0; y < p2.board[x].Length; ++y)
                        {
                            int value2 = p2.board[x][y];
                            if (value2 != value1) continue;
                            //PuzzlePiece p2p = new PuzzlePiece(x, y);
                            //Agora que temos ambas peças em nossa posse!
                            //E sabemos que sao ambas as mesma peca! Vamos calcular o custo!
                            md += Math.Abs(i - x) + Math.Abs(k - y);
                        }
                    }
                    //-->End Brutalidade de ciclo interior
                }
            }
            return md;
        }

        private static void MethodContract(EightPuzzle p1, EightPuzzle p2)
        {
            p1.NotNull();
            p2.NotNull();
            
            if (p1.board.Length != p2.board.Length || p1.board[0].Length != p2.board[0].Length)
            {
                throw new ArgumentException("Invalid Arguments");
            }
        }

        #endregion
        
        #region ILayout<EPuzzle> Members

        public IList<EightPuzzle> Children()
        {
            IList<EightPuzzleCell> moves = Neighbors(blank.Line, blank.Column);

            IList<EightPuzzle> layout = new List<EightPuzzle>();
            foreach (EightPuzzleCell item in moves)
            {
                layout.Add(PerformMove(Clone(), item));
            }

            return layout;
        }

        public bool IsGoal(EightPuzzle layout)
        {
            return this.Equals(layout);
        }

        public int GetCost(EightPuzzle destination)
        {
            return MoveCost;
        }

        public int GetHeuristic(EightPuzzle source, EightPuzzle destination)
        {
            return ManhattanDistance(source, destination);
        }

        #endregion

        #region ICloneable<EightPuzzle> Members

        // Done!
        public EightPuzzle Clone()
        {
            return new EightPuzzle(new[]
            {
                new [] { board[0][0], board[0][1], board[0][2] },
                new [] { board[1][0], board[1][1], board[1][2] },
                new [] { board[2][0], board[2][1], board[2][2] },
            },
            blank.Clone());
        }

        #endregion
    }
}