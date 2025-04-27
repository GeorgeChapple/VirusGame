using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaisyScript : MonoBehaviour
{
    [Serializable]
    public struct Animation {
        public string name;
        public float duration;
    }


    [HideInInspector] public bool chatBoxActive = false;

    [SerializeField] private float idleTimeMin = 10;
    [SerializeField] private float idleTimeMax = 20;

    private Animator animator;
    public List<Animation> animations = new List<Animation>();
    


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(IdleAnimation());
    }

    public void UpdateAnimator(string animation) {
        animator.SetTrigger(animation);
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
}
