using Nez;

namespace Game.Game.Scenes.GameScene.Components.Enemies.EnemyState
{
    public class EnemyState_Shotgun : EnemyState_Default
    {
        public int Spread = 3;
        public float AngleDifDegrees = 30;

        public override void Fire()
        {
            float playerAngle = PlayerAngle();
            float angleDifRadians = AngleDifDegrees * Mathf.deg2Rad;
            float totalAngles = (Spread - 1) * angleDifRadians;
            float start = -(totalAngles / 2.0f);
            for (int i = 0; i < Spread; ++i)
            {
                var bullet = CreateBullet(_context.BulletSprite);
                bullet.BulletComponent.Angle = start + (angleDifRadians * i) + playerAngle;
            }
            
        }
    }
}