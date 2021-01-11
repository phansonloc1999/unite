using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiElements;

    [SerializeField] private float _eachUiElementDuration;

    private int _currentUiElementIndex;

    // Start is called before the first frame update
    void Start()
    {
        _currentUiElementIndex = 0;
        _uiElements[_currentUiElementIndex].SetActive(true);

        IEnumerator MyCoroutine()
        {
            for (int i = 0; i < _uiElements.Length; i++)
            {
                yield return new WaitForSeconds(_eachUiElementDuration);
                ShowNextUIPage();
            }
            yield return null;
        }
        StartCoroutine(MyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            ShowNextUIPage();
        }
    }

    public void ShowNextUIPage()
    {
        _uiElements[_currentUiElementIndex].SetActive(false);
        _currentUiElementIndex++;

        if (_currentUiElementIndex >= _uiElements.Length)
        {
            SceneManager.LoadScene("Play");
            return;
        }

        _uiElements[_currentUiElementIndex].SetActive(true);
    }
}
