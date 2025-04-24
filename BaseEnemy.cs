using System.Numerics;
using Raylib_cs;

public class BaseEnemy
{
    private Vector2 InitialPosition, FinalPosition, CurrentPosition, TextureSize;
    private int Level;
    private float Speed = 30f, TimeToReachFinalPos = 2f;
    private bool reachedFinalPosition;
    private float PosTimer = 0, BulletTimer;
    private float shootDelay = 1f;

    public int HP = 10;

    public BaseEnemy(Vector2 initialPos, Vector2 finalPos, int level, Vector2 textureSize)
    {
        InitialPosition = initialPos;
        FinalPosition = finalPos;
        Level = level;
        TextureSize = textureSize;
    }
    
    public void Initialize()
    {
        CurrentPosition = InitialPosition;
        reachedFinalPosition = false;
        PosTimer = 0;
        BulletTimer = 0;
    }

    public void Update(float deltaTime)
    {
        if(!reachedFinalPosition)
        {
            PosTimer += deltaTime;
            CurrentPosition = Vector2.Lerp(InitialPosition, FinalPosition, PosTimer/TimeToReachFinalPos);

            if(PosTimer >= TimeToReachFinalPos)
                reachedFinalPosition = true;
        }

        BulletTimer += deltaTime;

        if (BulletTimer > shootDelay)
        {
            BulletTimer = 0;
            
            Game.SpawnBullet(new Bullet(CurrentPosition + new Vector2(0, TextureSize.Y), true, Level));
        }

        
        foreach (var i in Game.bullets)
        {
            if (i.IsDead || i.IsEnemy)
                continue;
            
            var colliding = Raylib.CheckCollisionCircles(CurrentPosition, TextureSize.X, i.position, i.Size);

            if (colliding)
            {
                HP--;
                Game.DespawnBullet(i);
            }
        }
    }

    public void Draw()
    {
        Raylib.DrawTextureV(Program.EnemyTexture,CurrentPosition, Color.White);
    }
}