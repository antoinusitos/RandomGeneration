using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class QuestReader : MonoBehaviour
{

    // file to read for the data
    private static string fileName = "QuestFile.txt";

    // folder containing the file to read
    private static string watcherFolder = "/Data";

    private static string fullPath = "";

    void Start()
    {
        fullPath = Application.dataPath + watcherFolder;
    }

    public void StartReading()
    {
        ReadFile();
    }

    static void ReadFile()
    {
        StreamReader sr = new StreamReader(fullPath + "/" + fileName);
        string fileContents = sr.ReadToEnd();
        sr.Close();

        List<string> infos = new List<string>();

        string[] lines = fileContents.Split("\n"[0]);
        foreach (string line in lines)
        {
            // not a comment line
            if (!line.Contains("#"))
            {
                infos.Add(line);
            }
        }

        int id = 0;
        string name = "";
        string description = "";
        int questType = 0;
        int progressType = 0;
        int distanceToQuest = 0;
        int progressToEnd = 0;
        int stepNumber = 0;
        int[] questsToActivate = new int[0];

        for (int i = 0; i < infos.Count; i++)
        {
            if (!infos[i].Contains("="))
            {
                // white line
                Quest newQuest = new Quest(id, name, description, (Quest.QuestType)questType, questsToActivate, (Quest.ProgressType)progressType, distanceToQuest, progressToEnd, stepNumber);
                questsToActivate = new int[0];
                QuestManager.GetInstance().AddQuest(newQuest);
            }
            else
            {
                string[] TheInfo = infos[i].Split('=');

                switch (TheInfo[0])
                {
                    case "iD":
                        id = int.Parse(TheInfo[1]);
                        break;
                    case "name":
                        name = TheInfo[1];
                        break;
                    case "description":
                        description = TheInfo[1];
                        break;
                    case "questType":
                        questType = int.Parse(TheInfo[1]);
                        break;
                    case "progressType":
                        progressType = int.Parse(TheInfo[1]);
                        break;
                    case "distanceToQuest":
                        distanceToQuest = int.Parse(TheInfo[1]);
                        break;
                    case "progressToEnd":
                        progressToEnd = int.Parse(TheInfo[1]);
                        break;
                    case "stepNumber":
                        stepNumber = int.Parse(TheInfo[1]);
                        break;
                    case "questsToActivate":
                        string[] temp = TheInfo[1].Split(',');
                        questsToActivate = new int[temp.Length];
                        for(int j = 0; j < temp.Length; j++)
                        {
                            int.TryParse(temp[j], out questsToActivate[j]);
                        }
                        break;
                }
            }
        }
    }
}
