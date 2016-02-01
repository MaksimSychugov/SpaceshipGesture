using UnityEngine;
using System.Collections;

public class ControlObject : MonoBehaviour {

	//UnityCvTest vec = new UnityCvTest();
	float x1 = 0;
	float y1 = 0;
	float x2 = 0;
	float y2 = 0;
	float sx = 0;
	float sy = 0;
    int t = 0;
	public GameObject spaceShip;
	float speed = 0;
	// Use this for initialization
	void Start () {
		x1 = UnityCvTest.moveVec.x;
		y1 = UnityCvTest.moveVec.y;
	}
	
	// Update is called once per frame
	void Update () {
       
            x2 = UnityCvTest.moveVec.x;
            y2 = UnityCvTest.moveVec.y;
            sx = x1 - x2;
            sy = y1 - y2;
            Debug.Log(sx + "  " + sy);
            x1 = x2;
            y1 = y2;

            if (sx > 0)
            {
                t++;
                if(t>4)
                speed = 1 * Mathf.Abs(sx) / 5;
            }
            else if (sx < 0)
            {
                t++;
                if(t>4)
                speed = -1 * Mathf.Abs(sx) / 5;
            }

            //	spaceShip.transform.RotateAround (new Vector3 (0.0f, -1.0f, 0.0f), new Vector3 (0.0f, 0.0f, 1.0f), -speed * Time.deltaTime);
            spaceShip.transform.RotateAround(new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), -speed * Time.deltaTime);

        
       
		//} 
	/*	else if(Mathf.Abs(sx) < Mathf.Abs(sy)){
			if (sy > 0) {
				speed = 10;
			} else if (sy < 0) {
				speed = -10;
			} 
			spaceShip.transform.RotateAround (new Vector3 (0.0f, -1.0f, 0.0f), new Vector3 (1.0f, 0.0f, 0.0f), speed * Time.deltaTime);
		}*/
		//this.GetComponent<Transform> ().position = new Vector3(x/20,y/20,0);
		//c++;
	}
}
