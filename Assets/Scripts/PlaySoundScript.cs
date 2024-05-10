using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] AudioClips;
    [Range(0, 1)]
    public float AudioVolume = 0.5f;

    public void PlaySound()
    {
        if (AudioClips.Length > 0)
        {
            var index = Random.Range(0, AudioClips.Length);
            AudioSource.PlayClipAtPoint(AudioClips[index],
                transform.position, AudioVolume); 
        }
    }
}
