using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField] KeyCode key;
    GameObject statistics;
    TMP_Text[] texts;
    private void Start()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (key == KeyCode.None || key == KeyCode.Escape)
        {
            Debug.Log("Keycode either set to None or to Escape. Defaulting key to Space");
            key = KeyCode.Space;
        }
        statistics = gameObject.transform.GetChild(3).GetComponent<Transform>().gameObject;
        Debug.Log(statistics.name);
        text.text = "PRESS " + key + "TO GO BACK TO MENU";
        for (int i=0; i<statistics.transform.childCount; i++){
            Debug.Log(statistics.transform.GetChild(i));
            statistics.transform.GetChild(i).GetComponent<TMP_Text>().text = statistics.transform.GetChild(i).GetComponent<Transform>().gameObject.name + ": " + Statistics.stats[i].ToString();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

}