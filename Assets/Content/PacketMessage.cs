using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Packet Message")]
public class PacketMessage : ScriptableObject
{
    public enum StepToAdvance { 
        TerminatePacket, ActivateServer, ClickMessagePanel, UpgradeCapacity,
        UpgradeEncryption, UpgradeRepair
    }

    public enum SpecialThings
    {
        None, ShowTimer, ShowValue, ShowEncryption,
        SetupStartTutorialTerminals,
        SetupTargetTutorialTerminals,
        EncryptServers,
        HideMessagePanel,
        ShowPacketPanel
    }

[SerializeField] Sprite _sendingImage = null;
    [SerializeField] [TextArea(3,10)] string _message = null;
    [SerializeField][TextArea(1, 2)] string _hint = null;
    [SerializeField] StepToAdvance _stepToAdvance = StepToAdvance.TerminatePacket;

    [SerializeField] bool _invokesTutorialPair = false;
    [SerializeField] SpecialThings _specialThing = SpecialThings.None;

    public Sprite SendingImage => _sendingImage;
    public string Message => _message;
    public string Hint => _hint;
    public StepToAdvance stepToAdvance => _stepToAdvance;

    public bool InvokesTutorialPair => _invokesTutorialPair;

    public SpecialThings SpecialThing => _specialThing;

    
}
