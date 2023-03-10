using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    public PlayerInfo[] infos;
    public static PlayerInfoUI instance;

    private void Start()
    {
        instance = this;
        for (int i = 0; i <4; ++i)
        {
            infos[i].charname.text = TurnManager.instance.characters[i].name;
        }
    }


    private void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            infos[i].UpdateDisplay(TurnManager.instance.characters[i].stats);
            if (i == TurnManager.instance.gm.turnCount) infos[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 320f);
            else { infos[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160f); }
        }

    }
}
