
namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Row : CellAggregate
{
    private Row(int id, List<Cell> cellsOfRow) : base(cellsOfRow)
    {
        Id = id;
    }

    public int Id { get; }
    public bool IsSolvableBasedOnCandidatesOfRow => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Row Create(int id, List<Cell> cellsOfRow)
    {
        return new(id, cellsOfRow);
    }
    public void AssignRowReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignRowReference(this);
            }
        }
    }
    public void AddCellReference(Cell cell)
    {
        if (cell.RowId == Id)
        {
            Cells[cell.ColumnId] = cell;
        }
    }
    public static void EliminateCandidatesByDistinctInNeighborhood(List<Row> rows)
    {
        foreach (var row in rows)
        {
            row.EliminateCandidates();
        }
    }
    public static bool IsSolvableBasedOnCandidates(List<Row> rows)
    {
        foreach (var row in rows)
        {
            if (!row.IsSolvableBasedOnCandidatesOfRow)
            {
                return false;
            }
        }
        return true;
    }
}
