using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Game.Scenes.GameScene.Components;
using Game.Game.Scenes.GameScene.Entities.Upgrades;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Tweens;

namespace Game.Game.Scenes.GameScene.SceneComponents
{
    public class PlayerManagerComponent : SceneComponent, IUpdatable
    {
        public PlatformPlayerComponent PlatformPlayer;
        public FreemovePlayerComponent FreemovePlayer;

        public Effect InvertEffect;
        public CameraFollowComponent CameraFollow;

        private bool _isPlatforming = false;

        // public PostProcessor PostProcessor;
        public Action<float> IsCoolingDown;
        public Action<bool> OnTogglePlatforming;

        public float CoolDown = 2.0f;
        private float _currentCooldownTime = 0.0f;
        private bool _canToggle = true;


        public override void update()
        {
            if (PlatformPlayer.getComponent<PlayerUpgradesComponent>().ContainsUpgrade<PlayerSwitchModeUpgrade>())
            {
                _currentCooldownTime += Time.deltaTime;
                IsCoolingDown?.Invoke(_currentCooldownTime / CoolDown);
                if (_currentCooldownTime > CoolDown)
                    _canToggle = true;
                if (Input.isKeyPressed(Keys.J, Keys.X) && _canToggle)
                {

                    TogglePlatforming();
                }
            }
        }

        [InspectorCallable]
        public void TogglePlatforming()
        {
            _isPlatforming = !_isPlatforming;
            if (_isPlatforming)
            {
                // FreemovePlayer.entity.setEnabled(false);
                FreemovePlayer.transform.tweenScaleTo(new Vector2(2.0f, 0.5f), 2.0f).start();
                FreemovePlayer.getComponent<PositionBoundsLimitComponent>().setEnabled(false);
                FreemovePlayer.setEnabled(false);
                Flags.setFlag(ref PlatformPlayer.getComponent<Collider>().collidesWithLayers, 1);

                PlatformPlayer.setEnabled(true);
                // FreemovePlayer.transform.tweenScaleTo(new Vector2(5.0f, 0.5f), 0.1f).start();
                CameraFollow.ObjectToFollow = PlatformPlayer.transform;

                // _pixelGlitch.tween("verticalSize", 0.0f, 0.5f).start();
                // ScanLineEffect.tween("linesFactor", 0.0f, 1.0f).start();

                // scene.removePostProcessor(PostProcessor);
            }
            else
            {
                FreemovePlayer.setEnabled(true);
                Flags.unsetFlag(ref PlatformPlayer.getComponent<Collider>().collidesWithLayers, 1);
                // FreemovePlayer.entity.setEnabled(true);
                FreemovePlayer.transform.position = PlatformPlayer.transform.position;
                FreemovePlayer.getComponent<PositionBoundsLimitComponent>().setEnabled(true);
                PlatformPlayer.setEnabled(false);
                FreemovePlayer.transform.scale = new Vector2(0.5f);
                CameraFollow.ObjectToFollow = FreemovePlayer.transform;
                _canToggle = false;
                _currentCooldownTime = 0.0f;
                // _pixelGlitch.tween("verticalSize", 0.2f, 0.5f).start();
                // ScanLineEffect.tween("linesFactor", 800.0f, 1.0f).start();

                // scene.addPostProcessor(PostProcessor);
            }

            OnTogglePlatforming?.Invoke(_isPlatforming);
        }

    }
}
