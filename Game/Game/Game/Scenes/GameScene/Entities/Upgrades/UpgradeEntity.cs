using Game.Game.Scenes.GameScene.Components;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Game.Game.Scenes.GameScene.Entities.Upgrades
{
    public class UpgradeEntity : Entity
    {
        public UpgradeEntity(Texture2D texture, PlayerUpgrade upgrade)
        {
            addComponent(new Sprite(texture));
            addComponent(new SnapToGroundComponent()
            {

            });
            addComponent(new UpgradeComponent(upgrade));
            addComponent(new BoxCollider()
            {
                collidesWithLayers = 1 << 1,
                isTrigger = true
            });
            
        }
    }
}