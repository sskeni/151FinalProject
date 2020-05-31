using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float fov = 80f;
    [SerializeField] private float reachDistance = 100f;
    [SerializeField] private float grappleSpeed = 500f;
    [SerializeField] private float moveSpeed = 500f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private Camera cam;

    private float rotationX;
    private float rotationY;
    private float xLock = 90f;

    private bool grappled;
    private bool swinging;

    private Vector3 grappleTarget;
    private Vector3 swingTarget;

    private ConfigurableJoint joint;
    private Rigidbody body;
    private GameObject grappleRope;
    private GameObject swingRope;

    void Awake()
    {
        cam.fieldOfView = fov;

        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;

        grappleRope = new GameObject();
        grappleRope.name = "Grapple Rope";
        grappleRope.AddComponent<LineRenderer>();
        grappleRope.GetComponent<LineRenderer>().startWidth = 0.05f;
        grappleRope.GetComponent<LineRenderer>().endWidth = 0.05f;
        grappleRope.GetComponent<LineRenderer>().positionCount = 2;
        grappleRope.GetComponent<LineRenderer>().material.color = Color.white;

        swingRope = new GameObject();
        swingRope.name = "Swing Rope";
        swingRope.AddComponent<LineRenderer>();
        swingRope.GetComponent<LineRenderer>().startWidth = 0.05f;
        swingRope.GetComponent<LineRenderer>().endWidth = 0.05f;
        swingRope.GetComponent<LineRenderer>().positionCount = 2;
        swingRope.GetComponent<LineRenderer>().material.color = Color.white;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        DrawRopes();
        Rotate();
        Grapple();
        Swing();
        /*if(!grappled && !swinging)
        {
            Move();
            Jump();
        }*/
        ApplyMoreGravity();
    }

    private void Rotate()
    {
        cam.transform.position = transform.position;

        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX = Mathf.Clamp(rotationX, -xLock, xLock);

        //transform.localEulerAngles = new Vector3(0, rotationY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Move()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity *= moveSpeed;
        Quaternion camRot = cam.transform.rotation;
        camRot.x = 0;
        targetVelocity = camRot * targetVelocity;
        Debug.Log(targetVelocity);
        body.velocity = new Vector3(targetVelocity.x * Time.deltaTime, body.velocity.y, targetVelocity.z * Time.deltaTime);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
        }
    }

    private void Grapple()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, reachDistance))
            {
                grappleTarget = hit.point;
                grappled = true;
            }
        }

        if(grappled)
        {
            if(Input.GetMouseButtonUp(0))
            {
                grappled = false;
            }

            Vector3 moveDirection = grappleTarget - transform.position;
            if(body.velocity.magnitude > 0.1f && Vector3.Angle(body.velocity, moveDirection) > 10f)
            {
                body.velocity = Vector3.RotateTowards(body.velocity, moveDirection, 0.02f, 0);
            }

            if(Input.GetMouseButton(0))
            {
                moveDirection = moveDirection.normalized;
                moveDirection *= grappleSpeed;
                body.AddForce(moveDirection * Time.deltaTime);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && body.velocity.magnitude > 15f)
        {
            body.velocity *= 0.9f * Time.deltaTime;
        }
    }

    private void Swing()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, reachDistance))
            {
                body.freezeRotation = false;
                swingTarget = hit.point;
                swinging = true;

                joint = gameObject.AddComponent<ConfigurableJoint>();
                joint.anchor = transform.InverseTransformPoint(swingTarget);
                joint.xMotion = ConfigurableJointMotion.Locked;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                joint.axis = new Vector3(1f, 1f, 1f);
            }
        }

        if(swinging)
        {
            if (Input.GetMouseButtonUp(1))
            {
                Destroy(joint);
                swinging = false;
                body.freezeRotation = true;
            }
        }
    }

    private void DrawRopes()
    {
        if(grappled)
        {
            grappleRope.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            grappleRope.GetComponent<LineRenderer>().enabled = false;
        }
        grappleRope.GetComponent<LineRenderer>().SetPosition(0, transform.position + new Vector3(0f, 0.25f, 0f));
        grappleRope.GetComponent<LineRenderer>().SetPosition(1, grappleTarget);

        if (swinging)
        {
            swingRope.GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            swingRope.GetComponent<LineRenderer>().enabled = false;
        }
        swingRope.GetComponent<LineRenderer>().SetPosition(0, transform.position + new Vector3(0f, 0.25f, 0f));
        swingRope.GetComponent<LineRenderer>().SetPosition(1, swingTarget);
    }

    private void ApplyMoreGravity()
    {
        if(body.velocity.y < 0)
        {
            body.AddForce(Physics.gravity * (fallMultiplier - 1) * Time.deltaTime);
        }
    }
}
