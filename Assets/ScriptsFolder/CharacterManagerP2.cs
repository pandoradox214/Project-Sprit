using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterManagerP2 : MonoBehaviour
{
    public CharacterDatabase CharacterDB;//This variable will be use to access the methods or functions from the CharacterDatabase class\
    public SpriteRenderer characterFrame;
    public SpriteRenderer ArtworkSprite;
    private int checkedOption = 0; //Variable that will keep track of the Character Selected

    void Start()
    {
        if (!PlayerPrefs.HasKey("checkedOption"))
        {
            checkedOption = 0;
        }
        else
        {
            LoadIt();
        }
        updateCharacter(checkedOption);
    }
    private void updateCharacter(int sOption)
    {
        CharacterSelect character = CharacterDB.GetCharacterSelect(sOption); //A CharacterSelect type variable that is initialized to the Selected Character
        ArtworkSprite.sprite = character.characterSprite;
        characterFrame.sprite = character.CharacterFrame;
    }
    public void LoadIt()
    {
        checkedOption = PlayerPrefs.GetInt("checkedOption");
    }
    public void SaveIt()
    {
        PlayerPrefs.SetInt("checkedOption", checkedOption);
    }

    public void RightOption()
    {
        checkedOption++;//Increments the selecteedOption by 1
        if (checkedOption >= CharacterDB.CharacterCount)//If selected option is more than or equal to the number of characters
        {
            checkedOption = 0;//Go back to index 0
        }
        updateCharacter(checkedOption);
        SaveIt();
    }
    public void LeftOption()
    {
        checkedOption--;//Increments the selecteedOption by 1
        if (checkedOption < 0)//If selected option is less than 0
        {
            checkedOption = CharacterDB.CharacterCount-1;//Go back to the last index 
        }
        updateCharacter(checkedOption);
        SaveIt();
    }

}
