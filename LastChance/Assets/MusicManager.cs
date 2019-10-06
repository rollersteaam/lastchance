using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What do you think it does? ;)
/// </summary>
public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] AudioClip beginning;
    [SerializeField] AudioClip ending;
    [SerializeField] AudioClip actionLow;
    [SerializeField] AudioClip actionMed;
    [SerializeField] AudioClip actionHigh;
    AudioSource audioSource;
    float action;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // PlayBeginning();
    }

    void Update()
    {
        if (ProgressionManager.Instance.gameOver) return;
        if (!ProgressionManager.Instance.gameStarted) return;

        float action = SpawnManager.Instance.CalculateActionValue();

        if (action == 2)
        {
            Play(actionHigh);
        }
        else if (action == 1)
        {
            Play(actionMed);
        }
        else
        {
            // Play(actionLow);
        }
    }

    void Play(AudioClip clip)
    {
        if (audioSource.clip == clip) return;

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayBeginning()
    {
        Play(beginning);
    }

    public void PlayEnding()
    {
        Play(ending);
    }
}
