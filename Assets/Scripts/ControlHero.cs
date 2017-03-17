using UnityEngine;
using System.Collections;

public class ControlHero : MonoBehaviour
{
    public float speed = 6.0F;
    CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float maxX, maxY, minX, minY;
    public Animator animator;
    public GameObject muzzeLight;
    public GameObject hero;
    private bool flipped = false;
    public VirtualJoyStick virtualJoyStick;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") ,0 );
           
        if(virtualJoyStick.InputDirection != Vector3.zero)
        {
            moveDirection = virtualJoyStick.InputDirection;
        }
        moveDirection *= speed;
        if (checkBound(moveDirection * Time.deltaTime))
            {
                controller.Move(moveDirection * Time.deltaTime);
                if (moveDirection.x > 0 && hero.transform.localScale.x == -1)
                {
                    hero.transform.localScale = new Vector3(1, 1, 1);
                }
                if (moveDirection.x < 0 && hero.transform.localScale.x == 1)
                {
                    hero.transform.localScale = new Vector3(-1, 1, 1);
                }
                
            }
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
        //    Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        if(moveDirection.x != 0 || moveDirection.y != 0)
        {
                animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        //}
        if (Input.GetKeyDown(KeyCode.F)){
                animator.SetBool("isFire", true);
                Debug.DrawLine(muzzeLight.transform.position,
                    muzzeLight.transform.position + 20 * Vector3.right * hero.transform.localScale.x);
                muzzeLight.GetComponent<SpriteRenderer>().enabled = true;
                Ray ray = new Ray(muzzeLight.transform.position, 20 * Vector3.right * hero.transform.localScale.x);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if(hit.collider.gameObject != null)
                    {
                        hit.collider.gameObject.SetActive(false);
                    Debug.Log("name " + hit.collider.gameObject.name);
                }
                }

        }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) ||
                Input.GetKeyUp(KeyCode.F))
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isFire", false);
                muzzeLight.GetComponent<SpriteRenderer>().enabled = false;
            }
            
	}
    bool checkBound(Vector3 move)
    {
        if (controller.transform.position.x + move.x < minX)
            return false;
        if (controller.transform.position.x + move.x > maxX)
            return false;
        if (controller.transform.position.y + move.y < minY)
            return false;
        if (controller.transform.position.y + move.y > maxY)
            return false;
        return true;
    }


    public void Fire()
    {
        animator.SetBool("isFire", true);
        Debug.DrawLine(muzzeLight.transform.position,
            muzzeLight.transform.position + 20 * Vector3.right * hero.transform.localScale.x);
        muzzeLight.GetComponent<SpriteRenderer>().enabled = true;
        Ray ray = new Ray(muzzeLight.transform.position, 20 * Vector3.right * hero.transform.localScale.x);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject != null)
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }

    public void NoFire()
    {
        animator.SetBool("isRun", false);
        animator.SetBool("isFire", false);
        muzzeLight.GetComponent<SpriteRenderer>().enabled = false;
    }
}
