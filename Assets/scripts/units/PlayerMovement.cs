using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Storage
{
    //DEFINITELY SHOULD CLEAN UP THIS CODE SOMEDAY
    private const float WALK_SPEED = 20f;
    private const float POINTER_DISTANCE = 5f;
    private const float OBJ_INTERACTABLE_SQR_DISTANCE = 0.75f;

    private Animator animator;
    private Rigidbody2D rb2D;

    private Vector3 pointerPosition = new Vector3(0,0,0);

    private int moveX = 0;
    private int moveY = 0;

    private bool pressedZ = false;
    private bool pressedX = false;

    protected override void Start()
    {
        //PLAYER COMPONENTS
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    protected override void FixedUpdate()
    {   
        base.FixedUpdate();

        Vector2 moveDir = new Vector2(moveX, moveY);
        moveDir = Vector2.ClampMagnitude(moveDir, 1);

        Vector2 displacement = moveDir * WALK_SPEED * Time.deltaTime;
        Vector2 newPosition = (Vector2) transform.position + displacement;

        rb2D.MovePosition(newPosition);
    }

    private void HandleInput()
    {
        moveX = (int) Input.GetAxisRaw("Horizontal");
        moveY = (int) Input.GetAxisRaw("Vertical");

        pressedZ = Input.GetKeyDown(KeyCode.Z);
        pressedX = Input.GetKeyDown(KeyCode.X);

        //DELETE
        if (pressedZ)
            WithdrawItem(moveX, moveY);
        //DELETE
    }

    private void HandleAnimation()
    {

        bool isMoving = moveX != 0 || moveY != 0;
        bool isCarrying = storedItem.itemType != ItemType.NONE;

        if (isMoving)
        {

            //PLAYER ANIMATION
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);

            //DELETE
            pointerPosition = transform.position + new Vector3(moveX, moveY, 0) * POINTER_DISTANCE;
        }
        
        //PLAYER ANIMATION
        animator.SetBool("Carrying", isCarrying);
        animator.SetBool("Moving", isMoving);
    }

    //DELETE
    private void WithdrawItem(int moveX, int moveY)
    {
        Storage storage = Globals.CheckCollision<Storage>(pointerPosition);
        if (storage != null)
        {
            if (storedItem.itemType == ItemType.NONE) storage.WithdrawItem(storedItem);
            else storage.StoreItem(storedItem);
        }
    }
    
    public bool ObjIsCloseEnough(GameObject obj)
    {
        return Globals.SqrDistance(obj, gameObject) <= OBJ_INTERACTABLE_SQR_DISTANCE;
    }

}
