using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class FileReader : MonoBehaviour
{
    // file to read for the data
    private string fileName = "SystemInfos.txt";

    // folder containing the file to read
    private string watcherFolder = "/Data";

    private string fullPath = "";

    private FileSystemWatcher watcher;
    
    void Start()
    {
        fullPath = Application.dataPath + watcherFolder;
        CreateFileWatcher();
        StartReading();
    }

    public void StartReading()
    {
        ReadFile(true);
    }

    private void ReadFile(bool mainThread)
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

        for (int i = 0; i < infos.Count; i++)
        {
            if (!infos[i].Contains("="))
            {
                // white line
            }
            else
            {
                string[] TheInfo = infos[i].Split('=');

                switch (TheInfo[0])
                {
                    case "Exemple":
                        // DO STUFF with TheInfo[1]
                        break;
                    case "Vsync":
                        InfoManager.GetInstance().TryReadVSyncValue(TheInfo[1], TheInfo[0]);
                        break;
                    case "ReadQuestFile":
                        if(mainThread)
                            QuestManager.GetInstance().SetReadquestFile(TheInfo[1], TheInfo[0]);
                        break;
                }
            }
        }
    }

    public void CreateFileWatcher()
    {
        // Create a new FileSystemWatcher and set its properties.
        watcher = new FileSystemWatcher(fullPath);
        /* Watch for changes in LastAccess and LastWrite times, and 
           the renaming of files or directories. */
        watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
        // Only watch text files.
        watcher.Filter = "*.txt";

        // Add event handlers.
        watcher.Changed += new FileSystemEventHandler(OnChanged);

        // CAN BE ADDED
        //watcher.Created += new FileSystemEventHandler(OnChanged);
        //watcher.Deleted += new FileSystemEventHandler(OnChanged);
        //watcher.Renamed += new RenamedEventHandler(OnRenamed);

        // Begin watching.
        watcher.EnableRaisingEvents = true;
    }

    // Define the event handlers.
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        // Specify what is done when a file is changed, created, or deleted.
        print("File: " + e.FullPath + " " + e.ChangeType);
        ReadFile(false);
    }
}
