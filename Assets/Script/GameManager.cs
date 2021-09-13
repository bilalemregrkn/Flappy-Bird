using System.Collections.Generic;
using System.Linq;
using App.Helpers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }

		[SerializeField] private float limitVerticalPosition = 3;
		[SerializeField] private float distanceHorizontal = 3;
		[SerializeField] private float distanceVertical = 1.5f;
		[SerializeField] private List<BlockController> listBlock = new List<BlockController>();

		[SerializeField] private TextMeshProUGUI textScore;
		[SerializeField] private BoingAnimation textScoreBoing;

		[SerializeField] private PlayerController playerController;
		[SerializeField] private CanvasGameOver canvasGameOver;
		[SerializeField] private Canvas canvasHUD;
		[SerializeField] private Canvas canvasStart;

		private readonly Queue<Transform> _queue = new Queue<Transform>();

		public bool IsGameOver { get; private set; }

		private int _count;

		public int Score
		{
			get => _score;
			private set
			{
				_score = value;
				textScore.text = $"{_score}";
				if (_score == 0)
					textScore.text = "";

				textScoreBoing.Play();
			}
		}

		private int _score;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			Score = 0;

			playerController.transform.position = Vector3.zero;
			foreach (var item in listBlock)
			{
				item.gameObject.SetActive(false);
			}
		}

		public void StartGame()
		{
			IsGameOver = false;
			Spawn();
			canvasStart.enabled = false;
		}

		public void GameOver()
		{
			if (IsGameOver) return;
			IsGameOver = true;

			var best = PlayerPrefs.GetInt("BEST");
			if (best < _score)
				PlayerPrefs.SetInt("BEST", _score);

			canvasHUD.enabled = false;
			canvasGameOver.Populate();
		}

		public void OnPlayerPass()
		{
			Score++;
			_count++;
			if (_count < 3)
				return;

			var first = _queue.Dequeue();
			var last = _queue.Last();

			float y = Random.Range(-limitVerticalPosition, limitVerticalPosition);
			float x = last.position.x + distanceHorizontal;

			first.position = new Vector3(x, y, 0);
			_queue.Enqueue(first);
		}

		[ContextMenu(nameof(Spawn))]
		private void Spawn()
		{
			var playerPosition = playerController.transform.position.x;
			for (var i = 0; i < listBlock.Count; i++)
			{
				var block = listBlock[i];
				float y = Random.Range(-limitVerticalPosition, limitVerticalPosition);
				block.transform.position = new Vector3((i + 1) * distanceHorizontal + playerPosition + 5, y, 0);
				block.Setup(distanceVertical);
				block.gameObject.SetActive(true);

				_queue.Enqueue(block.transform);
			}
		}
	}
}