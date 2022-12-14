using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject selectedWizard;
    private int selectedWizardNumber = 0;

    [SerializeField] public List<GameObject> wizards;

    // Start is called before the first frame update
    void Start()
    {
        selectedWizard = wizards[0]; // red wizard is first selected always
        selectedWizard.GetComponent<Wizard>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            selectedWizardNumber++;
            selectedWizard.GetComponent<PlayerMovement>().enabled = false;
            selectedWizard.GetComponent<Rigidbody2D>().velocity = new Vector2(0, selectedWizard.GetComponent<Rigidbody2D>().velocity.y);
            if (selectedWizardNumber > 2)
            {
                selectedWizardNumber = 0;
            }

            selectedWizard = wizards[selectedWizardNumber];
            selectedWizard.GetComponent<PlayerMovement>().enabled = true;

        }
    }
}
