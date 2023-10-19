using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


	public class PlayerController : MyCharacterController
	{

		[SerializeField] private ScreenTouchController input;
		[SerializeField] private ShootController shootController;

        [SerializeField] private List<Transform> WalkingFromRightEnemies = new();
        [SerializeField] private List<Transform> WalkingFromLeftEnemies = new();
		private bool _isShooting;

        [SerializeField] GameObject UziRight,UziLeft;
        [SerializeField] Transform UziRightShotPos,UziLeftShotpos;

        void Start()
        {
			Events.EnemiesAdd += EnemiesAdd;
			Events.EnemiesRemove += EnemiesRemove;
        }
        void OnDestroy()
        {
            Events.EnemiesAdd -= EnemiesAdd;
            Events.EnemiesRemove -= EnemiesRemove;
        }
        private void FixedUpdate()
		{
			var direction = new Vector3(input.Direction.x, 0, input.Direction.y);
			Move(direction);
		}

		private void Update()
		{
			if (WalkingFromRightEnemies.Count > 0)
			{
                UziRight.transform.LookAt(WalkingFromRightEnemies[0]);
				UziRight.transform.Rotate(Vector3.down * 90);
            }
            if (WalkingFromLeftEnemies.Count > 0)
            {
                UziLeft.transform.LookAt(WalkingFromLeftEnemies[0]);
                UziLeft.transform.Rotate(Vector3.up * 90);
            }
        }

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.transform.CompareTag($"Enemy"))
			{
				Dead();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag($"FinishPoint"))
			{
				OnReachSavePoint();
			}
		}


		private void AutoShoot()
		{
			IEnumerator Do()
			{
				while (WalkingFromRightEnemies.Count > 0)
				{
					var enemy = WalkingFromRightEnemies[0];
					var myTransform = transform;
					var position = myTransform.position;
					var direction = enemy.transform.position - position;
					direction.y = 0;
					direction = direction.normalized;
					shootController.Shoot(direction, UziRightShotPos.position);
					WalkingFromRightEnemies.RemoveAt(0);
					yield return new WaitForSeconds(shootController.Delay);
				}
                while (WalkingFromLeftEnemies.Count > 0)
                {
                    var enemy = WalkingFromLeftEnemies[0];
                    var myTransform = transform;
                    var position = myTransform.position;
                    var direction = enemy.transform.position - position;
                    direction.y = 0;
                    direction = direction.normalized;
                    shootController.Shoot(direction, UziLeftShotpos.position);
                    WalkingFromLeftEnemies.RemoveAt(0);
                    yield return new WaitForSeconds(shootController.Delay);
                }
                _isShooting = false;
			}

			if (!_isShooting)
			{
				_isShooting = true;
				StartCoroutine(Do());
			}
		}
		private void EnemiesAdd(bool temp, GameObject Enemy)
		{
			if (temp)
			{
				if (!WalkingFromRightEnemies.Contains(Enemy.transform))
					WalkingFromRightEnemies.Add(Enemy.transform);
			}
			else
			{
                if (!WalkingFromLeftEnemies.Contains(Enemy.transform))
                    WalkingFromLeftEnemies.Add(Enemy.transform);
            }
            AutoShoot();
        }
        private void EnemiesRemove(bool temp, GameObject Enemy)
        {
            if (temp)WalkingFromRightEnemies.Remove(Enemy.transform);
            else WalkingFromLeftEnemies.Remove(Enemy.transform);
        }
        private void Dead()
		{
            Events.GameOver?.Invoke();
        }

		private void OnReachSavePoint()
		{
			Events.GameWin?.Invoke();
		}
	}
