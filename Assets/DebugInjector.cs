using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInjector : MonoBehaviour
{
    private void Update()
    {
        ListenForKeyboard();
    }

    private void ListenForKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PathController.Instance.CreateNewPathProblem();
        }
    }
}
