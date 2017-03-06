using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ColliderControl(int nForm)
	{
		Collider2D[] t;
		t = GetComponents<BoxCollider2D> ();
		CollidersStatusChange (t, false);

		t = GetComponents<CircleCollider2D> ();
		CollidersStatusChange (t, false);

		t = GetComponents<PolygonCollider2D> ();
		CollidersStatusChange (t, false);

		switch (nForm) {
		case 0:
			t = GetComponents<BoxCollider2D> ();
			CollidersStatusChange (t, true);
			for (int i = 0; i < t.Length; i++) {
				ChangeColliderSize ((BoxCollider2D)t [i], 205);
			}
			break;
		case 1:
			if (PlayerUpgrades.upgrades.sphere) {
				t = GetComponents<CircleCollider2D> ();
				CollidersStatusChange (t, true);
			}
			break;
		case 2:
			if (PlayerUpgrades.upgrades.triangle) {
				t = GetComponents<PolygonCollider2D> ();
				CollidersStatusChange (t, true);
			}
			break;
		case 3:
			if (PlayerUpgrades.upgrades.dense) {
				t = GetComponents<BoxCollider2D> ();
				CollidersStatusChange (t, true);
				for (int i = 0; i < t.Length; i++) {
					ChangeColliderSize ((BoxCollider2D)t [i], 25);
				}
			}
			break;
		}
	}

	void CollidersStatusChange(Collider2D[] t, bool stat)
	{
		for (int i = 0; i < t.Length; i++) {
			t [i].enabled = stat;
		}
	}

	void ChangeColliderSize (BoxCollider2D b, float N_size)
	{
		Vector2 Bsize = b.size;
		Bsize = b.size;
		Bsize.x = N_size;
		Bsize.y = N_size;
		b.size = Bsize;
	}
}
