using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float _moveSpeed = 4.0f;

    RaycastHit _hit;
    Ray _ray;

    [Header("壁登りのセッティング")]
    float rayDistance = 0.5f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        WallCheck();
    }

    void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var velocity = new Vector3(horizontal , 0 , vertical).normalized;

        rb.velocity = velocity * _moveSpeed;

        Debug.Log(rb.velocity);
    }

    /// <summary>
    /// 壁と接しているかどうかのチェック
    /// </summary>
    void WallCheck()
    {
        _ray = new Ray(transform.position,transform.forward * rayDistance);

        if (Physics.Raycast(_ray , out _hit , rayDistance) )
        {
            if (_hit.collider.CompareTag("Wall"))
            {
                Debug.Log("壁");
            }
        }
    }
}