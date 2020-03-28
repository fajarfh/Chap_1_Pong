using UnityEngine;

public class SideWall : MonoBehaviour {

	// Pemain yang akan bertambah skornya jika bola menyentuh dinding ini.
	public PlayerControl player;
	public BallControl bombBall;
	public BallControl bonusBall;

	// Skrip GameManager untuk mengakses skor maksimal
	//	public GameManager gameManager; 
	[SerializeField]
	private GameManager gameManager;


	// Akan dipanggil ketika objek lain ber-collider (bola) bersentuhan dengan dinding.
	void OnTriggerEnter2D(Collider2D anotherCollider)
	{
		// Jika objek tersebut bernama "Ball":
		if (anotherCollider.name == "Ball")
		{
			// Tambahkan skor ke pemain
			player.IncrementScore();

			// Jika skor pemain belum mencapai skor maksimal...
			if (player.Score < gameManager.maxScore)
			{
				// ...restart game setelah bola mengenai dinding.
				anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);

				//comment jika ingin bomb ball dan bonus ball ga ke reset pas bola masuk
				bonusBall.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
				bombBall.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
			}
		} else
		{

			anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);

		}
	}

}
