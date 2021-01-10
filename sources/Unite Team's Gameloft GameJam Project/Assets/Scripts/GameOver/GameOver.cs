using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Text _scoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        _scoreTxt.text = Score._currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMenu()
    {
        Pause._gamePaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgain()
    {
        Pause._gamePaused = false;
        SceneManager.LoadScene("Play");
    }
}
