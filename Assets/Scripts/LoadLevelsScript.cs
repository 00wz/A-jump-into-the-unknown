using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelButtonsRoot;
    [SerializeField]
    private Button[] LevelButtons;

    private const string LEVELS_DATA_KEY = "level_progress";

    private void OnValidate()
    {
        if(LevelButtonsRoot!=null)
        {
            LevelButtons = LevelButtonsRoot.GetComponentsInChildren<Button>();
        }
    }

    void Start()
    {
        int achievedLevel = 1;
        if (PlayerPrefs.HasKey(LEVELS_DATA_KEY))
        {
            achievedLevel = PlayerPrefs.GetInt(LEVELS_DATA_KEY);
        }

        int i = 0;
        for(; i < achievedLevel; i++)
        {
            LevelButtons[i].gameObject.SetActive(true);
        }
        for(; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].gameObject.SetActive(false);
        }
    }
}
