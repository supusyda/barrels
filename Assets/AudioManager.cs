using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private SoundLiberi sfx;
    [SerializeField] private SoundLiberi music;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private float musicFadeDurrationSec = 1;
    [SerializeField] AudioMixer audioMixer;
    // private AudioSource source;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        Event.OnDoneSpawnBarrel.AddListener(OnDoneSpawnBarrel);
        Event.OnWinLevel.AddListener(OnWinLevel);
    }


    private void OnDisable()
    {
        Event.OnDoneSpawnBarrel.RemoveListener(OnDoneSpawnBarrel);
        Event.OnWinLevel.RemoveListener(OnWinLevel);
    }
    private void OnWinLevel()
    {

        PlaySound(AudioName.OnWinSound);
    }

    private void OnDoneSpawnBarrel()
    {
        PlaySound(AudioName.OnBarrelSpawn);
    }

    public void PlaySound(string soundName)
    {
        sfxSource.pitch = Random.Range(.80f, 1.2f);
        sfxSource.PlayOneShot(sfx.GetAudioClipsFromName(soundName));
    }
    public void PlayMusic(string musicName)
    {
        StartCoroutine(CrossFadeMusic(musicName));
    }

    IEnumerator CrossFadeMusic(string musicName)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;

        }
        musicSource.clip = music.GetAudioClipsFromName(musicName);
        musicSource.Play();
        percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;

        }
    }
    public void UpdateMusicVol(float vol)
    {
        audioMixer.SetFloat(AudioMixName.MUSIC, vol);
    }
    public void UpdateSfxVol(float vol)
    {
        audioMixer.SetFloat(AudioMixName.SFX, vol);
    }
    public void SaveMixer()
    {
        if (audioMixer == null) return;
        audioMixer.GetFloat(AudioMixName.MUSIC, out float musicVol);
        audioMixer.GetFloat(AudioMixName.SFX, out float sfxVol);

        PlayerPrefs.SetFloat("musicVol", musicVol);
        PlayerPrefs.SetFloat("sfxVol", sfxVol);
        PlayerPrefs.Save();
    }

}
