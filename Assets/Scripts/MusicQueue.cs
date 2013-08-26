using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicQueue : MonoBehaviour {

    public AudioClip[] src;
    public Queue<AudioClip> clips;

    private AudioClip current;

    void Start () {

        DontDestroyOnLoad(gameObject);


        clips = new Queue<AudioClip>(src);

        current = clips.Dequeue();
        audio.clip = current;
        audio.Play();
    }
    
    void Update () {
        if (!audio.isPlaying) {
            clips.Enqueue(current);
            current = clips.Dequeue();
            audio.clip = current;
            audio.Play();
        }
    }
}
