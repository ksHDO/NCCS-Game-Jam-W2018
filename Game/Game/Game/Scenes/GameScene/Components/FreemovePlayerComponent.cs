using Game.Game.Classes;
using Game.Game.Static;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class FreemovePlayerComponent : Component, IUpdatable
    {
        public float MovementSpeed = 20f;

        private SimpleRigidComponent _rigidbody;

        public override void onAddedToEntity()
        {
            _rigidbody = entity.getOrCreateComponent<SimpleRigidComponent>();
            _rigidbody.HasGravity = false;
        }

        public void update()
        {
            Vector2 inputDir = VirtualInputGlobal.Direction;
            
            _rigidbody.AddForce(inputDir * MovementSpeed);
        }
    }
}