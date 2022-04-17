using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private const float INTERACTABLE_DISTANCE = 15f;
    protected void OnMouseOver()
    {
        CursorManager.SetToHand();
    }

    protected void OnMouseExit()
    {
        CursorManager.SetToPointer();
    }

    public bool PlayerIsCloseEnough() 
    {
        return Globals.CheckCollision<PlayerMovement>(transform.position, INTERACTABLE_DISTANCE) != null;
    }
}
