using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;  //兩發子彈之間的時間
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(speed * Time.deltaTime,0,0);

        Destroy(gameObject,1);
    }
}
