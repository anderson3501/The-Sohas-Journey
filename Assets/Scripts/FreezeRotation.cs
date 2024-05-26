using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
   private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found on this GameObject.");
        }
    }

    void Update()
    {
        if (rb != null)
        {
            rb.rotation = 0f; // Mantiene la rotación en 0 grados.
        }
    }
}
