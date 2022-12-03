using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sample : MonoBehaviour
{
    //問題1　:　一度触ったら離れない（戻れない）

    //レイを二本飛ばす

    [Header("プレイヤーの詳細設定")]
    public const float _moveSpeed = 4.0f;
  

    [Header("プレイヤーにかかる重力の設定")]
    public const float _gravityPower = 5.0f;
    
    Vector3 localGravity = Vector3.down;


    [Header("壁登りのセッティング")]
    public const float _rayDistance = 1.0f;

    public float _climeSpeed = 1.5f;
    public string _wallName = "Wall";
    public bool _climbing = false;

    
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
        Move();//Playerの動き全般
    }

    private void FixedUpdate()
    {
        WallClim();//壁登り
        FallAcceleration();//重力計算
    }

    void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical   = Input.GetAxisRaw("Vertical");
        var velocity   = new Vector3(horizontal, 0, vertical).normalized;

        rb.velocity = velocity * _moveSpeed;
        Debug.Log(rb.velocity);

        // != nullしてるのは、wallNameだけだと、wallNameが見つかるまでNullが出るため。
        //とりあえずレイが何かに当たっていればNullは出ないから書いた。

        if (_hit.collider != null && _hit.collider.CompareTag(_wallName))
        {
            _climbing = true;
            velocity = new Vector3(horizontal, vertical, 0);
            rb.velocity = velocity * _climeSpeed;

            Debug.Log(_climbing);
        }
        else
        {
            _climbing = false;
            velocity = new Vector3(horizontal, 0, vertical).normalized;
            rb.velocity = velocity * _moveSpeed;
            Debug.Log(_climbing);
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


        Debug.DrawRay(rayPosition, _ray.direction * _rayDistance, Color.red);

        if (Physics.Raycast(_ray, out _hit, _rayDistance))
        {
            if (_hit.collider.CompareTag(_wallName))
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
    /// 接地処理もここに書く
    /// </summary>
    void FallAcceleration()
    {
        var gravity = localGravity.y * _gravityPower;

        rb.AddForce(Physics.gravity * gravity * Time.deltaTime, 
                                                ForceMode.Impulse);
    }
}