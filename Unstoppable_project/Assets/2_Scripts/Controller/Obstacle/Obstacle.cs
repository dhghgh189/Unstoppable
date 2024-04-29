using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InitBase
{
    protected Rigidbody2D rb;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float score;

    LayerMask playerLayer;
    protected bool isPassed;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        rb = GetComponent<Rigidbody2D>();

        // player layer
        playerLayer = LayerMask.NameToLayer("Player");

        isPassed = false;

        return true;
    }

    public virtual void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        if (theApp.Game.isGameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        UpdateMove();

        if (isPassed == false)
        {
            CheckCondition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
                OnTrigger(player);
        }
    }

    public virtual void UpdateMove()
    {
        // TODO : 장애물이 움직일 때의 동작 구현
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        if (transform.position.x <= theApp.Data.spawnData.destroyOffsetX)
            theApp.Res.Destroy(gameObject);
    }

    public virtual void CheckCondition()
    {
        if (theApp.Game.Player.PosX > transform.position.x)
        {
            OnPassed();
        }
    }

    protected void OnPassed()
    {
        theApp.Game.Score += score;
        Vector3 pos = theApp.Game.Player.transform.position;
        theApp.Game.GenerateScoreEffect(pos, score);
        isPassed = true;
    }

    public virtual void OnTrigger(PlayerController player)
    {
        // TODO : 플레이어 충돌시 동작 구현
        player.OnDead();
    }
}
