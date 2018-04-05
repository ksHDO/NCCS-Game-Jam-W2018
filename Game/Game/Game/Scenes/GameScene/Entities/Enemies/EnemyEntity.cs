using Game.Game.Scenes.GameScene.Components;
using Game.Game.Scenes.GameScene.Components.Enemies;
using Game.Game.Scenes.GameScene.Components.Enemies.EnemyState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;

namespace Game.Game.Scenes.GameScene.Entities.Enemies
{
    public class EnemyEntity : Entity
    {
        public static ContainerEntity Container;
        public static FreemovePlayerEntity Player;

        public EnemyEntity(Texture2D enemyTexture, Texture2D bulletTexture)
        {
           
            scale = new Vector2(3f);
            addComponent(new Sprite(enemyTexture)
            {
                renderLayer = -1
            });
            addComponent(new EnemyComponent(bulletTexture)
            {
                FireRate = 0.5f,
                Player = Player
            });
            addComponent(new BoxCollider()
            {
                physicsLayer = 1 << 2,
                collidesWithLayers = 0
            });
            
            addComponent(new SnapToGroundComponent());
            transform.setParent(Container);
        }

        public override void onAddedToScene()
        {
            
        }

    }
}