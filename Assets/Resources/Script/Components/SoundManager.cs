using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseManager 
{
    [System.Serializable]
    public struct SoundToPlay
    {
        public string name;
        public AudioClip clip;
        public bool loop;

        public SoundToPlay(string newName, AudioClip newClip, bool mustLoop)
        {
            name = newName;
            clip = newClip;
            loop = mustLoop; // @Implementation : must be implemented to looped or not
        }
    }

    public List<SoundToPlay> _allPlayableSound;

    private AudioSource _theAudioSource = null;
    private GameObject _soundsParent = null;

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {
        _theAudioSource = gameObject.AddComponent<AudioSource>();
        _theAudioSource.playOnAwake = false;
        _theAudioSource.spatialBlend = 0; // set the audio source to 2D
        InitAllPlayableSounds();
    }

    private void Awake()
    {
        InitSingleton();

        if (_theAudioSource == null)
            _theAudioSource = GetComponent<AudioSource>();

        if (_soundsParent == null)
        {
            _soundsParent = new GameObject("All Playing Sounds");
        }
    }

    private void InitAllPlayableSounds()
    {
        _allPlayableSound = new List<SoundToPlay>();
    }

    public void playSound2D(string theSoundName)
    {
        for (int i = 0; i < _allPlayableSound.Count; i++)
        {
            if (_allPlayableSound[i].name == theSoundName)
            {
                GameObject sound = new GameObject("UI Sound");
                sound.transform.position = Vector3.zero;
                UISound sp = sound.AddComponent<UISound>();
                sp.transform.parent = _soundsParent.transform;
                sp.Init();
                sp.Play(_allPlayableSound[i].clip, _allPlayableSound[i].loop);
                return;
            }
        }

        Debug.LogError("Could not find sound " + theSoundName + " to play it 2D");
    }

    public void playSound3D(string theSoundName, Vector3 thePosition)
    {
        for (int i = 0; i < _allPlayableSound.Count; i++)
        {
            if (_allPlayableSound[i].name == theSoundName)
            {
                GameObject sound = new GameObject("Spatialized Sound");
                sound.transform.position = thePosition;
                SpatializedSound sp = sound.AddComponent<SpatializedSound>();
                sp.transform.parent = _soundsParent.transform;
                sp.Init();
                sp.Play(_allPlayableSound[i].clip, _allPlayableSound[i].loop);
                return;
            }
        }

        Debug.LogError("Could not find sound " + theSoundName + " to play it 3D");
    }

    //
    // Singleton Stuff
    // 

    private static SoundManager _instance;

    public static SoundManager GetInstance()
    {
        return _instance;
    }

    private void InitSingleton()
    {
        _instance = this;
    }

}
