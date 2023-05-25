using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject GameOverMenu;
    public TextMeshProUGUI GameOverText;

    public void Awake()
    {
        Instance = this;
    }

    public void EndGame(string winningPlayer)
    {
        StartCoroutine(LoadScene());
        this.GameOverMenu.SetActive(true);
        this.GameOverText.text = winningPlayer + " lost!";
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("MainMenu");
    }
}