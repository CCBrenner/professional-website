namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Column : CellAggregate
{
    public Column(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public Cell? Cell(int position) => Cells.FirstOrDefault(y => y.PosInCol == position);
    public bool IsSolvableBasedOnCandidatesOfColumn => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Column[] CreateArrayFromCellReferences(List<Cell> cells)
    {
        Column[] columns = new Column[10];
        for (int i = 0; i < 10; i++) { columns[i] = new(i); };

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                columns[cell.ColumnId].AddCellReference(cell);
            }
        }

        return columns;
    }
    public static void AssignColumnReferenceToCellsPerColumn(List<Column> columns)
    {
        foreach (var column in columns)
        {
            column.AssignColumnReferenceToCells();
        }
    }
    public void AddCellReference(Cell cell)
    {
        if (cell.ColumnId == Id)
        {
            Cells[cell.RowId] = cell;
        }
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

    public static List<Column> CreateFromCellList(List<Cell> cells)
    {
        List<Column> result = new()
        {
            new Column(1),
            new Column(2),
            new Column(3),
            new Column(4),
            new Column(5),
            new Column(6),
            new Column(7),
            new Column(8),
            new Column(9),
        };

        foreach (var cell in cells)
        {
            result[cell.ColumnId - 1].Cells.Add(cell);
        }

        return result;
    }
}

