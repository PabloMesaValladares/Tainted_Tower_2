using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    public float MaxStamina;
    public float StaminaReducing;
    public float StaminaAugmentg;
    [SerializeField]
    float stamina;
    public Slider StaminaSlider;
    bool reducing;
    public bool tired;
    public Vector3 offset;
    [Range(0, 1)]
    public float speedDampTime = 0.1f;

    [Header("Color")]
    public Image Fill;
    public Color FullFillColor;
    public Color MediumFillColor;
    public Color LowFillColor;
    public float mediumPoint;
    public float lowPoint;


    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.firstSet)
            stamina = MaxStamina;
        StaminaSlider.gameObject.SetActive(false);
        Fill.color = FullFillColor;
    }

    // Update is called once per frame
    void Update()
    {

        StaminaSlider.transform.position = Vector3.Lerp(StaminaSlider.transform.position, Camera.main.WorldToScreenPoint(transform.position + offset), speedDampTime);
        if (stamina != MaxStamina)
        {
            StaminaSlider.value = stamina / 100;
            StaminaSlider.gameObject.SetActive(true);

            if(!reducing)
            {
                stamina += StaminaAugmentg * Time.deltaTime;
                if (stamina > MaxStamina)
                    stamina = MaxStamina;
            }
        }
        else
        {
            StaminaSlider.gameObject.SetActive(false);
        }
    }


    public void ChangeColor()
    {
        float percStamina = stamina * MaxStamina / 100;
        
        if (percStamina < lowPoint)
        {
            Fill.color = Color.Lerp(Fill.color, LowFillColor, (lowPoint - stamina) / 10);
            tired = true;
        }
        else if (percStamina < mediumPoint)
        {
            tired = false;
            Fill.color = Color.Lerp(Fill.color, MediumFillColor, (mediumPoint - stamina) / 10);
        }
        else
        {
            Fill.color = Color.Lerp(Fill.color, FullFillColor, StaminaSlider.value);
        }
    }

    public float ReturnStamina()
    {
        return stamina;
    }

    public void SetStamina(float s)
    {
        stamina = s;
    }

    public void addStamina(int s)
    {
        stamina += s;
    }
    public void addStaminaPerc(int s)
    {
        float perc = MaxStamina * s / 100;
        stamina += perc;
    }
    public void ReduceStamina()
    {
        stamina -= StaminaReducing * Time.deltaTime;
    }

    public void Reduce(bool b)
    {
        reducing = b;
    }
}
