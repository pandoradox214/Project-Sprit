using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterManagerP1 : MonoBehaviour
{
    public CharacterDatabase characterDB;//This variable will be use to access the methods or functions from the CharacterDatabase class\
    public SpriteRenderer CharacterFrame;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0; //Variable that will keep track of the Character Selected

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))//If there are save data from the previous session
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }
    private void UpdateCharacter(int sOption)
    {
        CharacterSelect character = characterDB.GetCharacterSelect(sOption); //A CharacterSelect type variable that is initialized to the Selected Character
        artworkSprite.sprite = character.characterSprite;
        CharacterFrame.sprite = character.CharacterFrame;
    }

    public void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
    //Stores and Accesses player preferences between game sessions
    public void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void RightOption()
    {
        selectedOption++;//Increments the selecteedOption by 1
        if (selectedOption >= characterDB.CharacterCount)//If selected option is more than or equal to the number of characters
        {
            selectedOption = 0;//Go back to index 0
        }
        UpdateCharacter(selectedOption);
        Save();
    }
    public void LeftOption()
    {
        selectedOption--;//Increments the selecteedOption by 1
        if (selectedOption < 0)//If selected option is less than 0
        {
            selectedOption = characterDB.CharacterCount-1;//Go back to the last index 
        }
        UpdateCharacter(selectedOption);
        Save();
    }

}
