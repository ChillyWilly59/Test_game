using Spine.Unity;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Player player;
    public SkeletonAnimation skeletonAnimation;

    private void Start()
    {
        PlayRunAnimation();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Враг задел игрока");
            PlayAngryAnimation();
            //Time.timeScale = 0f;
        }
    }
    private void MoveTowardsPlayer()
    {
        if (!player.IsDead)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
    public void PlayRunAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "run", true);
    }
    public void PlayAngryAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "angry", false);
    }
}
