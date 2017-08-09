using UnityEngine;

public class Trumpet : MonoBehaviour {

    public Hamster HamsterMan;

	void Start () {}
	
	void Update () {}

    void OnMouseDown()
    {
        HamsterMan.GetComponent<CircleCollider2D>().enabled = false;
    }

    void OnMouseUp()
    {
        HamsterMan.GetComponent<CircleCollider2D>().enabled = true;
    }
}
