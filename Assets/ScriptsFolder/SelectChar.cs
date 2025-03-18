using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChar : MonoBehaviour
{
    [SerializeField] private AudioSource SelectorSoundEffect;

    void Start()
    {

    }
    public void playEffect()
    {
        SelectorSoundEffect.Play();
    }
    
}
