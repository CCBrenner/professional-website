namespace ProfessionalWebsite.Client.Services.SudokuService;

public class TxnLedger
{
    private TxnLedger()
    {
        Txns = new();
        ValueTxns = new();
        CandidateTxns = new();
    }

    public List<Txn> Txns { get; private set; }
    public List<Txn> ValueTxns { get; private set; }
    public List<Txn> CandidateTxns { get; private set; }
    public Puzzle Puzzle { get; private set; }

    public static TxnLedger Create() => new();
    public void RecordNewTxn(int cellId, int indexOfValue, int previousValue, int newValue)
    {
        int txnId = Txns.Count + 1;
        Txn newTxn = new(txnId, cellId, indexOfValue, previousValue, newValue);

        Txns.Add(newTxn);

        if (indexOfValue == 0)
            ValueTxns.Add(newTxn);
        else
            CandidateTxns.Add(newTxn);
    }
    public void StepThroughTxnsByValue()
    {
        /*
        int intervalInMilliseconds = 1000;

        // CreateWithBruteForceSolver Matrix of cells through process of superimposition; cells are created here
        Cell[,] matrix = MatrixFactory.CreateCellMatrix(Puzzle.StartingIntMatrix);
        Cell[,] compositionMatrix = Puzzle.SuperimposeNonGivenCellValues(matrix, Puzzle.StartingIntMatrixToSuperimpose);

        // CreateWithBruteForceSolver starter list of references to cells of previously created matrix
        List<Cell> cells = Cell.CreateCellList(compositionMatrix);

        StepThroughTxns stepper = new(cells, this);
        stepper.StepForwardAutomaticallyOnInterval(intervalInMilliseconds);
        // Reconstructing using txns
        // Txns used in order forward and backward give the state of the sudoku Puzzle at that time of assignment
        // Animate data changes on an interval
        // Every step needs to be correct


        // Then allow user to change speed of animation
        // Then allow user to control steps
        // Then allow user to go backward
        // Then allow user to skip to exact txn
        */
    }
    /*
    private static void StepForwardByValue(ref int txn, ref List<Cell> cells)
    {
        // Take txn data and update cells to the next matrix state
        txn++;
        Cell cell = cells.FirstOrDefault(x => x.Id == txn.CellId);
    }

    private int GetValueTxnCount(List<Txn> txns)
    {
        int count = 0;
        for (int i = 0; i < Txns.Count; i++)
        {
            if (Txns[i].IndexOfValue == 0)
            {
                count++;
            }
        }
        return count;
    }
    */
    public void AssignPuzzleReference(Puzzle puzzle)
    {
        Puzzle = puzzle;
    }
    public string RenderTxns(int startingIndex = 1, int finishingIndex = 1000000000)
    {
        string returnStr = string.Empty;
        int count = 0;
        for (int i = startingIndex; i <= finishingIndex; i++)
        {
            returnStr += $"{Txns[i - 1].GetDetails()}\n";
            count++;
        }
        return $"{returnStr}Total Results: {count}";
    }
    
    public string RenderValueTxns(int startingIndex = 1, int finishingIndex = 1000000000)
    {
        string returnStr = string.Empty;
        int end = Txns.Count < finishingIndex ? Txns.Count + 1 : finishingIndex;
        int count = 0;
        for (int i = startingIndex; i < end; i++)
        {
            if (Txns[i - 1].IndexOfValue == 0)
            {
                returnStr += $"{Txns[i - 1].GetDetails()}\n";
                count++;
            }
        }
        return $"{returnStr}Total Results: {count}";
    }
}
