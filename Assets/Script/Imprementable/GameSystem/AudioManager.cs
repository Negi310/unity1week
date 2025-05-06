using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SESource;
    public AudioSource BGMSource;

    [SerializeField] List<SE> seList;
    [SerializeField] List<BGM> bgmList;

    public static AudioManager I;

    public enum VolumeParam
    {
        SE,
        BGM
    }

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySE(SE.Name seName, AudioSource audioSource = null)
    {
        // 引数を指定しなかったら、sESourceから出力
        if(audioSource == null)
        {
            audioSource = SESource;
        }

        var data = seList.Find(data => data.seName == seName);
        audioSource.PlayOneShot(data.sEClip);
    }

    public void PlayBGM(BGM.Name bgmName)
    {
        var data = bgmList.Find(data => data.bgmName == bgmName);
        BGMSource.clip = data.bGMClip;
        BGMSource.Play();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }
}

[System.Serializable]
public class SE
{
    public enum Name
    {
        
    }

    public Name seName;
    public AudioClip sEClip;
}

[System.Serializable]
public class BGM
{
    public enum Name
    {
        
    }

    public Name bgmName;
    public AudioClip bGMClip;
}