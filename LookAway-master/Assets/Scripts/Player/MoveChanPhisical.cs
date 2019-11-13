using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveChanPhisical : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 movaxis, turnaxis;
    public GameObject currentCamera;
    public float jumpspeed = 8;
    public float gravity = 20;

    float jumptime;
    bool jumpbtn = false;
    bool jumpbtndown = false;
    bool jumpbtnrelease = false;

    public Transform rightHandObj, leftHandObj;
    GameObject closeThing;
    GameObject grablable;
    float weight;
    bool canhold;
    bool holding;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameInformation.returningFromBattle || SceneManager.GetActiveScene().name.Equals(GameInformation.LastScene))
        {
       
            transform.position = GameInformation.LastPos;
            GameInformation.returningFromBattle = false;
       
        }
      
        //if (SceneManager.GetActiveScene().name.Equals("mapa1"))     //maneira antiga de carregar a posição anterior
        //{
        //   if (PlayerPrefs.HasKey("OldPlayerPosition"))
        //   {
        //     print("movendo "+ PlayerPrefsX.GetVector3("OldPlayerPosition"));
        //     transform.position = PlayerPrefsX.GetVector3("OldPlayerPosition");       
        //   }
        // }
        currentCamera = Camera.main.gameObject;
       
    }
    private void Update()
    {
        if(Input.GetButtonDown("Jump") && !holding)
        {
            jumpbtn = true;
            jumpbtndown = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpbtn = false;
            jumptime = 0;
        }
        movaxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    }

    void FixedUpdate()
    {

   
        Vector3 relativedirection = currentCamera.transform.TransformVector(movaxis);
        relativedirection = new Vector3(relativedirection.x, jumptime, relativedirection.z);

        Vector3 relativeDirectionWOy = relativedirection;
        relativeDirectionWOy = new Vector3(relativedirection.x,0, relativedirection.z);

        
        anim.SetFloat("Speed", rdb.velocity.magnitude);

       
        
            rdb.velocity = relativeDirectionWOy*5 + new Vector3(0,rdb.velocity.y,0);
            //rdb.AddForce(relativeDirectionWOy * 1000);
            Quaternion rottogo = Quaternion.LookRotation(relativeDirectionWOy * 2 + transform.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, rottogo, Time.fixedDeltaTime * 50);
        
        if (Input.GetButtonDown("Fire1"))
        {

            //anim.SetTrigger("PunchA");
            holding = true;
        }

        if(Input.GetButtonUp("Fire1"))
        {
            holding = false;
        }

      

        RaycastHit hit;
        if (Physics.Raycast(transform.position-(transform.forward*0.1f)+transform.up*0.3f, Vector3.down,out hit, 1000))
        {
            anim.SetFloat("JumpHeight", hit.distance);
            if (hit.distance < 0.5f && jumpbtn)
            {
                jumptime = 0.25f;
            }
            if (hit.distance>0.5f && jumpbtndown)
            {
                
                
                jumpbtndown = false;
                return;
            }
            
        }

        

        if (jumpbtn)
        {
            jumptime -= Time.fixedDeltaTime;
            jumptime = Mathf.Clamp01(jumptime);
            rdb.AddForce(Vector3.up * jumptime * jumpspeed);

        }

        jumpbtndown = false;

    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {

        if (closeThing)
        {
            //calcula a direcao do ponto de toque para a personagem
            Vector3 handDirection = closeThing.transform.position - transform.position;
            //verifica se o objeto ta na frente do personagem >0
            float lookto = Vector3.Dot(handDirection.normalized, transform.forward);
            //calcula e interpola o peso pela formula (l*3)/distancia^3
            weight=Mathf.Lerp(weight,(lookto*3 / (Mathf.Pow(handDirection.magnitude,3))),Time.fixedDeltaTime*2);
           
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, closeThing.transform.position + transform.right * 0.2f);
            anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.identity);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, closeThing.transform.position - transform.right*0.2f);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.identity);

            if (weight <= 0)
            {
                canhold = false;
                grablable.transform.parent = null;
                Destroy(closeThing);
            }

            if (canhold && holding)
            {
                grablable.transform.parent = rightHandObj.transform;

            }
            if(!holding)
            {
                grablable.transform.parent = null;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.position.y + 0.5f > transform.position.y && collision.gameObject.tag == "Grabbable")
        {
            if (!closeThing)
            {
                closeThing = new GameObject("Handpos");
                canhold = true;
                grablable = collision.gameObject;

            }

            weight = 1;
            closeThing.transform.parent = collision.gameObject.transform;
            closeThing.transform.position= collision.GetContact(0).point;

           

        }

    }

    private void OnCollisionExit(Collision collision)
    {


    }

    public Vector3 GetPlayerPos()
    {
        return transform.position;
    }
}
