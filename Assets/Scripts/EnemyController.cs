using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


	public class EnemyController : MyCharacterController
	{
		[SerializeField] private ParticleController deadParticlePrefab;
		private GameObject Player;
		IEnumerator Start()
		{
			Player = GameObject.Find("Player");
			yield return new WaitForSeconds(.2f);
			Events.EnemyAmount?.Invoke(1);
		}

		private void FixedUpdate()
		{
			if(Player != null)
			{
                var delta = -transform.position + Player.transform.position;
                delta.y = 0;
                var direction = delta.normalized;
                Move(direction);
                transform.LookAt(Player.transform);
            }

		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.transform.CompareTag($"Bullet"))
			{
			    GetShot(other.gameObject);
            }
		}
	    public virtual void GetShot(GameObject Other)
		{
			gameObject.SetActive(false);
			Other.SetActive(false);
			Instantiate(deadParticlePrefab, transform.position, Quaternion.identity);
			Events.EnemyDeadCounter?.Invoke(1);
        }

    }
