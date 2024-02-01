using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] KeyCode key;

    private void Start(){
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (key == KeyCode.None || key == KeyCode.Escape) {
            Debug.Log("Keycode either set to None or to Escape. Defaulting key to Space");
            key = KeyCode.Space;
        }
        text.text = "PRESS " + key;
        
    }
    private void Update() {
        if (Input.GetKeyDown(key)) {
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

}