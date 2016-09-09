using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public Image foodImage, healthMeter;

    public float health = 100;
    public GameObject theBig;
    public GameObject foodPrefab;

    private bool overfed;

    // Use this for initialization
    void Start()
    {
        foodImage.fillAmount = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        foodImage.fillAmount -= Time.deltaTime / 200;

        if (foodImage.fillAmount <= 0)
            Destroy(theBig);

        if (foodImage.fillAmount <= .4f || overfed)
        {
            health -= Time.deltaTime;
        }

        if (theBig.transform.localScale.x <= 0.3f)
        {
            SceneManager.LoadScene(1);
        }
        if (theBig.transform.localScale.x >= 5f)
        {
            // Go to game over screen
            SceneManager.LoadScene(1);

        }

        healthMeter.fillAmount = (health / 100);
    }

    /// <summary>
    /// when the food button is pressed, the pig gets fatter.
    /// </summary>
    public void FoodButton()
    {
        foodImage.fillAmount += .01f;
        Instantiate(foodPrefab, new Vector3(2.5f, -4f, 0), Quaternion.identity);

        if (foodImage.fillAmount >= 1)
        {
            overfed = true;
        }
    }

    public void FitnessButton()
    {
        health += 10;

        if (health <= 100)
        {
            healthMeter.fillAmount = (health / 100);
        }
        else
        {
            health = 100;
        }
        
        foodImage.fillAmount -= 0.01f;
        theBig.transform.localScale -= new Vector3(.1f, .1f, .1f);

    }
}
