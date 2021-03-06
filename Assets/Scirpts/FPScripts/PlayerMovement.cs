﻿using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Interactable focus;
    Camera cam;

    public float speed = 12f;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    //检测是否触碰地面
    bool isGrounded;

    //用于显示提示对话框
    public Animator animator;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //Basic Movement include Jump & Move

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Left Click to interactable
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag == "Items")
            {
                //显示提示框文字
                animator.SetBool("IsShowTip", true);
                if (Input.GetMouseButtonDown(0))
                {
                // check if we hit an interactable
                // if we did set it as our focus
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
                //Right Click to cancel
                if (Input.GetMouseButtonDown(1))
                {
                    RemoveFocus();
                }
            }
            else
            {
                //隐藏提示框文字
                animator.SetBool("IsShowTip", false);
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }
    void RemoveFocus()
    {
        if (focus != null)
                focus.OnDefocused();

        focus = null;
    }
}
