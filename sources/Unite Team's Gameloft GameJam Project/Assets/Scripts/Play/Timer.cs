using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _remainingTime;

    [SerializeField] private float _maxPlayTime;

    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _remainingTime = _maxPlayTime;
    }

    // Update is called once per frame
    void Update()
    {
        _remainingTime -= Time.deltaTime;
        _text.text = "Time left: " + (int)_remainingTime;

        if (_remainingTime <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
