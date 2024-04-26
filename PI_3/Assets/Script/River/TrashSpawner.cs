using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashPrefab; // Prefab do lixo a ser spawnado
    public Transform spawnPoint; // Ponto de spawn do lixo
    public float spawnInterval = 2f; // Intervalo de spawn em segundos
    public float spawnDistance = 10f; // Dist�ncia de spawn em rela��o � c�mera
    public float spawnOffset = 1f; // Dist�ncia de offset em rela��o � c�mera

    private float nextSpawnTime; // Pr�ximo tempo de spawn

    void Start()
    {
        // Define o pr�ximo tempo de spawn
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        // Verifica se � hora de spawnar um novo lixo
        if (Time.time >= nextSpawnTime)
        {
            SpawnTrash();
            nextSpawnTime = Time.time + spawnInterval; // Atualiza o pr�ximo tempo de spawn
        }
    }

    // M�todo para spawnar o lixo
    void SpawnTrash()
    {
        // Calcula uma posi��o aleat�ria na dire��o oposta � c�mera
        Vector3 spawnDirection = -Camera.main.transform.forward;
        Vector3 spawnPosition = Camera.main.transform.position + spawnDirection * spawnDistance + spawnDirection * spawnOffset;

        // Spawn do lixo
        Instantiate(trashPrefab, spawnPosition, Quaternion.identity);
    }
}
