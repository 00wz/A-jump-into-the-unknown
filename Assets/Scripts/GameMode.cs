using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private RagdollToggle RagdollToggle;
    [SerializeField]
    private SmoothedSpeedCheck SmoothedSpeedCheck;
    [SerializeField]
    private CheckBox CheckBox;
    [SerializeField]
    private GameObject WinPanel;
    [SerializeField]
    private GameObject GameOverPanel;

    [SerializeField]
    private AudioClip[] WinAudioClips;
    [SerializeField]
    private AudioClip[] LoseAudioClips;
    [Range(0, 1)]
    public float AudioVolume = 0.5f;

    private bool _isEndOfGame = false;

    private void Start()
    {
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (_isEndOfGame)
        {
            return;
        }

        if(RagdollToggle.IsRagdoll && SmoothedSpeedCheck.SmoothedSpeed == Vector3.zero)
        {
            if(CheckBox.CheckOverlap() != null)
            {
                WinPanel.SetActive(true);
                PlayRandomSound(WinAudioClips);
            }
            else
            {
                GameOverPanel.SetActive(true);
                PlayRandomSound(LoseAudioClips);
            }
            _isEndOfGame = true;
        }
    }

    private void PlayRandomSound(AudioClip[] AudioClips)
    {
        if (AudioClips.Length > 0)
        {
            var index = Random.Range(0, AudioClips.Length);
            AudioSource.PlayClipAtPoint(AudioClips[index],
                transform.position, AudioVolume);
        }
    }
}
