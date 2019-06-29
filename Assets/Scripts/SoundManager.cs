using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance => instance ?? (instance = SoundManager.GetCreateSelf());

    private AudioSource m_musicSource = null;
    private AudioSource m_sfxSource = null;


    private void Awake()
    {
        // init
        DontDestroyOnLoad(this.gameObject);

        m_musicSource = this.gameObject.AddComponent<AudioSource>();
        m_sfxSource = this.gameObject.AddComponent<AudioSource>();

        m_musicSource.loop = true;
        m_sfxSource.loop = false;

    }

    private static SoundManager GetCreateSelf()
    {
        GameObject go = new GameObject();
        SoundManager mangerWa = go.AddComponent<SoundManager>();
        return mangerWa;
    }

    public void PlayMusic(AudioClip _music)
    {
        m_musicSource.clip = _music;
        m_musicSource.Play();
    }

    public void PlaySfx(AudioClip _sfx)
    {
        m_sfxSource.PlayOneShot(_sfx);
        // AudioSource.PlayClipAtPoint();
    }
    public void PlaySfx(AudioClip _sfx, float _volume)
    {
        m_sfxSource.PlayOneShot(_sfx, _volume);
        // AudioSource.PlayClipAtPoint();
    }



    // class end
}
