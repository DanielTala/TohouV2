using System.Numerics;
using Raylib_cs;

public class Game
{
    public static Player player;
    public static List<Bullet> bullets = new List<Bullet>();
    private static List<Bullet> toRemove = new();
    public static GameStates CurrentState = GameStates.Menu;
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

    public void Initialize()
    {
        player = new Player();
        player.Initialize();
        SetState(GameStates.Menu);
    }

    public void SetState(GameStates newState)
    {
        CurrentState = newState;

        player = null;
        bullets.Clear();
        toRemove.Clear();

        if (CurrentState == GameStates.Ingame) 
        {
            player = new Player();
            player.Initialize();
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

                timer += deltaTime;
                if (timer >= 0.5f)
                {
                    timer = 0;
                    SpawnBullet(new Bullet(new Vector2(Raylib.GetScreenWidth() / 2, 4), true));
                }

                foreach (var i in bullets)
                {
                    i.Update(deltaTime);
                }

                foreach (var i in toRemove)
                {
                    bullets.Remove(i);
                }

                toRemove.Clear();

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

                foreach (var i in bullets)
                {
                    i.Draw();
                }

                Raylib.DrawText($"Bullets: {bullets.Count}", 4, 30, 20, Color.Green);
                Raylib.DrawText($"HP: {player.HP}", Raylib.GetScreenWidth() / 2, 30, 20, Color.Red);
                break;
            
            case GameStates.Lose:
                Raylib.DrawText("You Lose \n Press Enter to Retry \n Press Esc to Menu", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 20, Color.Black);
                break;

            case GameStates.Win:

                break;
        }
    }
}