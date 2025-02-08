using System;
using System.Collections;
using UnityEngine;

public class DestructionTimer : MonoBehaviour
{
    public event Action TimeUntilDestructionExpired;

    public void ActivateDestruction()
    {
        const float MinTimeToDestroy = 4f;
        const float MaxTimeToDestroy = 9f;

        float timeToDestroy = UnityEngine.Random.Range(MinTimeToDestroy, MaxTimeToDestroy);

        StartCoroutine(CountDownToSelfDestruction(timeToDestroy));
    }

    private IEnumerator CountDownToSelfDestruction(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);

        TimeUntilDestructionExpired();
    }
}
