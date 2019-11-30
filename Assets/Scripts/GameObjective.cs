using System.Collections.Generic;
using UnityEngine;

public class GameObjective : MonoBehaviour {
    #region Variables
    public static GameObjective Instance;

    private bool _isGameOver = false;
    private bool _isGameEnding = false;
    // Count how long the player has survived.
    private float _stopwatch = 0f;

    private float _spawnTimer = 6;
    private const float TIME_TILL_NEXT_SPAWN = 7;

    public UnityEngine.UI.Text displayText;

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
    
   /* void Start(){
        GameObject en1 = GetEnemy();
        en1.transform.position = gate1SpawnPoint.position + new Vector3(-16.7f, 0f, 7.66f);
        //en1.transform.position = spawnPoints[0].position + new Vector3(-16.7f, 0f, 7.66f);
        en1.SetActive(true);
#warning pull the vector3 out from every spawn method and place it in a variable since it is the same everywhere
#warning what is this new vector3 actually supposed to be doing? see output/console
        Debug.LogError("Gate 1 spawn position: " + gate1SpawnPoint.position);
        Debug.LogError("Position with vector 3 offset: " + gate1SpawnPoint.position + new Vector3(-16.7f, 0f, 7.66f));
#warning possibly could create a method to spawn char at spawn point (en1, gate2SpawnPoint);

        GameObject en2 = GetEnemy();
        en2.transform.position = gate2SpawnPoint.position + new Vector3(-16.7f, 0f, 7.66f);
        //en2.transform.position = spawnPoints[1].position + new Vector3(-16.7f, 0f, 7.66f);
        en2.SetActive(true);
    }*/
    
    private void Update() {
        if (_isGameOver) return;
        
        // Increment/decrement the timers
        _stopwatch += Time.deltaTime;
        _isGameEnding = _stopwatch > 60;
        
        if (_isGameEnding) {
            _isGameOver = true;
            Debug.Log("You have Completed the Game!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            return;
        }
        
        _spawnTimer += Time.deltaTime;
        // Show the timer on screen somehow Canvas item or OnGui?
        UpdateTimerOnScreen();
        
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
        var minsDisplay = ((int)(_stopwatch / 60)).ToString();
        var secsDisplay = ((int)_stopwatch).ToString();

        secsDisplay = ((int)_stopwatch - (int.Parse(minsDisplay) * 60)).ToString();

        if (_stopwatch - (int.Parse(minsDisplay) * 60) < 10) {
            secsDisplay = "0" + secsDisplay;
        }
       
        displayText.text = minsDisplay + " : " + secsDisplay;
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

        GUILayout.Label("Hack");
        GUILayout.Label("Enemies Killed: " + GameManagement.Instance.GetKills());
        GUILayout.Label("Your Score: " + "GameManagement.Instance.getScore().ToString()");
#warning TODO: How to score this game. Kills? Points per kill?
        GUILayout.Label("Your Time:  " + Time.timeSinceLevelLoad);
        float potentialTotalScore = 10;//GameManagement.Instance.getScore() + GameManagement.manage.getTotalScore();
        GUILayout.Label("Total Score: " + potentialTotalScore);

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
        _isGameOver = true;

        foreach (var enemy in _enemies) {
            enemy.gameObject.SetActive(false);
        }
    }
}
