using UnityEngine;

public abstract class PushableObject : MonoBehaviour, InteractableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void OnHit()
    {
        Debug.Log("");  //������ �ٸ� ������Ʈ�� ȣ���Ϸ��� public�̾�� ��.
    }
}
