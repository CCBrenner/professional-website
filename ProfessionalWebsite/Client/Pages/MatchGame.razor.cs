using Microsoft.AspNetCore.Components;
using System.Timers;
// using System.Timers;

namespace ProfessionalWebsite.Client.Pages;

public partial class MatchGame : ComponentBase
{
    public MatchGame()
    {
        InitializeMatchGame();
    }
    
    private int matchesFound;
    private bool isComparingForMatch = false;
    private Block lastBlockClicked = new Block() { Id = -1, AnimalEmoji = "", Visibility = "non-used string", IsMatched = false };

    private System.Timers.Timer timer;
    private int tenthsOfSecondsElapsed;
    public string TimerText { get; set; } = "Find the matches!"; 

    public List<Block> Blocks { get; set; } = new List<Block>();
    public Random random = new Random();
    public MatchGameStatus GameStatus = MatchGameStatus.Ready;

    private void InitializeMatchGame()
    {
        GameStatus = MatchGameStatus.Ready;
        matchesFound = 0;
        TimerText = "Find the matches!";
        InitializeBlocks();
        InitializeTimer();
    }
    private void InitializeBlocks()
    {
        Blocks = new List<Block>();
        List<string> animalEmoji = new List<string>()
        {
            "🦍", "🦍",
            "🦊", "🦊",
            "🦄", "🦄",
            "🐖", "🐖",
            "🦥", "🦥",
            "🦆", "🦆",
            "🐋", "🐋",
            "🐌", "🐌"
        };
        int k = 16;
        for (int i = 0; i < 16; i++)
        {
            int j = random.Next(k);
            Blocks.Add(new Block() { Id = i, AnimalEmoji = animalEmoji[j], Visibility = "", IsMatched = false });
            animalEmoji.RemoveAt(j);
            k--;
        }
    }
    
    private void InitializeTimer()
    {
        timer = new System.Timers.Timer(100);  // interval: 100ms = 0.1 seconds
        timer.Elapsed += new ElapsedEventHandler(PerTimerInterval);
        tenthsOfSecondsElapsed = 0;
    }

    private void PerTimerInterval(object sender, ElapsedEventArgs e)
    {
        tenthsOfSecondsElapsed++;
        TimerText = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        StateHasChanged();
        if (matchesFound >= 8)
            timer.Stop();
    }
    
    public void UserSelectsBlock(Block block)
    {
        if (TimerText == "Find the matches!")
        {
            GameStatus = MatchGameStatus.Playing;
            timer.Start();
        }
        if (block.IsMatched == false)
        {
            if (isComparingForMatch == false)
            {
                block.Visibility = "block-showing";
                lastBlockClicked = block;
                isComparingForMatch = true;
            }
            else if (block.Id == lastBlockClicked.Id) { }
            else if (block.AnimalEmoji == lastBlockClicked.AnimalEmoji)
            {
                matchesFound++;
                block.IsMatched = true;
                lastBlockClicked.IsMatched = true;
                block.Visibility = "block-showing";
                isComparingForMatch = false;
                if (matchesFound >= 8)
                    GameStatus = MatchGameStatus.Done;
            }
            else
            {
                lastBlockClicked.Visibility = "";
                block.Visibility = "block-showing";
                lastBlockClicked = block;
            }
        }
    }

    public void UserSelectsNewGamePrompt()
    {
        if (GameStatus == MatchGameStatus.Done) InitializeMatchGame();
    }
}

public class Block
{
    public int Id;
    public string AnimalEmoji;
    public string Visibility;
    public bool IsMatched;
}

public enum MatchGameStatus
{
    Ready,
    Playing,
    Done
}
