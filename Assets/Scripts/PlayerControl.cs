using UnityEngine;

public class PlayerControl : MonoBehaviour {
	// Tombol untuk menggerakkan ke atas
	public KeyCode upButton = KeyCode.W;

	// Tombol untuk menggerakkan ke bawah
	public KeyCode downButton = KeyCode.S;

	// Kecepatan gerak
	public float speed = 10.0f;

	// Batas atas dan bawah game scene (Batas bawah menggunakan minus (-))
	public float yBoundary = 9.0f;

	// Rigidbody 2D raket ini
	private Rigidbody2D rigidBody2D;

	// RESET RAKET
	private Vector3 posisiAwal;

	public void ResetRaket()
	{
		// Reset posisi
		transform.position = posisiAwal;

	}


	// SCORE FUNCTION
	// Skor pemain
	private int score;

	// Menaikkan skor sebanyak 1 poin
	public void IncrementScore()
	{
		score++;
	}

	// Menurunkan skor sebanyak 1 poin
	public void DecreaseScore()
	{
		score--;
	}

	private void ScaleUp()
	{
		transform.localScale += new Vector3(0, 4);
		yBoundary -= 4.0f;
	}

	private void ScaleDown()
	{
		transform.localScale -= new Vector3(0, 4);
		yBoundary += 4.0f;
	}

	public void PowerUp()
	{
		ScaleUp();
		Invoke("ScaleDown", 10);
	}

	// Mengembalikan skor menjadi 0
	public void ResetScore()
	{
		score = 0;
	}

	// Mendapatkan nilai skor
	public int Score
	{
		get { return score; }
	}

	// CONTACT POINT DEBUG
	// Titik tumbukan terakhir dengan bola, untuk menampilkan variabel-variabel fisika terkait tumbukan tersebut
	private ContactPoint2D lastContactPoint;

	// Untuk mengakses informasi titik kontak dari kelas lain
	public ContactPoint2D LastContactPoint
	{
		get { return lastContactPoint; }
	}

	// Ketika terjadi tumbukan dengan bola, rekam titik kontaknya.
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.name.Equals("Ball"))
		{

			lastContactPoint = collision.GetContact(0);

		} 
		//else if(collision.gameObject.name.Equals("Bonus Ball"))
		//{
			
		//	PowerUp();
		//	collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);

		//} else if (collision.gameObject.name.Equals("Bomb Ball"))
		//{

		//	DecreaseScore();
		//	collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);

		//}
	}

	//void OnTriggerEnter(Collider someObject)
	//{
		
	//	if (someObjectgameObject.name.Equals("Bonus Ball"))
	//	{

	//		someObject.gameObject.SendMessage("PowerUp", 2.0f, SendMessageOptions.RequireReceiver);
	//		RestartGame();

	//	}
	//	else if (gameObject.name.Equals("Bomb Ball"))
	//	{

	//		someObject.gameObject.SendMessage("DecreaseScore", 2.0f, SendMessageOptions.RequireReceiver);
	//		RestartGame();

	//	}
		
	//}

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D>();
		posisiAwal = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// CONTROL
		// Dapatkan kecepatan raket sekarang.
		Vector2 velocity = rigidBody2D.velocity;

		// Jika pemain menekan tombol ke atas, beri kecepatan positif ke komponen y (ke atas).
		if (Input.GetKey(upButton))
		{
			velocity.y = speed;
		}

		// Jika pemain menekan tombol ke bawah, beri kecepatan negatif ke komponen y (ke bawah).
		else if (Input.GetKey(downButton))
		{
			velocity.y = -speed;
		}

		// Jika pemain tidak menekan tombol apa-apa, kecepatannya nol.
		else
		{
			velocity.y = 0.0f;
		}

		// Masukkan kembali kecepatannya ke rigidBody2D.
		rigidBody2D.velocity = velocity;

		// BOUNDARY
		// Dapatkan posisi raket sekarang.
		Vector3 position = transform.position;

		// Jika posisi raket melewati batas atas (yBoundary), kembalikan ke batas atas tersebut.
		if (position.y > yBoundary)
		{
			position.y = yBoundary;
		}

		// Jika posisi raket melewati batas bawah (-yBoundary), kembalikan ke batas atas tersebut.
		else if (position.y < -yBoundary)
		{
			position.y = -yBoundary;
		}

		// Masukkan kembali posisinya ke transform.
		transform.position = position;


	}

}


