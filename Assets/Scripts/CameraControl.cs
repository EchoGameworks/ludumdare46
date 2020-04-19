using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CameraControl instance;
    public StageManager stageManager;

    public Camera cam;
    public Transform cameraTransform;

    public Transform followTransform;

    public bool AllowRotation = false;
    public bool AllowZoom = true;
    public bool AllowDragMove = true;
    public bool AllowEdgePan = true;

    public float fastSpeed;
    public float normalSpeed;
    public float movementSpeed;
    public float movementTime;

    public float rotationAmount;

    public float zoomAmountMouseMultiplier;
    public float rotateMouseSensitivity;
    public float rotateKeyboardSensitivity;
    public Vector3 zoomAmount;
    public bool invertDrag;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 rotateStartPosition;
    private Vector3 rotateCurrentPosition;
    private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;
    public float DragThresholdSensitivity = 1f;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }


    void Update()
    {
        if(followTransform == null)
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    void HandleMouseInput()
    {
        if (AllowZoom) HandleZoom();
        if (AllowRotation) HandleRotation();
        if (AllowDragMove) HandleMove();        
    }

    void HandleMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
                dragCurrentPosition = dragStartPosition;
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                if (invertDrag)
                {
                    newPosition = transform.position - dragStartPosition + dragCurrentPosition;
                }
                else
                {
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }

            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            float dragDist = Vector3.SqrMagnitude(dragStartPosition - dragCurrentPosition);
            if(dragDist < DragThresholdSensitivity)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    Vector3 moveLocation = ray.GetPoint(entry);
                    stageManager.Move(moveLocation);
                }
            }
            
        }
    }

    void HandleZoom()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount * zoomAmountMouseMultiplier;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            Vector3 diff = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.forward * (-diff.x / rotateMouseSensitivity));
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * rotationAmount * rotateKeyboardSensitivity);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount * rotateKeyboardSensitivity);
        //}

        if (Input.GetKey(KeyCode.Q))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.E))
        {
            newZoom -= zoomAmount;
        }

        Vector3 finalPosition = new Vector3(Mathf.Clamp(newPosition.x, -65, 220f), Mathf.Clamp(newPosition.y, -400f, 808f), Mathf.Clamp(newPosition.z, -320f, 650f));
        newPosition = finalPosition;
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);

        Vector3 finalZoom = new Vector3(newZoom.x, Mathf.Clamp(newZoom.y, -40f, 80f), newZoom.z);
        newZoom.y = finalZoom.y;
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, finalZoom, Time.deltaTime * movementTime);
    }
}
