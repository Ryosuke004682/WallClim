using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("�v���C���[�̏ڍאݒ�")]
    public float _moveSpeed = 4.0f;



    [Header("�v���C���[�ɂ�����d�͂̐ݒ�")]
    public float gravityPower = 5.0f;
    Vector3 localGravity = Vector3.down;



    [Header("�Ǔo��̃Z�b�e�B���O")]
    public string wallName = "Wall";
    const float rayDistance = 0.5f;
    public float climeSpeed = 1.5f;
    bool Climbing = false;

    bool groundCheck;

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
        var vertical = Input.GetAxisRaw("Vertical");

        var velocity = new Vector3(horizontal , 0 , vertical).normalized;

        rb.velocity = velocity * _moveSpeed;
        Debug.Log(rb.velocity);

        // != null���Ă�̂́AwallName�������ƁAwallName��������܂�Null���o�邽�߁B
        //�Ƃ肠�����������Ă����Null�͏o�Ȃ����珑�����B
       
        if(_hit.collider != null && _hit.collider.CompareTag(wallName))
        {
            Climbing = true;
            velocity = new Vector3(horizontal ,vertical , 0);
            rb.velocity = velocity * climeSpeed ;

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
    /// �Ǔo��
    /// </summary>
    public void WallClim()
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
    }

    /// <summary>
    /// �d�͂��v�Z����B
    /// </summary>
    void FallAcceleration()
    {
        
    }

    /// <summary>
    /// �n�ʂ̓����蔻��
    /// </summary>
    /// <param name="ground"></param>
    private void OnTriggerEnter(Collider ground)
    {
        
    }

}