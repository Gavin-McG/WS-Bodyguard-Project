using System;
using UnityEngine;
using TMPro;

public class Note : MonoBehaviour
{
    public
    float TimeToHit;
    public
    RhythmGameManager.Direction direction;

    public bool hit = false;
    SpriteRenderer spriteRenderer;
    [SerializeField] TMP_Text TESTtime_to_hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        TESTtime_to_hit.text = (TimeToHit - RhythmGameManager.instance.Game_Time + "").Substring(0, Math.Min(3, (TimeToHit - RhythmGameManager.instance.Game_Time + "").Length));

    }
    public void Hit()
    {
        Debug.Log("whoopee");
        hit = true;
        spriteRenderer.enabled = false;
    }
}
