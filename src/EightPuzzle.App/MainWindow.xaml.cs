using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using StateSearch;
using System.Threading;

namespace EightPuzzleR
{
    public partial class MainWindow
    {
        #region Internal Data

        private Point space;
        private bool locked; 
        
        #endregion

        #region .Ctor

        // Done!
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        // Done!
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Interoperability.SendMessage(new WindowInteropHelper(this).Handle, 0xA1, 0x2, 0);
        }

        // Done!
        private void MoveBlock(object sender, RoutedEventArgs e)
        {
            if (locked) return;
            Button b = e.Source as Button;
            if (b == null) return;
            this.Move(b, Grid.GetColumn(b), Grid.GetRow(b));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Random r = new Random();

            IList<int> reference = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 0};

            // Gerar o puzzle!
            IList<int> startState = (reference).OrderBy(w => r.Next()).ToList();
            this.DataBindGrid(startState);

            // Gerar o puzzle!
            IList<int> closeState = (reference).OrderBy(w => r.Next()).ToList();
            this.DataBindGrid(closeState);

            StartState.Text = ConvertToString(startState);
            CloseState.Text = ConvertToString(closeState);

        }

        private static string ConvertToString(IEnumerable<int> state)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in state)
            {
                sb.Append(item);
            }

            return sb.ToString();
        }

        // Done!
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.locked) return;

            if (IsValidState(StartState.Text) == false || IsValidState(CloseState.Text) == false)
            {
                MessageBox.Show("Invalid Puzzles");
                return;
            }

            // O user pode ter mexido na configuração!
            DataBindGrid(StartState.Text);

            IStateSearchable<EightPuzzle> puzzle = new AStar<EightPuzzle>(EightPuzzleFactory.Create(StartState.Text), EightPuzzleFactory.Create(CloseState.Text));

            // Resolver
            puzzle.FindPath();

            // Gerar o puzzle!
            IList<int> startState = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            this.DataBindGrid(startState);

            if (puzzle.HasSolution)
            {
                this.locked = true;
                new Thread(() =>
                {
                    try
                    {
                        this.PlayAnimation(puzzle);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }).Start();
            }
            else
            {
                MessageBox.Show("Puzzle does not have a solution!");
            }

        }

        #endregion

        // Done!
        private void Move(UIElement b, int x, int y)
        {
            if (b == null)
            {
                return;
            }

            int deltaX = (int)Math.Abs(x - this.space.X);
            int deltaY = (int)Math.Abs(y - this.space.Y);

            double sqrt = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (sqrt > 1)
            {
                return;
            }

            Point current = new Point(x, y);

            Grid.SetColumn(b, (int)this.space.X);
            Grid.SetRow(b, (int)this.space.Y);

            Button sp = (from UIElement w in ContentHolder.Children
                         where w is Button && ((Button)w).Content.Equals("0")
                         select (Button)w).First();

            Grid.SetColumn(sp, (int)current.X);
            Grid.SetRow(sp, (int)current.Y);

            this.space = current;
        }

        // Done!
        private void DataBindGrid(IList<int> puzzle)
        {
            for (int i = 0; i < puzzle.Count; ++i)
            {
                Button button = ((Button)this.ContentHolder.Children[i]);
                button.Content = puzzle[i].ToString();
                button.Visibility = puzzle[i] == 0 ? Visibility.Hidden : Visibility.Visible;
            }
            space = ConvertToPoint(puzzle.IndexOf(0));
        }

        // Done!
        private void DataBindGrid(string puzzle)
        {
            for (int i = 0; i < puzzle.Length; ++i)
            {
                Button button = ((Button)this.ContentHolder.Children[i]);
                button.Content = puzzle[i].ToString();
                button.Visibility = puzzle[i] == '0' ? Visibility.Hidden : Visibility.Visible;
            }
            space = ConvertToPoint(puzzle.IndexOf("0"));
        }

        // Done!
        private static Point ConvertToPoint(int i)
        {
            switch (i)
            {
                case 0: return new Point(0, 0);
                case 1: return new Point(1, 0);
                case 2: return new Point(2, 0);
                case 3: return new Point(0, 1);
                case 4: return new Point(1, 1);
                case 5: return new Point(2, 1);
                case 6: return new Point(0, 2);
                case 7: return new Point(1, 2);
                case 8: return new Point(2, 2);
            }

            throw new ArgumentException();

        }

        // Done!
        private void PlayAnimation(IStateSearchable<EightPuzzle> puzzle)
        {
            IEnumerable<EightPuzzle> solution = puzzle.Path.Reverse();

            AlterBoardConfigurationDelegate call = this.AlterBoardConfiguration;

            // Vamos percorer cada estado!
            foreach (var item in solution)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Send, call, this.ContentHolder, item.ToString());
                Thread.Sleep(300);
            }

            locked = false;
        }

        // Done!
        private delegate void AlterBoardConfigurationDelegate(Grid control, string configuration);

        // Done!
        public void AlterBoardConfiguration(Grid control, string configuration)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(DispatcherPriority.Send, new AlterBoardConfigurationDelegate(this.AlterBoardConfiguration), control, configuration);
                return;
            }
            this.DataBindGrid(configuration);
        }

        // Done!
        private static bool IsValidState(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length != 9) return false;

            foreach (char c in text)
            {
                if (char.IsNumber(c) == false)
                {
                    return false;
                }
            }

            return true;
        }
    
    }
}
