using System.Collections;
using UnityEngine;
public interface IEnemyState
{
    void EnterState(ObstacleController obstacle);
    void UpdateState(ObstacleController obstacle);
}

public class AliveState : IEnemyState
{
    public void EnterState(ObstacleController obstacle) { }
    public void UpdateState(ObstacleController obstacle) { }
}

public class DeadState : IEnemyState
{
    public void EnterState(ObstacleController obstacle)
    {
        obstacle.animator.SetTrigger("Hit"); // 죽음 애니메이션 트리거
        //enemy.enabled = false; // 추가적인 처리가 필요할 경우
        obstacle.boxCollider.enabled = false;
        obstacle.StartFadeOut();
        
    }
    public void UpdateState(ObstacleController obstacle) { }
}

public class ObstacleController : MonoBehaviour
{
    public IEnemyState aliveState = new AliveState();
    public IEnemyState deadState = new DeadState();

    public IEnemyState currentState;
    public Animator animator;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;

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
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void TakeDamage()
    {
        TransitionToState(deadState); //피격시 Dead 상태로 전환
    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        Color color = spriteRenderer.color;
        yield return new WaitForSeconds(0.5f);
        for(float i = 1.0f; i >= 0f; i -= 0.05f)
        {
            color.a = i;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        color.a = 0f;
        yield return spriteRenderer.color = color;
    }
    public void TransitionToState(IEnemyState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
