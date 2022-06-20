using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    bool firstFlagPlaced = false;
    Vector2 firstFlagPosition;

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
            placeFlag();
        }
    }

    //Platziert Fagge(Rechteck um den Schnitt, Schnitte werden an den Stellen vordefiniert)
    void placeFlag()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Tilemap tilemap = GameObject.Find("Flags").GetComponent<Tilemap>();

        Sprite sprite = Resources.Load<Sprite>("Flag");

        if(firstFlagPlaced) {
            createFlagObject("flag2", playerPosition);
        } else {
            createFlagObject("flag1", playerPosition);
        }
              
        if(firstFlagPlaced) {
            firstFlagPlaced = false;
            SceneManager.LoadScene("Digging");
        } else {
            firstFlagPosition.x = playerPosition.x; 
            firstFlagPosition.y = playerPosition.y; 
            firstFlagPlaced = true;
        }    
    }

    void createFlagObject(string name, Vector3 position){
        Debug.Log(position);
        GameObject flag = new GameObject();
        flag.name = name;
        flag.AddComponent<SpriteRenderer>();
        flag.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Flag");
        flag.transform.position = position;
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
