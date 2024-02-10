using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gun : MonoBehaviour
{
    public GameObject bulletHit;
    public GameObject gunTip;
    public GameObject playerObject;
    public int gunDamage;
    public float maxGunDistance;
    public Text displayBulletCount;
    public int MAX_BULLETS;

    public static int currentBullets;

    private GameManager gameManager;
    private AudioSource audioSource;
    //public AudioClip bulletSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentBullets = MAX_BULLETS;
        UpdateBulletText();
        //gameManager = playerObject.GetComponent<GameManager>();

       // audioSource.clip = bulletSound
        //audioSource.play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && currentBullets > 0)
        {
            currentBullets--;
            UpdateBulletText();
            RaycastHit hit;
            if(Physics.Raycast(gunTip.transform.position, transform.forward, out hit, 1000f))
            {
                HealthSystemCollectable healthCollectable = hit.transform.GetComponent<HealthSystemCollectable>();
                HealthSystem health = hit.transform.GetComponent<HealthSystem>();
                // Collectable score counter
                if (healthCollectable != null)
                {
                    healthCollectable.TakeDamage(gunDamage);
                   // GameManager.scoreTreasure++;
                    Debug.Log("You got it");
                }
                Instantiate(bulletHit, hit.point, Quaternion.identity);
            // AI
             if (health != null)
                {
                    health.TakeDamage(gunDamage);
                }
                //Instantiate(bulletHit, hit.point, Quaternion.identity);
                audioSource.Play();
            }

        }
        
            
        }
    void UpdateBulletText()
    {
        displayBulletCount.text = currentBullets.ToString() + "/" + MAX_BULLETS.ToString();
    }
    }
