using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace Game.Game.Static
{
    public static class VirtualInputGlobal
    {
        public static VirtualJoystick Direction;

        public static void Setup()
        {
            Direction = new VirtualJoystick(false);
            Direction.addGamePadDPad()
                .addKeyboardKeys(
                    VirtualInput.OverlapBehavior.TakeNewer,
                    Keys.Left, Keys.Right, Keys.Up, Keys.Down)
                .addKeyboardKeys(
                    VirtualInput.OverlapBehavior.TakeNewer,
                    Keys.A, Keys.D, Keys.W, Keys.S);
            Direction.nodes.Add(new InvertedYGamePadLeftStick());
        }
    }

    public class InvertedYGamePadLeftStick : VirtualJoystick.Node
    {
        public int gamepadIndex;
        public float deadzone;

        public InvertedYGamePadLeftStick(int gamepadIndex = 0, float deadzone = Input.DEFAULT_DEADZONE)
        {
            this.gamepadIndex = gamepadIndex;
            this.deadzone = deadzone;
        }

        public override Vector2 value
        {
            get
            {
                Vector2 val = Input.gamePads[gamepadIndex].getLeftStick(deadzone);
                val.Y *= -1;
                return val;
            }
        }
    }

    public class InvertedYGamePadRightStick : VirtualJoystick.Node
    {
        public int gamepadIndex;
        public float deadzone;

        public InvertedYGamePadRightStick(int gamepadIndex = 0, float deadzone = Input.DEFAULT_DEADZONE)
        {
            this.gamepadIndex = gamepadIndex;
            this.deadzone = deadzone;
        }

        public override Vector2 value
        {
            get
            {
                Vector2 val = Input.gamePads[gamepadIndex].getRightStick(deadzone);
                val.Y *= -1;
                return val;
            }
        }
    }
}
