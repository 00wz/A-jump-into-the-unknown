using UnityEngine;

public class CollisionSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] CollisionAudioClips;
    [Range(0, 1)]
    public float AudioVolume = 0.5f;

    private void OnCollisionEnter(Collision collision)
    {
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        if (CollisionAudioClips.Length > 0)
        {
            var index = Random.Range(0, CollisionAudioClips.Length);
            AudioSource.PlayClipAtPoint(CollisionAudioClips[index], 
                transform.position, AudioVolume);
        }
    }
}
