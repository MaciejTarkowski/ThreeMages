using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{

    public enum WizardType
    {
        Fire,
        Wind,
        Ice
    };
    [SerializeField] public WizardType wizardType;
    //Movement
    [SerializeField] public float currentSpeed = 3f;
    [SerializeField] public float normalSpeed = 3f;
    [SerializeField] public float sprintSpeed = 4f;
    [SerializeField] public float jumpForce = 3f;

    //Magic
    [SerializeField] public GameObject castHolder;
    [SerializeField] public GameObject wetFloor;
    [SerializeField] public float glidingSpeed = 1f;
    [SerializeField] public GameObject fireballPrefab;
    [SerializeField] public float fireballSpeed = 3f;
    [SerializeField] public float nextShot = 0.15f;
    [SerializeField] public float cooldownTime = 0.5f;
    [SerializeField] public GameObject iceWall;
    [SerializeField] public GameObject iceFloor;


    [SerializeField] public float holdTimeToSprint = 2f;
    private float moveInputDownTimer = 0f;


    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight = true;
    private bool isGrounded;

    private float initialGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;
    }


    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (Input.GetAxisRaw("Vertical") > 0 && isGrounded && wizardType == WizardType.Wind)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (wizardType == WizardType.Wind)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                {
                    CastStorm();
                }
            }

            if (moveInput != 0 && isGrounded)
            {
                moveInputDownTimer += Time.deltaTime;
                if (moveInputDownTimer >= holdTimeToSprint)
                {
                    StartSprint();
                }
            }
            else
            {
                StopSprint();
            }
        }


        if (wizardType == WizardType.Fire)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextShot)
            {

                CastFireball();
                nextShot = Time.time + cooldownTime;

            }
            if (Input.GetButton("Fire2"))
            {
                StartGlide();
            }
            else
            {
                StopGlide();
            }
        }

        if (wizardType == WizardType.Ice)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Destroy(GameObject.FindGameObjectWithTag("IceWall"));
                SpawnIceWall();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                SpawnIceFloor();
            }
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "WetFloor")
        {
            Debug.Log("You are on wet floor!s");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    void StartSprint()
    {
        if (wizardType == WizardType.Wind)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            currentSpeed = sprintSpeed;
        }
    }
    void StopSprint()
    {
        moveInputDownTimer = 0f;
        if (wizardType == WizardType.Wind)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            currentSpeed = normalSpeed;
        }
    }

    void CastStorm()
    {

        RaycastHit2D hit = Physics2D.Raycast(castHolder.transform.position, Vector2.down, 20f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {

            Instantiate(wetFloor, hit.point, Quaternion.identity);
        }
    }

    void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, castHolder.transform.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(facingRight ? fireballSpeed : -fireballSpeed, 0);
    }
    private void StartGlide()
    {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, -glidingSpeed);
    }
    private void StopGlide()
    {
        rb.gravityScale = initialGravityScale;
    }

    private void SpawnIceWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(castHolder.transform.position, Vector2.down, 20f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            Instantiate(iceWall, hit.point, Quaternion.identity);
        }
    }
    private void SpawnIceFloor()
    {
        RaycastHit2D hit = Physics2D.Raycast(castHolder.transform.position, Vector2.down, 20f);
        if (hit.collider != null && hit.collider.tag == "WetFloor")
        {
            Destroy(hit.collider.gameObject);
            Instantiate(iceFloor, hit.collider.gameObject.transform.position, Quaternion.identity);
        }
    }

}
