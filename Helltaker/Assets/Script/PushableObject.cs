using System.Collections;
using UnityEngine;

namespace PushableStates
{
    public class AliveState : IEnemyState<PushableObject>
    {
        public void EnterState(PushableObject obstacle) { }
        public void UpdateState(PushableObject obstacle) { }
    }
    public class DeadState : IEnemyState<PushableObject>
    {
        public void EnterState(PushableObject obstacle)
        {
            //obstacle.animator.SetTrigger("Hit"); // 죽음 애니메이션 트리거
            //enemy.enabled = false; // 추가적인 처리가 필요할 경우
            //obstacle.boxCollider.enabled = false;
            //obstacle.StartFadeOut();
        }
        public void UpdateState(PushableObject obstacle) { }
    }
}

public class PushableObject : MonoBehaviour
{
    public static readonly IEnemyState<PushableObject> aliveState = new PushableStates.AliveState();
    public static readonly IEnemyState<PushableObject> deadState = new PushableStates.DeadState();

    bool isPushed = false;

    const float Duration = 0.065f;
    const float RayDistance = 0.1f;

    public IEnemyState<PushableObject> currentState;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;

    LayerMask raycastLayerMask;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = aliveState;
        raycastLayerMask = ~LayerMask.GetMask("Ignore Raycast");
    }
    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void Pushed(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dir, dir, RayDistance, raycastLayerMask);
        if (hit.collider != null && hit.collider.tag == "Boundary")
        {
            return;
        }
        else 
        {
            StartCoroutine(MovedByPush(dir));
        }
        
    }
    private IEnumerator MovedByPush(Vector3 dir)
    {
        float elapsedTime = 0f;

        Vector3 parentPos = transform.position;
        Vector3 targetPos = dir + parentPos;

        if (!isPushed)
        {
            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / Duration);
                transform.position = Vector2.Lerp(parentPos, targetPos, t);
                yield return null;
            }
        }
    }
    public void TransitionToState(IEnemyState<PushableObject> newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        currentState.EnterState(this);
    }
}
