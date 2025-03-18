using UnityEngine;
//The Script that Loads The Choice of Character of Player 1 from Character Selection
public class Player1Script : MonoBehaviour
{
    public CharacterDatabase characterDB;//This variable will be use to access the methods or functions from the CharacterDatabase class\
    public SpriteRenderer artworkSprite;

    public Sprite Alucards;
    public Sprite Staarbles;
    public Sprite Vampy;

    public GameObject Dagger;
    public GameObject Arrow;
    public GameObject FireBall;

    public ProjectileScript daggerPrefab;
    public ProjectileScript arrowPrefab;
    public ProjectileScript FireBallPrefab;


    public Animator animator;
    public TurnBasedScript tb;

    private int selectedOption = 0; //Variable that will keep track of the Character Selected

    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();//Get the Animator Component of the Object that is attach with this script (Player1)
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }
    private void Update()
    {
        if (tb.horizontalMove < 0 && (Input.GetKey(KeyCode.LeftArrow)||(Input.GetKey(KeyCode.A))))
        {
            artworkSprite.flipX = true;
            animator.SetBool("Running1", true);
        }
        else if (tb.horizontalMove > 0 && (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))))
        {
            artworkSprite.flipX = false;
            animator.SetBool("Running1", true);
        }
        else if (tb.hasMove)
        {
            artworkSprite.flipX = false;
            animator.SetBool("Running1", false);
        }
        else
        {
            artworkSprite.flipX = false;
            animator.SetBool("Running1", false);
        }
    }
    private void UpdateCharacter(int sOption)
    {
        CharacterSelect character = characterDB.GetCharacterSelect(sOption); //A CharacterSelect type variable that is initialized to the Selected Character
                                                                             // artworkSprite.sprite = character.characterSprite;
        if (sOption == 0)
        {
            artworkSprite.sprite = Alucards;
            animator.Play("Base Layer.AlucardsIdle");
            tb.projectile = Dagger;
            tb.projectilePrefab = daggerPrefab;
        }
        else if (sOption == 1)
        {
            artworkSprite.sprite = Staarbles;
            animator.Play("Base Layer.StaarblesIdle");
            tb.projectile = Arrow;
            tb.projectilePrefab = arrowPrefab;
        }
        else if (sOption == 2)
        {
            artworkSprite.sprite = Vampy;
            animator.Play("Base Layer.VampyIdle");
            tb.projectile = FireBall;
            tb.projectilePrefab = FireBallPrefab;
        }
    }

    public void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
