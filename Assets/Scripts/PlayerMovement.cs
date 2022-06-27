using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;
    bool canStartDigging = false;
    bool firstFlagPlaced = false;
    Vector2 firstFlagPosition;
    Vector2 secondFlagPosition;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.y == 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown("space"))
        {
            if (!canStartDigging)
            {
                placeFlag();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("f"))
        {
            checkPlayerAtDesk();
        }
    }

    //Platziert Fagge(Rechteck um den Schnitt, Schnitte werden an den Stellen vordefiniert)
    void placeFlag()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Tilemap tilemap = GameObject.Find("Flags").GetComponent<Tilemap>();

        Sprite sprite = Resources.Load<Sprite>("Flag");

        if (firstFlagPlaced)
        {
            createFlagObject("flag2", playerPosition);
        }
        else
        {
            createFlagObject("flag1", playerPosition);
        }

        if (firstFlagPlaced)
        {
            secondFlagPosition.x = playerPosition.x;
            secondFlagPosition.y = playerPosition.y;
            firstFlagPlaced = false;
            canStartDigging = true;
        }
        else
        {
            firstFlagPosition.x = playerPosition.x;
            firstFlagPosition.y = playerPosition.y;
            firstFlagPlaced = true;
            canStartDigging = false;
        }
    }

    void createFlagObject(string name, Vector3 position)
    {
        GameObject flag = new GameObject();
        flag.name = name;
        flag.AddComponent<SpriteRenderer>();
        flag.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flag");
        flag.transform.position = position;
    }

    void checkPlayerAtDesk()
    {
        float distance = Vector3.Distance(GameObject.Find("Player").transform.position, GameObject.Find("Desk").transform.position);

        if (canStartDigging && distance < 1.5)
        {
            canStartDigging = false;
            Vector2 rect = secondFlagPosition - firstFlagPosition;
            Debug.Log(rect);
            //Skalierung von Sandbox zu Ausgrabungs Scene ist noch nicht klar z.B * 2 ???
            
            //Camera position x
            //firstFlagPosition - rect.x / 2
            //Camera position y
            //firstFlagPosition - rect.y / 2
            SceneManager.LoadScene("Digging");
        }
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            animator.SetFloat("XInput", movement.x);
            animator.SetFloat("YInput", movement.y);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        rb
            .MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);
    }
}
