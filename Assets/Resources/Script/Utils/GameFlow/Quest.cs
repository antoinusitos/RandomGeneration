using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int iD;
    public string name;
    public string description;
    public QuestType questType;
    public ProgressType progressType;
    public float distanceToQuest;
    public int progressToEnd;
    public int stepNumber;
    public int[] questsToActivate;
    // ...

    public enum QuestType
    {
        None,
        Collect,
        Reach,
        Destroy,
        Achieve,
        //...
    }

    public enum ProgressType
    {
        Pending,
        InProgress,
        Ended,
        Failed,
        //...
    }

    public Quest(
        int theID,
        string theName,
        string theDescription,
        QuestType theQuestType,
        int[] theQuestsToActivate,
        ProgressType theProgressType = ProgressType.Pending,
        float theDistanceToQuest = 0,
        int theProgressToEnd = 0,
        int theStepNumber = 1
        )
    {
        iD = theID;
        name = theName;
        description = theDescription;
        questType = theQuestType;
        questsToActivate = theQuestsToActivate;
        progressType = theProgressType;
        progressToEnd = theProgressToEnd;
        distanceToQuest = theDistanceToQuest;
        stepNumber = theStepNumber;
    }

    public void ChangeState(ProgressType theProgressType)
    {
        progressType = theProgressType;
    }

    public void NotifyQuest()
    {
        progressToEnd++;
        if (stepNumber == progressToEnd)
        {
            progressType = ProgressType.Ended;
            for(int i = 0; i < questsToActivate.Length; i++)
            {
                QuestManager.GetInstance().StartQuest(questsToActivate[i]);
            }
        }
    }
}