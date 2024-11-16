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
        //다른 vfx 실행 동작
    }
    public void KickVFX()
    {
        VFXKick.SetAnimatorKick();
    }
    public void ChangeAttackVFXDir(Vector3 dir)
    {
        VFXAttack.ChangeDirection(dir);
        //다른 vfx 위치변경 동작
    }
    public void ChangeKickVFXDir(Vector3 dir)
    {
        VFXKick.ChangeDirection(dir);
        //다른 vfx 위치변경 동작
    }
}
