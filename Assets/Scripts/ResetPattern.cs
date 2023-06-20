using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPattern : MonoBehaviour
{
    KeyCode[] _resetPattern = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.W, KeyCode.S, KeyCode.E };

    float _secondsToComplete = 2f;
    float _currentSeconds = 0;
    int _currentIndex = 0;

    bool _startedSequence = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) _startedSequence = true;

        if (!_startedSequence) return;

        _currentSeconds += Time.deltaTime;

        if (_currentIndex == _resetPattern.Length)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (Input.GetKeyDown(_resetPattern[_currentIndex])) _currentIndex++;

        if (_currentSeconds >= _secondsToComplete) ResetSequence();
    }

    private void ResetSequence()
    {
        _currentSeconds = 0;
        _currentIndex = 0;
        _startedSequence = false;
    }
}
