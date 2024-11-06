using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Transform playerTransform;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Animator VFXanimator;

    public AttackVFXController VFXAttack;

    float duration = 0.065f;

    bool isMove = false;
    bool isAttack = false;

    private void Awake()
    {
        playerTransform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spriteRenderer.bounds.size.x);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove && !isAttack)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1f, 0f, 0f), transform.right, 0.1f);
                var interactable = hit.collider.GetComponent<InteractableObject>();
                if (hit.collider == null)
                {
                    spriteRenderer.flipX = true;
                    animator.SetTrigger("Move");
                    StartCoroutine(Move(1f, false));
                }
                else if (hit.collider.tag == "Obstacle")
                {
                    spriteRenderer.flipX = true;
                    StartCoroutine(Attack(hit.collider));
                    VFXAttack.FlipX(true);
                    VFXAttack.ChangeLocalPos(new Vector3(1f, 0f, -1f));
                    VFXAttack.ChangeRotation(0f);
                }
                //else if (hit.collider == PushableController) 
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-1f, 0f, 0f), transform.right, 0.1f);
                if (hit.collider == null)
                {
                    spriteRenderer.flipX = false;
                    animator.SetTrigger("Move");
                    StartCoroutine(Move(-1f, false));
                }
                else if (hit.collider.tag == "Obstacle")
                {
                    spriteRenderer.flipX = false;
                    StartCoroutine(Attack(hit.collider));
                    VFXAttack.FlipX(false);
                    VFXAttack.ChangeLocalPos(new Vector3(-1f, 0f, -1f));
                    VFXAttack.ChangeRotation(0f);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, 1f, 0f), transform.up, 0.1f);
                if (hit.collider == null)
                {
                    animator.SetTrigger("Move1");   //위로 이동 모션 다름
                    StartCoroutine(Move(1f, true));
                }
                else if (hit.collider.tag == "Obstacle")
                {
                    StartCoroutine(Attack(hit.collider));
                    VFXAttack.FlipX(false);
                    VFXAttack.ChangeLocalPos(new Vector3(0f, 1f, -1f));
                    VFXAttack.ChangeRotation(-90f);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, -1f, 0f), transform.up, 0.1f);
                if (hit.collider == null)
                {
                    animator.SetTrigger("Move");
                    StartCoroutine(Move(-1f, true));
                }
                else if (hit.collider.tag == "Obstacle")
                {
                    StartCoroutine(Attack(hit.collider));
                    VFXAttack.FlipX(false);
                    VFXAttack.ChangeLocalPos(new Vector3(0f, -1f, -1f));
                    VFXAttack.ChangeRotation(90f);
                }
            }
        }
    }
    IEnumerator Move(float dir, bool isVert)
    {
        isMove = true;
        Vector2 parentPos = new Vector2();
        parentPos = new Vector2(playerTransform.position.x, playerTransform.position.y);

        float elapsedTime = 0f;

        if(!isVert)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                playerTransform.position = Vector2.Lerp(parentPos, new Vector2(parentPos.x + 1f * dir, parentPos.y), t);
                yield return null;
            }
            transform.position = new Vector2(parentPos.x + 1f * dir, parentPos.y);
        }
        else
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                playerTransform.position = Vector2.Lerp(parentPos, new Vector2(parentPos.x, parentPos.y + 1f * dir), t);
                yield return null;
            }
            transform.position = new Vector2(parentPos.x, parentPos.y + 1f * dir);
        }
        
        yield return isMove = false;
    }



    IEnumerator Attack(Collider2D col)
    {
        ObstacleController obstacle = col.GetComponent<ObstacleController>();
        obstacle.TakeDamage();
        isAttack = true;
        animator.SetTrigger("Attack");

        yield return null;
    }
    public void OffAttack()
    {
        isAttack = false;
    }
    public void AttackVFX()
    {
        VFXAttack.SetAnimatorAttack();
    }
    /*IEnumerator Push()
    {

    }*/
}
