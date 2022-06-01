using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Insanely basic audio system which supports 3D sound.
/// Ensure you change the 'Sounds' audio source to use 3D spatial blend if you intend to use 3D sounds.
/// </summary>
public class AudioSystem : StaticInstance<AudioSystem> {

    #region Variable
    private static AudioSource _musicSource;
    private static AudioSource _soundsSource;

    [SerializeField]
    private List<AudioClip> music;
    private int musicIndex = 0;
    [SerializeField]
    private AudioClip clic;
    [SerializeField]
    private AudioClip hit;
    [SerializeField]
    private AudioClip bonus;
    [SerializeField]
    private AudioClip item;
    [SerializeField]
    private AudioClip penalty;
    #endregion

    protected override void Awake() {
        base.Awake();
    }

    private void Start() {
        _musicSource = GetComponentsInChildren<AudioSource>()[0];
        _soundsSource = GetComponentsInChildren<AudioSource>()[1];

        musicIndex = Random.Range(0, music.Count);
        _musicSource.clip = music[musicIndex];
        _musicSource.Play();
    }

    private void Update() {
        if (!_musicSource.isPlaying) {
            PlayNextSong();
        }
    }

    public void SetMusicVolume(float volume) {
        _musicSource.volume = volume;
    }

    public void SetSoundVolume(float volume) {
        _soundsSource.volume = volume;
    }

    private void PlayNextSong() {
        musicIndex = (musicIndex + 1) % music.Count;
        _musicSource.clip = music[musicIndex];
        _musicSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1) {
        _soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1) {
        _soundsSource.PlayOneShot(clip, vol);
    }

    public void PlayClic() {
        PlaySound(clic);
    }

    public void PlayHit() {
        PlaySound(hit);
    }

    public void PlayBonus() {
        PlaySound(bonus);
    }

    public void PlayItem() {
        PlaySound(item);
    }

    public void PlayPenalty() {
        PlaySound(penalty);
    }
}