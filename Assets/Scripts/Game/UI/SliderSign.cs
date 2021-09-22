using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class SliderSign : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        [SerializeField] private Slider slider;

        private void UpdateText(float value)
        {
            text.text = value.ToString();
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(UpdateText);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(UpdateText);
        }
    }
}
