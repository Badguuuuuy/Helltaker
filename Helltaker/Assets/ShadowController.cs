using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public PlayerMovementController boolEventTrigger;

    SpriteRenderer spriteRenderer;

    Vector3 pos0 = new Vector3(0.05f, -0.2f, 0f);
    Vector3 pos1 = new Vector3(-0.05f, -0.2f, 0f);

    bool flip = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (boolEventTrigger != null)
        {
            boolEventTrigger.OnValueChanged += HandleValueChanged;
        }
    }

    private void HandleValueChanged()
    {
        if (flip)
        {
            transform.localPosition = pos0;
            flip = !flip;
        }
        else
        {
            transform.localPosition = pos1;
            flip = !flip;
        }

        Debug.Log("aaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (boolEventTrigger != null)
        {
            boolEventTrigger.OnValueChanged -= HandleValueChanged;
        }
    }
}
