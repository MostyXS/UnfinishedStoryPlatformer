using Game.Combat.Common;
using Game.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField] float accelerationSpeed;
    [SerializeField] float deccelerationSpeed;
    [SerializeField] float minSpeed = 0, maxSpeed = 5;

    [Header("Jump Settings")]
    [SerializeField] float jumpPower = 10f;
    [SerializeField] Transform foot1, foot2;
    [SerializeField] float distanceToGround;
    [SerializeField] LayerMask groundMask;

    public static BasePlayerControls.PlayerActions Inputs { get; private set; }

    float defaultXScale;
    float xAxis;
    int dir;
    float currentVelMultiplier = 0;

    private Health myHealth;
    private Animator anim;
    private Rigidbody2D rb;

    #region Unity Methods
    private void Awake()
    {
        Inputs = new BasePlayerControls().Player;
        myHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultXScale = transform.localScale.x;

        Inputs.Attack.performed += Attack;
        
        Inputs.Jump.performed += Jump;
    }
    private void OnEnable()
    {
        Inputs.Enable();
    }
    private void OnDisable()
    {
        Inputs.Disable();   
    }
    private void Start()
    {
        myHealth.onDeath += PerformDeath;
    }

    private void LateUpdate()
    {
        ProcessMovement();
    }

    private void OnDrawGizmos()
    {
        if (foot1 != null)
            Gizmos.DrawRay(foot1.position, Vector2.down * distanceToGround);
        if (foot2 != null)
            Gizmos.DrawRay(foot2.position, Vector2.down * distanceToGround);
    }

    #endregion
    #region Control Methods
    private void Attack(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger(AnimNames.ATTACK);
        
    }
    private void Jump(InputAction.CallbackContext ctx)
    {
        if (CanJump())
        {
            rb.velocity += new Vector2(0, jumpPower);
        }
    }
    private void ProcessMovement()
    {
        xAxis = Inputs.Move.ReadValue<float>();
        if (IsBraking())
        {
            currentVelMultiplier = Mathf.Clamp(currentVelMultiplier - deccelerationSpeed * Time.deltaTime, 0, 1);

            rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, currentVelMultiplier) * dir, rb.velocity.y);
        }
        else
        {
            dir = xAxis > 0 ? 1 : -1;
            currentVelMultiplier = Mathf.Clamp(currentVelMultiplier + accelerationSpeed * Time.deltaTime, 0, 1);

            rb.velocity = new Vector2(Mathf.Lerp(minSpeed, maxSpeed, currentVelMultiplier) * dir, rb.velocity.y);
            transform.localScale = new Vector2(defaultXScale * dir, transform.localScale.y);
        }

    }
    #endregion
    #region Delegated Methods
    public void PerformDeath()
    {
    }
    #endregion
    #region Bool Methods
    private bool CanJump()
    {
        return IsOnGround(foot1) || IsOnGround(foot2);
    }

    private bool IsOnGround(Transform foot)
    {
        return Physics2D.Raycast(foot.position, Vector2.down, distanceToGround, groundMask).collider != null;
    }
    private bool IsBraking()
    {
        return Mathf.Approximately(xAxis, 0) || xAxis > 0 && rb.velocity.x < 0 || xAxis < 0 && rb.velocity.x > 0;
    }
    #endregion
}
