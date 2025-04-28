using Raylib_cs;

public class LevelManager
{
    public List<LevelData> levelData = new List<LevelData>()
    {
        new Level1Data(),
        new Level2Data()
    };
    public LevelData currentLevelData;
    public int currentLevelIndex;
    private float timer;
    public void Initialize(int level)
    {
        currentLevelIndex = level;
        currentLevelData = levelData[currentLevelIndex];
        currentLevelData.Initialize();
        timer = 0;
    }

    public void Update(float deltaTime)
    {
        currentLevelData.Update(deltaTime);
        timer += deltaTime;
        if (timer >= 5f)
        {
            Game.LoadLevel(1);
            timer = 0;
        }
    }

    public void Draw()
    {
        Raylib.DrawText($"Timer: {timer}", Raylib.GetScreenWidth() / 2, 70, 20, Color.Red);
        currentLevelData.Draw();
    }
}
