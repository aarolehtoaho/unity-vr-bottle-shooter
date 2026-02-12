using UnityEngine;

public class BottleHandler : MonoBehaviour
{
    public bool BottleHit = false;

    private GameObject[] bottles;
    private int currentBottleIndex = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bottles = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            bottles[i] = transform.Find("Bottle" + (i + 1)).gameObject;
            bottles[i].SetActive(false);
        }

        ActivateBottle(GetRandomBottleIndex());
    }

    // Update is called once per frame
    void Update()
    {
        if (BottleHit)
        {
            IncreaseScore();
            BottleHit = false;

            int randomIndex = GetRandomBottleIndex();
            if (randomIndex == currentBottleIndex)
            {
                randomIndex = (randomIndex + 1) % bottles.Length;
            }
            ActivateBottle(randomIndex);
        }
    }

    private void ActivateBottle(int index)
    {
        if (index >= 0 && index < bottles.Length)
        {
            bottles[index].SetActive(true);
            currentBottleIndex = index;
        }
    }

    private int GetRandomBottleIndex()
    {
        return Random.Range(0, bottles.Length);
    }

    private void IncreaseScore()
    {
        // TODO
    }
}
