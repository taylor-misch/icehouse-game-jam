using System.Collections.Generic;
using UnityEngine;

public class GameObjective : MonoBehaviour {
    #region Variables
    public static GameObjective Instance;

    private bool _isGameOver = false;
    private bool _isGameEnding = false;
    // Count how long the player has survived.
    private float _stopwatch = 0f;
    private string _finalTime;
    private float _spawnTimer = 3;
    private const float TIME_TILL_NEXT_SPAWN = 3.5f;

    public UnityEngine.UI.Text displayText;
    [SerializeField] private UnityEngine.UI.Text killsText;

    // Enemies Pooling
    private List<GameObject> _enemies;
    [SerializeField] private GameObject enemiesParent;

    public GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    #endregion
    
    void Awake() {
        Instance = this;
        _enemies = new List<GameObject>();
        InitialLoadOfEnemies();
    }
    
    void InitialLoadOfEnemies() {
        /*for (int i = 0; i < enemyPrefabs.Length; i++) {
            GameObject obj = (GameObject)Instantiate(enemyPrefabs[i]);
            obj.SetActive(false);
            _enemies.Add(obj);
        }*/
    }

    private void Update() {
        if (_isGameOver) return;
        
        // Increment/decrement the timers
        _stopwatch += Time.deltaTime;
        //_isGameEnding = _stopwatch > 60;
        
        if (_isGameEnding) {
            _isGameOver = true;
            Debug.Log($"Game over {displayText.text}");
            Debug.Log("You have Completed the Game!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            return;
        }
        
        _spawnTimer += Time.deltaTime;
        // Show the timer on screen somehow Canvas item or OnGui?
        UpdateTimerOnScreen();
        UpdateKillsOnScreen();
        
        if(_spawnTimer >= TIME_TILL_NEXT_SPAWN) {
            _spawnTimer = 0;
            SpawnNewEnemy();
        }
    }
    
    private void SpawnNewEnemy() {
        GameObject enemy = GetEnemy();
        Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
//        Debug.Log("Spawning Enemy: " + enemy.name + " at " + spawnPosition);
        enemy.transform.position = spawnPosition.position;
        enemy.SetActive(true);
    }
    
    private GameObject GetEnemy(){
        // Grab the first enemy in array that is not currently active
        foreach (var enemy in _enemies) {
            if (!enemy.activeInHierarchy) {
                return enemy;
            }
        }
        
        // Otherwise create a new enemy.
        var obj = (GameObject)Instantiate(GetEnemyPrefab(), enemiesParent.transform, transform);
        obj.SetActive(false);
        _enemies.Add(obj);
        return obj;
    }
    
    private GameObject GetEnemyPrefab() {
        int i = Random.Range(1, 101);
        //Debug.Log($"I is: {i} Returning prefab to create from based on this number");
        //return i > 85 ? enemyPrefabs[2] : i > 42 ? enemyPrefabs[1] : enemyPrefabs[0];
        return enemyPrefabs[0];
    }
    
    private void UpdateTimerOnScreen() {
        var minutesDisplay = ((int)(_stopwatch / 60)).ToString();
        var secsDisplay = ((int)_stopwatch).ToString();

        secsDisplay = ((int)_stopwatch - (int.Parse(minutesDisplay) * 60)).ToString();

        if (_stopwatch - (int.Parse(minutesDisplay) * 60) < 10) {
            secsDisplay = "0" + secsDisplay;
        }
       
        displayText.text = minutesDisplay + " : " + secsDisplay;
    }
    
    private void UpdateKillsOnScreen() {
        killsText.text = $"Kills: {GameManagement.Instance.GetKills()}";
    }
    void OnGUI() {
        if (!_isGameOver) return;
        GUI.color = Color.white;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

        GUILayout.BeginArea(new Rect(x: (Screen.width - 300f) / 2, (Screen.height - 200f) / 2, 500, 400));

        GUILayout.Label("Hack Game Name");
        GUILayout.Label("Enemies Killed: " + GameManagement.Instance.GetKills());
        GUILayout.Label("Your Time:  " + _finalTime);

        if (GUILayout.Button("Restart")) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            Application.LoadLevel(Application.loadedLevel);
        }

        if (GUILayout.Button("Quit to Main Menu")) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Application.LoadLevel(Constants.MAIN_MENU);
        }

        GUILayout.EndArea();
    }

    public void GameOver() {
        _finalTime = displayText.text;
        _isGameEnding = true;
        _isGameOver = true;

        foreach (var enemy in _enemies) {
            enemy.gameObject.SetActive(false);
        }
    }
}
