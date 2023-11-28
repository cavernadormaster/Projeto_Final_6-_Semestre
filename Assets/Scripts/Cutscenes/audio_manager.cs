using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip zumbis;
    public AudioClip corpo_caindo;

    // Start is called before the first frame update
    void Start()
    {
        MusicSource.Play();
        StartCoroutine(Tempo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }


    IEnumerator Tempo()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(0.1f);
        audio.clip = corpo_caindo;
        audio.Play();
    }
}
