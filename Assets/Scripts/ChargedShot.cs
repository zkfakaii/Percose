using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedShot : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Interrumpe el movimiento del enemigo
            EnemyRandomMovement enemyMovement = other.GetComponent<EnemyRandomMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.InterruptMovement(); // Implementa esta función en tu script EnemyRandomMovement
            }
        }
    }
}
