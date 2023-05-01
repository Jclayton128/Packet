using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioHandler : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void MouseOver()
    {
        if (button && button.interactable)
        {
            SoundController.Instance.PlaySound(SoundController.SoundID.ButtonHoverOver0);
        }        
    }
    public void MouseExit()
    {
        if (button && button.interactable)
        {
            SoundController.Instance.PlaySound(SoundController.SoundID.ButtonHoverExit1);
        }
    }


    public void MouseUp()
    {
        if (button && button.interactable)
        {
            SoundController.Instance.PlaySound(SoundController.SoundID.ButtonRelease3);
        }
    }
}
