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
            }
            else
            {
                GameOverPanel.SetActive(true);
            }
            _isEndOfGame = true;
        }
    }
}
