using UnityEngine;
public interface IEnemyState<T>
{
    void EnterState(T controller);
    void UpdateState(T controller);
}