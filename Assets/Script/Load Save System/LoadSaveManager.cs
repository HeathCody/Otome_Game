using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class LoadSaveManager : MonoBehaviour
{
    #region Load Save Manager
    public static LoadSaveManager instance;
    [FoldoutGroup("Load Save Manager")][SerializeField] private PlayerData playerData = new PlayerData();
    [FoldoutGroup("Load Save Manager")] public List<PlayerData> listPlayerData = new List<PlayerData>();
    [FoldoutGroup("Load Save Manager")] public GameDataGallery galleryData = new GameDataGallery();
    public static List<ILoadSaveObjects> listLoadSaveObjects;
    [FoldoutGroup("Load Save Manager")][SerializeField] public bool dataIsReady = false;
    [FoldoutGroup("Load Save Manager")] public bool isFromLoadManager = false;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            listLoadSaveObjects = new List<ILoadSaveObjects>();
        }
    }
    void Start()
    {
        LoadAllFilePlayerData();
        LoadGameGalleryData();
        dataIsReady = true;
    }
    public void NewGameData()
    {
        playerData = new PlayerData();
    }
    private void LoadAllFilePlayerData()
    {
        string directoryFile = Path.Combine(Application.persistentDataPath, DirectoryPlayerData);
        listPlayerData = new List<PlayerData>();
        if (!Directory.Exists(directoryFile))
        {
            Debug.Log("Save directory not found: " + directoryFile);
            return;
        }
        string[] saveFiles = Directory.GetFiles(directoryFile, "*.json");
        foreach (string filePath in saveFiles)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                json = EncripDecrip(json);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                listPlayerData.Add(data);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Failed to load save file: {filePath}\n{ex.Message}");
            }
        }
        listPlayerData.Sort((a, b) => string.Compare(a.fileName, b.fileName));
    }
    private void LoadGameGalleryData()
    {
        string fullpathGalleryData = Path.Combine(Application.persistentDataPath, fileNameGalleryDataHeader);
        if (File.Exists(fullpathGalleryData))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpathGalleryData, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                dataToLoad = EncripDecrip(dataToLoad);
                galleryData = JsonUtility.FromJson<GameDataGallery>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error on Load Gallery Data " + fullpathGalleryData + "\n" + ex);
            }
            LoadObjectData();
        }
    }
    public void LoadObjectData()
    {
        for (int i = 0; i < listLoadSaveObjects.Count; i++)
            listLoadSaveObjects[i].LoadGameData(playerData, galleryData);

    }
    public void LoadGamePlayerData(int index)
    {
        string fullpathPlayerData = Path.Combine(Application.persistentDataPath, DirectoryPlayerData, listPlayerData[index].fileName);
        if (File.Exists(fullpathPlayerData))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullpathPlayerData, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                dataToLoad = EncripDecrip(dataToLoad);
                playerData = JsonUtility.FromJson<PlayerData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error on Load Player Data " + fullpathPlayerData + "\n" + ex);
            }
            LoadObjectData();
        }
    }
    public void SaveGameData(int index)
    {
        for (int i = 0; i < listLoadSaveObjects.Count; i++)
            listLoadSaveObjects[i].SaveGameData(ref playerData, ref galleryData);

        if (index > listPlayerData.Count)
        {
            int indexFile = index + 1;
            playerData.fileName = fileNamePlayerDataHeader + "-" + indexFile;
        }

        string fullpathPlayerData = Path.Combine(Application.persistentDataPath, DirectoryPlayerData, playerData.fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpathPlayerData));
            string playerDataToStore = JsonUtility.ToJson(playerData, true);
            playerDataToStore = EncripDecrip(playerDataToStore);
            using (FileStream stream = new FileStream(fullpathPlayerData, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(playerDataToStore);
                }
            }
            if (index > listPlayerData.Count)
                listPlayerData.Add(playerData);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error on Save Player Data " + fullpathPlayerData + "\n" + ex);
        }
        string fullpathGalleryData = Path.Combine(Application.persistentDataPath, fileNameGalleryDataHeader);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpathGalleryData));
            string galleryDataToStore = JsonUtility.ToJson(playerData, true);
            galleryDataToStore = EncripDecrip(galleryDataToStore);
            using (FileStream stream = new FileStream(fullpathGalleryData, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(galleryDataToStore);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error on Save Player Data " + fullpathGalleryData + "\n" + ex);
        }
    }
    public static void Register(ILoadSaveObjects obj)
    {
        if (!listLoadSaveObjects.Contains(obj)) listLoadSaveObjects.Add(obj);
    }

    public static void Unregister(ILoadSaveObjects obj)
    {
        if (listLoadSaveObjects.Contains(obj)) listLoadSaveObjects.Remove(obj);
    }
    #endregion

    #region File Path
    [FoldoutGroup("File Path")][SerializeField] private string DirectoryPlayerData;
    [FoldoutGroup("File Path")][SerializeField] private string fileNamePlayerDataHeader;
    [FoldoutGroup("File Path")][SerializeField] private string fileNameGalleryDataHeader;
    private readonly string somewords = "AoreanTeam";

    private string EncripDecrip(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)data[i] ^ somewords[i % somewords.Length];
        }
        return modifiedData;
    }
    #endregion
}
