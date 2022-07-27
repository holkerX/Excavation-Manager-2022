using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using DataStorage;
using SandboxTileManagement;

public class SandboxPlayerMovement : MonoBehaviour
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

            GameObject dataStorageObject = GameObject.Find("DataStorageObject");
            DataStorageClass dataStorage =
                dataStorageObject.GetComponent<DataStorageClass>();

            // Clean and Save the Data  
            dataStorage.startingPoint.x = (float)Math.Round(firstFlagPosition.x);
            dataStorage.startingPoint.y = (float)Math.Round(firstFlagPosition.y);

            dataStorage.size.x = (float)Math.Floor(secondFlagPosition.x - firstFlagPosition.x);
            dataStorage.size.y = (float)Math.Floor(secondFlagPosition.y - firstFlagPosition.y);

            // Set the Number of found Artifacts
            GameObject sandboxTileManagement = GameObject.Find("SandboxTileManagement");
            SandboxTileManagementScript sandboxTileManagementScript =
                sandboxTileManagement.GetComponent<SandboxTileManagementScript>();
            int overallTileValue = 0;
            int numberOfTiles = 0;
            for (int i = (int) dataStorage.startingPoint.x ; i < (int) dataStorage.startingPoint.x + (int) dataStorage.size.x; i++)
            {
                for (int j = (int) dataStorage.startingPoint.y ; j < (int) dataStorage.startingPoint.y + (int) dataStorage.size.y; j++)
                {
                    overallTileValue = overallTileValue + sandboxTileManagementScript.sandboxTileValues[i][j];
                    numberOfTiles++;
                }
            }

            if(overallTileValue >= 4 / (numberOfTiles / 2)) {
                dataStorage.artifactsEnabled = 4;
            } else {
                dataStorage.artifactsEnabled = overallTileValue;
            }

            // Load the new Scene
            SceneManager.LoadScene("Digging");
        }
    }
}
