using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class SpawnCars : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private CarMovement player1;
    [SerializeField] private CarMovement player2;

    [Header("SpawnLocations")]
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();
    private void Start()
    {
        SpawnPlayerCars();
    }
    public void SpawnPlayerCars()
    {
        StopAllCoroutines();
        player1.StopAllCoroutines();
        player2.StopAllCoroutines();

        for (int i = InputHandler.Instance.mCoroutines.Count - 1; i >= 0; i--)
        {
            Coroutine coroutine = InputHandler.Instance.mCoroutines[i];
            InputHandler.Instance.StopCoroutine(coroutine);
            InputHandler.Instance.mCoroutines.Remove(coroutine);
        }

        List<Transform> spawnLocations2 = new List<Transform>();
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            spawnLocations2.Add(spawnLocations[i]);
        }

        // Player 1
        int rand = Random.Range(0, spawnLocations2.Count);
        player1.transform.position = spawnLocations2[rand].position;
        player1.transform.rotation = spawnLocations2[rand].rotation;
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (spawnLocations2[rand] == spawnLocations[i])
            {
                switch(i)
                {
                    case 0:
                        player1.spawnLocation = CarMovement.targets.Left;
                        break;
                    case 1:
                        player1.spawnLocation = CarMovement.targets.Right;
                        break;
                    case 2:
                        player1.spawnLocation = CarMovement.targets.Up;
                        break;
                    case 3:
                        player1.spawnLocation = CarMovement.targets.Down;
                        break;
                }
            }
        }
        StartCoroutine(player1.DriveToStopLocation());
        spawnLocations2.RemoveAt(rand);

        // Player 2
        rand = Random.Range(0, spawnLocations2.Count);
        player2.transform.position = spawnLocations2[rand].position;
        player2.transform.rotation = spawnLocations2[rand].rotation;
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            if (spawnLocations2[rand] == spawnLocations[i])
            {
                switch (i)
                {
                    case 0:
                        player2.spawnLocation = CarMovement.targets.Left;
                        break;
                    case 1:
                        player2.spawnLocation = CarMovement.targets.Right;
                        break;
                    case 2:
                        player2.spawnLocation = CarMovement.targets.Up;
                        break;
                    case 3:
                        player2.spawnLocation = CarMovement.targets.Down;
                        break;
                }
            }
        }
        StartCoroutine(player2.DriveToStopLocation());
        spawnLocations2.RemoveAt(rand);
    }
}
