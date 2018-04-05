using System.CodeDom;
using System.Collections.Generic;
using Game.Game.Classes;
using Game.Game.Scenes.GameScene.Entities;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class PlayerUpgradesComponent : Component
    {
        private readonly List<PlayerUpgrade> _upgrades;

        public PlayerUpgradesComponent()
        {
            _upgrades = new List<PlayerUpgrade>();
        }

        public void AddUpgrade(PlayerUpgrade upgrade)
        {
            _upgrades.Add(upgrade);
        }

        public void RemoveUpgrade(PlayerUpgrade upgrade)
        {
            _upgrades.Remove(upgrade);
        }

        public PlayerUpgrade FindUpgrade<T>() where T : PlayerUpgrade
        {
            return _upgrades.Find(u => u.GetType() == typeof(T));
        }

        public bool ContainsUpgrade<T>() where T : PlayerUpgrade
        {
            return FindUpgrade<T>() != null;
        }

        public IEnumerable<PlayerUpgrade> FindUpgrades<T>() where T : PlayerUpgrade
        {
            return _upgrades.FindAll(u => u.GetType() == typeof(T));
        }
    }
}