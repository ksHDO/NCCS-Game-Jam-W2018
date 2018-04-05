using Microsoft.Xna.Framework;

namespace Game.Game.Classes
{
    public struct Bounds
    {
        public Vector2 Min;
        public Vector2 Max;

        // Create centered bounds with dimensions
        public Bounds(float width, float height)
        {
            float halfWidth = width / 2;
            float halfHeight = height / 2;
            Min = new Vector2(-halfWidth, -halfHeight);
            Max = new Vector2(halfWidth, halfHeight);
        }

        public Bounds(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public Bounds(float minX, float minY, float maxX, float maxY)
            : this(new Vector2(minX, minY), new Vector2(maxX, maxY))
        {
        }

        // Check if a point is within bounds
        public bool Contains(Vector2 point)
        {
            return
                (point.X > Min.X && point.X < Max.X) &&
                (point.Y > Min.Y && point.Y < Max.Y);
        }
    }
}