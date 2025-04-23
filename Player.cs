using System.Globalization;
using System.Numerics;
using Raylib_cs;

public class Player
{
    public Vector2 Position = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() - 200);
    public float Speed = 512;
    public float Size = 64;

    private float timer = 0;
    public int HP;

    public void Initialize()
    {
        HP = 5;
    }

    public void Update(float deltaTime)
    {
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            Position.Y -= Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            Position.Y += Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A))
        {
            Position.X -= Speed * deltaTime;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            Position.X += Speed * deltaTime;
        }

        Position.X = Raymath.Clamp(Position.X, Size/2, Raylib.GetScreenWidth() - Size/2);
        Position.Y = Raymath.Clamp(Position.Y, Size/2, Raylib.GetScreenHeight() - Size/2);
        
        timer += deltaTime;

        if (timer >= .25f)
        {
            timer = 0;
            Game.SpawnBullet(new Bullet(Position, false));
        }

        foreach (var i in Game.bullets)
        {
            if (i.IsDead || !i.IsEnemy)
                continue;
            
            var colliding = Raylib.CheckCollisionCircles(Position, Size, i.position, i.Size);

            if (colliding)
            {
                HP--;
                Game.DespawnBullet(i);
            }
        }
    }

    public void Draw()
    {            
        var textureSize = new Vector2(Program.PlayerTexture.Width, Program.PlayerTexture.Height);
        Raylib.DrawTextureV(Program.PlayerTexture, Position - (textureSize / 2), Color.White);
        Raylib.DrawCircleLinesV(Position, Size, Color.Red);
    }
}