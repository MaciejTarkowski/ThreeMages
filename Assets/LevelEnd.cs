using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private int wizardCounter = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wizard")
        {
            wizardCounter += 1;
        }
        if (wizardCounter == 3)
            Debug.Log("Level finished");

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wizard")
        {
            wizardCounter -= 1;
        }
    }

}
