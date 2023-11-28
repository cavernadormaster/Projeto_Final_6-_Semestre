using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;
    
    public AudioClip alarm;
    public AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        MusicSource.Play();
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }


    IEnumerator Timer()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(8.7f);
        audio.clip = alarm;
        audio.Play();
    }
}
