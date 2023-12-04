using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public Vector3 originalRot;
    public Vector3 direct;
    public Rigidbody m_Rigidbody;
    public float m_Thrust = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.eulerAngles;
        m_Rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = originalRot;
        originalRot.y += Input.GetAxis("Horizontal");
        direct = new Vector3(transform.forward.x, 0 ,transform.forward.z);
        m_Rigidbody.AddForce(direct * m_Thrust * Input.GetAxis("Vertical"));
    }
}
