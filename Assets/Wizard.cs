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
    }
    [SerializeField] public WizardType wizardType;

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


    private void CastStorm()
    {

        RaycastHit2D hit = Physics2D.Raycast(castHolder.transform.position, Vector2.down, 20f);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            Destroy(GameObject.FindGameObjectWithTag("WetFloor"));

            Instantiate(wetFloor, hit.point, Quaternion.identity);
        }
    }

    private void CastFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, castHolder.transform.position, castHolder.transform.rotation);
    }


    private void SpawnIceWall()
    {
        Destroy(GameObject.FindGameObjectWithTag("IceWall"));
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

    public void CastFirstSpell()
    {
        switch (wizardType)
        {
            case WizardType.Fire:
                if (Time.time > nextShot)
                {
                    CastFireball();
                    nextShot = Time.time + cooldownTime;
                }
                break;
            case WizardType.Wind:
                CastStorm(); break;
            case WizardType.Ice:
                SpawnIceWall();
                break;

        }

    }
    public void CastSecondSpell()
    {
        switch (wizardType)
        {
            case WizardType.Ice:
                SpawnIceFloor();
                break;
        }

    }

}
