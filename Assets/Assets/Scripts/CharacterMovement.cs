using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 5f;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private bool isCooldown = false;
    private float cooldownTimer = 0f;
    private Inventory inventory;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= dashCooldown)
            {
                isCooldown = false;
                cooldownTimer = 0f;
            }
        }
        else if (isDashing)
        {
            dashTimer += Time.deltaTime;
            rb.velocity = movement.normalized * dashSpeed;

            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0f;
                StartCooldown();
            }
        }
        else
        {
            rb.velocity = movement * movementSpeed;
        }

        UpdateAnimator(moveHorizontal, moveVertical);

        if (inventory.HasItem("starDash") && !isCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    private void UpdateAnimator(float moveHorizontal, float moveVertical)
    {
        bool isMoving = moveHorizontal != 0 || moveVertical != 0;

        animator.SetBool("isMoving", isMoving);
        animator.SetFloat("moveX", moveHorizontal);
        animator.SetFloat("moveY", moveVertical);

        if (moveHorizontal > 0)
            spriteRenderer.sprite = rightSprite;
        else if (moveHorizontal < 0)
            spriteRenderer.sprite = leftSprite;
        else if (moveVertical > 0)
            spriteRenderer.sprite = upSprite;
        else if (moveVertical < 0)
            spriteRenderer.sprite = downSprite;
    }

    private void Dash()
    {
        isDashing = true;
        Invoke("StopDash", dashDuration);
        Invoke("StartCooldown", dashCooldown);
    }

    private void StopDash()
    {
        isDashing = false;
    }

    private void StartCooldown()
    {
        isCooldown = true;
        Invoke("ResetCooldown", dashCooldown);
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }
}
