using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] crashSounds;
    [SerializeField] private AudioClip[] explodeSounds;
    [SerializeField] private AudioClip[] repairSounds;
    [SerializeField] private AudioClip[] refuelSounds;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrashSound()
    {
         int rng = Random.Range(0, crashSounds.Length);
        AudioSource.PlayClipAtPoint(crashSounds[rng], transform.position, 5);
    }

    public void PlayExplosionSound()
    {
        int rng = Random.Range(0, explodeSounds.Length);
        AudioSource.PlayClipAtPoint(explodeSounds[rng], transform.position, 5);
    }

    public void PlayRocketNozzleSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    public void StopRocketNozzleSound()
    {
        _audioSource.Stop();
    }

    public void PlayRepairSound()
    {
        int rng = Random.Range(0, repairSounds.Length);
        AudioSource.PlayClipAtPoint(repairSounds[rng], transform.position, 5);
    }

    public void PlayRefuelSound()
    {
        int rng = Random.Range(0, refuelSounds.Length);
        AudioSource.PlayClipAtPoint(refuelSounds[rng], transform.position, 5);
    }
}