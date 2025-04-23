namespace astar1
{
    public class Cell
    {
        public int Row { get; } //sor - readonly
        public int Col { get; } //oszlop - readonly
        public bool IsObstacle { get; set; } //fal vagy nem
        public int G { get; set; } // starttól levő táv
        public int H { get; set; } // finishtől levő táv
        public int F => G + H; 
        public Cell Previous { get; set; } // őscella

        public Cell(int row, int col) //konstruktor
        {
            Row = row;
            Col = col;
            IsObstacle = false;
        }
    }
}
