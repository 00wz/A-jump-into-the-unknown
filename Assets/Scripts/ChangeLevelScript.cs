using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelScript : MonoBehaviour
{
    [SerializeField]
    private string LevelName = "SampleScene";

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}
