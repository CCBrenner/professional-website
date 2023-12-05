
namespace ProfessionalWebsite.Client.Services.SudokuService;

public class BlockRow
{
    private BlockRow(int id, List<Block> blocksOfBlockRow)
    {
        Id = id;
        Blocks = blocksOfBlockRow;
    }
    public int Id { get; }
    public List<Block> Blocks { get; private set; }

    public static BlockRow Create(int id, List<Block> blocksOfBlockRow)
    {
        return new(id, blocksOfBlockRow);
    }
    public void AssignBlockRowReferenceToBlocks()
    {
        foreach (var block in Blocks)
        {
            if (block is not null)
            {
                block.AssignBlockRowReference(this);
            }
        }
    }
}
