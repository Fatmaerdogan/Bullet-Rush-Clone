using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class GameManager : MonoBehaviour
	{
        public int _enemyDeadCounter;
        public int _enemyAmount;
        public int EnemyAmount
        {
            get => _enemyAmount;
			set=>_enemyAmount += value;
        }

        public int EnemyDeadCounter
		{
			get => _enemyDeadCounter;
			set
			{
				_enemyDeadCounter += value;
				Events.BarSet?.Invoke(SuccessValue);
			}
		}
		private float SuccessValue => EnemyDeadCounter /(float) EnemyAmount;
        private void EnemyAmountSet(int value) => EnemyAmount=value;
        private void EnemyDeadCounterSet(int value) => EnemyDeadCounter = value;
   
        private IEnumerator Start()
		{
			Events.GameOver += GameOver;
			Events.GameWin += Win;
			Events.EnemyAmount += EnemyAmountSet;
			Events.EnemyDeadCounter += EnemyDeadCounterSet;

			yield return new WaitForSeconds(.2f);

            Events.BarSet?.Invoke(SuccessValue);
		}
        private void OnDestroy()
        {
            Events.GameOver -= GameOver;
            Events.GameWin -= Win;
            Events.EnemyAmount -= EnemyAmountSet;
            Events.EnemyDeadCounter -= EnemyDeadCounterSet;
        }
        public void GameOver()
		{
			Time.timeScale = 0;
		}

		public void Win()
		{
			Time.timeScale = 0;
			var current = FindObjectsOfType<EnemyController>().Length;
			var result = current /(float)EnemyAmount;
			var success = Mathf.Lerp(100, 0, result);
		}
	}
