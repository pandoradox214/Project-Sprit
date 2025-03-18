using UnityEngine;
using TMPro;
public enum BattleState { P1, P2}

public class BattleSystem : MonoBehaviour
{
    public TextMeshProUGUI stateMessage;
    public static BattleState state;
    public ProjectileScript projectile;
    public GameObject player1, player2;
    private TurnBasedScript currentPlayer;
    public static bool isMoving = false;
    public static bool isFiring = false;
    
    public static bool isFlying = false;
    public TextMeshProUGUI movement1, movement2;

    private void Start()
    {
        state = BattleState.P1;
        currentPlayer = player1.GetComponent<TurnBasedScript>();
        stateMessage.text = "Player 1's Turn";
        movement2.text = "MOVEMENT: KEY UP / NO MOVEMENTS LEFT ";//Added by Ylman
        isMoving = true;
        isFiring = true;
    }

    void Update()
    {
        if (!isFlying)
        {
            if (currentPlayer != null)
            {
                if (!PauseMenu.isPaused && Time.timeScale != 0f)//If Resumed Turn Base Continues
                {
                    if (isMoving)
                    {
                        if (state == BattleState.P1)
                        {
                            float decoy = (currentPlayer.maxMovements / 250) * 100;
                            movement1.text = "MOVEMENT: " + (int)decoy;
                        }
                        else if (state == BattleState.P2)
                        {
                            float decoy = (currentPlayer.maxMovements / 250) * 100;
                            movement2.text = "MOVEMENT: " + (int)decoy;
                        }
                        currentPlayer.ActionMove();
                        if (currentPlayer.HasMove())
                        {
                            if (state == BattleState.P1)
                            {
                                movement1.text = "MOVEMENT: KEY UP / NO MOVEMENTS LEFT " ;
                            }
                            else if (state == BattleState.P2)
                            {
                                movement2.text = "MOVEMENT: KEY UP / NO MOVEMENTS LEFT ";
                            }
                            isMoving = false;
                        }
                    }
                    if (isFiring)
                    {
                        //If the game was paused pause time should be greater and will ignore any input action 
                        //If the gamplay time was greater then allow the action to happen
                        //This Prevents Input Queue of Left Mouse button to the Game  when clicking the resume button
                        if (Time.time > PauseMenu.pauseTime)
                        {
                            currentPlayer.CalculateAngle();
                            //currentPlayer.CalculatePower();

                            currentPlayer.ActionUp();
                            if (currentPlayer.HasAimed())
                            {
                                isFiring = false;
                                endTurn();
                            }
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
    private void endTurn()
    {
        if (state == BattleState.P1)
        {
            currentPlayer.ClearTrajectory(); //Clears the Trajectory path
            currentPlayer.hasMove = currentPlayer.hasAimed = false;
            currentPlayer.maxMovements = 250;
            state = BattleState.P2;
            currentPlayer = player2.GetComponent<TurnBasedScript>();
            Debug.Log("Current player is Player 2");
            stateMessage.text = "Player 2's Turn";       
        }
        else if (state == BattleState.P2)
        {
            currentPlayer.ClearTrajectory(); //Clears the Trajectory path
            currentPlayer.hasMove = currentPlayer.hasAimed = false;
            currentPlayer.maxMovements = 250;
            state = BattleState.P1;
            currentPlayer = player1.GetComponent<TurnBasedScript>();
            Debug.Log("Current player is Player 1");
            stateMessage.text = "Player 1's Turn";
            
        }
        isMoving = isFiring = true; 
    }
}
