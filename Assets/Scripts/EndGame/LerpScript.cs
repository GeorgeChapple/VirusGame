using System.Collections;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class LerpScript : MonoBehaviour
{
    [HideInInspector] public Vector3 lerpVector;
    [HideInInspector] public int activeLerps;

    public Easing.eases ease;
    private Easing.EasingDelegate easing;

    private void Awake() {
        easing = Easing.GetEase(ease);
    }

    public void StartVector3Lerp(Vector3 startVal, Vector3 endVal, float timeToTake) {
        StartCoroutine(LerpVector3(startVal, endVal, timeToTake));
    }

    private IEnumerator LerpVector3(Vector3 startVal, Vector3 endVal, float timeToTake) {
        activeLerps++;
        float time = 0;
        float timeElapsed = 0;
        float perc = 0;
        while (time < 1) {
            perc = easing(time);
            lerpVector = Vector3.Lerp(startVal, endVal, perc);
            timeElapsed += Time.deltaTime;
            time = timeElapsed / timeToTake;
            yield return null;
        }
        activeLerps--;
    }
}
