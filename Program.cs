using System.Numerics;
using Raylib_cs;
using System.Linq;

public class Program
{
    public static Game game;

    public static Texture2D PlayerTexture;
    public static Dictionary<int, Texture2D> BulletTextures = new();
    public static Texture2D EnemyTexture;

    public static void Main(string[] args)
    {
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint | ConfigFlags.MaximizedWindow | ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1366, 768, "Hehe");
        Raylib.MaximizeWindow();
        Raylib.SetExitKey(0);

        PlayerTexture = Raylib.LoadTexture("assets/playerSprite.png");
        EnemyTexture = Raylib.LoadTexture("assets/enemySprite.png");
        var bullet1 = Raylib.LoadTexture("assets/playerBulletLvl1.png");
        var bullet2 = Raylib.LoadTexture("assets/playerBulletLvl2.png");
        var bullet3 = Raylib.LoadTexture("assets/playerBulletLvl3.png");
        var bullet4 = Raylib.LoadTexture("assets/playerBulletLvl4.png");
        var bullet5 = Raylib.LoadTexture("assets/playerBulletLvl5.png");
        
        BulletTextures.Add(0, bullet1);
        BulletTextures.Add(1, bullet2);
        BulletTextures.Add(2, bullet3);
        BulletTextures.Add(3, bullet4);
        BulletTextures.Add(4, bullet5);
        
        game = new Game();
        game.Initialize();

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape) && Game.CurrentState == Game.GameStates.Menu)
                break;
                
            var deltaTime = Raylib.GetFrameTime();

            //update
            game.Update(deltaTime);

            //rendering/drawing
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);
            game.Draw();


            Raylib.DrawFPS(4, 4);
            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(PlayerTexture);
        
        foreach(Texture2D texture in BulletTextures.Values)
        {
            Raylib.UnloadTexture(texture);
        }
        Raylib.CloseWindow();
    }
}
