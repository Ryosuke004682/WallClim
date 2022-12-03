using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sample : MonoBehaviour
{
    //���1�@:�@��x�G�����痣��Ȃ��i�߂�Ȃ��j

    //���C���{��΂�

    [Header("�v���C���[�̏ڍאݒ�")]
    public const float _moveSpeed = 4.0f;
  

    [Header("�v���C���[�ɂ�����d�͂̐ݒ�")]
    public const float _gravityPower = 5.0f;
    
    Vector3 localGravity = Vector3.down;


    [Header("�Ǔo��̃Z�b�e�B���O")]
    public const float _rayDistance = 1.0f;

    public float _climeSpeed = 1.5f;
    public string _wallName = "Wall";
    public bool _climbing = false;

    
    bool groundCheck;//������

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
        Move();//Player�̓����S��
    }

    private void FixedUpdate()
    {
        WallClim();//�Ǔo��
        FallAcceleration();//�d�͌v�Z
    }

    void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical   = Input.GetAxisRaw("Vertical");
        var velocity   = new Vector3(horizontal, 0, vertical).normalized;

        rb.velocity = velocity * _moveSpeed;
        Debug.Log(rb.velocity);

        // != null���Ă�̂́AwallName�������ƁAwallName��������܂�Null���o�邽�߁B
        //�Ƃ肠�������C�������ɓ������Ă����Null�͏o�Ȃ����珑�����B

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
    /// �Ǔo��
    /// </summary>
    
    //�����������@:�@���ɍ~�肽�猳�̈ړ��ɖ߂肽���B

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
    /// �d�͂��v�Z����B
    /// �ڒn�����������ɏ���
    /// </summary>
    void FallAcceleration()
    {
        var gravity = localGravity.y * _gravityPower;

        rb.AddForce(Physics.gravity * gravity * Time.deltaTime, 
                                                ForceMode.Impulse);
    }
}