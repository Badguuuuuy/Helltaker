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
    public event AttackEventHandler OnPush;

    LayerMask raycastLayerMask;
    SpriteRenderer spriteRenderer;
    Animator animator;

    int cnt = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        raycastLayerMask = ~LayerMask.GetMask("Ignore Raycast");
    }
    private void Update()
    {
        Debug.Log("isMove: " + isMove);
        Debug.Log("isAttack: " + isAttack);
    }
    public void MoveOrAttack(Vector3 direction)
    {
        if (!isAttack)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + direction, direction, RayDistance, raycastLayerMask);

            if (hit.collider == null)
            {
                if (direction == Vector3.up)
                {
                    Debug.Log("콜라이더 널");
                    animator.SetTrigger("Move1");
                    StartCoroutine(Move(1f, true));     //상하좌우 방향 +-, 수직여부
                }
                else if (direction == Vector3.down)
                {
                    animator.SetTrigger("Move");
                    StartCoroutine(Move(-1f, true));
                }
                else if (direction == Vector3.left)
                {
                    spriteRenderer.flipX = false;
                    animator.SetTrigger("Move");
                    StartCoroutine(Move(-1f, false));
                }
                else if (direction == Vector3.right)
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
                    StartCoroutine(Attack(hit.collider));
                }
                else if (direction == Vector3.down)
                {
                    StartCoroutine(Attack(hit.collider));
                }
                else if (direction == Vector3.left)
                {
                    spriteRenderer.flipX = false;
                    StartCoroutine(Attack(hit.collider));
                }
                else if (direction == Vector3.right)
                {
                    spriteRenderer.flipX = true;
                    StartCoroutine(Attack(hit.collider));
                }
                // 공격 및 VFX 로직 추가
            }
            else if (hit.collider.CompareTag("Pushable"))
            {
                OnPush?.Invoke(direction);      //푸쉬 방향 이벤트 전달
                if(direction == Vector3.up)
                {
                    //animator
                    StartCoroutine(Push(hit.collider, direction));
                }
                else if (direction == Vector3.down)
                {
                    StartCoroutine(Push(hit.collider, direction));
                }
                else if (direction == Vector3.left)
                {
                    spriteRenderer.flipX = false;
                    StartCoroutine(Push(hit.collider, direction));
                }
                else if (direction == Vector3.right)
                {
                    spriteRenderer.flipX = true;
                    StartCoroutine(Push(hit.collider, direction));
                }
            }
        }
    }
    IEnumerator Move(float dir, bool isVert)
    {
        cnt++;
        Debug.Log("move코루틴호출" + cnt);
        isAttack = true;
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
        //isAttack = false;
        Debug.Log("저 거짓임" + cnt);
    }
    IEnumerator Attack(Collider2D col)
    {
        col.GetComponent<BreakableObject>().TakeDamage();
        isAttack = true;
        animator.SetTrigger("Attack");

        yield return null;
    }
    IEnumerator Push(Collider2D col, Vector3 dir)
    {
        col.GetComponent<PushableObject>().Pushed(dir);
        isAttack = true;
        animator.SetTrigger("Kick");
        yield return null;
    }
    public void OffAttack()
    {
        Debug.Log("앙기모띠");
        isAttack = false;
    }
}