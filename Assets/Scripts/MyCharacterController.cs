using UnityEngine;


	public class MyCharacterController : MonoBehaviour
	{
        [SerializeField] private Rigidbody myRigidbody;
		[Range(20,2000)][SerializeField] private float moveSpeed;

        void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
        }
        protected void Move(Vector3 direction)
		{
			if(myRigidbody!=null) myRigidbody.velocity = direction * moveSpeed * Time.deltaTime;
		}
	}
