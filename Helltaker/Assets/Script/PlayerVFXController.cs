using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public AttackVFXController VFXAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttackVFX()
    {
        VFXAttack.SetAnimatorAttack();
        //�ٸ� vfx ���� ����
    }
    public void ChangeVFXDir(Vector3 dir)
    {
        VFXAttack.ChangeDirection(dir);
        //�ٸ� vfx ��ġ���� ����
    }
}
