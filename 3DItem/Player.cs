using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    public GameController gameController;

    public TMP_Text itemCounterText;
    public int maxItemValue = 8;
    public float speed;
    public item[] items; 

    public int food, water, oxygentank, bordgame, medicine, fixtool, map, research, book, battery, yellow, blue, white;

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    Rigidbody rigid;
    Animator anim;

    bool isBorder;
    bool wDown;

    List<item> nearbyItems = new List<item>();
    List<int> heldItems = new List<int>();

    Storage nearbyStorage = null;

    public AudioClip pickupSound;
    public AudioClip storageSound;
    AudioSource audioSource;
    public Image slot1;
    public Sprite emptySlotSprite;
    public Sprite[] itemSprites;

    int GetTotalItemValue()
    {
        return food + water + oxygentank + bordgame + medicine + fixtool +
               map + research + book + battery + yellow + blue + white;
    }

    public int GetTotalItemCount()
    {
        return food + water + oxygentank + bordgame + medicine + fixtool + map + research + book + battery + yellow + blue + white;
    }

    void UpdateItemUI()
    {
        int total = GetTotalItemValue();
        itemCounterText.text = total.ToString() + " / " + maxItemValue.ToString();

        Color targetColor = total >= maxItemValue ? Color.red : Color.yellow;

        foreach (item i in item.allItems)
        {
            if (i != null && !i.collected)
                i.SetColor(targetColor);
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateItemUI();
        GetInput();
        Move();
        //Turn();
        RotateToMouse();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyStorage != null)
            {
                anim.SetTrigger("doPickup");

                if (storageSound != null)
                    audioSource.PlayOneShot(storageSound);

                nearbyStorage.ReceiveItemsFrom(this);
            }
            else
            {
                TryPickUp();
            }
        }
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetKey(KeyCode.W);
    }

    void Move()
    {
        float currentSpeed = speed;

    

        if (gameController != null && gameController.timeLeft <= 30f)
        {
            currentSpeed *= 0.8f;
        }

        if (wDown && !isBorder)
        {
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
        }

        anim.SetBool("isRun", wDown);
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.yellow);
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }

    void RotateToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Floor", "Wall")))
        {
            Vector3 targetPoint = hit.point;
            targetPoint.y = transform.position.y;
            Vector3 direction = (targetPoint - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            Debug.DrawLine(transform.position + Vector3.up * 1f, targetPoint + Vector3.up * 1f, Color.red);
        }
    }

    void AddItem(ref int current, int value)
    {
        current += value;
    }

    void TryPickUp()
    {
        if (nearbyItems.Count == 0) return;
        if (GetTotalItemCount() >= 8) return;

        item closestItem = null;
        float minDist = Mathf.Infinity;

        int totalValue = GetTotalItemValue();
        if (totalValue >= maxItemValue)
        {
            return;
        }


        foreach (item i in nearbyItems)
        {
            if (i == null || i.collected) continue;
            float dist = Vector3.Distance(transform.position, i.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestItem = i;
            }
        }

        if (closestItem != null)
        {
            Vector3 toItem = (closestItem.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, toItem);
            if (angle > 90f) return;

            anim.SetTrigger("doPickup");
      
            if (pickupSound != null)
                audioSource.PlayOneShot(pickupSound);

            
            StartCoroutine(PickUpItemAfterAnimation(closestItem));
            
        }
    }

    IEnumerator PickUpItemAfterAnimation(item item)
    {
        yield return new WaitForSeconds(0.5f);

        if (item == null || item.collected) yield break;

        item.collected = true;

        switch (item.type)
        {
            case item.Type.Food: AddItem(ref food, item.value); break;
            case item.Type.Water: AddItem(ref water, item.value); break;
            case item.Type.Oxygen: AddItem(ref oxygentank, item.value); break;
            case item.Type.Bordgame: AddItem(ref bordgame, item.value); break;
            case item.Type.Book: AddItem(ref book, item.value); break;
            case item.Type.Medicine: AddItem(ref medicine, item.value); break;
            case item.Type.Fixtool: AddItem(ref fixtool, item.value); break;
            case item.Type.Map: AddItem(ref map, item.value); break;
            case item.Type.Research: AddItem(ref research, item.value); break;
            case item.Type.Battery: AddItem(ref battery, item.value); break;
            case item.Type.yellow: AddItem(ref yellow, item.value); break;
            case item.Type.blue: AddItem(ref blue, item.value); break;
            case item.Type.white: AddItem(ref white, item.value); break;
        }

        nearbyItems.Remove(item);
        Destroy(item.gameObject);
        slot1.sprite = itemSprites[(int)item.type];
        UpdateItemUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            item i = other.GetComponent<item>();
            if (i != null && !nearbyItems.Contains(i))
                nearbyItems.Add(i);
        }
        else if (other.CompareTag("Storage"))
        {
            nearbyStorage = other.GetComponent<Storage>();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            item i = other.GetComponent<item>();
            if (i != null && nearbyItems.Contains(i))
                nearbyItems.Remove(i);
        }
        else if (other.CompareTag("Storage"))
        {
            if (nearbyStorage == other.GetComponent<Storage>())
                nearbyStorage = null;
        }
    }
}