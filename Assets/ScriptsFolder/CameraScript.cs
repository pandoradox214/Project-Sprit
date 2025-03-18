using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using System.ComponentModel.Design;

public class CameraScript : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] private AudioSource BattleBackgroundMusic;

    GameObject  d, a, f;

    private void Start()
    {
        BattleBackgroundMusic.Play();
    }
    void Update()
    {
        virtualCamera.Follow = ToFollow(); 
    }
    public Transform ToFollow()
    {
        //Either the instance of this object will occur once the player shoots
        a = GameObject.Find("Arrow(Clone)");
        d = GameObject.Find("Dagger(Clone)");
        f = GameObject.Find("FireBall(Clone)");

        if (a != null)
        {
            return a.transform;
        }
        else if (d != null)
        {
            return d.transform;
        }
        else if (f != null)
        {
            return f.transform;
        }
        else if (BattleSystem.state == BattleState.P1)
        {
            BattleSystem.isFlying = false;
            return Player1;       
        }
        else if (BattleSystem.state == BattleState.P2)
        {
            BattleSystem.isFlying = false;
            return Player2;
        }
        else
        {
            return null;
        }
    }

}
