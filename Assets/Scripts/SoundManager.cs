using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// todo: load files from somewhere 
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance => instance;
    // public static SoundManager Instance => instance ?? (instance = SoundManager.GetCreateSelf());

    [SerializeField] private AudioSource m_musicSource = null;
    [SerializeField] private AudioSource m_sfxSource01 = null;
    [SerializeField] private AudioSource m_sfxSource02 = null;

    [SerializeField] private AudioClip[] m_sfxClips = null;
    [SerializeField] private string[] m_clipNames = null;
    private Dictionary<string, AudioClip> m_clipsToPlay = null;

    private void Awake()
    {
        if (null == Instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }


        // init
        DontDestroyOnLoad(this.gameObject);

        //bind name and clips
        if (m_sfxClips.Length == m_clipNames.Length)
        {
            m_clipsToPlay = new Dictionary<string, AudioClip>();
            for (int i = 0; i < m_clipNames.Length; i++)
            {
                m_clipsToPlay.Add(m_clipNames[i], m_sfxClips[i]);
            }
        }
        else
        {
            //
            Debug.LogError("CLIPS ERROR, PLZ CHECK");
        }

        // m_musicSource = this.gameObject.AddComponent<AudioSource>();
        // m_sfxSource = this.gameObject.AddComponent<AudioSource>();

        // m_musicSource.loop = true;
        // m_sfxSource.loop = false;

    }

    // private static SoundManager GetCreateSelf()
    // {
    //     GameObject go = new GameObject();
    //     SoundManager mangerWa = go.AddComponent<SoundManager>();
    //     return mangerWa;
    // }

    // public void PlayMusic(AudioClip _music)
    // {
    //     m_musicSource.clip = _music;
    //     m_musicSource.Play();
    // }

    // public void PlaySfx(AudioClip _sfx)
    // {
    //     m_sfxSource.PlayOneShot(_sfx);
    //     // AudioSource.PlayClipAtPoint();
    // }
    // public void PlaySfx(AudioClip _sfx, float _volume)
    // {
    //     m_sfxSource.PlayOneShot(_sfx, _volume);
    //     // AudioSource.PlayClipAtPoint();
    // }

    [ContextMenu("play sound test")]
    public void PlaySfxTest()
    {
        AudioSource.PlayClipAtPoint(m_sfxClips[2], Vector3.zero);
    }

    public void PlaySfxTemp(string _clipName)
    {
        if (m_sfxSource01.isPlaying)
        {
            m_sfxSource02.clip = m_clipsToPlay[_clipName];
            m_sfxSource02.Play();
        }
        else
        {
            m_sfxSource01.volume = 0.6f;
            m_sfxSource01.clip = m_clipsToPlay[_clipName];
            m_sfxSource01.Play();
        }
        AudioSource.PlayClipAtPoint(m_clipsToPlay[_clipName], Vector3.zero);
    }

    // class end
}
