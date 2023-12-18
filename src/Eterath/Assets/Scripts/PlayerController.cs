using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 originalRot;
    public Vector3 direct;
    public Rigidbody m_Rigidbody;
    public Animator playerAnimator;
    public GameObject player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        originalRot = transform.eulerAngles;
        m_Rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        m_Rigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = originalRot;
        originalRot.y += Input.GetAxis("Mouse X")*3;
        //direct = new Vector3((transform.forward.x  * Input.GetAxis("Vertical"))*speed, 0 ,(transform.forward.z  * Input.GetAxis("Vertical"))*speed);
        //player.transform.position += direct;
        player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
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
        if (Input.GetAxis("Vertical") > 0) 
        {
            direct = new Vector3((transform.forward.x  * Input.GetAxis("Vertical"))*speed, 0 ,(transform.forward.z  * Input.GetAxis("Vertical"))*speed);
            player.transform.position += direct;
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
        } 
        else if(Input.GetAxis("Vertical") < 0) 
        {
            direct = new Vector3((transform.forward.x  * Input.GetAxis("Vertical"))*speed, 0 ,(transform.forward.z  * Input.GetAxis("Vertical"))*speed);
            player.transform.position += direct;
            player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
        }

        if (Input.GetAxis("Vertical") != 0) 
        {
            direct = new Vector3((transform.forward.x  * Input.GetAxis("Vertical"))*speed, 0 ,(transform.forward.z  * Input.GetAxis("Vertical"))*speed);
            player.transform.position += direct;
        }
        
        // Vector3 pos = player.transform.position;
        //     pos.x += Input.GetAxis("Horizontal") * speed; //change vertical position based on input from specific player
        //     pos.y += Input.GetAxis("Vertical") * speed; //change horizontal position based on input from specific player
        //     player.transform.position = pos; //update localPosition of player
        bool change = false;
        bool direction = false;
        switch(Input.GetAxis("Horizontal")) 
        {
            case > 0:
                player.transform.eulerAngles += new Vector3(0,90,0);
                //player.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                change = true;
                direction = true;
                break;
            // case 0:
            //     if(change) 
            //     {
            //         if(direction) 
            //         {
            //             player.transform.eulerAngles += new Vector3(0,-90,0);
            //         } else 
            //         {
            //             player.transform.eulerAngles += new Vector3(0,90,0);
            //         }
            //         change = false;
            //     }  
            //     break;
            case < 0:
                player.transform.eulerAngles += new Vector3(0,-90,0);
                change = true;
                direction = false;
                break;
        }
        switch (Input.GetAxis("Vertical"))
        {
            case >0:
                player.transform.eulerAngles += new Vector3(0,0,0);
                break;
            case <0:
                player.transform.eulerAngles += new Vector3(0,180,0);
                break;
        }
    }
}
