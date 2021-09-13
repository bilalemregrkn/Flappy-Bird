using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
	public class CanvasGameOver : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI best;
		[SerializeField] private TextMeshProUGUI score;
		[SerializeField] private Animator myAnimator;
		[SerializeField] private Canvas myCanvas;
		
		public void Populate()
		{
			myCanvas.enabled = true;
			best.text = PlayerPrefs.GetInt("BEST").ToString();
			score.text = GameManager.Instance.Score.ToString();
			myAnimator.enabled = true;
		}
		
		public void OnClick_TryAgain()
		{
			SceneManager.LoadScene(0);
		}
	}
}