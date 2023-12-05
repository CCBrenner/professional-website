
namespace ProfessionalWebsite.Client.Services.SudokuService;

public class BlockColumn
{
    public BlockColumn(int id, List<Block> blocksOfCol)
    {
        Id = id;
        Blocks = blocksOfCol;
    }
    public int Id { get; }
    public List<Block> Blocks { get; private set; }

    public static BlockColumn Create(int id, List<Block> blocksOfBlockCol)
    {
        return new(id, blocksOfBlockCol);
    }
    public void AssignBlockColumnReferenceToBlocks()
    {
        foreach (var block in Blocks)
        {
            if (block is not null)
            {
                block.AssignBlockColumnReference(this);
            }
        }
    }
}