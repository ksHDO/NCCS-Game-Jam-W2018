using Game.Game.Scenes.GameScene.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace Game.Game.Scenes.GameScene.Entities
{
    public class FreemovePlayerEntity : Entity
    {
        private readonly Texture2D _sprite;

        public FreemovePlayerEntity(Texture2D sprite)
        {
            _sprite = sprite;

            Sprite spriteComponent = new Sprite(_sprite)
            {
                renderLayer = -3
            };
            BoxCollider collider = new BoxCollider()
            {
                physicsLayer = 1 << 1,
                collidesWithLayers = 1 << 0
            };
            SimpleRigidComponent rigid = new SimpleRigidComponent()
            {
                Damping = 0.92f
            };
            FreemovePlayerComponent controller = new FreemovePlayerComponent()
            {
                MovementSpeed = 30f
            };
            transform.scale = new Vector2(0.5f);            

            addComponent(spriteComponent);
            addComponent(collider);
            addComponent(controller);
            addComponent(rigid);

        }
        public override void onAddedToScene()
        {
            

            base.onAddedToScene();
        }
    }
}