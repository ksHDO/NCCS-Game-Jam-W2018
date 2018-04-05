using Game.Game.Classes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tweens;

namespace Game.Game.Scenes.GameScene.Components
{
    public class CameraFollowComponent : Component, IUpdatable
    {
        public Transform ObjectToFollow;
        public Bounds Deadzone = new Bounds(50, 50);
        //public float CameraWidth = 1280;
        //public float CameraHeight = 720;
        public Bounds MapEdge;
        public float SmoothTime = 0.2f;

        private Camera _camera;
        public CameraFollowComponent(Transform objectToFollow)
        {
            ObjectToFollow = objectToFollow;
        }

        public override void onAddedToEntity()
        {
            _camera = this.getComponent<Camera>();
            
            transform.position = ObjectToFollow.position;
        }

        public void update()
        {
            Vector2 cameraPos = transform.position;
            Vector2 objectToFollowPos = ObjectToFollow.position;

            float halfWidth = _camera.bounds.width / 2.0f;
            float halfHeight = _camera.bounds.height / 2.0f;

            if (!Deadzone.Contains(objectToFollowPos))
            {
                cameraPos = Lerps.ease(EaseType.QuadInOut, cameraPos, objectToFollowPos, SmoothTime, 1.0f);
                // cameraPos = Lerps.lerpDamp(cameraPos, objectToFollowPos, SmoothSpeed);
            }
            if (cameraPos.X - halfWidth < MapEdge.Min.X)
            {
                cameraPos.X = MapEdge.Min.X + halfWidth;
            }
            if (cameraPos.Y - halfHeight < MapEdge.Min.Y)
            {
                cameraPos.Y = MapEdge.Min.Y + halfHeight;
            }
            if (cameraPos.X + halfWidth > MapEdge.Max.X)
            {
                cameraPos.X = MapEdge.Max.X - halfWidth;
            }
            if (cameraPos.Y + halfHeight > MapEdge.Max.Y)
            {
                cameraPos.Y = MapEdge.Max.Y - halfHeight;
            }


            

            transform.position = cameraPos;
        }
    }
}