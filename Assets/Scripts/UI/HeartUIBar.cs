using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUIBar : MonoBehaviour
{
    public GameObject heartPrefab;

    List<HeartUI> hearts = new List<HeartUI>();

    private void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
    }
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = StatsManager.Instance.maxHealth % 4;
        int heartsToMake = (int)(StatsManager.Instance.maxHealth / 4 + maxHealthRemainder);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();

        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = Mathf.Clamp(StatsManager.Instance.currentHealth - (i * 4), 0, 4);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }

    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1, 1, 1);
        HeartUI heartComponent = newHeart.GetComponent<HeartUI>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }


    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartUI>();
    }

}
