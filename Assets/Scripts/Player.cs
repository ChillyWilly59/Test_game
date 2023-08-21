using Spine;
using Spine.Unity;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public LayerMask enemyLayer;
    public SkeletonAnimation skeletonAnimation;
    
    public GameObject gameOverUI;
    public GameObject winUI;

    private bool isDead = false;
    private bool isShooting = false;
    public bool IsDead { get;}
    
    private void Start()
    {
        PlayRunAnimation();
    }
    private void Update()
    {
        if (!isDead)
        {
            CheckForEnemies();
        }
    }
    private void CheckForEnemies()
    {
        if (Input.GetMouseButtonDown(0)&& !isShooting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Нажал на мышь");
            if (Physics.Raycast(ray,out hit, Mathf.Infinity,enemyLayer))
            {
                Debug.Log("Выстрел");
                isShooting = true;
                skeletonAnimation.AnimationState.SetAnimation(0, "shoot", false);
                skeletonAnimation.AnimationState.Complete += OnShootAnimationComplete;
                Destroy(hit.rigidbody.gameObject);
            }
        }

        isShooting = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Враг задел игрока");
            skeletonAnimation.AnimationState.SetAnimation(0, "loose", false);
            GameOver();
        }
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Победа");
            Time.timeScale = 0f;
            WinGame();
        }
    }
    public void PlayRunAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "run", true);
    }
    private void OnShootAnimationComplete(TrackEntry trackEntry)
    {
        isShooting = false;
        PlayRunAnimation();
        skeletonAnimation.AnimationState.Complete -= OnShootAnimationComplete;
    }
    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void WinGame()
    {
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }
}