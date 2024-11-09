using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    const float Duration = 0.065f;
    const float RayDistance = 0.1f;

    public static bool isMove = false;
    public static bool isAttack = false;

    public delegate void AttackEventHandler(Vector3 attackDirection);
    public event AttackEventHandler OnAttack;

    LayerMask raycastLayerMask;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        raycastLayerMask = ~LayerMask.GetMask("Ignore Raycast");
    }
    public void MoveOrAttack(Vector3 direction)
    {
        if (!isMove && !isAttack)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + direction, direction, RayDistance, raycastLayerMask);

            if (hit.collider == null)
            {
                if (direction == Vector3.up)
                {
                    animator.SetTrigger("Move1");
                    StartCoroutine(Move(1f, true));     //상하좌우 방향 +-, 수직여부
                }
                if (direction == Vector3.down)
                {
                    animator.SetTrigger("Move");   
                    StartCoroutine(Move(-1f, true));
                }
                if (direction == Vector3.left)
                {
                    spriteRenderer.flipX = false;
                    animator.SetTrigger("Move");   
                    StartCoroutine(Move(-1f, false));
                }
                if (direction == Vector3.right)
                {
                    spriteRenderer.flipX = true;
                    animator.SetTrigger("Move");   
                    StartCoroutine(Move(1f, false));
                }
                // 이동 로직 추가
            }
            else if (hit.collider.CompareTag("Obstacle"))
            {
                OnAttack?.Invoke(direction);    //공격 방향 이벤트 전달
                if (direction == Vector3.up)
                {
                    animator.SetTrigger("Attack");   
                    StartCoroutine(Attack(hit.collider));
                }
                if (direction == Vector3.down)
                {
                    animator.SetTrigger("Attack");   
                    StartCoroutine(Attack(hit.collider));
                }
                if (direction == Vector3.left)
                {
                    spriteRenderer.flipX = false;
                    animator.SetTrigger("Attack");   
                    StartCoroutine(Attack(hit.collider));
                }
                if (direction == Vector3.right)
                {
                    spriteRenderer.flipX = true;
                    animator.SetTrigger("Attack");   
                    StartCoroutine(Attack(hit.collider));
                }
                // 공격 및 VFX 로직 추가
            }
        }
    }
    IEnumerator Move(float dir, bool isVert)
    {
        isMove = true;
        Vector2 parentPos = transform.position;
        Vector2 targetPos = transform.position;

        float elapsedTime = 0f;

        if (!isVert)
        {
            targetPos.x += dir;

            animator.SetTrigger("Move");
        }
        else
        {
            targetPos.y += dir;

            if(dir == 1f)
            {
                animator.SetTrigger("Move1");
            }
            else
            {
                animator.SetTrigger("Move");
            }
        }
        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / Duration);
            transform.position = Vector2.Lerp(parentPos, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
        isMove = false;
    }
    IEnumerator Attack(Collider2D col)
    {
        BreakableObject obstacle = col.GetComponent<BreakableObject>();
        obstacle.TakeDamage();
        isAttack = true;
        animator.SetTrigger("Attack");

        yield return null;
    }
    public void OffAttack()
    {
        isAttack = false;
    }
}