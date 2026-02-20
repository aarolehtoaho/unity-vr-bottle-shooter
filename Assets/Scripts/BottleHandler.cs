using UnityEngine;
using TMPro;

public class BottleHandler : MonoBehaviour
{
    public TMP_Text ScoreText;
    public bool BottleHit = false;
    public Vector3 HitDirection = Vector3.zero;
    public AudioClip hitSound;

    private GameObject[] bottles;
    private GameObject brokenBottle;
    private Vector3 brokenBottlePositionOffset = new Vector3(0, 0.5f, 0);
    private Quaternion brokenBottleRotation = Quaternion.Euler(90, 0, 0);
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
        brokenBottle = transform.Find("BrokenBottle").gameObject;

        ActivateBottle(GetRandomBottleIndex());
    }

    // Update is called once per frame
    void Update()
    {
        if (BottleHit)
        {
            IncreaseScore();
            SpawnBrokenBottle();
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
        int currentScore = int.Parse(ScoreText.text);
        currentScore += 5;
        ScoreText.text = currentScore.ToString();
    }

    private void SpawnBrokenBottle()
    {
        GameObject brokenBottleInstance = Instantiate(brokenBottle, bottles[currentBottleIndex].transform.position + brokenBottlePositionOffset, brokenBottleRotation);
        brokenBottleInstance.SetActive(true);

        Rigidbody rb = brokenBottleInstance.GetComponent<Rigidbody>();
        rb?.AddForce(HitDirection * 5f, ForceMode.Impulse);

        AudioSource hitAudioSource = brokenBottleInstance.GetComponent<AudioSource>();
        if (hitAudioSource != null && hitSound != null)
        {
            hitAudioSource.PlayOneShot(hitSound);
        }

        Destroy(brokenBottleInstance, 2f);
    }
}
