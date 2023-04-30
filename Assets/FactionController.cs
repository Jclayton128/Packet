using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionController : MonoBehaviour
{
    public static FactionController Instance;

    //state

    /// <summary>
    /// 0: Rebel/Gov, 1: Love/Apathy
    /// </summary>
    int[] _scores;


    private void Awake()
    {
        Instance = this;
        _scores = new int[2] {0,0};
    }

    public void ModifyScore(int storyline, int amountToAdd)
    {
        _scores[storyline] += amountToAdd;
    }

    public int GetScore(int storyline)
    {
        return _scores[storyline];
    }
}
