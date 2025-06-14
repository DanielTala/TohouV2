using Raylib_cs;

public class PlayerEXPManager
{
    private float playerEXP;
    public int CurrentLevel => currentLevel;
    private int currentLevel = 1;
    public static List<BaseItem> ItemList = new List<BaseItem>();
    public static List<BaseItem> ToRemove = new List<BaseItem>();
    private List<int> levelRequirements = new List<int>() { 10, 20, 30, 40, 50 };
    private float baseEXP = 50f;
    private float scaleFactor = 1.5f;
    private float offset = 10f;

    public void Initialize()
    {

    }
    public void AddEXP(BaseEnemy enemy)
    {
        Console.WriteLine(GetEXPReward(enemy));
        playerEXP += GetEXPReward(enemy);
        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        if (playerEXP >= GetEXPForLevel(currentLevel + 1))
        {
            playerEXP = 0;
            currentLevel++;
        }
    }

    private float GetEXPForLevel(int level)
    {
        if (level <= 1) return 0f;
        return baseEXP * (float)(Math.Floor(Math.Pow(level - 1, scaleFactor)) + offset);
    }

    public float GetEXPReward(BaseEnemy enemy)
    {
        float levelDifferenceFactor = Math.Max(0f, Math.Min(0.5f, 0.1f * (enemy.Level - currentLevel)));
        Console.WriteLine("Level Diff: "+levelDifferenceFactor);
        Console.WriteLine("Enemy Level: "+enemy.Level);
        Console.WriteLine("Enemy Diff: "+enemy.DifficultyMultiplier);
        return 10f * enemy.Level * enemy.DifficultyMultiplier * (1f + levelDifferenceFactor);
    }

    public void Draw()
    {

        Raylib.DrawText($"Current Level: {currentLevel}", 10, 900, 20, Color.Black);
        Raylib.DrawText($"Current EXP: {playerEXP} / Needed EXP: {GetEXPForLevel(currentLevel+1)}", 10, 950, 20, Color.Black);
    }
}