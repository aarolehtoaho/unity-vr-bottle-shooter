using UnityEngine;

public class BottleHandler : MonoBehaviour
{
    private GameObject[] bottles;
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
        
    }

    private void ActivateBottle(int index)
    {
        if (index >= 0 && index < bottles.Length)
        {
            bottles[index].SetActive(true);
        }
    }

    private int GetRandomBottleIndex()
    {
        return Random.Range(0, bottles.Length);
    }
}
