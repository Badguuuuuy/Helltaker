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
        Debug.Log("");  //어차피 다른 오브젝트가 호출하려면 public이어야 함.
    }
}
