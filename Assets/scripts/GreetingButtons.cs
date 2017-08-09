using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GreetingButtons : MonoBehaviour
{
    [SerializeField]
    Text record;
    GreetingButtons recordInt;
    void Start() { }

    void Update() {
        record.text = ("Record Game: " + PlayerPrefs.GetInt("record"));
    }

    public void GoStartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void GoExitGame()
    {
        Application.Quit();
    }
}
