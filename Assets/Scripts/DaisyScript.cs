using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class DaisyScript : MonoBehaviour
{
    [Serializable]
    public struct Animation {
        public string name;
        public float duration;
    }


    [HideInInspector] public bool chatBoxActive = false;
     public bool daisyActive = false;

    [SerializeField] private float idleTimeMin = 10;
    [SerializeField] private float idleTimeMax = 20;
    [SerializeField] private float spookyTimeMin = 10;
    [SerializeField] private float spookyTimeMax = 20;
    [SerializeField] private string[] searchPrompts;
    [SerializeField] private BoxCollider col;

    private SoundScript sounds;
    private Animator animator;
    public List<Animation> animations = new List<Animation>();

    private void Awake() {
        sounds = GetComponent<SoundScript>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameEventsManager gem = FindFirstObjectByType<GameEventsManager>();
        if ((gem.dialogueIndex >= 2 && gem.dialogueIndex < 7) || gem.dialogueIndex >= 9) {
            daisyActive = true;
            if (gem.dialogueIndex >= 9){
                transform.position = Vector3.zero + new Vector3(9999999, 0, 0);
            } else {
                transform.position = Vector3.zero;
            }
        }
        StartCoroutine(IdleAnimation());
        if (gem.dialogueIndex < 7) {
            StartCoroutine(DoSomethingSpooky());
        }
    }

    public void UpdateAnimator(string animation) {
        animator.SetTrigger(animation);
        col.size = new Vector3(2, 2, 1);
        if (animation == "HAPPY") {
            sounds.PlaySound(0, 0.95f, 1.05f);
        } else if (animation == "ANGRY") {
            sounds.PlaySound(1, 0.95f, 1.05f);
        }
    }

    private IEnumerator IdleAnimation() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(idleTimeMin, idleTimeMax));
        if (!chatBoxActive) {
            int animationIndex = UnityEngine.Random.Range(1, 3);
            UpdateAnimator(animations[animationIndex].name);
            yield return new WaitForSeconds(animations[animationIndex].duration);
            UpdateAnimator(animations[0].name);
        }
        StartCoroutine(IdleAnimation());
    }

    private IEnumerator DoSomethingSpooky() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(spookyTimeMin, spookyTimeMax));
        if (!chatBoxActive && daisyActive) {
            int chooseScare = UnityEngine.Random.Range(0, 4);
            switch (chooseScare) {
                case 0:
                    if (searchPrompts.Length > 0) {
                        int searchIndex = UnityEngine.Random.Range(0, searchPrompts.Length);
                        Commands.Search(searchPrompts[searchIndex]);
                    }
                    sounds.PlaySound(2, 0.95f, 1.05f);
                    break;
                case 1:
                    Commands.FlashCMD(UnityEngine.Random.Range(10, 25));
                    sounds.PlaySound(3, 0.95f, 1.05f);
                    break;
                case 2:
                    Commands.ShowIP();
                    sounds.PlaySound(4, 0.95f, 1.05f);
                    break;
                case 3:
                    Commands.ShowDir();
                    sounds.PlaySound(5, 0.95f, 1.05f);
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(DoSomethingSpooky());
    }
}
