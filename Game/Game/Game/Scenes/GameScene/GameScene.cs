using System;
using System.Collections;
using System.Collections.Generic;
using Game.Game.Classes;
using Game.Game.Scenes.GameScene.Components;
using Game.Game.Scenes.GameScene.Components.Enemies;
using Game.Game.Scenes.GameScene.Entities;
using Game.Game.Scenes.GameScene.Entities.Enemies;
using Game.Game.Scenes.GameScene.Entities.Upgrades;
using Game.Game.Scenes.GameScene.EntitySystems;
using Game.Game.Scenes.GameScene.SceneComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.DeferredLighting;
using Nez.Sprites;
using Nez.Tiled;
using Nez.Tweens;
using Nez.UI;

namespace Game.Game.Scenes.GameScene
{
    public class GameScene : Scene
    {
        private Texture2D[] _enemyTextures;
        private Texture2D[] _bulletTextures;
        private Texture2D[] _upgradeTextures;

        private TiledMap _currentMap;
        private TiledObjectGroup _mapBoundsGroup;
        private TiledObject _currentMapBounds;
        private TiledObjectGroup _mapEnemies;
        private TiledObjectGroup _mapItems;

        private ScanlinesEffect _scanLines;
        private PixelGlitchPostProcessor _pixelGlitch;
        private VignettePostProcessor _vignette;
        private CinematicLetterboxPostProcessor _letterbox;
        private BloomPostProcessor _bloom;
        private readonly BloomSettings _bloomSettingsNone = new BloomSettings(0.8f, 1.0f, 1.2f, 1f, 1f, 1f);
        private readonly BloomSettings _bloomSettingsFlying = new BloomSettings(0.7f, 1.0f, 2.0f, 1.0f, 1.0f, 0.5f);

        private ContainerEntity _enemiesContainer;
        private ContainerEntity _bulletContainer;
        private ContainerEntity _itemsContainer;

        public override void initialize()
        {
            base.initialize();
            this.addRenderer(new DefaultRenderer());
            SetupPostProcess();

            //var deferredRenderer = addRenderer(new DeferredLightingRenderer(
            //    0, 1, 10, 20));
            //deferredRenderer.setAmbientColor(new Color(10, 10, 10));

            _enemiesContainer = addEntity(new ContainerEntity
            {
                name = "container-enemies"
            });
            EnemyEntity.Container = _enemiesContainer;
            _bulletContainer = addEntity(new ContainerEntity
            {
                name = "container-bullet"
            });
            BulletEntity.Container = _bulletContainer;
            _itemsContainer = addEntity(new ContainerEntity()
            {
                name = "container-items"
            });

            _currentMap = content.Load<TiledMap>(Content.Tilemaps.map1);
            _mapBoundsGroup = _currentMap.getObjectGroup("MapBounds");
            var startPos = _currentMap.getObjectGroup("Points").objectWithName("StartPos");
            _mapEnemies = _currentMap.getObjectGroup("Enemies");
            _mapItems = _currentMap.getObjectGroup("Upgrades");
            Texture2D freePlayerTexture = content.Load<Texture2D>(Content.Textures.tempFlightSprite);
            
            Entity map = createEntity("map");
            TiledMapComponent tiledMap = map.addComponent(new TiledMapComponent(_currentMap, "Level"));
            
            // Vector2 halfMapDimensions = new Vector2(tiledMap.width, tiledMap.height) / 2.0f;
            // map.setPosition(-halfMapDimensions);
            tiledMap.setRenderLayer(10);
            var flyingPlayer = new FreemovePlayerEntity(freePlayerTexture)
            {
                name = "player-flying"
            };
            flyingPlayer.addComponent<PositionBoundsLimitComponent>();
            this.addEntity(flyingPlayer);
            EnemyEntity.Player = flyingPlayer;

            var platformPlayer = new PlatformPlayerEntity(freePlayerTexture)
            {
                name = "player-platform"
            };
            platformPlayer.transform.position = new Vector2(startPos.x, startPos.y);
            this.addEntity(platformPlayer);

            CameraFollowComponent cameraFollow = new CameraFollowComponent(platformPlayer.transform);
            camera.addComponent(cameraFollow);

            PlayerManagerComponent p = addSceneComponent<PlayerManagerComponent>();
            p.PlatformPlayer = platformPlayer.getComponent<PlatformPlayerComponent>();
            p.FreemovePlayer = flyingPlayer.getComponent<FreemovePlayerComponent>();
            p.CameraFollow = cameraFollow;
            p.OnTogglePlatforming += OnPlatformToggle;
            p.TogglePlatforming();
            //var scanLines = content.loadEffect(@"nez\effects\Invert.mgfxo");
            
            _bulletTextures = new Texture2D[1];
            _enemyTextures = new Texture2D[1];
            _upgradeTextures = new Texture2D[1];
            _bulletTextures[0] = content.Load<Texture2D>(Content.Textures.Enemies.bullet);
            _enemyTextures[0] = content.Load<Texture2D>(Content.Textures.Enemies.enemy1);
            _upgradeTextures[0] = content.Load<Texture2D>(Content.Textures.tempFlightSprite);
            //// EnemyEntity e = new EnemyEntity(enemyTexture, bullet);
            //e.getComponent<EnemyComponent>().Player = flyingPlayer;
            //addEntity(e);

            addEntityProcessor(new UpgradeTriggerProcessor(new Matcher().all(typeof(UpgradeComponent))));

            CreateUi();
            Time.timeScale = 0.0f;
            Core.startCoroutine(WaitToStart(0.5f));

            ChangeCameraPos();
        }

        private IEnumerator WaitToStart(float time)
        {
            float currentTime = 0;
            while (currentTime < time)
            {
                currentTime += Time.altDeltaTime;
                yield return null;
            }

            Time.timeScale = 1.0f;
        }

        public override void update()
        {
            var playerManager = getSceneComponent<PlayerManagerComponent>();
            var position = playerManager.PlatformPlayer.transform.position;
            if (!playerManager.CameraFollow.MapEdge.Contains(position))
            {
                ChangeCameraPos();
            }
            base.update();
        }

        public void SpawnEnemy(TiledObject enemy)
        {
            switch (enemy.objectType)
            {
                case "Enemy1":
                    EnemyEntity e = new EnemyEntity(_enemyTextures[0], _bulletTextures[0]);
                    e.transform.position = enemy.position;
                    addEntity(e);
                    break;
            }
        }

        public void SpawnSpecificItem<T>(PlayerUpgradesComponent playerUpgrades, Texture2D texture, T upgrade, Vector2 pos) where T : PlayerUpgrade
        {
            if (playerUpgrades.ContainsUpgrade<T>()) return;
            UpgradeEntity e = new UpgradeEntity(texture, upgrade);
            e.transform.position = pos;
            addEntity(e);
        }
        public void SpawnItem(TiledObject item)
        {
            var platformPlayer = this.getSceneComponent<PlayerManagerComponent>().PlatformPlayer;
            var upgrades = platformPlayer.getComponent<PlayerUpgradesComponent>();
            switch (item.objectType)
            {
                case "upgrade_pew":
                    SpawnSpecificItem(upgrades, _upgradeTextures[0], new PewUpgrade(), item.position);
                    break;
                case "upgrade_fairy":
                    SpawnSpecificItem(upgrades, _upgradeTextures[0], new PlayerSwitchModeUpgrade(), item.position);
                    break;
            }
        }

        public void SpawnEnemies(Bounds bounds)
        {
            _enemiesContainer.DestroyChildren();
            var objects = _mapEnemies.objects;
            for (int i = 0; i < objects.Length; ++i)
            {
                Vector2 point = objects[i].position;
                if (bounds.Contains(point))
                {
                    SpawnEnemy(objects[i]);
                }
            }

            _enemiesContainer.EnableChildrenComponents<EnemyComponent>(false);
            _enemiesContainer.SetChildrenComponents<Sprite>(sprite =>
            {
                Color c = sprite.color;
                c.R = 127;
                c.G = 127;
                c.B = 127;
                sprite.color = c;
            });
            _bulletContainer.DestroyChildren();
        }

        public void SpawnItems(Bounds bounds)
        {
            _itemsContainer.DestroyChildren();
            var objects = _mapItems.objects;
            for (int i = 0; i < objects.Length; ++i)
            {
                Vector2 pos = objects[i].position;
                if (bounds.Contains(pos))
                {
                    SpawnItem(objects[i]);
                }
            }
        }

        public void ChangeCameraPos()
        {
            var playerManager = getSceneComponent<PlayerManagerComponent>();
            var position = playerManager.PlatformPlayer.transform.position;
            foreach (var tiledObject in _mapBoundsGroup.objects)
            {
                Vector2 pos = new Vector2(tiledObject.position.X, tiledObject.position.Y);
                Bounds bounds = new Bounds(pos, pos + new Vector2(tiledObject.width, tiledObject.height));
                if (bounds.Contains(position))
                {
                    _currentMapBounds = tiledObject;
                    playerManager.FreemovePlayer.getComponent<PositionBoundsLimitComponent>().Bounds = bounds;
                    playerManager.CameraFollow.MapEdge = bounds;
                    SpawnEnemies(bounds);
                    SpawnItems(bounds);
                    _letterbox.letterboxSize = 600f;
                    _letterbox.tween("letterboxSize", 0.0f, 0.01f).setDelay(0.1f).start();
                    break;
                }
            }
        }

        private void SetupPostProcess()
        {
            _scanLines = content.loadNezEffect<ScanlinesEffect>();

            _scanLines.attenuation = 0.0f;
            addPostProcessor(_vignette = new VignettePostProcessor(2));
            addPostProcessor(new PostProcessor(2, _scanLines));
            addPostProcessor(_pixelGlitch = new PixelGlitchPostProcessor(1));
            addPostProcessor(_letterbox = new CinematicLetterboxPostProcessor(2));

            _bloom = new BloomPostProcessor(1);
            addPostProcessor(_bloom);
            _bloom.settings = _bloomSettingsNone;
            // removePostProcessor(_bloom);
            
            _pixelGlitch.horizontalOffset = 0.0f;
            _pixelGlitch.verticalSize = 2f;
            _scanLines.linesFactor = 500f;
            _vignette.power = 0.8f;
            _vignette.radius = 1.0f;

            _letterbox.letterboxSize = 0.0f;
        }

        private void OnPlatformToggle(bool isPlatforming)
        {
            TweenManager.stopAllTweens(true);
            if (isPlatforming)
            {

                _scanLines.tween("attenuation", 0.0f, 0.5f).start();
                _pixelGlitch.tween("horizontalOffset", 0.0f, 0.3f).start();
                _vignette.tween("power", 0.8f, 0.5f).setEaseType(EaseType.Linear).start();
                _vignette.tween("radius", 1.0f, 0.5f).setEaseType(EaseType.Linear).start();
                //_letterbox.tween("letterboxSize", 0.0f, 0.1f).setEaseType(EaseType.Linear).start();
                _bloom.settings = _bloomSettingsNone;
                _enemiesContainer.EnableChildrenComponents<EnemyComponent>(false);
                _enemiesContainer.SetChildrenComponents<Sprite>(sprite =>
                {
                    Color c = sprite.color;
                    c.R = 127;
                    c.G = 127;
                    c.B = 127;
                    sprite.color = c;
                });
                _bulletContainer.DestroyChildren();
            }
            else
            {
                _scanLines.tween("attenuation", 0.01f, 0.5f).start();
                _pixelGlitch.tween("horizontalOffset", 0.5f, 0.5f).start();
                _vignette.tween("power", 1.0f, 0.3f).setEaseType(EaseType.Linear).start();
                _vignette.tween("radius", 1.5f, 0.3f).setEaseType(EaseType.Linear).start();
                //_letterbox.tween("letterboxSize", 20.0f, 0.1f).setEaseType(EaseType.Linear).start();
                _bloom.settings = _bloomSettingsFlying;
                // addPostProcessor(_bloom);
                _enemiesContainer.EnableChildrenComponents<EnemyComponent>(true);
                _enemiesContainer.SetChildrenComponents<Sprite>(sprite =>
                {
                    Color c = sprite.color;
                    c = Color.White;
                    sprite.color = c;
                });
            }
        }

        private void CreateUi()
        {
            Entity canvas = createEntity("canvas");
            UICanvas uiCanvas = new UICanvas();
            canvas.addComponent(uiCanvas);

            Table table = uiCanvas.stage.addElement(new Table());
            table.setFillParent(true);
            
        }

        public override void onStart()
        {
            base.onStart();
        }
    }
}