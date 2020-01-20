using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField] private AudioClip mainTheme = null;
    [SerializeField] private AudioClip battleTheme = null;
    [SerializeField] private AudioClip backstoryTheme = null;

    private AudioSource currentSource;

    public void PlayMainTheme() {
        PlayTrack(mainTheme);
    }

    public void PlayBattleTheme() {
        PlayTrack(battleTheme);
    }

    public void PlayBackstoryTheme() {
        PlayTrack(backstoryTheme);
    }

    private void PlayTrack(AudioClip clip) {
        if (currentSource != null) {
            if (currentSource.clip == clip) {
                return;
            }
        }
        if (currentSource != null) {
            StartCoroutine(FadeOut(currentSource, 0.5f));
        }
        currentSource = gameObject.AddComponent<AudioSource>();
        currentSource.clip = clip;
        currentSource.loop = true;
        currentSource.Play();
    }

    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        Destroy(audioSource);
    }
}


