using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int speed = 10;
    private int lifeTime = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        bool isBottle = other.gameObject.CompareTag("Bottle");
        if (isBottle)
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.parent.GetComponent<BottleHandler>().BottleHit = true;
        }
        bool notPistol = !other.gameObject.CompareTag("Pistol");
        if (notPistol)
        {
            Destroy(gameObject);
        }
    }
}
