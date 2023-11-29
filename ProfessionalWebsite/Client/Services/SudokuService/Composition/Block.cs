namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Block : CellAggregate
{
    public Block(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public int BlockRowId => ((Id - 1 - ((Id - 1) % 3)) / 3) + 1;
    public int BlockColumnId => ((Id - 1) % 3) + 1;
    public Cell? GetCell(int position) => Cells.FirstOrDefault(y => y.PosInBlock == position);
    public BlockRow BlockRow { get; private set; }
    public BlockColumn BlockColumn { get; private set; }
    public List<Block> BlockRowNeighbors => BlockRow.GetNeighborsOfBlock(this);
    public List<Block> BlockColumnNeighbors => BlockColumn.GetNeighborsOfBlock(this);
    public List<Block> BlockNeighbors => GetNeighborsOfBlock();

    public bool IsSolvableBasedOnCandidatesOfBlock => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Block[] CreateArrayFromCellReferences(List<Cell> cells)
    {
        Block[] blocks = new Block[10];
        for (int i = 0; i < 10; i++) { blocks[i] = new(i); };

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                blocks[cell.BlockId].AddCellReference(cell);
            }
        }

        return blocks;
    }
    public static List<Block> CreateFromCellList(List<Cell> cells)
    {
        List<Block> blocks = new()
        {
            new Block(1),
            new Block(2),
            new Block(3),
            new Block(4),
            new Block(5),
            new Block(6),
            new Block(7),
            new Block(8),
            new Block(9),
        };

        foreach (var cell in cells)
        {
            blocks[cell.BlockId - 1].Cells.Add(cell);
        }

        return blocks;
    }

    public void AddCellReference(Cell cell)
    {
        if (cell.BlockId == Id && cell.RowId != 0 && cell.ColumnId != 0)
        {
            int rowMod = (cell.RowId - 1) % 3;
            int colMod = (cell.ColumnId - 1) % 3;
            int index = (rowMod * 3) + colMod + 1;

            Cells[index] = cell;
        }
    }

    public void AssignBlockRowReference(BlockRow blockRow)
    {
        BlockRow = blockRow;
    }

    public void AssignBlockColumnReference(BlockColumn blockColumn)
    {
        BlockColumn = blockColumn;
    }
    private List<Block> GetNeighborsOfBlock()
    {
        List<Block> neighbors = new();

        foreach (var block in BlockRowNeighbors)
        {
            neighbors.Add(block);
        }
        foreach (var block in BlockColumnNeighbors)
        {
            neighbors.Add(block);
        }

        return neighbors;
    }
    public void AssignBlockReferenceToCells()
    {
        foreach (var cell in Cells)
        {
            if (cell is not null)
            {
                cell.AssignBlockReference(this);
            }
        }
    }
    public static void AssignBlockReferenceToCellsPerBlock(List<Block> blocks)
    {
        foreach (var block in blocks)
        {
            block.AssignBlockReferenceToCells();
        }
    }
    public static void EliminateCandidatesByDistinctInNeighborhood(List<Block> blocks)
    {
        foreach (var block in blocks)
        {
            block.EliminateCandidates();
        }
    }
    public static void EliminateCandidatesByCandidateLines(List<Block> blocks)
    {
        /*
        Candidate Lines are lines formed by 2 or three cells that exist in 2 neighborhoods,
        one neighbohhod being a line (row or column),
        and the other neighborhood being a matrix (in other words, a block).
        */

        foreach (var block in blocks)
        {
            block.EliminateCandidatesByCandidateLines();
        }
    }
    private void EliminateCandidatesByCandidateLines()
    {
        for (int candidateNumber = 1; candidateNumber < 10; candidateNumber++)
        {
            // Does this block have only two or three negative pencil markings for a given number?
            List<Cell> cellsWithCandidate = Cell.GetCellsWithCandidate(Cells.ToList(), candidateNumber);

            if (cellsWithCandidate.Count == 2 || cellsWithCandidate.Count == 3)
            {
                // Check if they exist in a line (all in the same row or the same column)
                Row? commonRow = Cell.GetCommonRow(cellsWithCandidate);
                Column? commonColumn = Cell.GetCommonColumn(cellsWithCandidate);

                // If they do for either, then the number should be eliinated as a negative pencil marking in that row or column
                if (commonRow is not null)
                {
                    List<Cell> cellsToRemoveCandidateFrom = Cell.GetCellsWithoutExceptions(commonRow.Cells.ToList(), cellsWithCandidate);
                    Cell.EliminateCandidateFromCells(candidateNumber, cellsToRemoveCandidateFrom);
                }
                if (commonColumn is not null)
                {
                    List<Cell> cellsToRemoveCandidateFrom = Cell.GetCellsWithoutExceptions(commonColumn.Cells.ToList(), cellsWithCandidate);
                    Cell.EliminateCandidateFromCells(candidateNumber, cellsToRemoveCandidateFrom);
                }
            }
        }
    }
    public static bool IsSolvableBasedOnCandidates(List<Block> blocks)
    {
        foreach (var block in blocks)
        {
            if (!block.IsSolvableBasedOnCandidatesOfBlock)
            {
                return false;
            }
        }
        return true;
    }
}

