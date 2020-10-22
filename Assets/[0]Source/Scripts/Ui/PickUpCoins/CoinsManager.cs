using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    #region Singleton:CoinsManager

    public static CoinsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    //References
    [Header ("UI references")]
    [SerializeField] TMP_Text coinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header ("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins = 0;
    Queue<GameObject> coinsQueue = new Queue<GameObject> ();


    [Space]
    [Header ("Animation settings")]
    [SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread = 0;

    Vector3 targetPosition;


    

    void Start ()
    {
        targetPosition = target.position;

        //prepare pool
        PrepareCoins ();
    }

    void PrepareCoins ()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++) {
            coin = Instantiate (animatedCoinPrefab);
            coin.transform.SetParent(transform);
            coin.SetActive (false);
            coinsQueue.Enqueue (coin);
        }
    }

    void Animate (Vector3 collectedCoinPosition, int amount)
    {
        for (int i = 0; i < amount; i++) {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0) {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue ();
                coin.SetActive (true);

                //move coin to the collected coin pos
                coin.GetComponent<RectTransform>().position = collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f);
                //coin.GetComponent<RectTransform>().localScale = new Vector3(100,100,0);

                //animate coin to target position
                float duration = Random.Range (minAnimDuration, maxAnimDuration);
                coin.transform.DOMove (targetPosition, duration)
                    .SetEase (easeType)
                    .OnComplete (() => {
                        //executes whenever coin reach target position
                        coin.SetActive (false);
                        coinsQueue.Enqueue (coin);

                        Game.Instance.Coins++;
                    });
            }
        }
    }

    public void AddCoins (Vector3 collectedCoinPosition, int amount)
    {
        Vector2 ViewSpacePos = Camera.main.WorldToScreenPoint(collectedCoinPosition);
        Animate (ViewSpacePos, amount);
    }
}
