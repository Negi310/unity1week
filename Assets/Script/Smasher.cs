using System;
using UnityEngine;
using UnityEditor;

public class Smasher : MonoBehaviour
{
    // スマッシュ
    private event Action OnSmash;
    public void AddOnSmashLisntener(Action listener) => OnSmash += listener;
    public void Smash()
    {
        // スマッシュイベント発火
        OnSmash.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Smasher))]
public class SmasherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Smasher t = target as Smasher;

        if (GUILayout.Button("スマッシュ"))
        {
            t.Smash();
        }
    }
}
#endif