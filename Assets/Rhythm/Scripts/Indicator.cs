using UnityEngine;

public class Indicator : MonoBehaviour
{

    [SerializeField] float indication_time;    
    float timer;
    SpriteRenderer spriteRenderer;

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
        spriteRenderer.color = Color.red; //color can later be changed by a serializedfield component for each

    }

    public void DeIndicate()
    {
        spriteRenderer.color = Color.white;
    }
}
