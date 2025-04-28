using System.Numerics;
using Raylib_cs;

public class EnemySpawner
{
    public static List<BaseEnemy> Enemies = new List<BaseEnemy>();
    public static List<BaseEnemy> ToRemove = new List<BaseEnemy>();
    public int enemyCount = 100;
    public float enemySpawnDelay = .25f, enemySpawnTimer;

    public static void SpawnEnemy(BaseEnemy e)
    {
        Enemies.Add(e);
    }

    public static void DespawnEnemy(BaseEnemy e)
    {
        ToRemove.Add(e);
    }

    public void DespawnAllEnemy()
    {
        Enemies.Clear();
    }
    public void Initialize()
    {
        enemySpawnTimer = 0;
    }

    public void Update(float deltaTime)
    {
        // if (Enemies.Count < enemyCount)
        // {
        //     enemySpawnTimer += deltaTime;

        //     if (enemySpawnTimer > enemySpawnDelay)
        //     {
        //         enemySpawnTimer = 0;

        //         var screenWidth = Raylib.GetScreenWidth();
        //         var screenHeight = Raylib.GetScreenHeight();
        //         var initialPos = new Vector2(screenWidth + Raylib.GetRandomValue(5, 20), (screenHeight / 2) - Raylib.GetRandomValue(5, 20));
        //         var randomHeight = Raymath.Clamp(screenHeight/2 - Raylib.GetRandomValue(0, screenHeight/2), 10, screenHeight/2);
        //         var finalPos = new Vector2(Raylib.GetRandomValue(5, screenWidth - 5), randomHeight);
        //         BaseEnemy enemy = new BaseEnemy(initialPos, finalPos, 0, new Vector2(Program.EnemyTexture.Width, Program.EnemyTexture.Height));
        //         enemy.Initialize();
        //         Enemies.Add(enemy);
        //     }
        // }
        
        foreach(BaseEnemy enemy in Enemies)
        {
            enemy.Update(deltaTime);

            if (enemy.HP <= 0)
            {
                DespawnEnemy(enemy);
            }
        }

        foreach(var i in ToRemove)
        {
            Enemies.Remove(i);
        }

    }

    public void Draw()
    {
        Raylib.DrawText($"Enemy Count: {Enemies.Count}", 4, 60, 20, Color.Green);
        foreach(BaseEnemy enemy in Enemies)
        {
            enemy.Draw();
        }
    }
}