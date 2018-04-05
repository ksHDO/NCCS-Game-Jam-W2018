using Nez;
using Nez.AI.GOAP;

namespace Game.Game.Scenes.GameScene.Entities
{
    public class ContainerEntity : Entity
    {
        public static implicit operator Transform(ContainerEntity e)
        {
            return e.transform;
        }

        public void DestroyChildren()
        {
            int childrenCount = childCount;
            Entity[] children = new Entity[childrenCount];
            for (int i = 0; i < childrenCount; ++i)
            {
                children[i] = transform.getChild(i).entity;
            }

            foreach (var child in children)
            {
                child.destroy();
            }
        }

        public void EnableChildrenComponents<T>(bool value) where T : Component
        {
            SetChildrenComponents<T>((val) => { val.setEnabled(value); });
        }

        public void SetChildrenComponents<T>(System.Action<T> componentAction) where T : Component
        {
            int childrenCount = childCount;
            for (int i = 0; i < childrenCount; ++i)
            {
                T component = transform.getChild(i).entity.getComponent<T>();
                if (component != null)
                    componentAction(component);
            }
        }
    }
}