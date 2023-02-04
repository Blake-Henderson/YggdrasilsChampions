using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameManager gm;
    public GameObject mainPanel;
    public GameObject playerSelectPanel;
    
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
        SceneManager.LoadScene("BoardTest");
    }

    public void back()
    {
        mainPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    private void Start()
    {
        back();
    }
}
