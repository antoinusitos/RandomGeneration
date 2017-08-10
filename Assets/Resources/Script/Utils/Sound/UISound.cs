using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour {

    private AudioSource _theAudioSource = null;
    private bool _loop = false;
    private AudioClip _theAudio = null;

    public void Init()
    {
        _theAudioSource = gameObject.AddComponent<AudioSource>();
        _theAudioSource.playOnAwake = false;
        _theAudioSource.spatialBlend = 0; // set the audio source to 2D
    }

    public void Play(AudioClip theAudio, bool mustLoop)
    {
        _loop = mustLoop;
        _theAudio = theAudio;
        _theAudioSource.PlayOneShot(theAudio);
        StartCoroutine(DestroyAfterEnd(theAudio.length));
    }

    private IEnumerator DestroyAfterEnd(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        if (!_loop)
            Destroy(gameObject);
        else
            Play(_theAudio, _loop);
    }
}
