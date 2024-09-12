using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    private Game2Manager game2Manager;

    public List<Quaternion> rotations;

    [SerializeField] private Quaternion startRot;
    [SerializeField] private float duration = 3;

    private Quaternion nextRot;
    private Quaternion newStartRot;
    
    void Awake()
    {
        game2Manager = FindObjectOfType<Game2Manager>();
        transform.rotation = startRot;
    }

    public void GetNextRotByRandom()
    {
        if (rotations.Count != 0)
        {
            newStartRot = transform.rotation;
            nextRot = rotations[Random.Range(0, rotations.Count)];
            rotations.Remove(nextRot);
            StartCoroutine(rotateSunOverDuration());
        }
        else
        {
            game2Manager.OnGameEnd();
        }
    }

    private IEnumerator rotateSunOverDuration()
    {
        yield return new WaitForSeconds(3);
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.rotation = Quaternion.Lerp(newStartRot, nextRot, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = nextRot;
        game2Manager.OnSunDestinationReached();
    }
}
