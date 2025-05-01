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
        if (IsGrounded()) // Move only when on ground
        {
            horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), Time.deltaTime * inputLerpTime);
        }
        else
        {
            horizontal = Mathf.Lerp(horizontal, Input.GetAxisRaw("Horizontal"), Time.deltaTime * simpleInertia);
        }


        if (Input.GetButtonDown("Jump") && IsGrounded()) // Jump when on ground
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Continuously move
    }

    private bool IsGrounded()
    {
        // Check if grounded by seeing if area around feet is overlapping ground
        bool grounded = false;
        Collider[] cols = Physics.OverlapSphere(groundCheck.position, 0.5f, groundLayer);
        if (cols.Length > 0 ) { grounded = true; }
        return grounded;
    }
    public void Kill()
    {
        // Move tami to spawn, set vel to 0 so she doesnt fly through objs
        transform.position = respawnPoint.position;
        rb.velocity = Vector3.zero;
        horizontal = 0;
    }
}
