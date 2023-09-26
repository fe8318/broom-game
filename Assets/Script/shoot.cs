using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet_R;  //子彈
    public GameObject bullet_L;  //子彈
    public Transform muzzlePoint;
    public GameObject player;
    public float cooldownTime = 0.2f;  // 射擊冷卻時間（秒）
    private float cooldownTimer = 0f;  // 冷卻計時器
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        // 更新冷卻計時器
        cooldownTimer -= Time.deltaTime;

        // 檢查是否可以射擊，並且按下了射擊按鍵（例如 "j" 鍵）
        if (cooldownTimer <= 0f && Input.GetKey("j"))
        {
        
            if(player.GetComponent<move>().facingRight)
            {
                // 創建子彈
                Instantiate(bullet_R, muzzlePoint.position, muzzlePoint.rotation);

                // 重置冷卻計時器
                cooldownTimer = cooldownTime;
            }
            else
            {
                // 創建子彈
                Instantiate(bullet_L, muzzlePoint.position, muzzlePoint.rotation);

                // 重置冷卻計時器
                cooldownTimer = cooldownTime;
            }
            
        }
    }

}
