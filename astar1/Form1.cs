using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace astar1
{
    public partial class Form1 : Form
    {
        private const int GridSize = 30;
        private const int Rows = 20;
        private const int Columns = 20;

        private Cell[,] grid = new Cell[Rows, Columns];
        private Cell startCell = null; //start
        private Cell endCell = null; //finish
        private List<Cell> path = new List<Cell>(); //path list

        private MazeGenerator generator = new MazeGenerator(Rows,Columns);   
        private bool[,] maze;
        private IEnumerator<AstarStepState> stepEnumerator;
        private Timer stepTimer;
        private bool cleared=true;

        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    grid[r, c] = new Cell(r, c);
                }
            }

            pnlGrid.Paint += PnlGrid_Paint;
            pnlGrid.MouseClick += PnlGrid_MouseClick;
            stepTimer = new Timer();
            stepTimer.Interval = trackBar1.Value;
            stepTimer.Tick += Timer1_Tick;
        }

        private void stepByStep_Click(object sender, EventArgs e)
        {
            if (startCell == null || endCell == null)
            {
                MessageBox.Show("Please set start and end cells.");
                return;
            }
            path = AStarAlgorithm.FindPath(grid, startCell, endCell);
            if (!path.Contains(endCell))
            {
                MessageBox.Show("Cannot reach destination - no path");
                return;
            }
            stepEnumerator = AStarAlgorithm.PathStepIterator(grid, startCell, endCell).GetEnumerator();
            stepTimer.Start();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            stepTimer.Interval = 301 - trackBar1.Value;
            textBox1.Text = "Speed of stepping: " + "\n" + (trackBar1.Value).ToString();
            if (stepEnumerator == null || !stepEnumerator.MoveNext())
            {
                stepTimer.Stop();
                path = AStarAlgorithm.FindPath(grid, startCell, endCell);
                foreach (var cell in path)
                {
                    Rectangle cellRect = new Rectangle(cell.Col * GridSize, cell.Row * GridSize, GridSize, GridSize);
                    pnlGrid.Invalidate(cellRect);
                }
                return;
            }

            var state = stepEnumerator.Current;
            foreach (var cell in state.OpenList) DrawCell(cell, Brushes.LightGreen);
            foreach (var cell in state.ClosedList) DrawCell(cell, Brushes.Orange);
            DrawCell(state.Current, Brushes.Purple);
        }

        private void DrawCell(Cell cell, Brush brush)
        {
            using (Graphics g = pnlGrid.CreateGraphics())
            {
                Rectangle rect = new Rectangle(cell.Col * GridSize, cell.Row * GridSize, GridSize, GridSize);
                g.FillRectangle(brush, rect);
                g.DrawRectangle(Pens.Gray, rect);
            }
        }

        private void PnlGrid_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    var cell = grid[r, c];
                    Brush brush = Brushes.White;

                    if (cell == startCell)
                        DrawCell(startCell, Brushes.Green); // start is green
                    else if (cell == endCell)
                        DrawCell(endCell, Brushes.Red);   // finish is red
                    else if (cell.IsObstacle)
                        DrawCell(cell, Brushes.Black); // wall is black
                    else if (path.Contains(cell))
                        DrawCell(cell, Brushes.Blue);  // path is blue
                    g.DrawRectangle(Pens.Gray, c * GridSize, r * GridSize, GridSize, GridSize);
                }
            }
        }


        private void PnlGrid_MouseClick(object sender, MouseEventArgs e)
        {
            int col = e.X / GridSize;
            int row = e.Y / GridSize;

            Rectangle cellRect = new Rectangle(col * GridSize, row * GridSize, GridSize, GridSize);
            pnlGrid.Invalidate(cellRect);

            if (row >= Rows || col >= Columns) return;

            var clickedCell = grid[row, col];

            if (e.Button == MouseButtons.Left)
            {
                if (startCell == null)
                {
                    startCell = grid[row, col];
                }
                else if (endCell == null && clickedCell != startCell)
                {
                    endCell = grid[row, col];
                }
                else if (clickedCell != startCell && clickedCell != endCell)
                {
                    grid[row, col].IsObstacle = true;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                grid[row, col].IsObstacle = false;
                if (grid[row, col] == startCell) startCell = null;
                if (grid[row, col] == endCell) endCell = null;
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (startCell == null || endCell == null)
            {
                MessageBox.Show("Please set start and end cells.");
                return;
            }

            path = AStarAlgorithm.FindPath(grid, startCell, endCell);
            foreach (var cell in path)
            {
                Rectangle cellRect = new Rectangle(cell.Col * GridSize, cell.Row * GridSize, GridSize, GridSize);
                pnlGrid.Invalidate(cellRect);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Speed of stepping: " + "\n" + (0).ToString();
            stepTimer.Stop();
            stepEnumerator = null;
            path.Clear();
            path.Add(null);
            foreach (var cell in grid)
            {
                cell.IsObstacle = false;
            }
            startCell = null;
            endCell = null;
            pnlGrid.Invalidate();
            cleared = true;
        }

        private void MazeGen_click(object sender, EventArgs e)
        {
            if (cleared)
            {
                maze = generator.Generate();
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if(grid[i,j].IsObstacle && maze[i, j]) grid[i,j].IsObstacle=false;
                        if (!maze[i, j]) grid[i, j].IsObstacle = true;
                        pnlGrid.Invalidate();
                    }
                }
                cleared = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }      
} 
