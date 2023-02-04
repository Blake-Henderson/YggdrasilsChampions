using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    //these are the panels needed to do battle
    [SerializeField] GameObject BattlePanel;
    [SerializeField] GameObject CutInPanel;

    //These are the images for the cut-in animation
    [SerializeField] Image C1Bust;
    [SerializeField] Image C2Bust;

    //These are the images for the characters
    [SerializeField] Image C1Battle;
    [SerializeField] Image C2Battle;

    //This is the textboxes to display character 1's stats, health, and Character portrait near health
    [SerializeField] Image C1Port;
    [SerializeField] Slider C1HealthBar;
    [SerializeField] TextMeshProUGUI C1HealthText;
    [SerializeField] TextMeshProUGUI C1AtkText;
    [SerializeField] TextMeshProUGUI C1DefText;
    [SerializeField] TextMeshProUGUI C1SpdText;
    [SerializeField] TextMeshProUGUI C1EvdText;

    //This is the textboxes to display character 2's stats and health
    [SerializeField] Image C2Port;
    [SerializeField] Slider C2HealthBar;
    [SerializeField] TextMeshProUGUI C2HealthText;
    [SerializeField] TextMeshProUGUI C2AtkText;
    [SerializeField] TextMeshProUGUI C2DefText;
    [SerializeField] TextMeshProUGUI C2SpdText;
    [SerializeField] TextMeshProUGUI C2EvdText;

    //These are the textboxes which will prompt users or hold numbers for attacks
    [SerializeField] TextMeshProUGUI C1Textbox;
    [SerializeField] TextMeshProUGUI C2Textbox;
    [SerializeField] TextMeshProUGUI BattleTextbox;

    //These are the empty game objects that the buttons are under
    [SerializeField] GameObject C1Buttons;
    [SerializeField] GameObject C1RollBut;
    [SerializeField] GameObject C2Buttons;
    [SerializeField] GameObject C2RollBut;

    //This is the image that we will apply the Die facing on
    [SerializeField] Image DiceD6;
    //This should contain 6 elements with 6 die facings
    [SerializeField] List<Sprite> DiceRolls;
    
    //These functions set the respective panels to active or not
    public void battlePan(bool s)
    {
        BattlePanel.SetActive(s);
    }

    public void cutinPan(bool s)
    {
        CutInPanel.SetActive(s);
    }

    //This function sets all sprites for future use
    public void setSprites(Character c1, Character c2)
    {
        //Sets all of Character 1's sprites
        C1Bust.sprite = c1.stats.baseStats.sprite;
        C1Battle.sprite = c1.stats.baseStats.sprite;
        C1Port.sprite = c1.stats.baseStats.sprite;

        //Sets all of Character 2's sprites
        C2Bust.sprite = c2.stats.baseStats.sprite;
        C2Battle.sprite = c2.stats.baseStats.sprite;
        C2Port.sprite = c2.stats.baseStats.sprite;
    }

    //This function sets all stat values
    public void setStats(Character c1, Character c2)
    {
        //Sets all of character 1's stat values
        C1HealthText.text = c1.stats.health + " / " + c1.stats.hp;
        C1AtkText.text = "ATK \n" + c1.stats.attack;
        C1DefText.text = "DEF \n" + c1.stats.defense;
        C1SpdText.text = "SPD \n" + c1.stats.speed;
        C1EvdText.text = "EVD \n" + c1.stats.evade;

        //Sets all of character 2's stat values
        C2HealthText.text = c2.stats.health + " / " + c2.stats.hp;
        C2AtkText.text = "ATK \n" + c2.stats.attack;
        C2DefText.text = "DEF \n" + c2.stats.defense;
        C2SpdText.text = "SPD \n" + c2.stats.speed;
        C2EvdText.text = "EVD \n" + c2.stats.evade;
    }

    //This function will update both healthbars
    public void updateHealth(Character c1, Character c2)
    {
        float f = 1;
        //This sets the value of the health bar according to the health values

        C1HealthBar.value = f * c1.stats.health / c1.stats.hp;
        C1HealthText.text = c1.stats.health + " / " + c1.stats.hp;

        C2HealthBar.value = f * c2.stats.health / c2.stats.hp;
        C2HealthText.text = c2.stats.health + " / " + c2.stats.hp;
    }

    //This function will set the battle text (the text between both characters)
    public void setBattleText(string s)
    {
        BattleTextbox.text = s;
    }
    //Self-explanatory
    public void setC1Text(string s)
    {
        C1Textbox.text = s;
    }
    public void setC2Text(string s)
    {
        C2Textbox.text = s;
    }

    //These functions are used to toggle the buttons "Defend" "Evade" on for each character
    public void setC1But(bool b)
    {
        C1Buttons.SetActive(b);
    }
    public void setC2But(bool b)
    {
        C2Buttons.SetActive(b);
    }

    //These functions are used to toggle the Roll buttons for each character
    public void setC1Roll(bool b)
    {
        C1RollBut.SetActive(b);
    }
    public void setC2Roll(bool b)
    {
        C2RollBut.SetActive(b);
    }

    //This function controls what Dice image is displayed and if it is currently active
    public void SetDieImg(bool b, int i)
    {
        DiceD6.gameObject.SetActive(b);
        DiceD6.sprite = DiceRolls[i - 1];
    }
}
