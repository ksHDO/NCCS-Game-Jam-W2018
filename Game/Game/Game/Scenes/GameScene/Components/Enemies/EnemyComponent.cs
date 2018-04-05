using System;
using System.Collections.Generic;
using Game.Game.Scenes.GameScene.Components.Enemies.EnemyState;
using Game.Game.Scenes.GameScene.Entities.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;

namespace Game.Game.Scenes.GameScene.Components.Enemies
{
    public class EnemyComponent : Component, IUpdatable
    {
        public float FireRate = 5.0f;
        public float MoveSpeed = 10.0f;
        public bool IsTargetFreemove = true;
        public float TimeToStart = 0.5f;
        
        public Texture2D BulletSprite;

        public Entity Player;
        private int _bulletIndex;

        public StateMachine<EnemyComponent> State;

        private float _timeToFire = 0.0f;

        public EnemyComponent(Texture2D bulletSprite)
        {
            BulletSprite = bulletSprite;
            State = new StateMachine<EnemyComponent>(this, new EnemyState_Default());
            State.addState(new EnemyState_Shotgun());
            State.changeState<EnemyState_Shotgun>();
        }

        //public void Fire()
        //{
        //    // Texture2D sprite = entity.scene.content.Load<Texture2D>(Content.Textures.Enemies.bullet);

        //    var bullet = entity.scene.addEntity(new BulletEntity(BulletSprite));
        //    bullet.position = transform.position;
        //    bullet.getComponent<BulletComponent>().Angle = Mathf.angleBetweenVectors(Vector2.UnitY, PlayerDir()); ;
        //}

        public Vector2 PlayerDir()
        {
            return Player.position - transform.position;
        }

        public override void onEnabled()
        {
            _timeToFire = -TimeToStart;
        }

        public void update()
        {
            if (IsTargetFreemove)
            {
                State.update(Time.deltaTime);
                // Fire
                //_timeToFire += Time.deltaTime;
                //if (_timeToFire >= FireRate)
                //{
                //    _timeToFire -= FireRate;
                //    Fire();
                //}

                // Movement behavior


                // Target
                // entity.rotation = Mathf.angleBetweenVectors(Vector2.UnitY, PlayerDir());
            }

        }
    }
}