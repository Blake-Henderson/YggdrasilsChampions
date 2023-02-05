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

    public bool inBattle = false;

    private Character tempC;

    public void Start()
    {
        gm = GameManager.instance;
    }
    public void pullUpInventory()
    {
        inventoryPanel.SetActive(true);
        rollManager.SetActive(false);
        inventorySlot = 0;
        setDescription();
        tempC = tm.characters[gm.turnCount];
        Item[] items = tempC.inventory;
        slot1.GetComponent<TextMeshProUGUI>().text = tempC.inventory[0].type;
        slot2.GetComponent<TextMeshProUGUI>().text = tempC.inventory[1].type;
        slot3.GetComponent<TextMeshProUGUI>().text = tempC.inventory[2].type;
    }

    public void pullUpInventory(Character c)
    {
        tempC = c;
        inventoryPanel.SetActive(true);
        inventorySlot = 0;
        setDescription(c);
        Item[] items = c.inventory;
        slot1.GetComponent<TextMeshProUGUI>().text = c.inventory[0].type;
        slot2.GetComponent<TextMeshProUGUI>().text = c.inventory[1].type;
        slot3.GetComponent<TextMeshProUGUI>().text = c.inventory[2].type;
    }

    public void setDescription()
    {
        descriptionBox.GetComponent<TextMeshProUGUI>().text = tm.characters[gm.turnCount].inventory[inventorySlot].description;
    }

    public void setDescription(Item i)
    {
        descriptionBox.GetComponent<TextMeshProUGUI>().text = i.description;
    }

    public void setDescription(Character c)
    {
        descriptionBox.GetComponent<TextMeshProUGUI>().text =c.inventory[inventorySlot].description;
    }
    public void useItem()
    {
        Debug.Log("USE ITEM CALLED");
        Item item;
        if (tempC == null)
        {
            item = tm.characters[gm.turnCount].inventory[inventorySlot];
        }
        else
        {
            Debug.Log(inventorySlot);
            item = tempC.inventory[inventorySlot];
        }

        if (!inBattle)
        {

            switch (item.type)
            {
                case "Stat Boost":
                    tempC.inventory[inventorySlot] = empty;
                    pullUpInventory();
                    int choice = Random.Range(1, 5);
                    tempC.stats.raiseStat(choice, 1);
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
        else
        {
            Debug.Log("INSIDE BATTLE ITEMS");
            switch (item.type)
            {
                case "Hidden Blade":
                    Debug.Log("HIDDEN BLADE INSIDE CASE");
                    if (Battle.instance.loop1 == true && Battle.instance.notEmpty && tempC == Battle.instance.c1temp)
                    {
                        Debug.Log("C1TEMP USED ITEM");
                        Battle.instance.c1temp.stats.tempAttack += 2;
                        close();
                    }
                    else if (Battle.instance.loop2 == true && Battle.instance.notEmpty && tempC == Battle.instance.c2temp)
                    {
                        Debug.Log("C2TEMP USED ITEM");
                        Battle.instance.c2temp.stats.tempAttack += 2;
                        close();
                    }
                    tempC.inventory[inventorySlot] = empty;
                    break;
                case "Bribe Bag":
                    if (Battle.instance.loop1 == true && Battle.instance.notEmpty && tempC == Battle.instance.c1temp)
                    {
                        Battle.instance.c1temp.stats.canRun = true;
                        close();
                    }
                    else if (Battle.instance.loop2 == true && Battle.instance.notEmpty && tempC == Battle.instance.c2temp)
                    {
                        Battle.instance.c2temp.stats.canRun = true;
                        close();
                    }
                    tempC.inventory[inventorySlot] = empty;
                    break;

                default:
                    break;
            }

        }
    }
    public void useItem(Character c)
    {
        Item item = c.inventory[inventorySlot];
        if (!inBattle)
        {
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
        }
        else
        {
            switch (item.type)
            {
                case "Hidden Blade":
                    c.stats.tempAttack += 2;
                    break;
                case "Bribe Bag":
                    c.stats.canRun = true;
                    break;
            }

        }
        //inventoryPanel.SetActive(false);
    }

    public void setSlot(int slot)
    {
        Debug.Log("SET SLOT CALLED");
        inventorySlot = slot;
        setDescription(tempC.inventory[inventorySlot]);
    }

    public void setChar(Character c)
    {
        tempC = c;
    }

    public void close()
    {
        if (!inBattle)
        {
            rollManager.SetActive(true);
        }
        else
        {
            if(Battle.instance.loop1 == true)
            {
                Battle.instance.invOpen = false;
                Battle.instance.notEmpty = false;
            }
            else if(Battle.instance.loop2 == true)
            {
                Battle.instance.invOpen = false;
                Battle.instance.notEmpty = false;
            }
        }
        inBattle = false;
        inventoryPanel.SetActive(false);
    }

}
