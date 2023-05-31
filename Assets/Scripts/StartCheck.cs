using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheck : MonoBehaviour
{
    [SerializeField] private KeyCode _keyToCheck;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) LoadingScene.Instance.LoadScene(3);
    }
}
