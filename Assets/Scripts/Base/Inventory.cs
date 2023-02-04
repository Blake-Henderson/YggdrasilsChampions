using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameManager gm;
    public TurnManager tm;
    public Item empty;
    private int inventorySlot = 0;
    public GameObject inventoryPanel;
    public GameObject rollManager;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject descriptionBox;

    public void pullUpInventory()
    {
        inventoryPanel.SetActive(true);
        rollManager.SetActive(false);
        inventorySlot = 0;
        setDescription();
        Item[] items = tm.characters[gm.turnCount].inventory;
        slot1.GetComponent<TextMeshProUGUI>().text = tm.characters[gm.turnCount].inventory[0].type;
        slot2.GetComponent<TextMeshProUGUI>().text = tm.characters[gm.turnCount].inventory[1].type;
        slot3.GetComponent<TextMeshProUGUI>().text = tm.characters[gm.turnCount].inventory[2].type;
    }

    public void pullUpInventory(Character c)
    {
        inventoryPanel.SetActive(true);
        inventorySlot = 0;
        setDescription(c);
        Item[] items = tm.characters[gm.turnCount].inventory;
        slot1.GetComponent<TextMeshProUGUI>().text = c.inventory[0].type;
        slot2.GetComponent<TextMeshProUGUI>().text = c.inventory[1].type;
        slot3.GetComponent<TextMeshProUGUI>().text = c.inventory[2].type;
    }

    public void setDescription()
    {
        descriptionBox.GetComponent<TextMeshProUGUI>().text = tm.characters[gm.turnCount].inventory[inventorySlot].description;
    }

    public void setDescription(Character c)
    {
        descriptionBox.GetComponent<TextMeshProUGUI>().text =c.inventory[inventorySlot].description;
    }
    public void useItem()
    {
        Item item = tm.characters[gm.turnCount].inventory[inventorySlot];
        switch (item.type)
        {
            case "Stat Boost":
                tm.characters[gm.turnCount].inventory[inventorySlot] = empty;
                pullUpInventory();
                int choice = Random.Range(1, 5);
                tm.characters[gm.turnCount].stats.raiseStat(choice, 1);
                switch (choice)
                {
                    case 0:
                        descriptionBox.GetComponent<TextMeshProUGUI>().text = "HP increased!";
                        break;
                    case 1:
                        descriptionBox.GetComponent<TextMeshProUGUI>().text = "Attack increased!";
                        break;
                    case 2:
                        descriptionBox.GetComponent<TextMeshProUGUI>().text = "Defense increased!";
                        break;
                    case 3:
                        descriptionBox.GetComponent<TextMeshProUGUI>().text = "Speed increased!";
                        break;
                    case 4:
                        descriptionBox.GetComponent<TextMeshProUGUI>().text = "Evade increased!";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }
    public void useItem(Character c)
    {
        Item item = c.inventory[inventorySlot];
        switch (item.type)
        {
            case "Stat Boost":
                int choice = Random.Range(1, 5);
                c.stats.raiseStat(choice, 1);
                switch (choice)
                {
                    case 0:
                        //hp increased display
                        break;
                    case 1:
                        //attack increased display
                        break;
                    case 2:
                        //defense increased display
                        break;
                    case 3:
                        //speed increased display
                        break;
                    case 4:
                        //evade increased display
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        inventoryPanel.SetActive(false);
    }

    public void setSlot(int slot)
    {
        inventorySlot = slot;
        setDescription();
    }

    public void close()
    {
        rollManager.SetActive(true);
        inventoryPanel.SetActive(false);
    }
}
