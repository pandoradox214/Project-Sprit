using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{
    public CharacterSelect[] character; //Creating an array data structure with CharacterSelect type which is referrenced to the CharacterSelect Class

    public int CharacterCount //Store the number of Characters in the Game
    {
        get { return character.Length; } //Return the number of Character we have in the Array
    }
    public  CharacterSelect GetCharacterSelect(int index) { return character[index]; } //Function that retrieves the selected Character information 
}
