using UnityEngine;

public interface IPhase
{
    GameObject GetNextSpawnObject();     // 生成するブロックまたはモアイのPrefabを返す
}