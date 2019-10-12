using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;     // 사운드 이름

    public AudioClip clip;      // 사운드 파일
    private AudioSource source;     // 사운드 플레이어

    public float Volumn;
    public bool loop;
    public bool playOnAwake;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;
        source.playOnAwake = playOnAwake;
    }

    public void SetVolumn()
    {
        source.volume = Volumn;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }

    public void SetPlayOnAwake()
    {
        source.playOnAwake = true;
    }

    public void SetMuteTrue()
    {
        source.mute = false;
    }

    public void SetMuteFalse()
    {
        source.mute = true;
    }
}

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]
    public Sound[] sounds;      // 배경음
    public Sound[] effectSounds;        // 효과음
    private bool isBackgroundSound;
    private bool isEffectSound;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;    // 싱글톤 사용
        InitSound();
        InitEffectSound();
    }

    private void Start()
    {
        isBackgroundSound = true;
        isEffectSound = true;
    }

    public void InitSound()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObj = new GameObject(sounds[i].name);
            sounds[i].SetSource(soundObj.AddComponent<AudioSource>());
            soundObj.transform.SetParent(this.transform);
        }
    }

    public void InitEffectSound()
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            GameObject soundObj = new GameObject(effectSounds[i].name);
            effectSounds[i].SetSource(soundObj.AddComponent<AudioSource>());
            soundObj.transform.SetParent(this.transform);
        }
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                effectSounds[i].Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolumn(string _name, float _Volumn)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Volumn = _Volumn;
                sounds[i].SetVolumn();
                return;
            }
        }
    }

    public void SetPlayOnAwake(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetPlayOnAwake();
                return;
            }
        }
    }

    public void SetBackGroundMute()
    {
        if (isBackgroundSound)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].SetMuteFalse();
            }
            isBackgroundSound = false;
        }
        else if (!isBackgroundSound)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].SetMuteTrue();
            }
            isBackgroundSound = true;
        }
    }

    public void SetEffectMute()
    {
        if (isEffectSound)
        {
            for (int i = 0; i < effectSounds.Length; i++)
            {
                effectSounds[i].SetMuteFalse();
            }
            isEffectSound = false;
        }
        else if (!isEffectSound)
        {
            for (int i = 0; i < effectSounds.Length; i++)
            {
                effectSounds[i].SetMuteTrue();
            }
            isEffectSound = true;
        }
    }

    //public void SetDestroy()
    //{
    //    Destroy(gameObject);
    //}

    private void OnDestroy()
    {
        instance = null;
    }
}
