using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSpriteRenderer : MonoBehaviour
{
    private void Start()
    {
        // gameObject.SetActive(false);
    }

    private void DisableSpriteVisibility()
    {
    }

    private void EnableSpriteVisibility()
    {
    }

    public void ReceiveBroadcast()
    {
        // 在接收物体中执行某些操作
        Debug.Log("Received the broadcast, show window.");
        EnableSpriteVisibility();
    }
}
