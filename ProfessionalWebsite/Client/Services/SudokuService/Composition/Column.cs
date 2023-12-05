
namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Column : CellAggregate
{
    private Column(int id, List<Cell> cellsOfCol) : base(cellsOfCol)
    {
        Id = id;
    }

    public int Id { get; }
    public bool IsSolvableBasedOnCandidatesOfColumn => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Column Create(int id, List<Cell> cellsOfCol)
    {
        return new(id, cellsOfCol);
    }
    public void AssignColumnReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignColumnReference(this);
            }
        }
    }
    public static void EliminateCandidatesByDistinctInNeighborhood(List<Column> columns)
    {
        foreach (var column in columns)
        {
            column.EliminateCandidates();
        }
    }
    public static bool IsSolvableBasedOnCandidates(List<Column> columns)
    {
        foreach (var column in columns)
        {
            if (!column.IsSolvableBasedOnCandidatesOfColumn)
            {
                return false;
            }
        }
        return true;
    }
}

