using Game.Game.Scenes.GameScene.Entities.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.FSM;

namespace Game.Game.Scenes.GameScene.Components.Enemies.EnemyState
{
    public class EnemyState_Default : State<EnemyComponent>
    {
        public float TimeToStartFire = 1.0f;
        public float FireTime = 0.5f;

        protected float CurrentTime = 0;

        public EnemyState_Default()
        {
            CurrentTime = -TimeToStartFire;
        }

        public virtual void Fire()
        {
            // Texture2D sprite = entity.scene.content.Load<Texture2D>(Content.Textures.Enemies.bullet);

            var bullet = CreateBullet(_context.BulletSprite);
            bullet.getComponent<BulletComponent>().Angle = PlayerAngle();
        }

        protected float PlayerAngle()
        {
            return Mathf.angleBetweenVectors(Vector2.UnitY, _context.PlayerDir());
        }

        protected BulletEntity CreateBullet(Texture2D texture)
        {
            var bullet = _context.entity.scene.addEntity(new BulletEntity(_context.BulletSprite));
            bullet.position = _context.transform.position;
            return bullet;
        }

        protected virtual void UpdateFire(float dt)
        {
            CurrentTime += dt;
            if (CurrentTime > FireTime)
            {
                Fire();
                CurrentTime -= FireTime;
            }
        }

        public override void update(float deltaTime)
        {
            UpdateFire(deltaTime);
        }
    }
}