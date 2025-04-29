using UnityEngine;
/*
    Script created by : Jason Lodge
    Edited by         : Jason Lodge
    Note              : This is actually a modified version of the 
                        player movement script Arthur used in his
                        puzzle scene.
*/
public class TamiMovement : MonoBehaviour
{
    public float horizontal;
    public float speed = 8f;
    public float inputLerpTime = 3f;
    public float simpleInertia = 5f;
    public float jumpingPower = 16f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform respawnPoint;

    void Update()
    {
        if (IsGrounded())
        {
            horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), Time.deltaTime * inputLerpTime);
        }
        else
        {
            horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), Time.deltaTime * simpleInertia);
        }


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    public void Kill()
    {
        transform.position = respawnPoint.position;
    }
}
