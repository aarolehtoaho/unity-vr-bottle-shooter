using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using TMPro;

public class Shoot : MonoBehaviour
{
    public TMP_Text ScoreText;
    public bool LeftHandTriggerPressed;
    public bool RightHandTriggerPressed;

    private GameObject BulletPrefab;
    private float lastShotTime = 0f;
    public float shootCooldown = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BulletPrefab = transform.Find("Bullet").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GunIsGrabbed() && TriggerPressed())
        {
            bool canShoot = Time.time - lastShotTime >= shootCooldown;
            if (canShoot)
            {
                ShootBullet();
                DecreaseScore();
                lastShotTime = Time.time;
            }
        }
    }

    private bool GunIsGrabbed()
    {
        return GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().isSelected;
    }

    private bool TriggerPressed()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null || grabInteractable.interactorsSelecting.Count == 0)
            return false;

        var selectingInteractor = grabInteractable.interactorsSelecting[0];
        string interactorName = selectingInteractor.transform.gameObject.name.ToLower();
        
        if (interactorName.Contains("left"))
        {
            return LeftHandTriggerPressed;
        }
        else if (interactorName.Contains("right"))
        {
            return RightHandTriggerPressed;
        }
        
        return false;
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        bullet.SetActive(true);
    }

    private void DecreaseScore()
    {
        int currentScore = int.Parse(ScoreText.text);
        currentScore--;
        currentScore = Mathf.Max(currentScore, 0);
        ScoreText.text = currentScore.ToString();
    }
}
