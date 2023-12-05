namespace ProfessionalWebsite.Client.Services.SudokuService;

public static class ListExtensions
{
    // These were originally static methods that accepted their own types as parameters.
    // I have opted instead for extension methods for the purpose of being succinct.
    public static List<Cell> CreateCellList(this Cell[,] cellMatrix)
    {
        List<Cell> cells = new();

        foreach (var cell in cellMatrix)
        {
            cells.Add(cell);
        }

        return cells;
    }
    public static List<Row> CreateRowList(this List<Cell> cellList)
    {
        List<Row> rowList = new();

        foreach (int id in Enumerable.Range(1,9))
        {
            List<Cell> cellsOfRow =
                cellList
                .Where(x => x.RowId == id)
                .Select(x => x)
                .ToList();
            Row newRow = Row.Create(id, cellsOfRow);
            rowList.Add(newRow);
        }

        return rowList;
    }
    public static void AssignRowsToCells(this List<Row> rowList)
    {
        foreach (var row in rowList)
        {
            row.AssignRowReferenceToCells();
        }
    }
    public static List<Column> CreateColumnList(this List<Cell> cellList)
    {
        List<Column> colList = new();

        foreach (int id in Enumerable.Range(1, 9))
        {
            List<Cell> cellsOfCol =
                cellList
                .Where(x => x.ColumnId == id)
                .Select(x => x)
                .ToList();
            Column newCol = Column.Create(id, cellsOfCol);
            colList.Add(newCol);
        }

        return colList;
    }
    public static void AssignColumnsToCells(this List<Column> columnList)
    {
        foreach (var column in columnList)
        {
            column.AssignColumnReferenceToCells();
        }
    }
    public static List<Block> CreateBlockList(this List<Cell> cellList)
    {
        List<Block> blockList = new();

        foreach (int id in Enumerable.Range(1, 9))
        {
            List<Cell> cellsOfBlock =
                cellList
                .Where(x => x.BlockId == id)
                .Select(x => x)
                .ToList();
            Block newBlock = Block.Create(id, cellsOfBlock);
            blockList.Add(newBlock);
        }

        return blockList;
    }
    public static void AssignBlocksToCells(this List<Block> blockList)
    {
        foreach (var block in blockList)
        {
            block.AssignBlockReferenceToCells();
        }
    }
    public static List<BlockRow> CreateBlockRowList(this List<Block> blockList)
    {
        List<BlockRow> blockRowList = new();

        foreach (int id in Enumerable.Range(1, 3))
        {
            List<Block> blocksOfBlockRow =
                blockList
                .Where(x => x.BlockRowId == id)
                .Select(x => x)
                .ToList();
            BlockRow newBlockRow = BlockRow.Create(id, blocksOfBlockRow);
            blockRowList.Add(newBlockRow);
        }

        return blockRowList;
    }
    public static void AssignBlockRowsToBlocks(this List<BlockRow> blockRowList)
    {
        foreach (var blockRow in blockRowList)
        {
            blockRow.AssignBlockRowReferenceToBlocks();
        }
    }
    public static List<BlockColumn> CreateBlockColumnList(this List<Block> blockList)
    {
        List<BlockColumn> blockColList = new();

        foreach (int id in Enumerable.Range(1, 3))
        {
            List<Block> blocksOfBlockCol =
                blockList
                .Where(x => x.BlockRowId == id)
                .Select(x => x)
                .ToList();
            BlockColumn newBlockCol = BlockColumn.Create(id, blocksOfBlockCol);
            blockColList.Add(newBlockCol);
        }

        return blockColList;
    }
    public static void AssignBlockColumnstoBlocks(this List<BlockColumn> blockColumnList)
    {
        foreach (var blockRow in blockColumnList)
        {
            blockRow.AssignBlockColumnReferenceToBlocks();
        }
    }
}
