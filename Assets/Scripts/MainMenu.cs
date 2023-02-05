using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameManager gm;
    public GameObject mainPanel;
    public GameObject playerSelectPanel, levelPanel;
    public TextMeshProUGUI levelText;
    int selectedLevel = 0;
    public string[] levels;
    
    public void quit()
    {
        Application.Quit();
    }
    
    public void play()
    {
        mainPanel.SetActive(false);
        playerSelectPanel.SetActive(true);
    }

    public void playerSelect(int players)
    {
        gm.playercount = players;
        playerSelectPanel.SetActive(false);
        levelPanel.SetActive(true);
        levelText.text = levels[selectedLevel];
    }

    public void levelSelect()
    {
        GameManager.instance.turnCount = 0;
        SceneManager.LoadScene(levels[selectedLevel]);
    }

    public void ChangeSelectedLevel(int change)
    {
        selectedLevel += change;
        if (selectedLevel < 0) selectedLevel = levels.Length - 1;
        else selectedLevel %= levels.Length;
        levelText.text = levels[selectedLevel];

    }

    public void back()
    {
        mainPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    private void Start()
    {
        gm = GameManager.instance;
        back();
    }
}
