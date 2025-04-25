namespace astar1
{
    public class AstarStepState
    {
        public List<Cell> OpenList { get; set; } = new List<Cell>();
        public HashSet<Cell> ClosedList { get; set; } = new HashSet<Cell>();
        public Cell Current { get; set; }
    }
}