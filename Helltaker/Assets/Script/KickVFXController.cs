using UnityEngine;

public class KickVFXController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 up = new Vector3(0f, 1f, -1f);
    Vector3 down = new Vector3(0f, -1f, -1f);
    Vector3 left = new Vector3(-1f, 0f, -1f);
    Vector3 right = new Vector3(1f, 0f, -1f);

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
    public void SetAnimatorKick()
    {
        animator.SetTrigger("Kick");
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
    public void ChangeDirection(Vector3 dir)
    {
        if (dir == Vector3.up)
        {
            FlipX(false);
            ChangeLocalPos(up);
            ChangeRotation(-90f);
        }
        else if (dir == Vector3.down)
        {
            FlipX(false);
            ChangeLocalPos(down);
            ChangeRotation(90f);
        }
        else if (dir == Vector3.left)
        {
            FlipX(false);
            ChangeLocalPos(left);
            ChangeRotation(0f);
        }
        else if (dir == Vector3.right)
        {
            FlipX(true);
            ChangeLocalPos(right);
            ChangeRotation(0f);
        }
    }
}
