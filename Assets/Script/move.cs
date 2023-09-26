//使用到的命名空間
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//整個程式架構是一個class，我命名為role  //繼承MonoBehaviour這個類別
public class move : MonoBehaviour       
{
	//物件變數，整個class都可以使用的變數
  [Range(0,2)]static public int roleID_Now = 0;
  private Rigidbody2D rb2d;
	public float moveSpeed = 8f;
  public bool facingRight = true;

  [Header("跳躍設定")]
  public float jumpForce = 300.0f;
  public Transform footPoint;
  public Transform footPoint2;
  public bool touchGround = true;
  private bool canJump = true;
  private Coroutine cor_canJump_dead;
    public string littleRedName;
    public Animator animator;
		
    // Start is called before the first frame update 
		// Start()函數 遊戲一開始會執行一次
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Debug.Log ("hello world");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
		// Update()函數 每一幀執行一次
    // 這裡會每一幀執行一次 移動方法();
    void Update()
    {
        footPointCheck();
        //Movement();
        Jump();
    }
    void FixedUpdate()
    {
        Movement();
    }
    // 自訂義方法，所有程式步驟都會包含在方法內，要使用時就呼叫。
	private void Movement()//方法
	  {  
      //地面上正常移動
      if(touchGround)
      {
        rb2d.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb2d.velocity.y);
      }
      else
      {
          rb2d.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal") * 0.85f, rb2d.velocity.y);
      }
      if(Input.GetAxis("Horizontal") != 0f)
      {
        if((facingRight && Input.GetAxis("Horizontal") < 0f)||(!facingRight && Input.GetAxis("Horizontal") > 0f))
        {
          Flip();
        }
      }
      
       //採用直接改變物件座標的方式
				//一、向右走
      /*
     if (Input.GetKey("d"))//輸入.來自鍵盤(“d”)
     {
         this.gameObject.transform.Translate(new Vector2(5, 0) * Time.deltaTime);
     }  //此類別.這個物件.座標系統.位移(delta向量)

				//二、向左走；依照一、的作法會發現物件飆很快，因此要乘上Time.deltaTime來延遲。
		 if (Input.GetKey("a"))
     {
        this.gameObject.transform.Translate(new Vector2(-5, 0) * Time.deltaTime);
     }  
				//向上走 //可以直接使用Vector的屬性Vector2.up，就不需要new一個變數
     if (Input.GetKey("w"))
     {
        //this.gameObject.transform.Translate(new Vector2(0, 5) * Time.deltaTime);
        Jump();
     }  
				//向下走
		 if (Input.GetKey("s"))
     {
        this.gameObject.transform.Translate(new Vector2(0, -5) * Time.deltaTime);
     }  
     */

  }

  private void Flip()
  {
    facingRight = !facingRight;
    Vector3 theScale = transform.localScale;
    theScale.x *= -1;
    transform.localScale = theScale;
  }

  private void footPointCheck()
  {
    if(Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true || 
        Physics2D.OverlapCircle(footPoint2.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true)
    {
      touchGround = true;
    }
    else
    {
      touchGround = false;
    }
    //touchGround = Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform"));
    if(touchGround == true) 
    {
      animator.SetBool("jump",false);
      animator.SetBool("standby",true);
      canJump = true;
      if(cor_canJump_dead != null) StopCoroutine(cor_canJump_dead);
    }
    else
    {
      animator.SetBool("standby",false);
      animator.SetBool("jump",true);
      if(cor_canJump_dead == null) cor_canJump_dead = StartCoroutine(canJump_dead());
    }
  }

  private IEnumerator canJump_dead()
  {
    yield return new WaitForSeconds(0.1f);
    canJump = false;
  }

  private void Jump()
  {
    if(Input.GetButtonDown("Jump") && canJump && touchGround)
    {
      animator.SetBool("standby",false);
      animator.SetBool("jump",true);
      canJump = false;
      rb2d.AddForce(Vector2.up * jumpForce);
    }
  }
}