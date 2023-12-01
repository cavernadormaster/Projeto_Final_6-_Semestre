using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource fundoMusical;
    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;
    }
}
