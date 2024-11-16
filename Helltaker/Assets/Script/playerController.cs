using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovementController playerMovementController;
    PlayerVFXController playerVFXController;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Animator VFXanimator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerMovementController = GetComponent<PlayerMovementController>();
        playerVFXController = GetComponent<PlayerVFXController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spriteRenderer.bounds.size.x);
    }
    private void OnEnable()
    {
        playerMovementController.OnAttack += HandleAttackEvent;
        playerMovementController.OnPush += HandleKickEvent;
    }
    private void OnDisable()
    {
        playerMovementController.OnAttack -= HandleAttackEvent;
        playerMovementController.OnPush -= HandleKickEvent;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.W))
            {
            Debug.Log("플레이어컨트롤러");
                playerMovementController.MoveOrAttack(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                playerMovementController.MoveOrAttack(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                playerMovementController.MoveOrAttack(Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                playerMovementController.MoveOrAttack(Vector3.right);
            }
    }
    private void HandleAttackEvent(Vector3 dir)
    {
        playerVFXController.ChangeAttackVFXDir(dir);
    }
    private void HandleKickEvent(Vector3 dir)
    {
        playerVFXController.ChangeKickVFXDir(dir);
    }
}
