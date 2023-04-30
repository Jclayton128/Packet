using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineController : MonoBehaviour
{
    public static StorylineController Instance;

    [SerializeField] StoryMessage _newGameStoryMessage = null;

    [SerializeField] StoryMessage[] _messagesRebel = null;
    [SerializeField] StoryMessage[] _messagesGov = null;

    [SerializeField] StoryMessage[] _messagesAI = null;
    [SerializeField] StoryMessage[] _messagesAntiAI = null;

    [SerializeField] StoryMessage[] _randomMessage = null;

    [SerializeField] string[] _randomRecipients;
    [SerializeField] string[] _randomSenders;


    //state
    List<StoryMessage> _unusedRandomMessages = new List<StoryMessage>();



    private void Awake()
    {
        Instance = this;

        PrepareUnusedRandomStoryList();

    }

    private void PrepareUnusedRandomStoryList()
    {
        _unusedRandomMessages.Clear();
        foreach (var message in _randomMessage)
        {
            _unusedRandomMessages.Add(message);
        }
    }


    public StoryMessage AdvanceToNextStoryMessage()
    {
        //randomly pick either a random mesage or a storyline message
        //if a storyline message, randomly pick a storyline.
        //once storyline picked, draw from either a happy or grim bucket based
        //on the faction score.

        return GenerateRandomStoryMessage();
    }


    private StoryMessage GenerateRandomStoryMessage()
    {
        if (_unusedRandomMessages.Count < 1)
        {
            PrepareUnusedRandomStoryList();
        }

        int rand = UnityEngine.Random.Range(0, _unusedRandomMessages.Count);
        StoryMessage sm = new StoryMessage(
            _unusedRandomMessages[rand].SenderImage,
            _unusedRandomMessages[rand].Message,
            PullRandomSender(),
            PullRandomReceiver());
        return sm;
    }

    private string PullRandomSender()
    {
        int rand = UnityEngine.Random.Range(0, _randomSenders.Length);
        return _randomSenders[rand];
    }

    private string PullRandomReceiver()
    {
        int rand = UnityEngine.Random.Range(0, _randomRecipients.Length);
        return _randomRecipients[rand];
    }

    public StoryMessage PullNewGameMessage()
    {
        return _newGameStoryMessage;
    }
}
