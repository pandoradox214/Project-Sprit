using Cinemachine;
using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;
    private SpriteRenderer r1, r2, currentSpriteRender;
    private TurnBasedScript Stats, Stats1;
    public bool hasHit = false; //hasHit bool variable to prevent the projectile from damaging more than once

    [SerializeField] private AudioSource DamageSoundEffect;
    [SerializeField] private AudioSource GroundHitSoundEffect;
    [SerializeField] private AudioSource ProjectileShootEffect;

    [SerializeField] private Material flashMaterial;
    private float flashDuration = 0.5f;
    [SerializeField] private Material originalMaterial;
    private Coroutine flashRoutine;

    public static bool locked;

    private void Start()
    {
     r1 = GameObject.Find("Player1").GetComponent<SpriteRenderer>();
     r2 = GameObject.Find("Player2").GetComponent<SpriteRenderer>();
     rb = GetComponent<Rigidbody2D>();
     Stats = GameObject.Find("Player1").GetComponent<TurnBasedScript>();
      Stats1 = GameObject.Find("Player2").GetComponent<TurnBasedScript>();
      ProjectileShootEffect.Play();
        BattleSystem.isFlying = true;
    }
    private void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    //The method belows damages the player and despawning the projectile
    float magnitudeVel()
    {
        Stats = GameObject.Find("Player1").GetComponent<TurnBasedScript>();
        return Stats.val;
    }

    //Method when the projectile is in contact with either of the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasHit)
        {
            if (collision.gameObject.TryGetComponent<TurnBasedScript>(out TurnBasedScript Pcomponent))
            {
                if (collision.gameObject.CompareTag("P1")) //If the Player 1 was hit the sprite will turn white for 0.2f seconds
                {
                    currentSpriteRender = r1;
                    Flash();
                    DamageSoundEffect.Play();

                }
                else if (collision.gameObject.CompareTag("P2")) //Else If the Player 1 was hit the sprite will turn white for 0.2f seconds
                {
                    currentSpriteRender = r2;
                    Flash();
                    DamageSoundEffect.Play();
                }

                Debug.Log(magnitudeVel()); //Displays the Damage in the console
                Pcomponent.takeDamage(magnitudeVel()); //Damage The Players
                hasHit = true;
                
                Destroy(gameObject, 1f);
            }
            else if (collision.gameObject.CompareTag("Terrain"))
            {
                GroundHitSoundEffect.Play();
                // Set the velocity and angular velocity to zero
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                // Destroy the projectile after a short delay
                Destroy(gameObject,0.1f);
            }

        }
        Destroy(gameObject, 5f);
    }


    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null||hasHit)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        currentSpriteRender.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(flashDuration);

        // After the pause, swap back to the original material.
        currentSpriteRender.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        currentSpriteRender = null;
    }


}
