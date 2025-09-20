using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine; // or Engine.*; instead of adding the next line
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = -2f; // How fast to slide down
    [SerializeField] private float wallHangTime = 0.3f; // Hang before sliding
    [SerializeField] private Image StaminaBar;
    [SerializeField] private GameObject aStaminaBar;
    [SerializeField] private float MaxStamina;
    [SerializeField] private float Stamina;
    [SerializeField] private float staminaDrainRate;// difficulty.cs 
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private VictoryScreen VictoryScreen;
    [SerializeField] private Difficulty Difficulty;

    private float staminaImmunityTimer = 0f;
    private bool staminaImmunityActive = false;
    private bool immunityCountdownStarted = false;
    private bool isWallSliding;
    private float wallHangTimer;
    private bool isJumping = false;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private Vector3 startScale;
    private bool dead;
    public int foodsCollected = 0;

    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        startScale = transform.localScale;
    }
    private void Update()
    {
        // adjusts stamina drain rate according to the difficulty of the game.
        if (Difficulty.Instance != null)
        {
            staminaDrainRate = Difficulty.Instance.staminaDrainRate;
        }
        else
        {
            Debug.LogWarning("Difficulty.Instance is null, using default staminaDrainRate!");

            staminaDrainRate = 8f;
        }
        // first checks if player is at zero stamina and hasnt died yet.
        if (Stamina == 0 && !dead)
        {
            HandleDeath();
        }

        horizontalInput = Input.GetAxis("Horizontal");
        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(startScale.x), startScale.y, startScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(startScale.x), startScale.y, startScale.z);
        //sets animation parameters


        // stamina depeletion
        bool isTryingToMove = horizontalInput != 0 || body.velocity.y > 0 && !isGrounded();

        if (staminaImmunityActive)
        {
            if (isTryingToMove)
            {
                immunityCountdownStarted = true;
            }

            // Start countdown only when movement begins
            if (immunityCountdownStarted)
            {
                staminaImmunityTimer -= Time.deltaTime;
                if (staminaImmunityTimer <= 0f)
                {
                    staminaImmunityActive = false;
                    immunityCountdownStarted = false;
                }
            }

            // Keep stamina full and prevent drain
            Stamina = MaxStamina;
        }
        else if (horizontalInput != 0 || body.velocity.y > 0 && !isGrounded()) // if not jumping up. a.k.a freefalling.
        {
            Stamina -= staminaDrainRate * Time.deltaTime;
            if (Stamina < 0) Stamina = 0;

        }
        StaminaBar.fillAmount = Stamina / MaxStamina;


        // Always update grounded so animator knows when player is on the ground
        anim.SetBool("grounded", isGrounded());

        // Only play walk animation when the player is on the ground
        anim.SetBool("walk", horizontalInput != 0 && isGrounded());

        // If we were marked as jumping but have landed, clear the jumping flag
        if (isJumping && isGrounded())
        {
            isJumping = false;
        }


        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if ((onWall() && !isGrounded()))
            {
                body.gravityScale = 0; // when player jumps, gravity is gone
                body.velocity = Vector2.zero; // velocity is now also gone while in the air
                                              // still need code that can drop you back down.
            }
            else body.gravityScale = 3; // if not under cooldown, gravity comes back
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else wallJumpCooldown += Time.deltaTime;
        // wall sliding
        if (onWall() && !isGrounded())
        {
            wallHangTimer += Time.deltaTime;
            if (wallHangTimer > wallHangTime)
            {
                isWallSliding = true;
                body.velocity = new Vector2(body.velocity.x, wallSlideSpeed);
            }
            else
            {
                isWallSliding = false;
            }
        }
        else
        {
            wallHangTimer = 0f;
            isWallSliding = false;
        }
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            // Ensure other animator booleans won't prevent the jump state from playing
            anim.SetBool("walk", false);
            anim.SetBool("grounded", false);
            anim.SetTrigger("jump");
            isJumping = true;
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 10);
            wallJumpCooldown = 0;
            // Clear conflicting booleans so the AnyState -> jump transition can occur naturally
            anim.SetBool("walk", false);
            anim.SetBool("grounded", false);
            anim.SetTrigger("jump"); // <-- Play jump animation on wall jump
            isJumping = true;
            // x force away from wall, y force up the wall.
            // it pushes away from the wall, then it uses another force to go up, before the user inputs a direction again, which can 
            // be going up to the wall once more. For the opposite direction, we just reverse the forces. 
            // mathf.sign returns either -1 or +1 depending on direction of player. 
            // the - in front of the Mathf pushes it in opposite direction aka pushes away from wall.
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private bool isGrounded()
    {
        // creates a ray that detects when you hit an intersection, so it knows when you're grounded, but has weakness so we need boxcast.
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        // if the ray 
        // 
        // hits no collider, and returns null then it is false.
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


    // there is unlimited stamina for 2 seconds afer you begin to move after eating.
    public void addStamina(float _value)
    {
        Stamina = Mathf.Clamp(Stamina + _value, 0, MaxStamina);
        staminaImmunityActive = true;
        immunityCountdownStarted = false; // Wait for movement to start timer
        staminaImmunityTimer = 2f;        // Immunity lasts for 2 seconds after move
    }

    public void AddFood()
    {
        foodsCollected++;
        if (foodsCollected >= 9)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        if (VictoryScreen != null)
        {
            VictoryScreen.gameWon();
            aStaminaBar.gameObject.SetActive(false);
        }

        body.velocity = Vector2.zero; // stops movement
        GetComponent<PlayerMovement>().enabled = false; // freezes controller
    }

    public void HandleDeath()
    {
        dead = true;
        anim.SetTrigger("dead");                    // Play death animation
        body.velocity = Vector2.zero;               // Stop movement
        aStaminaBar.gameObject.SetActive(false);     // Hide stamina bar

        StartCoroutine(ShowGameOverWithDelay()); // gives delay to death animation instead of insta- game over screen
        // disables keyboard press
        GetComponent<PlayerMovement>().enabled = false;
    }

    private IEnumerator ShowGameOverWithDelay()
    {
        yield return new WaitForSeconds(3f); //  3 second delay 
        if (gameOverScreen != null)
        {
            gameOverScreen.gameLost();
        }
    }

    public void ResetState()
    {
        dead = false;
        foodsCollected = 0;
        Stamina = MaxStamina;
        // Reset other critical flags and positions as needed
    }

    // Get stamina value for UI or other purposes
    public float GetStamina()
    {
        return Stamina;
    }

    // Set stamina value for external scripts (e.g., Spike). Kept simple as requested.
    public void SetStamina(float value)
    {
        Stamina = value;
    }

    public int GetFood()
    {
        // debug
        Debug.Log("Food collected: " + foodsCollected);
        return foodsCollected;
    }
}