using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndgameUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _endgamePanel = null;
    [SerializeField] TextMeshProUGUI _packetCounter = null;


    public void ShowEndgamePanel()
    {
        _endgamePanel.ShowHide(true);
    }

    public void HideEndgamePanel()
    {
        _endgamePanel.ShowHide(false);
    }

    public void SetPacketCounter(int count)
    {
        _packetCounter.text = count.ToString();
    }
    
    [ContextMenu("end game")]
    public void HandleEndgameClick()
    {
        Debug.Log("Endgame click");
        SceneManager.LoadScene(0);

    }
}
