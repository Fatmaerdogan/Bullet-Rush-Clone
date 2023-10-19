using TMPro;
using UnityEngine;
using UnityEngine.UI;


	public class BarController : MonoBehaviour
	{
		[SerializeField] private Image fillImage;
		[SerializeField] private TextMeshProUGUI percentText;
        private void Start()=>Events.BarSet += Display;
        private void OnDestroy()=>Events.BarSet -= Display;
        public void Display(float normalized)
		{
			fillImage.fillAmount = normalized;
			percentText.SetText($"%{(int)(normalized * 100)}");
		}
	}
