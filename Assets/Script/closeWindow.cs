using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideObject();
        }
    }
    
    void HideObject()
    {
        // 使用SetActive方法來顯示或隱藏物件
        gameObject.SetActive(false);
    }
}
