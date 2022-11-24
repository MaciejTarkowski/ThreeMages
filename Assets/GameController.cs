using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private GameObject selectedWizard;
    private int selectedWizardNumber = 0;

    [SerializeField] public List<GameObject> wizards;

    // Start is called before the first frame update
    void Start()
    {
        selectedWizard = wizards[0]; // red wizard is first selected always
        selectedWizard.GetComponent<PlayerMovement>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            selectedWizardNumber++;
            selectedWizard.GetComponent<PlayerMovement>().enabled = false;
            if (selectedWizardNumber > 2)
            {
                selectedWizardNumber = 0;
            }

            selectedWizard = wizards[selectedWizardNumber];
            selectedWizard.GetComponent<PlayerMovement>().enabled = true;

        }
    }
}
