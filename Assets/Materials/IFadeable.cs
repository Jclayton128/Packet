using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFadeable
{
    public void FadeIn(float duration);
    public void FadeOut(float duration);
}
