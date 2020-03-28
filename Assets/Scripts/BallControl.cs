using UnityEngine;

public class BallControl : MonoBehaviour {
	
	// Rigidbody 2D bola
	private Rigidbody2D rigidBody2D;

	// Besarnya gaya awal yang diberikan untuk mendorong bola
	public float xInitialForce;
	public float yInitialForce;
	public float initSpeed;


	// RESET BOLA
	void ResetBall()
	{
		// Reset posisi menjadi (0,0)
		transform.position = Vector2.zero;

		// Reset kecepatan menjadi (0,0)
		rigidBody2D.velocity = Vector2.zero;
	}

	// DORONG BOLA
	void PushBall()
	{
		// Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
		float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

		// Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
		float randomDirection = Random.Range(0, 2);

		// Jika nilainya di bawah 1, bola bergerak ke kiri. 
		// Jika tidak, bola bergerak ke kanan.
		if (randomDirection < 1.0f)
		{
			// Gunakan gaya untuk menggerakkan bola ini.
			Vector2 initForce = new Vector2(-xInitialForce, yRandomInitialForce).normalized;
			rigidBody2D.AddForce(initForce * initSpeed);
		}
		else
		{
			Vector2 initForce = new Vector2(xInitialForce, yRandomInitialForce).normalized;
			rigidBody2D.AddForce(initForce * initSpeed);
		
		}
	}

	// ACTIVATE BALLS
	void Activ8()
	{
		gameObject.SetActive(true);
		PushBall();
	}

	// RESTART
	void RestartGame()
	{
		// Kembalikan bola ke posisi semula
		ResetBall();

		if (IsInvoking())
		{
			CancelInvoke();
		}

		// Setelah 2 detik, berikan gaya ke bola
		if (gameObject.name == "Ball")
		{

			Invoke("PushBall", 2);
		}
		else if (gameObject.name == "Bonus Ball")
		{
			gameObject.SetActive(false);
			Invoke("Activ8", 10);
		}
		else if (gameObject.name == "Bomb Ball")
		{
			gameObject.SetActive(false);
			Invoke("Activ8", 14);
		}

	}


	// DEBUG
	// Titik asal lintasan bola saat ini
	private Vector2 trajectoryOrigin;

	// Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
	private void OnCollisionExit2D(Collision2D collision)
	{
		trajectoryOrigin = transform.position;
	}

	// Untuk mengakses informasi titik asal lintasan
	public Vector2 TrajectoryOrigin
	{
		get { return trajectoryOrigin; }
	}

	//Ganti dari collision ke trigger untuk bomb dan bonus ball
	void OnTriggerEnter2D(Collider2D someObject)
	{
		if (someObject.gameObject.name.Contains("Player"))
		{
			if (gameObject.name.Equals("Bonus Ball"))
			{

				someObject.gameObject.SendMessage("PowerUp", 2.0f, SendMessageOptions.RequireReceiver);
				RestartGame();

			}
			else if (gameObject.name.Equals("Bomb Ball"))
			{

				someObject.gameObject.SendMessage("DecreaseScore", 2.0f, SendMessageOptions.RequireReceiver);
				RestartGame();

			}
		}
	}


	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D>();

		trajectoryOrigin = transform.position;
		
		// Mulai game
		RestartGame();
	}

}