using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{

    public BabyControl _BabyControl;
    
    private Rigidbody rig;
    private Quaternion target;
    private Vector3 input;
    private bool isHolding = true;
    private bool canHold;
    private bool canPut;
    private bool touchKid;
    private bool firstPut = true;
    [SerializeField]private GameObject holdItem;
    private Transform shelter;

    private bool inputDisable;
    public float speed;
    public float rotateSpeed;
    public Transform parents;

    private Animator _animator;

    public AudioSource putAndDown;

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        base.Awake();
        rig = this.GetComponent<Rigidbody>();
        target = this.transform.rotation;
    }

    private void Update()
    {
        if(!inputDisable)
            GetInput();
        if(canHold && GameManager.Instance.gameState == 1)
        {
            
            if(Input.GetKeyDown(KeyCode.J) && !isHolding)
            {
                if (holdItem != null && holdItem.name != "IKUN666")
                {
                    holdItem.transform.SetParent(this.transform);
                    holdItem.transform.localScale /= 1.5f;
                    holdItem.transform.position = this.transform.position + Vector3.left / 3 + new Vector3(0, 1.2f,0);
                    isHolding = true;
                    putAndDown.Play();
                }
            }
        }
        
        if(isHolding && canPut)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                holdItem.transform.SetParent(shelter);
                holdItem.transform.position = shelter.position;
                holdItem = null;
                isHolding = false;
                canPut = false;
                putAndDown.Play();
                if(firstPut)
                {
                    _BabyControl.SetKunKunPos(shelter.GetSiblingIndex());
                    firstPut = false;
                }
            }
        }


        if (touchKid)
        {
            if (_BabyControl._playerState == PlayerState.ForcedMove && Input.GetKeyUp(KeyCode.J))
            {
                _BabyControl.ReturnSearch();
            }
            
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                _BabyControl.ForceMove();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        input.z = Input.GetAxis("Horizontal");
        input.x = Input.GetAxis("Vertical") * -1;
    }

    private void Move()
    {
        if (input.x != 0 || input.z != 0)
        {
            _animator.SetBool("isWalking", true);
            rig.MovePosition(this.transform.position + input * speed * Time.deltaTime);
            if (input.x > 0 && input.z == 0)
            {
                target = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else if (input.x < 0 && input.z == 0)
            {
                target = Quaternion.Euler(new Vector3(0, -90, 0));
            }
            else if (input.z > 0 && input.x == 0)
            {
                target = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (input.z < 0 && input.x == 0)
            {
                target = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (input.x > 0 && input.z > 0)
            {
                target = Quaternion.Euler(new Vector3(0, 45, 0));
            }
            else if (input.x > 0 && input.z < 0)
            {
                target = Quaternion.Euler(new Vector3(0, 135, 0));
            }
            else if(input.x < 0 && input.z > 0)
            {
                target = Quaternion.Euler(new Vector3(0, -45, 0));
            }
            else if(input.x < 0 && input.z < 0)
            {
                target = Quaternion.Euler(new Vector3(0, 225, 0));
            }
            TurnAround(target);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void TurnAround(Quaternion target)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("CanHold") == 0)
        {
            canHold = true;
            if (holdItem == null)
            {
                holdItem = other.gameObject;
                holdItem.transform.localScale *= 1.5f;
            }
        }

        if(other.tag.CompareTo("Shelter") == 0)
        {
            canPut = true;
            other.GetComponentInChildren<CanvasGroup>().alpha = 1;
            shelter = other.transform;
        }

        if(other.tag.CompareTo("Kid") == 0)
        {
            touchKid = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.CompareTo("BedRoom") == 0)
        {
            UIManager.Instance.StartAngerTiming(); 
        }

        if (other.tag.CompareTo("CanHold") == 0)
        {
            canHold = false;
            if (other.gameObject == holdItem)
            {
                holdItem.transform.localScale /= 1.5f;
                holdItem = null;
            }
        }

        if (other.tag.CompareTo("Shelter") == 0)
        {
            canPut = false;
            other.GetComponentInChildren<CanvasGroup>().alpha = 0;
        }
        
        if(other.tag.CompareTo("Kid") == 0)
        {
            touchKid = false;
        }
        
    }

    public void MoveToParents()
    {
        inputDisable = true;
        this.transform.position = parents.position;
        _BabyControl.ReturnSearch();
        StartCoroutine(Stand());
    }

    private IEnumerator Stand()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(10);

        UIManager.Instance.MinusAnger(100);
        inputDisable = false;
        GetComponent<Rigidbody>().isKinematic = false;
        UIManager.Instance.ChangeBarContainer();
    }
}
