using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private float masterVolumeMultiplier = 0.5f;
    [SerializeField] private float effectsVolumeMultiplier = 0.5f;
    [SerializeField] private float musicVolumeMultiplier = 0.5f;

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    public float MasterVolumeMultiplier { get => masterVolumeMultiplier; set => masterVolumeMultiplier = value; }
    public float EffectsVolumeMultiplier { get => effectsVolumeMultiplier; set => effectsVolumeMultiplier = value; }
    public float MusicVolumeMultiplier { get => musicVolumeMultiplier; set => musicVolumeMultiplier = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


        

    }

    public void PlaySFX(AudioSource source, AudioClip clip, float modifier)
    {

        source.volume =  MasterVolumeMultiplier * EffectsVolumeMultiplier * modifier;
        source.pitch = Random.Range(0.9f, 1.005f);
        source.PlayOneShot(clip);
    }

    public void PlayRandomSFX(AudioSource source, AudioClip []clip, float modifier)
    {

        source.volume = MasterVolumeMultiplier * EffectsVolumeMultiplier * modifier;
        source.pitch = Random.Range(0.9f, 1.00f);
        source.PlayOneShot(GetRandomSFX(clip));
    }

    public void PlayMusic(AudioSource source, AudioClip clip, float modifier)
    {
        source.volume =  MasterVolumeMultiplier * MusicVolumeMultiplier * modifier;
        source.PlayOneShot(clip);
    }

    public void StopAudioSource(AudioSource source)
    {
        source.Stop();  
    }

    private AudioClip GetRandomSFX(AudioClip[] sfx)
    {
        return sfx[Random.Range(0, sfx.Length)];
    }
    public void SaveSoundData()
    {
        SaveSystem.UpdateAudioSettings(this);
    }

    public void LoadSoundData()
    {
        SettingsData soundData = SaveSystem.LoadSettingsData();
        if (soundData != null)
        {
            MasterVolumeMultiplier = soundData.masterVolume;
            masterVolumeSlider.value = MasterVolumeMultiplier;

            MusicVolumeMultiplier = soundData.musicVolume;
            musicVolumeSlider.value = MusicVolumeMultiplier;

            EffectsVolumeMultiplier = soundData.effectsVolume;
            effectsVolumeSlider.value = EffectsVolumeMultiplier;
        }
    }
}
