using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float _moveSpeed = 4.0f;
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
        
    }

    void Move()
    {
        //WASD�œ���������
        //float�^�Œ�`�ł��Ȃ�����B�B�B�B

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var velocity = new Vector3(horizontal , 0 , vertical).normalized;

        rb.velocity = velocity * _moveSpeed;

        if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            rb.velocity *= vertical * 10 ;
            Debug.Log("�����ꂽ��");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            rb.velocity *= -horizontal * 10;
            Debug.Log("�����ꂽ��");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            rb.velocity *= -vertical * 10;
            Debug.Log("�����ꂽ��");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            rb.velocity *= horizontal * 10;
            Debug.Log("�����ꂽ��");
        }

        Debug.Log(rb.velocity);
        
    }
}