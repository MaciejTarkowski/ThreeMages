using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private List<string> safeTags = new List<string>() { "Wizard", "Fireball" };

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!safeTags.Contains(other.gameObject.tag))
            Destroy(gameObject);
    }
}
