using System;
using System.Collections.Generic;

namespace StateSearch
{
    internal class State<T> where T : ILayout<T>
    {
        #region Internal Data

        public T Layout         { get; private set; }
        public State<T> Parent  { get; private set; }
        public int Cost         { get; private set; }   // Custo Real

        #endregion

        #region .Ctor

        // Normal and most used ctor
        public State(T layout, State<T> parent)
        {
            Parent = parent;
            Layout = layout;
            Cost = parent == null ? 0 : parent.Cost + parent.Layout.GetCost(layout);
        }

        // State without a parent
        public State(T layout) : this(layout, null)
        { }

        // Don't remember!
        public State(State<T> state)
        {
            Parent = state.Parent;
            Layout = state.Layout;
        }

        #endregion

        #region Methods

        public bool IsGoal(State<T> value)
        {
            return Layout.IsGoal(value.Layout);
        }

        public IList<T> Children(State<T> node)
        {
            return Layout.Children();
        }

        public static int Estimate(State<T> node, T c, State<T> goal)
        {
            int h = c.GetHeuristic(c, goal.Layout);
            int g = node.Layout.GetCost(c);

            return g + h;
        }

        #endregion

        #region Base Methods

        public override string ToString()
        {
            return String.Format("({0}, {1})", Layout, Cost);
        }

        #endregion

    }
}
