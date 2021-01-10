using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool _gamePaused = false;

    [SerializeField] private GameObject _pauseMeneUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_pauseMeneUI.activeInHierarchy)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _pauseMeneUI.activeInHierarchy)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        _pauseMeneUI.SetActive(true);
        Physics2D.autoSimulation = false;
        DOTween.PauseAll();
        _gamePaused = true;
    }

    public void ResumeGame()
    {
        _pauseMeneUI.SetActive(false);
        Physics2D.autoSimulation = true;
        DOTween.PlayAll();
        _gamePaused = false;
    }
}
