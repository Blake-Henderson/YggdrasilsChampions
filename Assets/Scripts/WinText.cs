using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    public static WinText instance;
    public TextMeshProUGUI text;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Win()
    {
        gameObject.SetActive(true);
        text.text = TurnManager.instance.characters[TurnManager.instance.gm.turnCount] + " wins!";
    }
}
