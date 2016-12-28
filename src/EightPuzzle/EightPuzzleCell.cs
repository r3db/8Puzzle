using System;
using System.Diagnostics;
using Contracts;
using ICloneable;

namespace EightPuzzleR
{
    // Done!
    internal sealed class EightPuzzleCell : ICloneable<EightPuzzleCell>
    {
        #region Internal Static Data

        private const string Debug = "DEBUG";
        
        #endregion

        #region Internal Data

        public int Line    { get; private set; }
        public int Column  { get; private set; }

        #endregion

        #region .Ctor

        public EightPuzzleCell(int line, int column)
        {
            //CtorContract(line, column);

            this.Line = line;
            this.Column = column;
        }

        [Conditional(Debug)]
        private static void CtorContract(int line, int column)
        {
            line.BiggerOrEqualThanDebug(EightPuzzle.Min);
            line.LessThanDebug(EightPuzzle.Max);

            column.BiggerOrEqualThanDebug(EightPuzzle.Min);
            column.LessThanDebug(EightPuzzle.Max);
        }

        #endregion

        #region ICloneable<EightPuzzleCell> Members

        public EightPuzzleCell Clone()
        {
            return (EightPuzzleCell)MemberwiseClone();
        }

        #endregion
    }
}