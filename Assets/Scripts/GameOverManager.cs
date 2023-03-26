using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private GameOverText _gameOverText;

    void Start()
    {
        _gameOverText.ShowGameOverText();
    }

    void Update()
    {
        _gameOverText.ShowGameOverText();
    }
}
