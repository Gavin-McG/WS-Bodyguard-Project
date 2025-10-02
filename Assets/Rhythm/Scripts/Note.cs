using System;
using UnityEngine;

public class Note : MonoBehaviour
{
    public
    float TimeToHit;
    public
    GameManager.Direction direction;

    public bool hit = false;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color((TimeToHit - GameManager.instance.Game_Time) / TimeToHit, (TimeToHit - GameManager.instance.Game_Time) / TimeToHit, (TimeToHit - GameManager.instance.Game_Time) / TimeToHit);

    }

    public void Hit()
    {
        Debug.Log("whoopee");
        hit = true;
        Destroy(gameObject);
    }
}
