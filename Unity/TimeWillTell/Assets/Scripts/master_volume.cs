using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class master_volume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void ChangeVolume (float volume)
    {
        audioMixer.SetFloat("master_volume", volume);
    }
}
