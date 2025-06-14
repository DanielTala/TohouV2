using System.Numerics;
using Raylib_cs;

public class EnemySpawner
{
    
    public static List<BaseEnemy> Enemies = new List<BaseEnemy>();
    public static List<BaseEnemy> ToRemove = new List<BaseEnemy>();
    public int enemyCount = 100;
    public float enemySpawnDelay = .25f, enemySpawnTimer;

    public Vector2 patternSpacingX = new Vector2(10, 100), patternSpacingY = new Vector2(10, 100);

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
        if (Enemies.Count <= 0)
        {
            var positions = ReturnEnemyPatterns(0);

            foreach (var pos in positions)
            {
                BaseEnemy enemy = new BaseEnemy(pos, new Vector2(pos.X, pos.Y + (Raylib.GetScreenHeight() / 2)), 1, new Vector2(Program.EnemyTexture.Width, Program.EnemyTexture.Height));
                enemy.Initialize();
                Enemies.Add(enemy);
            }
        }
        
        foreach (BaseEnemy enemy in Enemies)
        {
            enemy.Update(deltaTime);

            if (enemy.HP <= 0)
            {
                ItemSpawner.OnEnemyDeath(enemy);
                DespawnEnemy(enemy);
            }
        }

        foreach(var i in ToRemove)
        {
            Enemies.Remove(i);
        }

    }


    public List<Vector2> ReturnEnemyPatterns(int patternNumber)
    {
        var enemyPattern = new List<Vector2>();
        switch (patternNumber)
        {
            case 0:
                var randomXSpacing = Raylib.GetRandomValue((int)patternSpacingX.X, (int)patternSpacingX.Y);
                var randomYSpacing = Raylib.GetRandomValue((int)patternSpacingY.X, (int)patternSpacingY.Y);
                var vectorCount = 0;
                var currentLine = 0;

                for (int i = 0; i < 7; i++)
                {
                    var saveVector = new Vector2(0, 0);
                    if (i == 0)
                    {
                        saveVector = new Vector2(Raylib.GetScreenWidth() / 2, -10f);
                    }
                    else
                    {
                        if (vectorCount == 2)
                        {
                            vectorCount = 0;
                            currentLine++;
                        }
                        saveVector = new Vector2(Raylib.GetScreenWidth() / 2 - ((randomXSpacing * (currentLine + 1)) * (i % 2 != 0 ? 1 : -1)), -10f - (randomYSpacing * (currentLine + 1)));

                        vectorCount++;

                    }

                    enemyPattern.Add(saveVector);
                }
                break;
        }

        return enemyPattern;
    }

    public void Draw()
    {
        Raylib.DrawText($"Enemy Count: {Enemies.Count}", 4, 60, 20, Color.Green);
        foreach (BaseEnemy enemy in Enemies)
        {
            enemy.Draw();
        }
    }
}