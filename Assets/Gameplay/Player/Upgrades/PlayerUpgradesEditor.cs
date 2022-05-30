#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CustomEditor(typeof(PlayerSpirit))]
    public class PlayerUpgradesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Earn Exp"))
            {
                (target as PlayerSpirit).EarnExp(100f);
            }
        }
    }
}

#endif