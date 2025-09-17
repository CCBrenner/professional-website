
namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Block : CellAggregate
{
    private Block(int id, List<Cell> cellsOfBlock) : base(cellsOfBlock)
    {
        Id = id;
    }

    public int Id { get; }
    public int BlockRowId => ((Id - 1 - ((Id - 1) % 3)) / 3) + 1;
    public int BlockColumnId => ((Id - 1) % 3) + 1;
    public BlockRow BlockRow { get; private set; }
    public BlockColumn BlockColumn { get; private set; }

    public bool IsSolvableBasedOnCandidatesOfBlock => GetIsSolvableBasedOnCandidates(Cells.ToList());

    public static Block Create(int id, List<Cell> cellsOfBlock)
    {
        return new(id, cellsOfBlock);
    }
    public void AssignBlockRowReference(BlockRow blockRow)
    {
        BlockRow = blockRow;
    }

    public void AssignBlockColumnReference(BlockColumn blockColumn)
    {
        BlockColumn = blockColumn;
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
                // CheckForOpponents if they exist in a line (all in the same row or the same column)
                Row? commonRow = Cell.GetCommonRow(cellsWithCandidate);
                Column? commonColumn = Cell.GetCommonColumn(cellsWithCandidate);

                // If they do for either, then the number should be eliminated as a negative pencil marking in that row or column
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

