using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField]
    private GameMode GameMode;

    private void OnTriggerEnter(Collider other)
    {
        GameMode.LoseGame();
    }
}
