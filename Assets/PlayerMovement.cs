using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public CharacterController2D controller;
    [SerializeField] Wizard wizardScript;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool glide = false;
    public float runSpeed = 40f;
    public float sprintSpeed = 50f;
    private float moveInputDownTimer = 0f;
    [SerializeField] public float holdTimeToSprint = 2f;
    private float initialSpeed;


    private void Start()
    {
        initialSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove != 0 && wizardScript.wizardType == Wizard.WizardType.Wind)
        {
            moveInputDownTimer += Time.deltaTime;
            if (moveInputDownTimer >= holdTimeToSprint)
            {
                runSpeed = sprintSpeed;
            }
        }
        else
        {
            moveInputDownTimer = 0f;
            runSpeed = initialSpeed;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && wizardScript.wizardType == Wizard.WizardType.Wind)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            wizardScript.CastFirstSpell();

        }
        if (Input.GetButtonDown("Fire2"))
        {
            wizardScript.CastSecondSpell();
        }

        if (Input.GetButton("Fire2") && wizardScript.wizardType == Wizard.WizardType.Fire)
        {
            glide = true;
        }
        else
        {
            glide = false;
        }





    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed, false, jump, glide);
        jump = false;


    }
}
