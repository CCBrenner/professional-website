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
        List<Row> result = new()
        {
            new Row(1),
            new Row(2),
            new Row(3),
            new Row(4),
            new Row(5),
            new Row(6),
            new Row(7),
            new Row(8),
            new Row(9),
        };

        foreach (var cell in cellList)
        {
            result[cell.RowId - 1].Cells.Add(cell);
        }

        return result;
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

        foreach (var cell in cellList)
        {
            result[cell.ColumnId - 1].Cells.Add(cell);
        }

        return result;
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

        foreach (var cell in cellList)
        {
            blocks[cell.BlockId - 1].Cells.Add(cell);
        }

        return blocks;
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
        List<BlockRow> result = new()
        {
            new BlockRow(1),
            new BlockRow(2),
            new BlockRow(3),
        };

        foreach (var block in blockList)
        {
            result[block.BlockRowId - 1].Blocks.Add(block);
        }

        return result;
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
        List<BlockColumn> result = new()
        {
            new BlockColumn(1),
            new BlockColumn(2),
            new BlockColumn(3),
        };

        foreach (var block in blockList)
        {
            result[block.BlockColumnId - 1].Blocks.Add(block);
        }

        return result;
    }
    public static void AssignBlockColumnstoBlocks(this List<BlockColumn> blockColumnList)
    {
        foreach (var blockRow in blockColumnList)
        {
            blockRow.AssignBlockColumnReferenceToBlocks();
        }
    }
}
