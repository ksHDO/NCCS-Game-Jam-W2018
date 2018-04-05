using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Game.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;

namespace Game.Game.Scenes.GameScene.Components
{
    public enum PlatformPlayerState
    {
        Standing, Jumping, InAir, Inactive
    }

    public class PlatformPlayerComponent : SimpleStateMachine<PlatformPlayerState>
    {
        public float JumpForce = 360f;
        public float JumpForce2 = 180f;
        public float MovementSpeed = 100f;
        public float HighJumpTime = 0.07f;

        private SimpleRigidComponent _rigidbody;

        public PlatformPlayerComponent()
        {
            initialState = PlatformPlayerState.Standing;
        }

        public override void onAddedToEntity()
        {
            _rigidbody = entity.getComponent<SimpleRigidComponent>();
        }

        public void Standing_Enter()
        {

        }

        public void Standing_Tick()
        {
            if (Input.isKeyDown(Keys.Space))
            {
                _rigidbody.AddForce(new Vector2(0, -JumpForce));
                currentState = PlatformPlayerState.Jumping;
            } else if (!_rigidbody.OnGround)
            {
                currentState = PlatformPlayerState.InAir;
            }
        }

        public void Standing_Exit()
        {

        }

        public void Jumping_Enter()
        {

        }

        public void Jumping_Tick()
        {
            if (Input.isKeyReleased(Keys.Space))
            {
                currentState = PlatformPlayerState.InAir;
            }
            else if (elapsedTimeInState > HighJumpTime)
            {
                _rigidbody.AddForce(new Vector2(0, -JumpForce2));
                currentState = PlatformPlayerState.InAir;
            }
        }

        public void Jumping_Exit()
        {

        }

        public void InAir_Enter()
        {

        }

        public void InAir_Tick()
        {
            if (_rigidbody.OnGround)
            {
                currentState = PlatformPlayerState.Standing;
            }
        }

        public void InAir_Exit()
        {

        }


        public override void update()
        {
            base.update();

            Vector2 inputDir = VirtualInputGlobal.Direction;
            inputDir.Y = 0;
            _rigidbody.AddForce(inputDir * MovementSpeed);
        }
    }
}
