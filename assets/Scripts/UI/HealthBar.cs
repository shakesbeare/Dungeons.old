using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float WHITE_BAR_MAX_TIMER = 1f;
    private float whiteBarZipDelay;

    private GameObject player;
    private Slider healthSlider;
    private Slider whiteSlider;
    private float sliderRange = 0.8f;
    private float sliderOffset = 0.2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSlider = GetComponent<Slider>();
        whiteSlider = GameObject.FindGameObjectWithTag("BarBackground").GetComponent<Slider>();

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.onUpdateHealth += OnHealthChange;
    }

	void Update()
	{
        whiteBarZipDelay -= Time.deltaTime;

        if (whiteBarZipDelay < 0)
		{
            if (healthSlider.value < whiteSlider.value)
			{
                float shrinkSpeed = 0.8f;
                whiteSlider.value -= shrinkSpeed * Time.deltaTime;
			}
		}
	}

	void OnHealthChange(object sender, PlayerHealth.OnUpdateHealthArgs args)
    {
        float sliderPos = (args.healthPercent * sliderRange) + sliderOffset;

        if (sliderPos < healthSlider.value)
		{
            
            whiteSlider.value = healthSlider.value;
            whiteBarZipDelay = WHITE_BAR_MAX_TIMER;
            
		}
        else
		{
            whiteSlider.value = sliderPos;
		}

        healthSlider.value = sliderPos;


    }

 

}
