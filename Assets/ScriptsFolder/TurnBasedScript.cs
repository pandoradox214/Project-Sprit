using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnBasedScript : MonoBehaviour
{
    public float val;
    public Slider Hp;
    public TextMeshProUGUI MessageState;
    public TextMeshProUGUI winText;
    public string sinoNanalo;
    public Rigidbody2D rb1;

    public Rigidbody2D p1;

    public float horizontalMove;
    public float forwardSpeed = 5f;
    public float maxMovements = 250;


    public ProjectileScript projectilePrefab;
    public GameObject projectile;

    public Transform launchOffSet;

    Vector2 DragStartPos;
    public GameObject projectileSpawner;

     public LineRenderer lr;

    //player stats
    [SerializeField] public float health = 100f;

    public bool hasMove = false;
    public bool hasAimed = false;

    public Animator animator;
    public Animator animBol;

    public TextMeshProUGUI AngleText;
    public TextMeshProUGUI PowerText;

    public int currentAngle;
    float currentPower;

    [SerializeField]private float minPower = 0f;
    [SerializeField]private float maxPower = 5f;
    [SerializeField]private Transform Rotator;

    Vector2 velocity;

    private void Start()
    {
        Hp.interactable = false;//Makes the Slider uninteractable
        PowerText.text = "POWER: " + 0 + "%";//Added By Ylman 
        AngleText.text = "ANGLE: " + 0 + "°";

    }

    public void Update()
    {
        SpawnerRotator(); //Rotates the position of the projectile spawner
    }

    public void CalculateAngle()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 dir = mousePos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            UpdateAngle((int)angle);
        }
        else if (Input.GetMouseButtonUp(0)) {
            AngleText.text = "ANGLE: " + 0 + "°";
        }
    }

 
    public int UpdateAngle(int angle)
    {
        currentAngle = angle;
        AngleText.text = "ANGLE: " + currentAngle + "°";
        return currentAngle;
    }


    //Added By Ylman
    public void UpdatePower()
    {
        if (velocity.x >= 190)
        {
            velocity = new Vector2(300f, velocity.y);
        }
        else if (velocity.x <= -190)
        {
            velocity = new Vector2(-300f, velocity.y);
        }

        currentPower = velocity.magnitude / 220f * 100;
        if (currentPower >= 100f)
        {
            PowerText.text = "POWER: " + 100 + "%";
        }
        else {
            PowerText.text = "POWER: " + (int)currentPower + "%";
        }
      
    }
    public void ActionMove() //Fix first dapat di maka move if mag aim
    {
        if (!PauseMenu.isPaused && Time.timeScale != 0f)//If Game is Resumed input will work
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                horizontalMove = Input.GetAxisRaw("Horizontal") * forwardSpeed;
                transform.position += new Vector3(horizontalMove, 0f, 0f) * Time.deltaTime * forwardSpeed;
                maxMovements--;
            }
            if (maxMovements == 0|| Input.GetMouseButtonDown(0))
            {
                horizontalMove = 0; //To stop the runninig animation
                hasMove = true; //The player can no longer move
            }

        }
    }
    public bool HasMove()
    {
        return hasMove;
    }
    public void ActionUp()
    {
        if (!PauseMenu.isPaused&&Time.timeScale!=0f)//If Game is Resumed input will work
        {
            //GetMouseButtonDown(0) is when the left click button of the mouse is press
            if (Input.GetMouseButtonDown(0))
            {
                hasMove = false;
                //Debug.Log("Aim ready");
                StartPull();
                animator.SetBool("Aiming1", true); //Animation for aiming player 1
                animator.SetBool("Aiming2", true); //Animation for aiming player 2
            }
            //When the button is being hold it will draw the estimated trajectory line. NOTE: Estimated not the Actual path 
            if (Input.GetMouseButton(0))
            {
                //DragEndPos is the last position of the pixel in the game screen
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Velocity is very self explainatory it is the rate of change of direction
                velocity = (DragEndPos - DragStartPos)*5;
                Vector2[] trajectory = Plot(projectile.GetComponent<Rigidbody2D>(), (Vector2)transform.position, velocity, 500);
                lr.positionCount = trajectory.Length;
                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; i++)
                {
                    positions[i] = trajectory[i];
                }
                lr.SetPositions(positions);
                //Function Call Added By Ylman
                UpdatePower();
            }
            //When the button is released this statement will call the FireArrow method which makes the projectile move
            if (Input.GetMouseButtonUp(0))
            {
                hasMove = false;
                animBol.Play("Base Layer.FireBolAnim");//Animation For FireBall
                animator.SetBool("Aiming1", false);
                animator.SetBool("Aiming2", false);
                hasAimed = true;
                PowerText.text = "POWER: " + 0 + "%";
                FireArrow();

            }
        }
    }
    public bool HasAimed()
    {
        return hasAimed;
    }
   void FireArrow()
    {
        if(!PauseMenu.isPaused&&Time.timeScale!=0f)//If Game is Resumed input will work
        {
            //DragEndPos is the last position of the pixel in the game screen
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("I am End Pos" + DragEndPos);
            //Velocity is very self explainatory 

            Vector2 _velocity = (DragEndPos - DragStartPos)*5;
            //projectile var is responsible for spawining the projectile 
            var projectile = Instantiate(projectilePrefab, launchOffSet.position, Quaternion.identity);
            //This line will give the rigidbody2d of the projectile a velocity

            //Function Call Added By Ylman


            if (_velocity.x >= 180f)
            {
                _velocity = new Vector2(300f, velocity.y);
            }
            else if (_velocity.x <= -180f)
            {
                _velocity = new Vector2(-300f, velocity.y);
            }

            projectile.GetComponent<Rigidbody2D>().velocity = _velocity;
            Debug.Log("I' am Magnitude:" + _velocity.magnitude);
            Debug.Log("I am velocity" +  _velocity);
            //damageAmp.velocityConvert(_velocity);
            if (_velocity.magnitude >= 60f)
            {
                this.val = 40f;
            }
            else if (_velocity.magnitude >= 40f && _velocity.magnitude <= 60f)
            {
                this.val = 30f;
            }
            else
            {
                this.val = 20f;
            }
        }
    }

    void StartPull()
    {
      DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("I am DragStartPos" + DragStartPos);
    }

    //Death Script
    public void takeDamage(float damage)
    {
        health -= damage;
        Hp.value = health;
        Debug.Log(health);
        if (health < 1)
        {
            MessageState.text = sinoNanalo;
            winText.text = sinoNanalo;
            Destroy(gameObject);
        }
    }

    void SpawnerRotator()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        //if (angle >= -145f && angle <= -50f)
        //{
        //    return;
        //}
        //else
        //{
            Rotator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
       // }        
    }
    public void ClearTrajectory()//Clears Line Renderer
    {
        lr.positionCount = 0;
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
            Vector2[] results = new Vector2[steps];
            float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
            Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;//Gravity Accelleration Formula
            float drag = 1 - timestep * rigidbody.drag;
            Vector2 moveStep = velocity * timestep;//Displacement Formula
            for (int i = 0; i < steps; i++)
            {
                moveStep += gravityAccel;
                moveStep *= drag;
                pos += moveStep;
                results[i] = pos;
            }
            return results;
    }
}

