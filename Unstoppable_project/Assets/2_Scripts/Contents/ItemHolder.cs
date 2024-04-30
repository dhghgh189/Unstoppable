using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    ItemData itemData;
    PlayerController owner;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.Find("ItemSprite").GetComponent<SpriteRenderer>();
    }

    public void SetInfo(int itemID, float speed)
    {
        moveSpeed = speed;

        if (theApp.Data.itemData.TryGetValue(itemID, out itemData) == false)
        {
            Debug.Log("Invalid Item Data!");
            return;
        }

        sr.gameObject.name = $"{itemData.ItemID}_{itemData.ItemName}";
        sr.sprite = itemData.ItemSprite;
    }

    void Update()
    {
        if (theApp.Game.isGameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        if (transform.position.x <= theApp.Data.spawnData.destroyOffsetX)
            theApp.Res.Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (theApp.Game.isGameOver)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController owner = other.gameObject.GetComponentInParent<PlayerController>();
            if (owner == null)
            {
                Debug.Log("Invalid owner!");
                return;
            }

            Item item = Item.MakeItem(itemData, owner);
            switch (item.ItemType)
            {
                // 아이템 type이 Passive인 경우
                case Define.EItemType.Passive:
                    item.Use(); // 바로 사용
                    // holder 파괴
                    theApp.Res.Destroy(gameObject);
                    break;
                // 아이템 type이 Active인 경우
                case Define.EItemType.Active:
                    bool isAdded = owner.PushItem(item); // Slot에 아이템 추가
                    // item이 slot에 추가됬으면 holder 파괴
                    if (isAdded)
                        theApp.Res.Destroy(gameObject);
                    break;
            }
        }
    }
}
