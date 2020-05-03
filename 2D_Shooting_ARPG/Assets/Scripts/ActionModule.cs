using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionModule : MonoBehaviour
{
    private Rigidbody2D rig;

    [HideInInspector] //移動方向(入力)
    public Vector2 valueMove = Vector2.zero;

    [HideInInspector] //視線方向
    public Vector2 valueDirection = Vector2.zero;

    /// <summary> 接地判定に利用するBoxColliderv </summary>
    [SerializeField]
    private BoxCollider2D bc;

    /// <summary> 地形のレイヤーマスク </summary>
    [SerializeField]
    private LayerMask groundLayerMask;

    /// <summary> 接地しているかどうかを取得する </summary>
    public bool isGround = false;

    /// <summary> 連続ジャンプを防止するためのフラグ </summary>
    private bool isJumpInt = false;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponentInChildren<Rigidbody2D>();
        bc = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Check_Ground();

        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity += Vector2.up * 40;
        }
        */
        
    }

    public void Jump()
    {
        if (isGround && !isJumpInt)
        {
            Debug.Log("JUMP");
            rig.velocity += Vector2.up * 40;
            StartCoroutine(C_JumpInterval());
        }
        

    }

    //ジャンプのインターバル
    private IEnumerator C_JumpInterval()
    {
        isJumpInt = true;
        yield return new WaitForSeconds(0.1f);
        isJumpInt = false;
    }


    private void Move()
    {
        //valueMove = new Vector2( Input.GetAxis("Horizontal"), 0);
        float speed = 40;

        //rig.velocity = new Vector2( 20 * valueMove.x - rig.velocity.x, rig.velocity.y);
        rig.AddForce(10 * Vector2.right * (valueMove.x * speed - rig.velocity.x));


    }

    private void Check_Ground()
    {
        //Colliderと地面の距離がこの値より小さければ接地と判定する
        float floatingDistance = 0.01f;

        //  BoxCastの発生元を少し上にずらす距離
        float epsilon = 0.005f;

        Vector2 origin = new Vector2(bc.transform.position.x, bc.transform.position.y) + bc.offset + new Vector2(0f, epsilon);


        RaycastHit2D raycastHit2 = Physics2D.BoxCast(
            origin: origin,
            size: bc.size * 10,
            angle: 0,
            direction: Vector2.down,
            distance: floatingDistance + epsilon,
            layerMask: groundLayerMask
        );

        isGround = raycastHit2;
        Debug.Log("isGround: " + isGround);
    }

}
