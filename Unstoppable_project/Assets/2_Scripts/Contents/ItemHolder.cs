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

        gameObject.name = $"{itemData.ItemID}_{itemData.ItemName}";
        sr.sprite = itemData.ItemSprite;
    }

    void Update()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        if (transform.position.x <= theApp.Data.spawnData.destroyOffsetX)
            theApp.Res.Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
                // ������ type�� Passive�� ���
                case Define.ItemType.Passive:
                    item.Use(); // �ٷ� ���
                    // holder �ı�
                    theApp.Res.Destroy(gameObject);
                    break;
                // ������ type�� Active�� ���
                case Define.ItemType.Active:
                    bool isAdded = owner.PushItem(item); // Slot�� ������ �߰�
                    // item�� slot�� �߰������� holder �ı�
                    if (isAdded)
                        theApp.Res.Destroy(gameObject);
                    break;
            }
        }
    }
}