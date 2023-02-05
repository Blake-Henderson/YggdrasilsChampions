using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI charname, hp;
    
    public void UpdateDisplay(StatManager stats)
    {
        hp.text = stats.health + "/" + stats.hp;
    }
}
