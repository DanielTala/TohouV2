using System.Numerics;
using Raylib_cs;

public class BaseItem
{
    public float speed;
    public Vector2 direction;
    public Vector2 initialPosition;
    public float size = 30;
    public Vector2 position;
    public BaseItem(float speed, Vector2 initialPosition)
    {
        this.speed = speed;
        this.initialPosition = initialPosition;
    }

    public void Initialize()
    {
        var xDirection = Raylib.GetRandomValue(1, 100);
        var yDirection = Raylib.GetRandomValue(1, 100);
        direction = Raymath.Vector2Normalize(new Vector2(xDirection, yDirection));
        position = initialPosition;
    }

    public void Update(float deltaTime)
    {
        position += direction * speed * deltaTime;

        if(position.X > Raylib.GetScreenWidth() - size/2) // reflect to left
        {
            direction = Vector2.Reflect(direction, new Vector2(1, 0));
        }
        else if (position.X < size / 2) // reflect to right
        {
            direction = Vector2.Reflect(direction, new Vector2(-1, 0));
        }
        else if (position.Y > Raylib.GetScreenHeight() - size/2) // reflect up
        {
            direction = Vector2.Reflect(direction, new Vector2(0, 1));
        }
        else if (position.Y < size / 2) // reflect down
        {
            direction = Vector2.Reflect(direction, new Vector2(0, -1));
        }
    }

    public void Draw()
    {
        Raylib.DrawCircleLinesV(position, size, Color.Pink);
    }
}