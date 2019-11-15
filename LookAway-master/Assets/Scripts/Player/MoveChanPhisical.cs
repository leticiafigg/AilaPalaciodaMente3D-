using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveChanPhisical : MonoBehaviour
{
    public Rigidbody rdb;
    public Animator anim;
    Vector3 movaxis, turnaxis;
    public GameObject currentCamera;
    public GameObject jumpBuffSlider;
    private float maxsliderVal;

    public float jumpspeed;
    public float gravity = 20;

    private float jumptime;
    private bool jumpbtn = false;
    private bool jumpbtndown = false;
    private bool grounded = true;

    //variáveis para pulos melhorados
    private float normalJumpspeed;
    private bool jumpbuffOn;
    public float startingBuffTime;
    private float buffTime;
    
    //Variáveis para agarragens
    public Transform sonTranform;
    GameObject closeThing;
    GameObject grabbable;
    float weight;
    bool canhold;
    bool holding;
    
    // Start is called before the first frame update
    void Start()
    {
        
        maxsliderVal = startingBuffTime;

        jumpBuffSlider.GetComponent<Slider>().minValue = 0;
        jumpBuffSlider.GetComponent<Slider>().maxValue = maxsliderVal;



        Cursor.lockState = CursorLockMode.Locked;
        jumpbuffOn = false;
        normalJumpspeed = jumpspeed;
        buffTime = startingBuffTime;

        if (GameInformation.returningFromBattle || SceneManager.GetActiveScene().name.Equals(GameInformation.LastScene))
        {
            transform.position = GameInformation.LastPos;
            GameInformation.returningFromBattle = false;      
        }
      
        currentCamera = Camera.main.gameObject;
       
    }
    private void Update()
    {
        if(Input.GetButtonDown("Jump") && !holding)
        {
            jumpbtn = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpbtn = false;
           
            jumptime = 0;     
        }
        movaxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (jumpbuffOn)
        {
            buffTime -= Time.deltaTime;

            if (buffTime<=0) //Se acabar o tempo de buff, retorna o jump para o original
            {
               jumpBuffSlider.SetActive(false);
               jumpbuffOn = false;
               buffTime = startingBuffTime;
               jumpspeed = normalJumpspeed;
            }
        }

        jumpBuffSlider.GetComponent<Slider>().value = buffTime;

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
        if(jumptime<=0)
        {
            rdb.AddForce(Vector3.down * gravity);
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
                grabbable.transform.SetParent(null);
                Destroy(closeThing);
            }

            if (canhold && holding)
            {
                grabbable.transform.SetParent(sonTranform.transform) ;

            }
            if(!holding)
            {
                grabbable.transform.SetParent (null);
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
                grabbable = collision.gameObject;

            }

            weight = 1;
            closeThing.transform.parent = collision.gameObject.transform;
            closeThing.transform.position= collision.GetContact(0).point;

           

        }

    }

    private void OnCollisionExit(Collision collision)
    {


    }

    public void SuperJumpEnabled(int superJump)
    {
       jumpspeed = superJump;
       jumpBuffSlider.SetActive(true);
       jumpbuffOn = true;
    }

    public Vector3 GetPlayerPos()
    {
        return transform.position;
    }
}
