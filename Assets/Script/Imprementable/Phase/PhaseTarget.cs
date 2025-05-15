using UnityEngine;

[System.Serializable]
public class PhaseTarget
{
    public GameObject prefab;
    public float baseWeight = 1f; // 基本の重み（設定値）
    [HideInInspector] public int appearCount = 0; // 出現回数
}