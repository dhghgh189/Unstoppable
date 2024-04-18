using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private CapsuleCollider2D standingCollider;
    [SerializeField] private BoxCollider2D slidingCollider;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float jumpPower;

    private bool isGround;
    private bool isSlide;
    public bool IsGround {  get { return isGround; } }
    public bool IsSlide 
    {
        get {  return isSlide; } 
        private set 
        {
            isSlide = value;
            ChangeCollider(); 
        } 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        theApp.Input.OnJumpInput -= OnJumpEvent;
        theApp.Input.OnJumpInput += OnJumpEvent;

        theApp.Input.OnSlideInput -= OnSlideEvent;
        theApp.Input.OnSlideInput += OnSlideEvent;
    }

    void Update()
    {
        UpdateAnimation();
        CheckStatus();
    }

    void UpdateAnimation()
    {
        anim.SetBool("isGround", isGround);
        anim.SetBool("isSlide", isSlide);
        anim.SetFloat("ySpeed", rb.velocity.y);
    }

    void CheckStatus()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        
        isGround = hit.collider != null;

        SeeRays();
    }

    void SeeRays()
    {
        Color color = isGround ? Color.green : Color.red;

        Debug.DrawRay(groundCheck.transform.position, Vector2.down * groundCheckDistance, color);
    }

    void OnJumpEvent(Define.EInputEventType eventType)
    {
        if (isSlide == true)
            return;

        switch (eventType)
        {
            case Define.EInputEventType.JumpDown:
                if (isGround == true)
                {
                    rb.velocity = Vector2.zero;
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                break;
            case Define.EInputEventType.JumpUp:
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
                break;
        }
    }

    void OnSlideEvent(Define.EInputEventType eventType)
    {
        if (isGround == false)
            return;

        switch (eventType)
        {
            case Define.EInputEventType.SlideDown:
                IsSlide = true;
                break;
            case Define.EInputEventType.SlideUp:
                IsSlide = false;
                break;
        }
    }

    void ChangeCollider()
    {
        standingCollider.gameObject.SetActive(!isSlide);
        slidingCollider.gameObject.SetActive(isSlide);
    }
}
