using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorylineController : MonoBehaviour
{
    public static StorylineController Instance;

    [SerializeField] StoryMessage _newGameStoryMessage = null;

    [SerializeField] List<StoryMessage> _messagesRebel = new List<StoryMessage>();
    [SerializeField] List<StoryMessage> _messagesGov = new List<StoryMessage>();

    [SerializeField] List<StoryMessage> _messagesAI = new List<StoryMessage>();
    [SerializeField] List<StoryMessage> _messagesAntiAI = new List<StoryMessage>();
    [SerializeField] List<StoryMessage> _son = new List<StoryMessage>();

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

        int randForStory = UnityEngine.Random.Range(0, 2);
        if (randForStory == 0)
        {
            //story
            int randForArc = UnityEngine.Random.Range(0, 5);
            if (randForArc == 0)
            {
                //rebel
                if (_messagesRebel.Count == 0)
                {
                    return GenerateRandomStoryMessage();
                }
                StoryMessage sm = new StoryMessage(
                    _messagesRebel[0].SenderImage,
                    _messagesRebel[0].Message,
                    _messagesRebel[0].From,
                    _messagesRebel[0].To);
                _messagesRebel.Remove(_messagesRebel[0]);
                return sm;
            }
            else if (randForArc == 1)
            {
                //gov
                if (_messagesGov.Count == 0)
                {
                    return GenerateRandomStoryMessage();
                }
                StoryMessage sm = new StoryMessage(
                    _messagesGov[0].SenderImage,
                    _messagesGov[0].Message,
                    _messagesGov[0].From,
                    _messagesGov[0].To);
                _messagesGov.Remove(_messagesGov[0]);
                return sm;
            }
            else if (randForArc == 2)
            {
                //ai
                if (_messagesAI.Count == 0)
                {
                    return GenerateRandomStoryMessage();
                }
                StoryMessage sm = new StoryMessage(
                    _messagesAI[0].SenderImage,
                    _messagesAI[0].Message,
                    _messagesAI[0].From,
                    _messagesAI[0].To);
                _messagesAI.Remove(_messagesAI[0]);
                return sm;
            }
            else if (randForArc == 3)
            {
                //anti ai
                if (_messagesAntiAI.Count == 0)
                {
                    return GenerateRandomStoryMessage();
                }
                StoryMessage sm = new StoryMessage(
                    _messagesAntiAI[0].SenderImage,
                    _messagesAntiAI[0].Message,
                    _messagesAntiAI[0].From,
                    _messagesAntiAI[0].To);
                _messagesAntiAI.Remove(_messagesAntiAI[0]);
                return sm;
            }
            else
            {
                //son
                if (_son.Count == 0)
                {
                    return GenerateRandomStoryMessage();
                }
                StoryMessage sm = new StoryMessage(
                    _son[0].SenderImage,
                    _son[0].Message,
                    _son[0].From,
                    _son[0].To);
                _son.Remove(_son[0]);
                return sm;
            }


        }
        else
        {
            //random
            return GenerateRandomStoryMessage();
        }

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
