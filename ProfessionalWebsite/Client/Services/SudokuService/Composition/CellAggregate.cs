namespace ProfessionalWebsite.Client.Services.SudokuService;

public abstract class CellAggregate
{
    public CellAggregate(List<Cell> cellsOfAggregate)
    {
        Cells = cellsOfAggregate;
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
    public string GetCandidatesAsFormattedString()
    {
        int front = 0;
        int back = 0;

        int temp = 10 - Candidates.Count;
        if (temp > 1)
        {
            int g = temp % 2;
            int p = temp - g;
            front = p / 2;
            back = front + g;
        }

        string result = string.Empty;

        for (int i = 0; i < front; i++)
        {
            result += " ";
        }

        result += string.Join("", Candidates);


        for (int i = 0; i < back; i++)
        {
            result += " ";
        }

        return result;
    }
    public string GetMatrixAsFormattedString()
    {
        string returnStr = string.Empty;
        string row = string.Empty;

        for (int i = 0; i < 9; i++)
        {
            row = $"[ {Cells[9 * i].Values[0]} {Cells[1 + (9 * i)].Values[0]} {Cells[2 + (9 * i)].Values[0]} ]  " +
                  $"[ {Cells[3 + (9 * i)].Values[0]} {Cells[4 + (9 * i)].Values[0]} {Cells[5 + (9 * i)].Values[0]} ]  " +
                  $"[ {Cells[6 + (9 * i)].Values[0]} {Cells[7 + (9 * i)].Values[0]} {Cells[8 + (9 * i)].Values[0]} ]\n";
            returnStr += row;

            if (i % 3 == 2)
                returnStr += row;
        }

        return returnStr;
    }
    protected void EliminateCandidates()
    {
        SortedSet<int> candidatesToEliminate = GetCandidatesToEliminate();
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
