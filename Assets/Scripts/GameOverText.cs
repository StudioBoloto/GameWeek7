using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    public TMP_Text _gameOverText;

    void Start()
    {
        _gameOverText.enabled = false;
    }

    public void ShowGameOverText()
    {
        _gameOverText.enabled = true;
    }
}
