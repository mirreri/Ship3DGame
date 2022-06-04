using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("Player Parmaters")]
    [SerializeField]
    public float currentHealth = 250.0f;
    public float maxHealth;

    [Header("Explosion")]
    public GameObject explosionPrefab;
    public int explosionLifeTime = 3;

    [Header("UI")]
    public Canvas gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    public void OnDamage(float attackValue)
    {
        currentHealth -= attackValue;
        // Upadate Player's Health Bar UI
        // Code Here
    }

    // Update is called once per frame
    void Update()
    {
        HealthHandler();
    }

    private void OnDestory()
    {   
        // Show GameOverUI
        if(gameOverUI != null)
            gameOverUI.gameObject.SetActive(true);

        
        Destroy(this.gameObject);

        // Exlosion effect
        GameObject exlosionEffect = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(exlosionEffect, explosionLifeTime);
    }

    private void HealthHandler()
    {   
        // Debug.Log("Here");
        if(currentHealth <= 0) // If stats class' health var <= 0, destroy enemy object
        {     
            OnDestory();
        }
    }
}