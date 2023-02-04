using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameManager gm;
    public TurnManager tm;

    private int inventorySlot = 0;
    public GameObject inventoryPanel;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject descriptionBox;

    public void pullUpInventory()
    {
        inventoryPanel.SetActive(true);
        inventorySlot = 0;
        setDescription();
        Item[] items = tm.characters[gm.turnCount].inventory;
        slot1.GetComponent<TextMesh>().text = tm.characters[gm.turnCount].inventory[0].type;
        slot2.GetComponent<TextMesh>().text = tm.characters[gm.turnCount].inventory[1].type;
        slot3.GetComponent<TextMesh>().text = tm.characters[gm.turnCount].inventory[2].type;
    }

    public void pullUpInventory(Character c)
    {
        inventoryPanel.SetActive(true);
        inventorySlot = 0;
        setDescription(c);
        Item[] items = tm.characters[gm.turnCount].inventory;
        slot1.GetComponent<TextMesh>().text = c.inventory[0].type;
        slot2.GetComponent<TextMesh>().text = c.inventory[1].type;
        slot3.GetComponent<TextMesh>().text = c.inventory[2].type;
    }

    public void setDescription()
    {
        descriptionBox.GetComponent<TextMesh>().text = tm.characters[gm.turnCount].inventory[inventorySlot].type;
    }

    public void setDescription(Character c)
    {
        descriptionBox.GetComponent<TextMesh>().text =c.inventory[inventorySlot].type;
    }
    public void useItem()
    {
        Item item = tm.characters[gm.turnCount].inventory[inventorySlot];
        switch (item.type)
        {
            case "Stat Boost":
                int choice = Random.Range(1, 5);
                tm.characters[gm.turnCount].stats.raiseStat(choice, 1);
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
        inventoryPanel.SetActive(false);
    }
}
