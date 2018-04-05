using Game.Game.Scenes.GameScene.Entities;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class UpgradeComponent : Component
    {
        public PlayerUpgrade Upgrade;

        public UpgradeComponent(PlayerUpgrade upgrade)
        {
            Upgrade = upgrade;
        }
    }
}