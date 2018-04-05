using Game.Game.Scenes.GameScene.Components;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;

namespace Game.Game.Scenes.GameScene.Entities
{
    public class PlatformPlayerEntity : Entity
    {
        private readonly Texture2D _sprite;

        public PlatformPlayerEntity(Texture2D sprite)
        {
            _sprite = sprite;

            Sprite spriteComp = new Sprite(_sprite)
            {
                renderLayer = -2
            };
            
            BoxCollider collider = new BoxCollider()
            {
                physicsLayer = 2,
                collidesWithLayers = 1 << 0
            };
            
            SimpleRigidComponent rigid = new SimpleRigidComponent();
            PlatformPlayerComponent controller = new PlatformPlayerComponent()
            {
                MovementSpeed = 50f
            };

            addComponent(spriteComp);
            addComponent(collider);
            addComponent(controller);
            addComponent(rigid);
            addComponent(new PlayerUpgradesComponent());

        }

        public override void onAddedToScene()
        {


            

            base.onAddedToScene();
        }
    }
}