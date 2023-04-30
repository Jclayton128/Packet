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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            FadeController.Instance.DecrementFadeOutPhase();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            FadeController.Instance.IncrementFadeInPhase();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            FadeController.Instance.InstafadeAll();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TutorialController.Instance.AdvanceTutorial();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            ToolResourceController.Instance.GainResources(100);
        }
    }
}
