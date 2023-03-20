using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
/// <summary>
/// singleton that interacts with the fmod bank + stores references for scripts
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy (this.gameObject);
        }

        instance = this;
    }

    //plays sound once (collecting coins, dying, explosions)
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
