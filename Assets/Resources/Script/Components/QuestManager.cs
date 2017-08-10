using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : BaseManager
{
    private bool readFile = false;

    // Accessor
    private GameManager _gameManager;

    public List<Quest> allQuests;

    private void Start()
    {
        _gameManager = GameManager.GetInstance();
    }

    public void SetReadquestFile(string value, string name)
    {
        if (bool.TryParse(value, out readFile))
        {
            if (readFile)
            {
                //print("reading the quest file...");
                allQuests = new List<Quest>();
                GetComponent<QuestReader>().StartReading();
            }
        }
        else
            Debug.LogError("CANNOT CAST VALUE FOR " + name);
    }

    public List<Quest> GetQuestsByType(Quest.ProgressType theType)
    {
        List<Quest> toReturn = new List<Quest>();

        for(int i = 0; i < allQuests.Count; i++)
        {
            if (allQuests[i].progressType == theType)
                toReturn.Add(allQuests[i]);
        }

        return toReturn;
    }

    public void AddQuest(Quest newQuest)
    {
        allQuests.Add(newQuest);
    }

    public void StartQuest(int questID)
    {
        for(int i = 0; i < allQuests.Count; i++)
        {
            if(allQuests[i].iD == questID && allQuests[i].progressType == Quest.ProgressType.Pending)
            {
                allQuests[i].ChangeState(Quest.ProgressType.InProgress);
                break;
            }
        }
    }

    public void NotifyQuest(int questID)
    {
        for (int i = 0; i < allQuests.Count; i++)
        {
            if (allQuests[i].iD == questID && allQuests[i].progressType == Quest.ProgressType.InProgress)
            {
                allQuests[i].NotifyQuest();
                break;
            }
        }
    }

    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static QuestManager _instance;

    public static QuestManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
