using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class DestroyOnCollide : Component, IUpdatable
    {
        private Collider _collider;
        public override void onAddedToEntity()
        {
            _collider = this.getComponent<Collider>();
            base.onAddedToEntity();
        }

        public void update()
        {
            
        }
    }
}