using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class SimpleRigidComponent : Component, IUpdatable
    {
        public Vector2 Velocity = Vector2.Zero;
        public float Damping = 0.9f;
        public float DampHardStop = 0.03f;

        // Gravity
        private float _gravity;
        public float Gravity
        {
            get => _gravity;
            set
            {
                _gravityVec = new Vector2(0, value * 100);
                _gravity = value;
            }
        }
        public bool HasGravity = true;
        private Vector2 _gravityVec;

        [Inspectable]
        private bool _onGround = false;
        public bool OnGround => _onGround;
        public float OnGroundFriction = 1f;

        public int CollidesWith = 1 << 0;

        // Cached components
        private Collider _selfCollider;
        
        public SimpleRigidComponent()
        {
            Gravity = 7.8f;
        }

        public override void onAddedToEntity()
        {
            _selfCollider = entity.getComponent<Collider>();
        }

        public void update()
        {
            if (HasGravity)
            {
                Velocity += _gravityVec * Time.deltaTime;
                Velocity.X *= Damping;
            }
            else
            {
                Velocity *= Damping;
            }
            Vector2 motion = Velocity;
            motion *= Time.deltaTime;
            if (_selfCollider.collidesWithAny(ref motion, out var collisionResult))
            {
                Vector2 translationVec = collisionResult.minimumTranslationVector;
                Velocity.X = Math.Abs(translationVec.X) > 0.01f ? 0 : Velocity.X;
                Velocity.Y = Math.Abs(translationVec.Y) > 0.01f ? 0 : Velocity.Y;
                if (translationVec.Y > 0 && HasGravity)
                {
                    _onGround = true;
                    Velocity.X *= OnGroundFriction;
                }
                else
                    _onGround = false;
            }
            else
            { 
                _onGround = false;
            }
            transform.position += motion;
        }

        public void AddForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}
