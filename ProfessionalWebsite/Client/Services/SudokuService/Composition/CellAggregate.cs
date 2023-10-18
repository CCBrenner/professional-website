namespace ProfessionalWebsite.Client.Services.SudokuService;

public abstract class CellAggregate
{
    public CellAggregate()
    {
        Cells = new List<Cell>();
    }
    public List<Cell> Cells { get; private set; }
    public List<int> Candidates => Cell.GetCandidates(Cells.ToList());

    public static bool GetIsSolvableBasedOnCandidates(List<Cell> cells)
    {
        int valuelessCellCount = 0;
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                // If any cell without a value has no more candidates (after candidates have been updated)
                if (cell.Candidates.Count == 0)
                {
                    return false;
                }

                if (cell.Values[0] != 0)
                {
                    valuelessCellCount++;
                }
            }
        }

        // If quantity of cells with no values is greater than the number of available candidates
        List<int> candidates = Cell.GetCandidates(cells.ToList());
        if (candidates.Count < valuelessCellCount)
        {
            return false;
        }

        return true;
    }
    protected void EliminateCandidates()
    {
        // Get Given, and Confirmed numbers of row
        SortedSet<int> candidatesToEliminate = GetCandidatesToEliminate();

        // Eliminate candidates of cells in the row
        EliminateCandidatesFromCells(candidatesToEliminate);
    }
    protected SortedSet<int> GetCandidatesToEliminate()
    {
        // Go through each cell in the row & if they have any of the flags marked as true, add them to the SortedSet.
        SortedSet<int> candidatesToEliminate = new();

        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                if (cell.Value != 0)
                {
                    int possiblityToEliminate = cell.Value;
                    candidatesToEliminate.Add(possiblityToEliminate);
                }
            }
        }

        return candidatesToEliminate;
    }
    protected void EliminateCandidatesFromCells(SortedSet<int> candidatesToEliminate)
    {
        foreach (var candidate in candidatesToEliminate)
        {
            foreach (var cell in Cells)
            {
                if (cell is not null)
                {
                    if (cell.ValueStatus != ValueStatus.Given && cell.ValueStatus != ValueStatus.Confirmed)
                    {
                        cell.EliminateCandidate(candidate);
                    }
                }
            }
        }
    }
}
