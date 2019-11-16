using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
    private bool getName = false;
    Canvas nameCanvas;
    Canvas NoNameEnteredCanvas;
    Canvas NoSaveGameCanvas;
    InputField nameInput;

    void Awake() {
        Screen.lockCursor = false;
        //GameObject canvas = ;
        //canvas.SetActive(false);
        nameCanvas = GameObject.Find("NameCanvas").GetComponent<Canvas>();
        nameCanvas.enabled = false;
        NoNameEnteredCanvas = GameObject.Find("NoNameEnteredCanvas").GetComponent<Canvas>();
        NoNameEnteredCanvas.enabled = false;
        NoSaveGameCanvas = GameObject.Find("NoNameEnteredCanvas").GetComponent<Canvas>();
        NoSaveGameCanvas.enabled = false;
        nameInput = GameObject.Find("txtName").GetComponent<InputField>();
    }

    public void CancelButton() {
        nameCanvas.enabled = false;
    }

    public void OkButton() {
        NoNameEnteredCanvas.enabled = false;
        nameCanvas.enabled = true;
    }

    public void NoSaveButton() {
        NoSaveGameCanvas.enabled = false;
    }

    public void BeginButton() {
        string playersName = nameInput.text;
        if (string.IsNullOrEmpty(playersName)) {
            Debug.Log("Player didn't input their name.");
            NoNameEnteredCanvas.enabled = true;
            nameCanvas.enabled = false;
            //UnityEditor.EditorUtility.DisplayDialog("No Name Entered", "Please enter your name in the box so you can begin.", "OK");
        }
        else {
            GameManagement.manage.playerName = playersName;
            Application.LoadLevel(0);
        }
    }

    public void StartGame() {
        nameCanvas.enabled = true;
    }

    public void LoadGame() {
    Debug.Log(GameManagement.manage.HasSaveFile());
        if (!GameManagement.manage.HasSaveFile()) {
            //Window.alert("no saved games, please start a new one");
            NoSaveGameCanvas.enabled = true;
            //UnityEditor.EditorUtility.DisplayDialog("No Saved Game","This computer does not have a saved game. Please click 'New Game' and enjoy! =D","OK", "Cancel");
        }
        else { 
            GameManagement.manage.Load();
            Application.LoadLevel(GameManagement.manage.CurrentLevel);
        }
    }

    public void EndGame(){
        Application.Quit();
    }

    public void OnMouseEnter() {
        GetComponent<Renderer>().material.color = Color.red;		//Change Color to red!
    }

    public void OnMouseExit(){
        GetComponent<Renderer>().material.color = Color.white;		//Change Color to white!
    }
}