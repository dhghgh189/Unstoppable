using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        UpdateMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
                OnTrigger(player);
        }
    }

    public virtual void UpdateMove()
    {
        // TODO : ��ֹ��� ������ ���� ���� ����
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    public virtual void OnTrigger(PlayerController player)
    {
        // TODO : �÷��̾� �浹�� ���� ����
        player.OnDead();
    }
}
