using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stickmove : MonoBehaviour {

	public Camera cam;
	public Canvas can;
	public GameObject prefab;
	public GameObject coinPre;
	public GameObject center;
	public GameObject coinPos;
	public GameObject asteroid;
	private GameObject c1;
	private GameObject a1;
	private Animator anim;
	private Animator astAnim;
	private Rigidbody2D rb;
	private Vector3 offset;
	private Vector2 direction;
	private int count;
	private bool gameOver;

	// Use this for initialization
	void Start () {
		count = 0;
		gameOver = false;
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		anim.SetBool("idle", true);
		offset = cam.transform.position - transform.position;

		c1 = (GameObject)Instantiate(coinPre);
		c1.transform.SetParent(can.transform, false);
		c1.transform.position = new Vector3(coinPos.transform.position.x, coinPos.transform.position.y, -7);
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis("Horizontal");

		if (!gameOver) {

			if (moveHorizontal == 0) {
				anim.SetBool("idle", true);
			}
			else if (moveHorizontal > 0) {
				anim.SetBool("idle", false);
				if (!anim.GetBool("goRight")) {
					anim.SetBool("goRight", true);
				}
			}
			else if (moveHorizontal < 0) {
				anim.SetBool("idle", false);
				if (anim.GetBool("goRight")) {
					anim.SetBool("goRight", false);
				}
			}

		
			transform.Translate(new Vector3(moveHorizontal, 0, 0) * .05f);
			cam.transform.position = transform.position + offset;
			cam.transform.position = new Vector3(cam.transform.position.x, -.25f, cam.transform.position.z);

			if (Input.GetKeyDown(KeyCode.Mouse0)) {
				a1 = (GameObject)Instantiate(asteroid);
				anim.SetBool("Shoot", true);

				Rigidbody2D rigid = a1.AddComponent<Rigidbody2D>();
				rigid.mass = 0;
				a1.transform.position = new Vector3(transform.position.x, transform. position.y, -6);
				if (anim.GetBool("goRight")) {
					direction = new Vector2(1, .15f);
				}
				else {
					direction = new Vector2(-1, .15f);
				}
				a1.GetComponent<Rigidbody2D>().AddForce(direction * .1f);
			}
			else {
				anim.SetBool("Shoot", false);
			}

		}
		else {
			anim.SetBool("idle", true);
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (!gameOver) {
			if (Input.GetKey("space")) {
				rb.AddForce(transform.up * 1.3f, ForceMode2D.Impulse);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "coin") {
			Destroy(other.gameObject);
			count += 1;
			Text txt = c1.transform.Find("Text").GetComponent<Text>();
			txt.text = count.ToString();
		}

		if (other.gameObject.tag == "spike") {
			gameOver = true;
			EndGame();
		}

		if (other.gameObject.tag == "enemy") {
			gameOver = true;
			EndGame();
		}

		if (other.gameObject.tag == "fall") {
			gameOver = true;
			EndGame();
		}
	}

	void EndGame() {
		GameObject gOver = (GameObject)Instantiate(prefab);
		gOver.transform.SetParent(can.transform, false);
		gOver.transform.position = new Vector3(center.transform.position.x, center.transform.position.y, -7);

		Button but = gOver.transform.Find("Restart").GetComponent<Button>();
		but.onClick.AddListener(() => ButtonClicked());
	}

	void ButtonClicked() {
		Application.LoadLevel("main");
	}
}
