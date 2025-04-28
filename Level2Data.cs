using System.Numerics;
using Raylib_cs;

public class Level2Data : LevelData
{
    private float enemySpawnDelay = .25f;
    private float enemySpawnTimer = 0f; 
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        enemySpawnTimer += deltaTime;

        if(enemySpawnTimer >= enemySpawnDelay)
        {
            var screenWidth = Raylib.GetScreenWidth();
            var screenHeight = Raylib.GetScreenHeight();
            var isFromRight = Raylib.GetRandomValue(0, 100) > 50? true:false;
            var initialPos = new Vector2(isFromRight? screenWidth + Raylib.GetRandomValue(5, 20) : -Raylib.GetRandomValue(5, 20), (screenHeight / 2) - Raylib.GetRandomValue(5, 20));
            var randomHeight = Raymath.Clamp(screenHeight/2 - Raylib.GetRandomValue(0, screenHeight/2), 10, screenHeight/2);
            var finalPos = new Vector2(Raylib.GetRandomValue(5, screenWidth - 5), randomHeight);
            EnemySpawner.SpawnEnemy(new BaseEnemy(initialPos, finalPos, 1, new Vector2(Program.EnemyTexture.Width, Program.EnemyTexture.Height)));
            enemySpawnTimer = 0;
        }
    }
    public override void Draw()
    {
        base.Draw();

        Raylib.ClearBackground(Color.DarkGray);
    }
}