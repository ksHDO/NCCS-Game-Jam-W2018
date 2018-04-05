using Microsoft.Xna.Framework;
using Nez;

namespace Game.Game.Scenes.GameScene.Components
{
    public class SnapToGroundComponent : Component, IUpdatable
    {
        private bool _hasSnapped = false;
        private Collider _selfCollider;

        public override void onAddedToEntity()
        {
            _selfCollider = this.getComponent<Collider>();
        }

        public void update()
        {
            if (!_hasSnapped)
            {
                Snap();
                _hasSnapped = true;
            }
        }

        public void Snap()
        {
            Vector2 pos = transform.position;
            Vector2 bot = new Vector2(pos.X, _selfCollider.bounds.bottom);
            RaycastHit h = Physics.linecast(bot, bot + (Vector2.UnitY * 100), 1 << 0);
            if (h.collider != null)
            {
                pos.Y += h.distance;
                transform.position = pos;
            }

        }

        public override void debugRender(Graphics graphics)
        {
            Vector2 bot = new Vector2(transform.position.X, _selfCollider.bounds.bottom);
            RaycastHit h = Physics.linecast(bot, bot + (Vector2.UnitY * 100), 1 << 0);
            graphics.batcher.drawLine(bot, new Vector2(bot.X, bot.Y + h.distance), Color.Black, 1.5f);
            base.debugRender(graphics);
        }
    }
}