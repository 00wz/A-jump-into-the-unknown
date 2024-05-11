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
    private float GameOverSqrVelocity = 0.01f;
    [SerializeField]
    private float Speed = 0f;
    [SerializeField]
    private GameObject[] Environment;
    [SerializeField]
    private Rigidbody[] EnvironmentWithRigitBody;
    [SerializeField]
    private GameObject[] BackgroundEnvironment;
    [SerializeField]
    private float BackgroundSpeed = 2.5f;

    [SerializeField]
    private AudioClip[] WinAudioClips;
    [SerializeField]
    private AudioClip[] LoseAudioClips;
    [Range(0, 1)]
    public float AudioVolume = 0.5f;

    private bool _isEndOfGame = false;
    private Vector3 _environmentVelocity;
    private Vector3 _backgroundVelocity;

    private void Start()
    {
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        _environmentVelocity = new Vector3(-Speed, 0f, 0f);
        _backgroundVelocity = new Vector3(-BackgroundSpeed, 0f, 0f);
    }

    void Update()
    {
        if (_isEndOfGame)
        {
            return;
        }

        if(RagdollToggle.IsRagdoll && 
            (SmoothedSpeedCheck.SmoothedSpeed - _environmentVelocity).sqrMagnitude 
            <=GameOverSqrVelocity)
        {
            if(CheckBox.CheckOverlap() != null)
            {
                WinGame();
            }
            else
            {
                LoseGame();
            }
        }

        MoveEnvironmentTic();
    }

    private void WinGame()
    {
        WinPanel.SetActive(true);
        PlayRandomSound(WinAudioClips);
        _isEndOfGame = true;
    }

    public void LoseGame()
    {
        GameOverPanel.SetActive(true);
        PlayRandomSound(LoseAudioClips);
        _isEndOfGame = true;
    }

    private void MoveEnvironmentTic()
    {
        Vector3 deltaPosition = _environmentVelocity * Time.deltaTime;
        for(int i =0; i < Environment.Length; i++)
        {
            Environment[i].transform.Translate(deltaPosition, Space.World);
        }
        Vector3 deltaBackgroundPosition = _backgroundVelocity * Time.deltaTime;
        for(int i =0; i < BackgroundEnvironment.Length; i++)
        {
            BackgroundEnvironment[i].transform.Translate(deltaBackgroundPosition, Space.World);
        }
    }

    private void MoveEnvironmentWithRigitBodyFixTic()
    {
        Vector3 deltaPosition = _environmentVelocity * Time.fixedDeltaTime;
        for (int i = 0; i < EnvironmentWithRigitBody.Length; i++)
        {
            EnvironmentWithRigitBody[i].MovePosition(
                EnvironmentWithRigitBody[i].position + deltaPosition);
        }
    }

    private void FixedUpdate()
    {
        if(!_isEndOfGame)
        {
            MoveEnvironmentWithRigitBodyFixTic();
        }
    }

    private void PlayRandomSound(AudioClip[] AudioClips)
    {
        if (AudioClips.Length > 0)
        {
            var index = Random.Range(0, AudioClips.Length);
            AudioSource.PlayClipAtPoint(AudioClips[index],
                SmoothedSpeedCheck.transform.position, AudioVolume);
        }
    }
}
