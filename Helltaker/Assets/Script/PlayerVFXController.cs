using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerVFXController : MonoBehaviour
{
    public AttackVFXController VFXAttack;
    public KickVFXController VFXKick;
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
    public void KickVFX()
    {
        VFXKick.SetAnimatorKick();
    }
    public void ChangeAttackVFXDir(Vector3 dir)
    {
        VFXAttack.ChangeDirection(dir);
        //�ٸ� vfx ��ġ���� ����
    }
    public void ChangeKickVFXDir(Vector3 dir)
    {
        VFXKick.ChangeDirection(dir);
        //�ٸ� vfx ��ġ���� ����
    }
}
