using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int additionalJump;

    private List<Item> itemSlot = new List<Item>();

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

    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
    }

    public float PosX { get { return transform.position.x; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        theApp.Input.OnJumpInput -= OnJumpEvent;
        theApp.Input.OnJumpInput += OnJumpEvent;

        theApp.Input.OnSlideInput -= OnSlideEvent;
        theApp.Input.OnSlideInput += OnSlideEvent;

        theApp.Input.OnUseItemInput -= OnUseItemEvent;
        theApp.Input.OnUseItemInput += OnUseItemEvent;

        additionalJump = 0;

        isDead = false;
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

#if UNITY_EDITOR
        SeeRays();
#endif
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
                    Jump();
                }
                else if (additionalJump > 0)
                {
                    additionalJump--;
                    Jump();
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

    public void OnDead()
    {
        // TODO : 플레이어 사망 처리

        if (isDead)
            return;

        // 임시 
        isDead = true;
        anim.enabled = false;
        standingCollider.enabled = false;
        slidingCollider.enabled = false;

        theApp.Game.GameOver();
    }

    public void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        theApp.Sound.PlaySound(Define.ESoundType.Sfx, "Audio/Sfx/Jump");
    }

    public void AddJump(int amount)
    {
        additionalJump += amount;
    }

    public bool PushItem(Item item)
    {
        if (itemSlot.Count >= Define.ITEM_SLOT_MAX)
            return false;

        itemSlot.Add(item);
        theApp.Game.OnBroadCastFunc(Define.EBroadCastType.AddItem, itemSlot);
        return true;
    } 

    public void OnUseItemEvent(Define.EInputEventType eventType)
    {
        if (itemSlot.Count <= 0)
            return;

        int index = itemSlot.Count - 1;

        Item activeItem = itemSlot[index];
        activeItem.Use();

        itemSlot.RemoveAt(index);
        theApp.Game.OnBroadCastFunc(Define.EBroadCastType.UseItem, itemSlot);
    }
}
