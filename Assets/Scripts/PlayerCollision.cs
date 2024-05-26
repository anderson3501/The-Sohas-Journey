using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
      private void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisiona con una pared, detén el movimiento
        if (other.CompareTag("SideWall"))
        {
            // Puedes agregar aquí cualquier otra lógica que necesites, como reproducir un sonido de golpe o detener una animación
            Debug.Log("¡Chocó con una pared!");
            // Aquí puedes detener el movimiento del jugador o tomar otras acciones según tus necesidades
        }
    }
}
