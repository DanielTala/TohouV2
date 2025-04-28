using System.Numerics;
using Raylib_cs;

public class Game
{
    public static Player player;
    public static List<Bullet> bullets = new List<Bullet>();
    private static List<Bullet> toRemove = new();
    
    public static GameStates CurrentState = GameStates.Menu;
    private static EnemySpawner enemySpawner;
    private static LevelManager levelManager;
    public enum GameStates
    {
        Menu,
        Ingame,
        Win,
        Lose
    }

    public static void SpawnBullet(Bullet b)
    {
        bullets.Add(b);
    }

    public static void DespawnBullet(Bullet b)
    {
        b.IsDead = true;
        toRemove.Add(b);
    }

    public static void LoadLevel(int level)
    {
        player = null;
        if(enemySpawner != null)
        {
            enemySpawner.DespawnAllEnemy();
        }
        bullets.Clear();
        toRemove.Clear();

        if (CurrentState == GameStates.Ingame) 
        {
            player = new Player();
            player.Initialize();
            levelManager.Initialize(level);
        }
    }

    public void Initialize()
    {
        player = new Player();
        player.Initialize();
        enemySpawner = new EnemySpawner();
        enemySpawner.Initialize();
        SetState(GameStates.Menu);
        levelManager = new LevelManager();
    }

    public void SetState(GameStates newState)
    {
        CurrentState = newState;

        player = null;
        if(enemySpawner != null)
        {
            enemySpawner.DespawnAllEnemy();
            enemySpawner = null;
        }
        bullets.Clear();
        toRemove.Clear();

        if (CurrentState == GameStates.Ingame) 
        {
            player = new Player();
            player.Initialize();
            levelManager.Initialize(0);
            enemySpawner = new EnemySpawner();
            enemySpawner.Initialize();
        }
    }

    float timer = 0;
    public void Update(float deltaTime)
    {

        switch (CurrentState)
        {
            case GameStates.Menu:
                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    SetState(GameStates.Ingame);
                break;

            case GameStates.Ingame:
                if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                {
                    SetState(GameStates.Menu);
                    break;
                }

                player.Update(deltaTime);
                enemySpawner.Update(deltaTime);
                levelManager.Update(deltaTime);

                foreach (var i in bullets)
                {
                    i.Update(deltaTime);
                }

                foreach (var i in toRemove)
                {
                    bullets.Remove(i);
                }

                //toRemove.Clear();

                if (player.HP <= 0)
                    SetState(GameStates.Lose);

                break;

            case GameStates.Lose:
                if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    SetState(GameStates.Ingame);
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                {
                    SetState(GameStates.Menu);
                }
                break;

        }

    }

    public void Draw()
    {
        switch (CurrentState)
        {
            case GameStates.Menu:
                Raylib.DrawText("Press enter to Play", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 20, Color.Black);
                break;

            case GameStates.Ingame:
                player.Draw();
                enemySpawner.Draw();
                levelManager.Draw();
                foreach (var i in bullets)
                {
                    i.Draw();
                }

                Raylib.DrawText($"Bullets: {bullets.Count}", 4, 30, 20, Color.Green);
                Raylib.DrawText($"HP: {player.HP}", Raylib.GetScreenWidth() / 2, 30, 20, Color.Red);
                Raylib.DrawText($"Current Level: {levelManager.currentLevelIndex + 1}", Raylib.GetScreenWidth() / 2, 50, 20, Color.Yellow);
                break;
            
            case GameStates.Lose:
                Raylib.DrawText("You Lose \n Press Enter to Retry \n Press Esc to Menu", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 20, Color.Black);
                break;

            case GameStates.Win:

                break;
        }
    }
}