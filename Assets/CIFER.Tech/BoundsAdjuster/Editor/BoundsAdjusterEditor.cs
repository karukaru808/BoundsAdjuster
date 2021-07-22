using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace CIFER.Tech.BoundsAdjuster.Editor
{
    public class BoundsAdjusterEditor : EditorWindow
    {
        private static Animator _animator;

        [MenuItem("CIFER.Tech/BoundsAdjuster")]
        private static void Open()
        {
            var window = GetWindow<BoundsAdjusterEditor>("BoundsAdjuster");
            window.minSize = new Vector2(350f, 200f);
        }

        private void OnGUI()
        {
            _animator = EditorGUILayout.ObjectField("Target", _animator, typeof(Animator), true) as Animator;

            if (GUILayout.Button("実行"))
            {
                BoundsAdjuster.AdjustBounds(_animator);
            }
        }
    }
}