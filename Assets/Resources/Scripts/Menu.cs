using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    // Przypisanie klawisza do póŸniejszego odpalenia gry
    [SerializeField] KeyCode key;

    private void Start() {
        // Pobranie komponentu TMP_Text z dziecka tego GameObjectu
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        // Sprawdzenie, czy key ma jak¹œ wartoœæ i czy ta wartoœæ to jest "Escape"
        // je¿eli key nie ma wartoœci lub jest przypisana na "Escape"
        // ustalana jest wartoœæ key na "Space"
        if (key == KeyCode.None || key == KeyCode.Escape) {
            Debug.Log("Keycode either set to None or to Escape. Defaulting key to Space");
            key = KeyCode.Space;
        }
        // Ustawienie tekstu w komponencie TMP_Text z informacj¹ o klawiszu do naciœniêcia
        text.text = "PRESS " + key;
        
    }
    private void Update() {
        // Sprawdzenie, czy klawisz zosta³ naciœniêty, a nastêpnie przejœcie do kolejnej sceny
        if (Input.GetKeyDown(key)) {// Sprawdzenie, czy klawisz zosta³ naciœniêty, a nastêpnie przejœcie do kolejnej sceny
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
        // Sprawdzenie, czy klawisz Escape zosta³ naciœniêty, a nastêpnie wyjœcie z aplikacji
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}