namespace ProfessionalWebsite.Client.Services.SudokuService;

public class Cell
{
    private Cell(int row, int column)
    {
        RowId = row;
        ColumnId = column;
        BlockId = GetBlockId(RowId, ColumnId);
        Id = GetCellId(RowId, ColumnId);

        Values = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  // [0] is "Value"; all others are Candidates
        ValueStatus = ValueStatus.Undefined;

        PositivePencilMarkings = new int[10];
        TriedCandidates = new();
    }

    public static int NonPossibilityPlaceholderValue = 0;
    private const ValueStatus DEFAULT_VALUE_STATUS = ValueStatus.Undefined;

    public ValueStatus ValueStatus { get; private set; }
    public int Id { get; private set; }
    public int RowId { get; }
    public int PosInRow => ColumnId;
    public int ColumnId { get; }
    public int PosInCol => RowId;
    public int BlockId { get; }
    public int PosInBlock => GetPositionInBlock();
    public Row Row { get; private set; }
    public Column Column { get; private set; }
    public Block Block { get; private set; }
    public BlockRow BlockRow => Block.BlockRow;
    public BlockRow BlockColumn => Block.BlockRow;
    public Puzzle Puzzle { get; private set; }
    public int[] Values { get; private set; }  // [0] is "Value"; all others are Candidates
    public int Value
    {
        get
        {
            return Values[0];
        }
        set
        {
            Values[0] = value;
        }
    }

    // vvv [0] is a placeholder; all others are PPMs (50/50 probabilities in nearly every case)
    public int[] PositivePencilMarkings { get; set; }
    public List<int> Candidates
    {
        get
        {
            SortedSet<int> sortedSet = new();

            for (int i = 1; i < Values.Count(); i++)
            {
                sortedSet.Add(Values[i]);
            }

            List<int> result = sortedSet.ToList();
            if (result[0] == 0) result.RemoveAt(0);

            return result;
        }
    }
    public List<int> TriedValuesAsCurrentCell { get; private set; }
    public List<int> CandidatesTried { get; private set; }
    public List<int> TriedCandidates { get; set; }
    public bool HasCandidate(int number) =>
        Values[number] == number;
    private int GetCellId(int row, int column) =>
        ((row - 1) * 9) + column;
    public static Cell Create(int rowId, int colId) => new(rowId, colId);
    public int UpdateValueBasedOnSingleCandidate()
    {
        int candidateCount = 0;
        int savedValueFromIteration = NonPossibilityPlaceholderValue;

        foreach (var val in Values)
        {
            if (val != 0)
            {
                candidateCount++;
                savedValueFromIteration = val;
            }
        }

        if (candidateCount == 1 && savedValueFromIteration != 0)
        {
            Value = savedValueFromIteration;
            if (Puzzle.NoExpectedCellValuesInCells)
                ValueStatus = ValueStatus.Confirmed;
            else
                ValueStatus = ValueStatus.Expected;
        }

        return savedValueFromIteration;
    }
    public int SetExpectedValue(int expectedValue)
    {
        //int previousValue = Value;  // store for later

        if (!IsCandidate(expectedValue) && ValueStatus != ValueStatus.Given)
        {
            return NonPossibilityPlaceholderValue;
        }

        Value = expectedValue;
        ValueStatus = ValueStatus.Expected;

        /*
        if (Puzzle is not null)
        {
            if (previousValue != Value)
            {
                Puzzle.Ledger.RecordNewTxn(Id, 0, previousValue, Value);
            }
        }
        */

        return Value;
    }
    public int SetExpectedValueAsPlayback(Txn txn)
    {
        Values[txn.IndexOfValue] = txn.New;
        return Values[txn.IndexOfValue];
    }
    public int EliminateCandidate(int candidate)
    {
        //int previousValue = Values[candidate];

        if (candidate == 0 || candidate > 9)
        {
            return NonPossibilityPlaceholderValue;
        }

        Values[candidate] = NonPossibilityPlaceholderValue;

        /*
        if (previousValue != Values[candidate])
        {
            Puzzle.Ledger.RecordNewTxn(Id, candidate, previousValue, Values[candidate]);
        }
        */

        return candidate;
    }
    public int AssignConfirmedValue(int newCellValue)
    {
        if (Values[newCellValue] == NonPossibilityPlaceholderValue || ValueStatus == ValueStatus.Given)
        {
            return NonPossibilityPlaceholderValue;
        }

        Value = newCellValue;
        ValueStatus = ValueStatus.Confirmed;

        // AddCellToQueueForChecking();  // not utilizing in brute force version; adding as reminder for non-brute force version

        return newCellValue;
    }
    public string GetAllValues()
    {
        string allValues = string.Empty;
        int spaceCounter = 0;
        int counter = 0;
        foreach (var val in Values)
        {
            if (counter == 0)
            {
                allValues += $"{val} ";
            }
            else if (val != 0)
            {
                allValues += val.ToString();
            }
            else
            {
                spaceCounter++;
            }
            counter++;
        }

        for (int i = 0; i < spaceCounter; i++)
        {
            allValues += " ";
        }

        return allValues;
    }
    private bool IsCandidate(int valueBeingChecked)
    {
        foreach (var val in Values)
        {
            if (val != NonPossibilityPlaceholderValue && val == valueBeingChecked)
            {
                return true;
            }
        }
        return false;
    }
    private int GetBlockId(int row, int column)
    {
        int rowMod = (row - 1) % 3;
        int blockRow = (row - 1 - rowMod) / 3;

        int colMod = (column - 1) % 3;
        int blockCol = (column - 1 - colMod) / 3;

        int blockId = (blockRow * 3) + blockCol + 1;

        return blockId;
    }
    public void AssignRowReference(Row row)
    {
        Row = row;
    }
    public void AssignColumnReference(Column column)
    {
        Column = column;
    }
    public void AssignBlockReference(Block block)
    {
        Block = block;
    }
    public static List<Cell> GetCellsWithCandidate(List<Cell> cells, int candidateNumber)
    {
        List<Cell> cellsWithCandidate = new();

        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                if (cell.HasCandidate(candidateNumber))
                {
                    cellsWithCandidate.Add(cell);
                }
            }
        }

        return cellsWithCandidate;
    }
    public static Row? GetCommonRow(List<Cell> cellsWithCandidate)
    {
        int rowId = 0;

        foreach (var cell in cellsWithCandidate)
        {
            if (rowId == 0)
            {
                rowId = cell.Row.Id;
            }
            else if (rowId != cell.Row.Id)
            {
                return null;
            }
        }

        return cellsWithCandidate[0].Row;
    }
    public static Column? GetCommonColumn(List<Cell> cellsWithCandidate)
    {
        int columnId = 0;

        foreach (var cell in cellsWithCandidate)
        {
            if (columnId == 0)
            {
                columnId = cell.Column.Id;
            }
            else if (columnId != cell.Column.Id)
            {
                return null;
            }
        }

        return cellsWithCandidate[0].Column;
    }
    public static List<Cell> GetCellsWithoutExceptions(List<Cell> cells, List<Cell> cellsWithCandidate)
    {
        List<Cell> result = new();

        foreach (var cellOne in cells)
        {
            if (cellOne is not null)
            {
                bool isException = false;

                foreach (var cellTwo in cellsWithCandidate)
                {
                    if (cellTwo is not null)
                    {
                        if (cellOne.Id == cellTwo.Id)
                        {
                            isException = true;
                            break;
                        }
                    }
                }

                if (!isException)
                {
                    result.Add(cellOne);
                }
            }
        }

        return result;
    }
    public static void EliminateCandidateFromCells(int candidateNumber, List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                cell.EliminateCandidate(candidateNumber);
            }
        }
    }
    public static List<int> GetCandidates(List<Cell> cells)
    {
        int[] tempArray = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        // eliminate candidates based on cell values from tempArray
        foreach (var cell in cells)
        {
            if (cell is not null)
            {
                tempArray[cell.Value] = 0;
            }
        }

        // only the remaining candidates, minus all zeros, are returned
        List<int> result = GetCandidates(tempArray);

        return result;
    }
    private static List<int> GetCandidates(int[] values)
    {
        List<int> result = new();

        for (int i = 0; i < values.Count(); i++)
        {
            if (values[i] != 0)
            {
                result.Add(values[i]);
            }
        }

        return result;
    }
    public static Cell[,] CreateCellMatrix()
    {
        Cell[,] matrix = new Cell[9, 9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                matrix[i, j] = Create(i + 1, j + 1);
            }
        }

        return matrix;
    }
    public void AssignPuzzleReference(Puzzle puzzle)
    {
        Puzzle = puzzle;
    }
    private int GetPositionInBlock()
    {
        int rowMod = (RowId - 1) % 3;
        int colMod = (ColumnId - 1) % 3;
        return (rowMod * 3) + colMod + 1;
    }
    private bool GetIsCandidate(int value)
    {
        foreach (var candidate in Candidates)
        {
            if (value == candidate)
            {
                return true;
            }
        }

        return false;
    }
    public void AddTriedCandidate(int candidate)
    {
        TriedCandidates.Add(candidate);
    }
    public int GetNextCandidate()
    {
        foreach (var val in Candidates)
        {
            if (!TriedCandidates.Contains(val))
            {
                return val;
            }
        }
        return NonPossibilityPlaceholderValue;
    }
    public int GetNextCandidateV2() =>
        Candidates
        .Where(x => !TriedCandidates.Contains(x))
        .Select(x => x)
        .FirstOrDefault(NonPossibilityPlaceholderValue);
    public void ResetTriedCandidates()
    {
        TriedCandidates = new();
    }
    public void RehydrateCandidates()
    {
        for (int i = 1; i < Values.Count(); i++)
        {
            //int previousValue = Values[i];
            
            Values[i] = i;
            
            /*
            if (previousValue != Values[i])
            {
                Puzzle.Ledger.RecordNewTxn(Id, i, previousValue, Values[i]);
            }
            */
        }
    }
    public void ResetValue()
    {
        Value = NonPossibilityPlaceholderValue;
    }
    public void SetValueStatusToGiven()
    {
        ValueStatus = ValueStatus.Given;
    }
    public void ResetCandidateTracking()
    {
        TriedCandidates = new List<int>();
    }
    public void EliminateAllNonValueCandidates()
    {
        for (int i = 1; i < 10; i++)
        {
            if (Values[i] != Value)
            {
                EliminateCandidate(i);
            }
        }
    }
    public void ResetValueStatus()
    {
        ValueStatus = DEFAULT_VALUE_STATUS;
    }
}
