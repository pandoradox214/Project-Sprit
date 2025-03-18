using UnityEngine;
//The Script that Loads The Choice of Character of Player 2 from Character Selection
public class Player2Script : MonoBehaviour
{
    public CharacterDatabase CharacterDB;//This variable will be use to access the methods or functions from the CharacterDatabase class\
    public SpriteRenderer ArtworkSprite;

    public Sprite Alucards;
    public Sprite Vampy;
    public Sprite Staarbles;

    public GameObject Dagger;
    public GameObject Arrow;
    public GameObject FireBall;

    public ProjectileScript daggerPrefab;
    public ProjectileScript arrowPrefab;
    public ProjectileScript FireBallPrefab;

    public Animator animator;
    public TurnBasedScript tb;

    private int checkedOption = 0; //Variable that will keep track of the Character Selected
    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();//Get the Animator Component of the Object that is attach with this script (Player2)
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
    private void Update()
    {
        if (tb.horizontalMove < 0 && (Input.GetKey(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.A))))
        {
            ArtworkSprite.flipX = true;
            animator.SetBool("Running2", true);
        }
        else if (tb.horizontalMove > 0 && (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.D))))
        {
            ArtworkSprite.flipX = false;
            animator.SetBool("Running2", true);
        }
        else if (tb.hasMove)
        {
            ArtworkSprite.flipX = true;
            animator.SetBool("Running2", false);
        }
        else
        {
            ArtworkSprite.flipX = true;
            animator.SetBool("Running2", false);
        }
    }

    private void updateCharacter(int sOption)
    {
        CharacterSelect character = CharacterDB.GetCharacterSelect(sOption); //A CharacterSelect type variable that is initialized to the Selected Character
        if (sOption == 0)
        {
            ArtworkSprite.sprite = Alucards;
            animator.Play("Base Layer.AlucardsIdle");
            tb.projectile = Dagger;
            tb.projectilePrefab = daggerPrefab;
        }
        else if (sOption == 1)
        {
            ArtworkSprite.sprite = Staarbles;
            animator.Play("Base Layer.StaarblesIdle");
            tb.projectile = Arrow;
            tb.projectilePrefab = arrowPrefab;
        }
        else if (sOption == 2)
        {
            ArtworkSprite.sprite = Vampy;
            animator.Play("Base Layer.VampyIdle");
            tb.projectile = FireBall;
            tb.projectilePrefab = FireBallPrefab;
        }
    }
    public void LoadIt()
    {
        checkedOption = PlayerPrefs.GetInt("checkedOption");
    }
}
