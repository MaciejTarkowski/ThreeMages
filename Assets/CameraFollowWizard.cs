using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWizard : MonoBehaviour
{

    private Transform player;
    public Vector3 offset;
    [SerializeField] public float damping = 0.5f;
    [SerializeField] GameController gameController;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offset.z = -10f;
    }




    void FixedUpdate()
    {
        player = gameController.selectedWizard.transform;
        Vector3 movePosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);

    }




}
