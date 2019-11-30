using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private Renderer _renderer;
    [SerializeField] private SpriteRenderer sprite;
    private void Awake() {
        _renderer = GetComponent<Renderer>();
    }
    public void BeginButton() {
        SceneManager.LoadScene(1);
    }

    public void EndGame(){
        Application.Quit();
    }

    public void OnMouseEnter() {
        _renderer.material.color = Color.red;		//Change Color to red!
    }

    public void OnMouseExit(){
        _renderer.material.color = Color.white;		//Change Color to white!
    }
}