using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InitBase
{
    Rigidbody2D rb;

    [SerializeField] protected float moveSpeed;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        rb = GetComponent<Rigidbody2D>();

        return true;
    }

    public virtual void SetSpeed(float speed)
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

        if (transform.position.x <= theApp.Data.spawnData.destroyOffsetX)
            theApp.Res.Destroy(gameObject);
    }

    public virtual void OnTrigger(PlayerController player)
    {
        // TODO : �÷��̾� �浹�� ���� ����
        player.OnDead();
    }
}
