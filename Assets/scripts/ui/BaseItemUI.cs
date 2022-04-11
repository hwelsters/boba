using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemUI : MonoBehaviour
{
    protected readonly Vector2 INITIAL_UI_POSITION = new Vector2(-150, 150);
    protected const float UI_WIDTH = 150f;
    protected const float UI_HEIGHT = 150f;
    
    // REDO
    [SerializeField] protected GameObject itemUIObject;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E)) DeactivateSelf();
    }

    private void DeactivateSelf()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
