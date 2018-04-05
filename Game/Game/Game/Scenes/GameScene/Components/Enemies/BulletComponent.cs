using System;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Game.Scenes.GameScene.Components.Enemies
{
    public class BulletComponent : Component, IUpdatable
    {
        public float Speed = 80.0f;
        public float Angle;
        public float DistanceToAutoDestroy = 1200f;

        private Collider _collider;
        private Vector2 _velocity;
        private float _currentDistance = 0;

        public override void onAddedToEntity()
        {
            _collider = this.getComponent<Collider>();
            _velocity = Mathf.angleToVector(Angle, 1f);
        }

        public void update()
        {

            Vector2 vel = _velocity * Speed * Time.deltaTime;
            _currentDistance = vel.LengthSquared();
            
            
            if (_collider.collidesWithAny(ref vel, out var result) || _currentDistance > Math.Pow(DistanceToAutoDestroy, 2))
            {
                entity.destroy();
            }
            transform.position += vel;
        }

        public override void debugRender(Graphics graphics)
        {
            Vector2 velocity = Mathf.angleToVector(Angle, Speed * Time.deltaTime * 60f);
            graphics.batcher.drawLine(transform.position, transform.position + velocity, Color.Blue);
            base.debugRender(graphics);
        }
    }
}