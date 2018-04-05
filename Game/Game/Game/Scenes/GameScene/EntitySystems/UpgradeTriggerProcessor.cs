using Game.Game.Scenes.GameScene.Components;
using Game.Game.Scenes.GameScene.Entities.Upgrades;
using Nez;

namespace Game.Game.Scenes.GameScene.EntitySystems
{
    public class UpgradeTriggerProcessor : EntityProcessingSystem
    {
        public UpgradeTriggerProcessor(Matcher matcher) : base(matcher)
        {
        }

        public override void process(Entity entity)
        {
            UpgradeComponent upgrade = entity.getComponent<UpgradeComponent>();
            Collider entityCollider = entity.getComponent<Collider>();
            var colliders = Physics.boxcastBroadphase(entityCollider.bounds, 1 << 1);
            foreach (var collider in colliders)
            {
                var upgrades = collider.getComponent<PlayerUpgradesComponent>();
                if (upgrades != null)
                {
                    upgrades.AddUpgrade(upgrade.Upgrade);
                    entity.destroy();
                    break;
                }
            }
        }
    }
}