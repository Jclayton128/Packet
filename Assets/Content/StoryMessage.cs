using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story Message")]
public class StoryMessage : ScriptableObject
{
    [SerializeField] Sprite _sendingImage = null;
    [SerializeField][TextArea(1, 2)] string _from = null;
    [SerializeField][TextArea(1, 2)] string _to = null;
    [SerializeField][TextArea(3, 10)] string _message = null;

    public Sprite SenderImage => _sendingImage;
    public string Message => _message;
    public string From => _from;
    public string To => _to;    


    public StoryMessage(Sprite sender, string message, string from, string to)
    {
        _sendingImage = sender;
        _message = message;
        _from = from;           
        _to = to;
    }
    
}
