using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Definir una estructura para almacenar los enemigos y sus pesos
    [System.Serializable]
    public struct EnemyWeight
    {
        public GameObject enemyPrefab;
        public float weight;
    }

    public List<EnemyWeight> enemyWeights = new List<EnemyWeight>();

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();

    // Método para agregar enemigos a la cola según sus pesos
    public void AddEnemyToQueue()
    {
        // Limpiar la cola antes de agregar nuevos enemigos
        enemyQueue.Clear();

        // Agregar enemigos a la cola según sus pesos
        foreach (var enemyWeight in enemyWeights)
        {
            for (int i = 0; i < Mathf.RoundToInt(enemyWeight.weight); i++)
            {
                enemyQueue.Enqueue(enemyWeight.enemyPrefab);
            }
        }

        // Mezclar la cola para generar una aparición aleatoria de enemigos
        ShuffleQueue();
    }

    // Método para mezclar aleatoriamente los elementos de la cola
    private void ShuffleQueue()
    {
        List<GameObject> tempList = new List<GameObject>(enemyQueue);
        enemyQueue.Clear();

        while (tempList.Count > 0)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            enemyQueue.Enqueue(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }
    }

    // Método para generar y hacer aparecer un enemigo en la habitación
    public void SpawnEnemy()
    {
        if (enemyQueue.Count > 0)
        {
            GameObject enemyPrefab = enemyQueue.Dequeue();
            // Generar una posición aleatoria dentro de la habitación
            Vector3 spawnPosition = GetRandomSpawnPosition();
            // Instanciar el enemigo en la posición generada
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Método para obtener una posición aleatoria dentro de la habitación
private Vector3 GetRandomSpawnPosition()
{
    // Obtener una lista de todos los puntos disponibles en la habitación
    List<Vector2> availablePoints = GetComponentInParent<GridController>().availablePoints;

    // Seleccionar aleatoriamente un punto de la lista de puntos disponibles
    Vector2 randomPoint = availablePoints[Random.Range(0, availablePoints.Count)];

    // Convertir el punto seleccionado a una posición en el espacio 3D
    return new Vector3(randomPoint.x, randomPoint.y, 0f);
}
}