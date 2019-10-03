using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChan : MonoBehaviour
{
    public CharacterController charctrl;
    public Animator anim;
    Vector3 movaxis, turnaxis;
    public GameObject currentCamera;
    public float jumpspeed = 8;
    public float gravity = 20;

    float yresult;
    float flyvelocity = 3;
    //public GameObject wing;
    public Transform rightHandObj, leftHandObj;
    bool jumpbtn = false;
    bool jumpbtnrelease = false;
    // Start is called before the first frame update
    void Start()
    {
        currentCamera = Camera.main.gameObject;
    }
    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            jumpbtn = true;
        }
    }

    void FixedUpdate()
    {

        movaxis = new Vector3(Input.GetAxis("Horizontal")*0.3f, 0, Input.GetAxis("Vertical"));

        
        yresult -= gravity * Time.fixedDeltaTime;

        

        Vector3 relativedirection = currentCamera.transform.TransformVector(movaxis);
        relativedirection = new Vector3(relativedirection.x, yresult, relativedirection.z);

        Vector3 relativeDirectionWOy = relativedirection;
        relativeDirectionWOy = new Vector3(relativedirection.x, 0, relativedirection.z);

       

        anim.SetFloat("Speed", charctrl.velocity.magnitude);
       
        
            charctrl.Move(relativedirection * 0.1f);
            Quaternion rottogo = Quaternion.LookRotation(relativeDirectionWOy * 2 + transform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, rottogo, Time.fixedDeltaTime * 50);
        
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("PunchA");
        }

        if (charctrl.isGrounded && jumpbtn)
        {
            anim.SetTrigger("Jump");
            yresult = jumpspeed;

        }

       
        



        RaycastHit hit;
        
        jumpbtn = false;



    }


    //a callback for calculating IK
    void OnAnimatorIK()
    {
        {

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);

                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    
    }
}
