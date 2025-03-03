using DG.Tweening;
using UnityEngine;

public class PickUpAnimation : MonoBehaviour
{
    [SerializeField] private float moveUpDistance = 1f;
    [SerializeField] private float moveUpDuration = 0.3f;
    [SerializeField] private float moveToPlayerDuration = 0.5f;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void OnPickup()
    {
        // Move up animation
        transform.DOMoveY(transform.position.y + moveUpDistance, moveUpDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Move toward player
                transform.DOMove(playerTransform.position, moveToPlayerDuration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        // Perform any additional actions (e.g., add to inventory)
                        AddToPool();
                        
                       
                    });
            });
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
    private void AddToPool()
    {
        gameObject.SetActive(false);

        // Only add to pool if it's not already in it
        if (!FoodObjectPooler.instance.deactivateFoodObj.Contains(gameObject))
        {
            FoodObjectPooler.instance.deactivateFoodObj.Add(gameObject);
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && transform.position.y <= -2.5f)
        {
            AddToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
           OnPickup();
        }
    }
}
