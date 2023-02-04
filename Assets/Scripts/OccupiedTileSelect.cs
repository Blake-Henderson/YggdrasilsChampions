using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OccupiedTileSelect : MonoBehaviour
{
    List<Character> toFight = new List<Character>();
    public GameObject[] buttons = new GameObject[3];
    public GameObject container;
    public TextMeshProUGUI[] buttonTexts;
    public TurnManager manager;

    public void DisplayOptions()
    {
        if (toFight.Count == 0)
        {
            Debug.Log("end");
            EndDisplay();
            return;
        }
        container.SetActive(true);
        foreach (GameObject o in buttons) o.SetActive(false);
        for (int i = 0; i < toFight.Count; ++i)
        {
            buttons[i].SetActive(true);
            buttonTexts[i].text = "Fight " + toFight[i].name;
        }
    }

    public void Fight(int i)
    {
        Debug.Log("Fighting " + toFight[i].name);
        toFight.RemoveAt(i);
        DisplayOptions();
    }

    public void DisplayOptions(List<Character> list)
    {
        toFight = list;
        DisplayOptions();
    }

    public void EndDisplay()
    {
        container.SetActive(false);
        manager.FinishInterrupt(true);
    }
}
