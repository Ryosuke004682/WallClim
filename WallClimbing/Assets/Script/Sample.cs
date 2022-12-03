using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sample : MonoBehaviour
{
    //問題1　:　上まで行っても登り切れない
    //問題2　:　一度触ったら離れない（戻れない）

    //レイを二本飛ばす
    //1本目、頭の上から前方斜めにレイを飛ばしてみる。（レイが上まで行くと壁の判定が無くなって、替わりにレイが地面につく。
    //そのポジションを取得して、瞬間移動）

    [Header("プレイヤーの詳細設定")]

    public float _moveSpeed = 4.0f;
  
    [Header("プレイヤーにかかる重力の設定")]
    public float gravityPower = 5.0f;
    Vector3 localGravity = Vector3.down;　//未実装


    [Header("壁登りのセッティング")]
    public string wallName = "Wall";
    const float rayDistance = 1.0f;
    public float climeSpeed = 1.5f;
    bool Climbing = false;

    bool groundCheck;//未実装

    RaycastHit _hit;
    Ray _ray;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        WallClim();
    }

    void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical   = Input.GetAxisRaw("Vertical");
        var velocity   = new Vector3(horizontal, 0, vertical).normalized;

        rb.velocity = velocity * _moveSpeed;
        Debug.Log(rb.velocity);

        // != nullしてるのは、wallNameだけだと、wallNameが見つかるまでNullが出るため。
        //とりあえず当たっていればNullは出ないから書いた。

        if (_hit.collider != null && _hit.collider.CompareTag(wallName))
        {
            Climbing = true;
            velocity = new Vector3(horizontal, vertical, 0);
            rb.velocity = velocity * climeSpeed;

            Debug.Log(Climbing);
        }
        else
        {
            Climbing = false;
            velocity = new Vector3(horizontal, 0, vertical).normalized;
            rb.velocity = velocity * _moveSpeed;
            Debug.Log(Climbing);
        }
    }

    /// <summary>
    /// 壁登り
    /// </summary>
    
    //今したい事　:　下に降りたら元の移動に戻りたい。

    public void WallClim()
    {
        Vector3 rayPosition = transform.position + Vector3.zero;
        _ray = new Ray(rayPosition , new Vector3(0,-1,1));


        Debug.DrawRay(rayPosition, _ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(_ray, out _hit, rayDistance))
        {
            if (_hit.collider.CompareTag(wallName))
            {
                rb.useGravity = false;
                Debug.Log("I'm on the wall.");
            }
            else
            {
                rb.useGravity = true;
                Debug.Log("I'm off the wall.");
            }
        }

      
    }

    /// <summary>
    /// 重力を計算する。
    /// </summary>
    void FallAcceleration()
    {

    }

    /// <summary>
    /// 地面の当たり判定
    /// </summary>
    /// <param name="ground"></param>
    private void OnTriggerEnter(Collider ground)
    {

    }
}