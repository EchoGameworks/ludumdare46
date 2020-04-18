using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterBase))]
public class CharacterBaseEditor : Editor {

    public override void OnInspectorGUI()
    {
        CharacterBase myTarget = (CharacterBase)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Take Damage"))
        {
            myTarget.TakeDamage(10f);
        }
    }
}
