using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public Vector3 originalRot;
    public Vector3 direct;
    public Rigidbody m_Rigidbody;
    public float m_Thrust = 0.001f;
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        originalRot = transform.eulerAngles;
        m_Rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        m_Rigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = originalRot;
        originalRot.y += Input.GetAxis("Mouse X")*3;
        direct = new Vector3(transform.forward.x, 0 ,transform.forward.z);
        m_Rigidbody.AddForce(direct * m_Thrust * Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") != 0)
        {
            playerAnimator.SetBool("walking", true);
            Debug.Log("walking");
        }
        else 
        {
            playerAnimator.SetBool("walking", false);
            Debug.Log("not walking");
        }
    }
}
