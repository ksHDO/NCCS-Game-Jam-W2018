using Game.Game.Scenes.GameScene.Components.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Game.Game.Scenes.GameScene.Entities.Enemies
{
    public class BulletEntity : Entity
    {
        public static ContainerEntity Container;

        public BulletComponent BulletComponent;

        public BulletEntity(Texture2D texture)
        {
            name = nameof(BulletEntity);
            transform.scale = new Vector2(4f);
            addComponent(new Sprite(texture));
            BulletComponent = addComponent(new BulletComponent
            {
                Speed = 340f
            });
            Collider c = addComponent(new CircleCollider()
            {
                isTrigger = true,
                
            });
            
            c.collidesWithLayers = 1 << 0;
            c.collidesWithLayers |= 1 << 1;
            transform.setParent(Container);
        }
    }
}