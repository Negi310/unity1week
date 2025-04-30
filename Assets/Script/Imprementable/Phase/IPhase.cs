using UnityEngine;

public interface IPhase
{
    void Initialize();                    // Phase開始時に呼ばれる初期化処理
    GameObject GetNextSpawnObject();     // 生成するブロックまたはモアイのPrefabを返す
}