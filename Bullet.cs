using System.Numerics;
using Raylib_cs;

public class Bullet
{
    public float Size = 16;
    public Vector2 position;
    public float Speed = 1000;


    public bool IsEnemy;

    public bool IsDead;
    public Bullet(Vector2 pos, bool isEnemy)
    {
        position = pos;
        IsEnemy = isEnemy;
    }

    public void Initialize()
    {

    }
    public void Update(float deltaTime)
    {
        if (IsEnemy)
        {
            position.Y += deltaTime * Speed;
        }
        else
        {
            position.Y -= deltaTime * Speed;
        }


        if (position.Y < 0 || position.Y > Raylib.GetScreenHeight())
            Game.DespawnBullet(this);
    }

    public void Draw()
    {            
        Raylib.DrawCircleLinesV(position, Size, IsEnemy? Color.Magenta : Color.Yellow);

        var textureSize = new Vector2(Program.BulletTexture.Width, Program.BulletTexture.Height);
        Raylib.DrawTextureV(Program.BulletTexture, position - (textureSize /2), IsEnemy? Color.Magenta : Color.Yellow);
    }
}