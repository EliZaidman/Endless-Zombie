using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private GameObject _menuButtons, _credits;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayCredits()
    {
        _title.text = "Credits";
        _menuButtons.SetActive(false);
        _credits.SetActive(true);
    }

    public void CloseCredits()
    {
        _title.text = "Protect The Heart";
        _credits.SetActive(false);
        _menuButtons.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
