using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 20;
    private int lifeTime = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(90, 0, 0, Space.Self);
        transform.Translate(Vector3.up * 0.1f, Space.Self);
        transform.Translate(Vector3.back * 0.07f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    private void OnEnable()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        bool isBottle = other.gameObject.CompareTag("Bottle");
        if (isBottle)
        {
            other.gameObject.SetActive(false);
            BottleHandler bottleHandler = other.gameObject.transform.parent.GetComponent<BottleHandler>();
            bottleHandler.BottleHit = true;
            bottleHandler.HitDirection = transform.up;
        }
        bool notPistol = !other.gameObject.CompareTag("Pistol");
        if (notPistol)
        {
            Destroy(gameObject);
        }
    }
}
