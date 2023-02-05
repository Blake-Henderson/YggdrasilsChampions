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
        PlayerInfoUI.instance.gameObject.SetActive(false);
        Battle.instance.finishEvent.AddListener(FightComplete);
        Battle.instance.BattleStart(manager.characters[manager.gm.turnCount], toFight[i]);
        toFight.RemoveAt(i);
        container.SetActive(false);
    }

    public void FightComplete()
    {
        PlayerInfoUI.instance.gameObject.SetActive(true);
        Battle.instance.finishEvent.RemoveListener(FightComplete);
        container.SetActive(true);
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
