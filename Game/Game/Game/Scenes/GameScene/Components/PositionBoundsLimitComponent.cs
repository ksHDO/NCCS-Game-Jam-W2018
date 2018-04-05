using Game.Game.Classes;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class PositionBoundsLimitComponent : Component, IUpdatable
    {
        public Bounds Bounds;


        public void update()
        {
            Vector2 pos = transform.position;
            
            pos.X = MathHelper.Clamp(pos.X, Bounds.Min.X, Bounds.Max.X);
            pos.Y = MathHelper.Clamp(pos.Y, Bounds.Min.Y, Bounds.Max.Y);

            transform.position = pos;
        }
    }
}