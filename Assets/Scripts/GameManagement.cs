using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManagement : MonoBehaviour {
    public static GameManagement Instance;
    private string SAVE_FILE_NAME;
    public int CurrentLevel { get; set;}
    private float score = 0f;
    private float totalScore = 0f;
    private int kills = 0;
    public string playerName = "";

    private void Awake() {
        if (Instance == null) {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }

    // Get and increase game Score
    public float GetScore(){
        return score;
    }

    public float GetTotalScore() {
        return totalScore;
    }

    public int GetKills() {
        return kills;
    }

    public void IncreaseKills() {
        kills++;
    }

    public void IncreaseScore(float pScore){
        score += pScore;
    }

    public void DecreaseScore(float pScore) {
        score -= pScore;
    }
    #region IO Methods
    // Could call these methods on enable and disable to have a constant autosave    
    public void Save() {
        Debug.Log("Saving game data to: " + SAVE_FILE_NAME);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SAVE_FILE_NAME);    
        DataToSave data = new DataToSave();
        data.Name = playerName;
        data.Score = totalScore;
        data.Level = CurrentLevel;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load() {
        if (File.Exists(SAVE_FILE_NAME)) {
            Debug.Log("Loading game data from: " + SAVE_FILE_NAME);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SAVE_FILE_NAME, FileMode.Open);
            DataToSave data = (DataToSave) bf.Deserialize(file);
            playerName = data.Name;
            totalScore = data.Score;
            CurrentLevel = data.Level;
            file.Close();
        }
    }

    public bool HasSaveFile() {
        return File.Exists(SAVE_FILE_NAME);
    }
    #endregion
}

// Class with data to save and load to/from file
[Serializable]
class DataToSave{
    // Total score. We don't save the score of individual levels.
    public float Score{get;set;}
    public string Name{ get; set; }
    public int Level { get; set; }
}