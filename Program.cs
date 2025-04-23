using System.Numerics;
using Raylib_cs;

public class Program
{
    public static Game game;

    public static Texture2D PlayerTexture;
    public static Texture2D BulletTexture;

    public static void Main(string[] args)
    {
        Raylib.SetConfigFlags(ConfigFlags.VSyncHint | ConfigFlags.MaximizedWindow | ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1366, 768, "Hello World");
        Raylib.MaximizeWindow();
        Raylib.SetExitKey(0);

        PlayerTexture = Raylib.LoadTexture("assets/player1.png");
        BulletTexture = Raylib.LoadTexture("assets/bullet1.png");

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
            Raylib.ClearBackground(Color.DarkBlue);
            game.Draw();


            Raylib.DrawFPS(4, 4);
            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(PlayerTexture);
        Raylib.UnloadTexture(BulletTexture);
        Raylib.CloseWindow();
    }
}
