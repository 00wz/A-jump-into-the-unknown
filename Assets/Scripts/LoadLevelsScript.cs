using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadLevelsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelButtonsRoot;
    [SerializeField]
    private Button[] LevelButtons;

    private const string LEVELS_DATA_KEY = "level_progress";

    static public void RefreshProgress(int achievedLevel)
    {
        if (PlayerPrefs.HasKey(LEVELS_DATA_KEY) && 
            (PlayerPrefs.GetInt(LEVELS_DATA_KEY) >= achievedLevel))
        {
            return;
        }
        PlayerPrefs.SetInt(LEVELS_DATA_KEY, achievedLevel);
    }

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

        for (int k = 0; k < LevelButtons.Length; k++)
        {
            int indx = k + 1;//c# closure fix (+1 because 0 scene is menu)
            LevelButtons[k].onClick.AddListener(() => SceneManager.LoadScene(indx));
        }

        int i = 0;
        int levelsCount = SceneManager.sceneCountInBuildSettings - 2;// -menu and endGame scenes
        for(; (i < achievedLevel) && (i < LevelButtons.Length) && (i < levelsCount); i++)
        {
            LevelButtons[i].interactable = true;
        }
        for(; (i < LevelButtons.Length) && (i < levelsCount); i++)
        {
            LevelButtons[i].interactable = false;
        }
        for(; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].gameObject.SetActive(false);
        }
    }
}
