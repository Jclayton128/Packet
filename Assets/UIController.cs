using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    MessageUIC _message;
    public MessageUIC Message => _message;


    private void Awake()
    {
        Instance = this;
        _message = GetComponent<MessageUIC>();
    }

    private void Start()
    {
        _message.HideMessage();
    }
}
