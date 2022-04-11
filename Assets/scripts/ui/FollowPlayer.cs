using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //VERY VERY SUS CODE
    //PLEASE CORRECT THIS WHEN YOU'RE SMARTER NOEL
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(player.transform.position);
        rectTransform.position = playerScreenPosition + offset;
    }
}
