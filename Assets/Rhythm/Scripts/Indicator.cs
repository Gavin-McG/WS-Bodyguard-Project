using UnityEngine;

public class Indicator : MonoBehaviour
{

    [SerializeField] float indication_time;    
    float timer;
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite hit_sprite;
    [SerializeField] Sprite base_sprite;

    private void Start()
    {
        
        timer = indication_time;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            DeIndicate();
        }
    }

    public void Indicate()
    {
        timer = indication_time;
        spriteRenderer.sprite = hit_sprite; //color can later be changed by a serializedfield component for each

    }

    public void DeIndicate()
    {
        spriteRenderer.sprite = base_sprite;
    }
}
