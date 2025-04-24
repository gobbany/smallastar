using System;
using System.Collections.Generic;
using System.Drawing;
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
                        brush = Brushes.Green; // start is green
                    else if (cell == endCell)
                        brush = Brushes.Red;   // finish is red
                    else if (cell.IsObstacle)
                        brush = Brushes.Black; // wall is black
                    else if (path.Contains(cell))
                        brush = Brushes.Blue;  // path is blue

                    // cell drawing
                    g.FillRectangle(brush, c * GridSize, r * GridSize, GridSize, GridSize);
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
                MessageBox.Show("Kérem a start és a finish cellát is helyezze el!");
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
            path.Clear();
            path.Add(null);
            foreach (var cell in grid)
            {
                cell.IsObstacle = false;
            }
            startCell = null;
            endCell = null;
            pnlGrid.Invalidate();

        }

        private void stepByStep_Click(object sender, EventArgs e)
        {
            btnStart.Hide();
            btnClear.Hide();
            
        }
    }
}
