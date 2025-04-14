using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CarMovement : MonoBehaviour
{
    [Header("Car Stats")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 0.01f;

    [Header("Transforms")]
    public List<Transform> stopLocations = new List<Transform>();
    public List<Transform> targetLocations = new List<Transform>();

    public enum targets { Left, Right, Up, Down };
    [Header("Enums")]
    public targets spawnLocation = targets.Left;
    public targets target = targets.Left;

    private bool reachedLocation = false;
    public void DriveToTarget()
    {
        StartCoroutine(ToTarget());
    }
    private IEnumerator ToTarget()
    {
        reachedLocation = false;
        while (!reachedLocation)
        {
            switch (target)
            {
                case targets.Left:
                    transform.position = Vector3.MoveTowards(transform.position, targetLocations[0].position, speed * Time.deltaTime);
                    Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetLocations[0].position - pos), rotationSpeed);
                    if (Vector3.Distance(transform.position, targetLocations[0].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Right:
                    transform.position = Vector3.MoveTowards(transform.position, targetLocations[1].position, speed * Time.deltaTime);
                    pos = new Vector3(transform.position.x, 0, transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetLocations[1].position - pos), rotationSpeed);
                    if (Vector3.Distance(transform.position, targetLocations[1].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Up:
                    transform.position = Vector3.MoveTowards(transform.position, targetLocations[2].position, speed * Time.deltaTime);
                    pos = new Vector3(transform.position.x, 0, transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetLocations[2].position - pos), rotationSpeed);
                    if (Vector3.Distance(transform.position, targetLocations[2].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Down:
                    transform.position = Vector3.MoveTowards(transform.position, targetLocations[3].position, speed * Time.deltaTime);
                    pos = new Vector3(transform.position.x, 0, transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetLocations[3].position - pos), rotationSpeed);
                    if (Vector3.Distance(transform.position, targetLocations[3].position) <= 0.5f) reachedLocation = true;
                    break;
            }

            yield return null;
        }
    }
    public IEnumerator DriveToStopLocation()
    {
        reachedLocation = false;
        while (!reachedLocation)
        {
            switch (spawnLocation)
            {
                case targets.Left:
                    transform.position = Vector3.MoveTowards(transform.position, stopLocations[0].position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, stopLocations[0].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Right:
                    transform.position = Vector3.MoveTowards(transform.position, stopLocations[1].position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, stopLocations[1].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Up:
                    transform.position = Vector3.MoveTowards(transform.position, stopLocations[2].position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, stopLocations[2].position) <= 0.5f) reachedLocation = true;
                    break;
                case targets.Down:
                    transform.position = Vector3.MoveTowards(transform.position, stopLocations[3].position, speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, stopLocations[3].position) <= 0.5f) reachedLocation = true;
                    break;
            }

            yield return null;
        }
    }
}
