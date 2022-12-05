using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーの詳細設定")]
    public float _moveSpeed   =  4.0f;
    public float gravityPower = -5.0f;
    Rigidbody rb;

    [Header("壁登りのセッティング")]
    public string wallName = "Wall";
    const  float rayDistance   = 0.5f;
    public float impalseSpeed = 1.5f;

    RaycastHit _hit;
    Ray _ray;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity     = true;
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
        var velocity   = new Vector3(horizontal , 0 , vertical).normalized;
        rb.velocity    = velocity * _moveSpeed;

        Debug.Log(rb.velocity);
    }

    /// <summary>
    /// 壁登り
    /// </summary>
    void WallClim()
    {
         

         _ray = new Ray(transform.position, transform.forward * rayDistance);

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

        //壁登り
        Vector3 firstTransform = Vector3.up * impalseSpeed;

        if (_hit.collider.CompareTag(wallName) && Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(firstTransform, ForceMode.Impulse);
        }
        else if (_hit.collider.CompareTag(wallName) && Input.GetKey(KeyCode.RightShift))
        {
            rb.AddForce(-firstTransform, ForceMode.Impulse);
        }

    }
}