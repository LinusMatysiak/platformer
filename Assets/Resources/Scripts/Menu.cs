using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    /* jak dzia�a ten skrypt?
     * zmienia wy�wietlany tekst w zale�no�ci od klawisza kt�ry przypiszemy.
     * p�niej pobiera ten klucz i w��cza lub wy��cza gre
     * 
     * jak u�ywa� ten skrypt?
     * skrypt nale�y podpi�c gdzie� w scenie najlepiej pod canvas
     * i p�niej podpi�� tekst kt�ry chcemy zmieni�
     */
    [SerializeField] KeyCode key;
    public TMP_Text text;
    public string scene;

    private void Start() {
        if (key == KeyCode.None || key == KeyCode.Escape) {
            Debug.Log("Keycode jest ustawiony albo na 'None' albo 'Escape', wi�c program domy�lnie ustawi Keycode na 'Space'");
            key = KeyCode.Space;
        }
        text.text = "PRESS " + key;
        
    }
    private void Update() {
        if (Input.GetKeyDown(key)) {
            SceneManager.LoadScene(scene);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            // wy��cza gre
            Application.Quit();
        }
    }
}