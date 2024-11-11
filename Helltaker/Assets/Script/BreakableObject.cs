using System.Collections;
using UnityEngine;

namespace BreakableStates
{
    public class AliveState : IEnemyState<BreakableObject>
    {
        public void EnterState(BreakableObject obstacle) { }
        public void UpdateState(BreakableObject obstacle) { }
    }
    public class DeadState : IEnemyState<BreakableObject>
    {
        public void EnterState(BreakableObject obstacle)
        {
            obstacle.animator.SetTrigger("Hit"); // ���� �ִϸ��̼� Ʈ����
            //enemy.enabled = false; // �߰����� ó���� �ʿ��� ���
            obstacle.boxCollider.enabled = false;
            obstacle.StartFadeOut();
        }
        public void UpdateState(BreakableObject obstacle) { }
    }
}

public class BreakableObject : MonoBehaviour
{
    public static readonly IEnemyState<BreakableObject> aliveState = new BreakableStates.AliveState();
    public static readonly IEnemyState<BreakableObject> deadState = new BreakableStates.DeadState();

    public IEnemyState<BreakableObject> currentState;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        currentState = aliveState;
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void TakeDamage()
    {
        TransitionToState(deadState); //�ǰݽ� Dead ���·� ��ȯ
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        yield return new WaitForSeconds(0.5f);
        for (float i = 1.0f; i >= 0f; i -= 0.05f)
        {
            color.a = i;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        color.a = 0f;
        yield return spriteRenderer.color = color;
    }
    public void TransitionToState(IEnemyState<BreakableObject> newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        currentState.EnterState(this);
    }
}