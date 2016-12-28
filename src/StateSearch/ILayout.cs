
using System.Collections.Generic;

namespace StateSearch
{
    public interface ILayout<T>
    {
        IList<T> Children();
        bool IsGoal(T layout);
        int GetCost(T destination);
        int GetHeuristic(T source, T destination);
    }
}
