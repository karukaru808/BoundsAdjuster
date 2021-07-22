using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CIFER.Tech.BoundsAdjuster
{
    public static class BoundsAdjuster
    {
        public static void AdjustBounds(Animator animator)
        {
            var scale = GetMaxExtent(animator);

            if (animator.isHuman)
            {
                var humanScale = animator.humanScale;
                scale = humanScale > scale ? humanScale : scale;
            }

            foreach (var smr in animator.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                var bounds = smr.localBounds;
                bounds.extents = Vector3.one * (scale * 1.25f);
                smr.localBounds = bounds;
            }
        }

        private static float GetMaxExtent(Component animator)
        {
            var smrs = animator.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var smr in smrs)
            {
                PrefabUtility.RevertPropertyOverride(new SerializedObject(smr).FindProperty("m_AABB"),
                    InteractionMode.AutomatedAction);
            }

            var extents = smrs.Select(smr => smr.localBounds.extents);
            var x = extents.Select(extent => extent.x).Max();
            var y = extents.Select(extent => extent.y).Max();
            var z = extents.Select(extent => extent.z).Max();

            var value = x;
            if (y > value)
            {
                value = y;
            }

            if (z > value)
            {
                value = z;
            }

            return value;
        }
    }
}