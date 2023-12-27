using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 originalRot;
    public Vector3 originalplayerRot;
    public Vector3 direct;
    public Rigidbody m_Rigidbody;
    public Animator playerAnimator;
    public GameObject player;
    public GameObject playerSkeleton;
    public GameObject torsoBone;
    public float speed;
    public float limit;
    public float turnSpeed;
    public bool w, a, s, d;
    public float speedMod = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        // Initalization
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        originalRot = transform.eulerAngles;
        m_Rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        m_Rigidbody.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Initalization
        turnSpeed = 0.75f; //- (Mathf.Abs((Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"))/2)/20);
        Cursor.lockState = CursorLockMode.Locked;

        // Setting position and rotation of camera object.
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z); 
        transform.eulerAngles = originalRot;

        // Change rotation of camera around player using mouse movement.
        originalRot.y += Input.GetAxis("Mouse X")*2;
        originalRot.x += -Input.GetAxis("Mouse Y")*2;

        // Set up easing so that when player presses a movement button they dont immediately start at max speed. Bascially acceleration/easing implementation.
        //Debug.Log("Speed Modifier: " + speedMod);
        if(w && speedMod <= 1 || a && speedMod <= 1 || s && speedMod <= 1 || d && speedMod <= 1) 
        {
            Debug.Log(w);
            speedMod += 0.005f;
        } 
        else 
        {
            if((Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")))/2 < 0.4) 
            {
                speedMod = 0.25f;  
            }
        }

        // Just better WASD decection because unity is cringe.
        if(Input.GetKeyDown(KeyCode.W)) 
        {
            w = true;
        } 
        else if(Input.GetKeyUp(KeyCode.W)) 
        {
            w = false;
        }

        if(Input.GetKeyDown(KeyCode.A)) 
        {
            a = true;
        } 
        else if(Input.GetKeyUp(KeyCode.A)) 
        {
            a = false;
        }

        if(Input.GetKeyDown(KeyCode.S)) 
        {
            s = true;
        } 
        else if(Input.GetKeyUp(KeyCode.S)) 
        {
            s = false;
        }

        if(Input.GetKeyDown(KeyCode.D)) 
        {
            d = true;
        } 
        else if(Input.GetKeyUp(KeyCode.D)) 
        {
            d = false;
        }

        // Using key inputs to rotate player character.
        if(d && !(Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))) 
        {
            limit = transform.eulerAngles.y - 90;
            rotateTo(limit, turnSpeed, player);
        } 
        if(d && w) 
        {
            limit = transform.eulerAngles.y - 135;
            rotateTo(limit, turnSpeed, player);
        }
        if(a && !(Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))) 
        {
            limit = transform.eulerAngles.y - 270;
            rotateTo(limit, turnSpeed, player);
        }
        if(a && s) 
        {
            limit = transform.eulerAngles.y - 315;
            rotateTo(limit, turnSpeed, player);
        }

        if(!(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) && w) 
        {
            limit = transform.eulerAngles.y - 180;
            rotateTo(limit, turnSpeed, player);
        } 
        if(!(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) && s) 
        {
            limit = transform.eulerAngles.y - 360;
            rotateTo(limit, turnSpeed, player);
        }
        if(a && w) 
        {
            limit = transform.eulerAngles.y - 225;
            rotateTo(limit, turnSpeed, player);
        }
        if(d && s) 
        {
            limit = transform.eulerAngles.y - 45;
            rotateTo(limit, turnSpeed, player);
        }

        // Using key inputs to rotate player character.
        if(d && !(Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))) 
        {
            limit = transform.eulerAngles.y - 90;
            rotateTo(limit, turnSpeed*2, torsoBone);
        } 
        if(d && w) 
        {
            limit = transform.eulerAngles.y - 90;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }
        if(a && !(Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))) 
        {
            limit = transform.eulerAngles.y - 270;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }
        if(a && s) 
        {
            limit = transform.eulerAngles.y - 315;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }

        if(!(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) && w) 
        {
            limit = transform.eulerAngles.y - 180;
            rotateTo(limit, turnSpeed*2, torsoBone);
        } 
        if(!(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) && s) 
        {
            limit = transform.eulerAngles.y - 360;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }
        if(a && w) 
        {
            limit = transform.eulerAngles.y - 225;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }
        if(d && s) 
        {
            limit = transform.eulerAngles.y - 45;
            rotateTo(limit, turnSpeed*2, torsoBone);
        }

        // Handling player animation based on if the user is inputing.        
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            playerAnimator.SetBool("walking", true);
            Debug.Log("walking");
        }
        else 
        {
            playerAnimator.SetBool("walking", false);
            Debug.Log("not walking");
        }

        // Defining forward and back/side to side movement of player.
        if (w) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        } 
        else if(s) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        }
        else if (d) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        } 
        else if(a) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        }
        else if (w && a) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        } 
        else if(w && d) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        }
        else if (s && d) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(playerSkeleton.transform.forward.x*speed*speedMod, 0 ,playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        } 
        else if(s && a) 
        {
            //player.transform.eulerAngles = new Vector3(0, originalRot.y + 180f, 0);
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
            direct = new Vector3(-playerSkeleton.transform.forward.x*speed*speedMod, 0 ,-playerSkeleton.transform.forward.z*speed*speedMod);
            player.transform.position += direct;
        }
    }

    // Wanted my turning to look fluid so implemented a weird sort of easing to make it smooth and not snappy.
    void rotateTo(float limit,  float turnSpeed, GameObject rotObject) 
    {
        if (limit < 0) 
        {
            limit += 360;
        }
        //Debug.Log("Player: " + player.transform.eulerAngles.y + "Camera: " + transform.eulerAngles.y + "Camera Diff: " + (transform.eulerAngles.y - 90) + "Limit: " + limit);
        if (rotObject.transform.eulerAngles.y < limit && (limit - rotObject.transform.eulerAngles.y) > 180f) 
        {
            if(rotObject.transform.eulerAngles.y > limit) 
            {
                rotObject.transform.eulerAngles += new Vector3(0,-turnSpeed,0);
            }
            if(rotObject.transform.eulerAngles.y < 180f) 
            {
                rotObject.transform.eulerAngles += new Vector3(0,-turnSpeed,0);
            }
        } 
        else if (rotObject.transform.eulerAngles.y < limit && (limit - rotObject.transform.eulerAngles.y) <= 180f) 
        {
            rotObject.transform.eulerAngles += new Vector3(0,turnSpeed,0);
        } 

        if (rotObject.transform.eulerAngles.y > limit && (rotObject.transform.eulerAngles.y - limit) > 180f) 
        {
            if(rotObject.transform.eulerAngles.y < limit) 
            {
                rotObject.transform.eulerAngles += new Vector3(0,turnSpeed,0);
            }
            if(rotObject.transform.eulerAngles.y > 180f) 
            {
                rotObject.transform.eulerAngles += new Vector3(0,turnSpeed,0);
            }
        } 
        else if (rotObject.transform.eulerAngles.y > limit && (rotObject.transform.eulerAngles.y - limit) <= 180f) 
        {
            rotObject.transform.eulerAngles += new Vector3(0,-turnSpeed,0);
        } 
    }
}
