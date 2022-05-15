#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    [CustomEditor(typeof(Player))]
    public class PlayerUpgradesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Earn Exp"))
            {
                (target as Player).EarnExp(100f);
            }
        }
    }
}

#endif