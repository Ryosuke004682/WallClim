using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clim : MonoBehaviour
{
    [Header("•Ç“o‚è‚ÌƒZƒbƒeƒBƒ“ƒO")]
    public string wallName = "Wall";
    const float rayDistance = 0.5f;
    public float impalseSpeed = 1.5f;

    Rigidbody rb;

    RaycastHit _hit;
    Ray _ray;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        WallClim();
    }

    /// <summary>
    /// •Ç“o‚è
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

        //•Ç“o‚è

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
