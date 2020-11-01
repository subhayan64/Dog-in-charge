using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider slider;


    public void MaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
   public void SetHealth(float health)
    {
        slider.value = health;
    }
}
