using System.Numerics;
using Raylib_cs;

public class Bullet
{
    public float Size = 16;
    public Vector2 position;
    public float Speed = 1000;
    public bool IsEnemy;
    public bool IsDead;
    public int Level;
    public Bullet(Vector2 pos, bool isEnemy, int level)
    {
        position = pos;
        IsEnemy = isEnemy;
        Level = level;

        if(!Program.BulletTextures.ContainsKey(Level))
            Level = 0;
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

        var textureSize = new Vector2(Program.BulletTextures[Level].Width, Program.BulletTextures[Level].Height);
        Raylib.DrawTextureV(Program.BulletTextures[Level], position - (textureSize /2), IsEnemy? Color.White : Color.Yellow);
    }
}