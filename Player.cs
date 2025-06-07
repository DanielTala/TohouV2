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
    public int BulletLevel;

    public void Initialize()
    {
        HP = 500;
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

        Position.X = Raymath.Clamp(Position.X, Size / 2, Raylib.GetScreenWidth() - Size / 2);
        Position.Y = Raymath.Clamp(Position.Y, Size / 2, Raylib.GetScreenHeight() - Size / 2);

        timer += deltaTime;

        if (timer >= .05f)
        {
            timer = 0;
            Game.SpawnBullet(new Bullet(Position, false, BulletLevel));
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

        foreach (var i in Game.ReturnItemList())
        {
            var colliding = Raylib.CheckCollisionCircles(Position, Size, i.position, i.size);

            if (colliding)
            {
                i.ItemCollided();
            }
        }
    }

    public void Draw()
    {
        var textureSize = new Vector2(Program.PlayerTexture.Width, Program.PlayerTexture.Height);
        Raylib.DrawTextureV(Program.PlayerTexture, Position - (textureSize / 2), Color.White);
        Raylib.DrawCircleLinesV(Position, Size, Color.Red);
    }

    public void AddHP(int value)
    {
        HP += value;
    }

    public void AddSpeed(float value)
    {
        Speed += value;
    }
}