using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private Vector3 camPos;

    public Vector3 direction = new Vector3(0, 0, 0); 
    public float speed = 50;
    public float maxSpeed = 100;
    public float acceleration = 3;

    protected Vector3 moveTranslation;

    enum MovementState { Reverse, SpeedToggle, DirectionToggle }
    MovementState currentMovementState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.DisplayHUD();
        this.KeepOnScreen();
        this.UpdateMovement();
    }

    void DisplayHUD()
    {

    }

   

    void KeepOnScreen()
    {
        camPos = camera.WorldToScreenPoint(transform.position);

        if(camPos.x < 0)
        {
            Debug.Log("Left bounds");
            direction.x = 1;
        }

        if(camPos.x > camera.pixelWidth)
        {
            Debug.Log("right bounds");
            direction.x = -1;
        }

        if(camPos.y < 0)
        {
            Debug.Log("bottom bounds");
            direction.y = 1;
        }

        if (camPos.y > camera.pixelHeight)
        {
            Debug.Log("Top bounds");
            direction.y = -1;
        }

    }

    void UpdateKeyInput()
    {
        #region Keyboard Movement
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += new Vector3(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { 
            direction += new Vector3(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += new Vector3(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += new Vector3(-1, 0);
        }
        #endregion

        #region Keyboard MovementModes
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentMovementState = MovementState.SpeedToggle;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentMovementState = MovementState.DirectionToggle;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentMovementState = MovementState.Reverse;
        }
        #endregion
    }

    void UpdateMovement()
    {
       // this.UpdateBasedOnState();
        this.UpdateKeyInput();

        //time corrected movement
        this.moveTranslation = new Vector3(this.direction.x, this.direction.y) * Time.deltaTime * this.speed;

        //move
        this.transform.position += new Vector3(moveTranslation.x, moveTranslation.y);
    }
}
