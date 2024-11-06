using UnityEngine;

public class AttackVFXController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimatorAttack()
    {
        animator.SetTrigger("Attack");
    }
    public void FlipX(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
    public void ChangeLocalPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }
    public void ChangeRotation(float angle)
    {
        if (spriteRenderer.flipX)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}