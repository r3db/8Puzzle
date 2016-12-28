using System.Collections.Generic;

namespace StateSearch
{
    public interface IStateSearchable<T> where T : ILayout<T>
    {
        void FindPath();
        IList<T> Path { get; }
        int MinimumCost { get; }
        T Start { get; }
        T Goal { get; }
        bool HasSolution { get; }
    }
}
