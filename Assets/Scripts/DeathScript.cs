using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class DeathScript : MonoBehaviour
{
    [SerializeField]
    private float deathTime;
    [SerializeField]
    [Tooltip("R G B")]
    private Vector3 startColour;
    [SerializeField]
    [Tooltip("R G B")]
    private Vector3 endColour;
    private Image deathImage;
    private LerpScript lerpScript;
    private SceneSwapper sceneSwapper;

    public bool dead;

    private void Awake() {
        lerpScript = GetComponent<LerpScript>();
        sceneSwapper = GetComponent<SceneSwapper>();
        deathImage = transform.GetChild(3).GetChild(0).GetComponent<Image>();
        deathImage.color = new Color(startColour.x, startColour.y, startColour.z, 1f);
    }

    private void Update() {
        if (dead) {
            dead = false;
            KillPlayer();
        }
    }

    // Enables and lerps the colour of an image over the whole screen, then resets the scene
    public void KillPlayer() {
        GetComponent<SoundScript>().PlaySound( 0, 1, 1);
        deathImage.gameObject.SetActive(true);
        lerpScript.StartVector3Lerp(startColour, endColour, deathTime);
        StartCoroutine(WaitForReset());
    }

    private IEnumerator WaitForReset() {
        float time = 0;
        Vector3 colourVector;
        while (time < deathTime + 2f) {
            colourVector = lerpScript.lerpVector;
            deathImage.color = new Color(colourVector.x, colourVector.y, colourVector.z, 1f);
            time += Time.deltaTime;
            yield return null;
        }
        sceneSwapper.ResetScene();
    }
}
