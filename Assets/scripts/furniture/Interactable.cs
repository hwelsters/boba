using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    protected bool mouseIsOver = false;

    protected void OnMouseOver()
    {
        CursorManager.SetToHand();
        mouseIsOver = true;
    }

    protected void OnMouseExit()
    {
        CursorManager.SetToPointer();
        mouseIsOver = false;
    }

    protected bool WasRightClicked()
    {
        return mouseIsOver && Input.GetMouseButtonDown(1);
    }

    protected bool WasLeftClicked()
    {
        return mouseIsOver && Input.GetMouseButtonDown(0);
    }
}
