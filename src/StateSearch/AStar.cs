
using System;
using System.Collections.Generic;

namespace StateSearch
{
    //Done!
    public sealed class AStar<T> : IStateSearchable<T> where T : ILayout<T>
    {
        #region Internal Data

        private readonly State<T> start;
        private readonly State<T> goal;

        private State<T> result;

        #endregion

        #region .Ctor

        public AStar(T start, T goal)
        {
            this.start = new State<T>(start);
            this.goal = new State<T>(goal);
        }

        #endregion

        #region Methods

        private bool Search()
        {
            PriorityQueue<State<T>> open = new PriorityQueue<State<T>>();
            IDictionary<int, State<T>> closed = new Dictionary<int, State<T>>(500);

            // Reinicializar as variáveis
            result = null;
            
            // Vamos começar a expandir a partir deste estado!
            open.Enqueue(0, start);

            while (open.Count > 0)
            {
                // Apanhar o "melhor" estado possivel!
                State<T> node = open.Dequeue();

                // Adicionar à lista de fechados! <<extension>>
                closed.Add(node);

                // Verificar se chegamos à solução!
                if(node.IsGoal(goal))
                {
                    result = node;
                    return true;
                }

                // Adicionar filhos à lista de abertos!
                // Depois de os filtramos é claro!
                open.FilterAdd(closed, node, goal);
            }

            return false;
        }

        public void FindPath()
        {
            HasSolution = Search();
        }

        #endregion

        #region Properties

        public bool HasSolution { get; private set; }

        public IList<T> Path
        {
            get
            {
                IList<T> path = new List<T>();
                State<T> currentState = result;
                
                while (currentState != null)
                {
                    path.Add(currentState.Layout);
                    currentState = currentState.Parent;
                }

                return path;
            }
        }

        public int MinimumCost
        {
            get
            {
                if(HasSolution == false)
                {
                    throw new ArgumentException();
                }
                return result.Cost;
            }
        }

        public T Start
        {
            get { return start.Layout; }
        }

        public T Goal
        {
            get { return goal.Layout; }
        }

        #endregion

    }
}