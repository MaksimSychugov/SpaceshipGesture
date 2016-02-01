using UnityEngine;
using System.Collections;
using OpenCvSharp;

public class OpeCVScript : MonoBehaviour {
	IplImage prvs = new IplImage (640, 480, BitDepth.U8, 3);
	IplImage next = new IplImage (640, 480, BitDepth.U8, 3);
	CvCapture capture;
	IplImage frame;
	CvWindow windowCapture;
	bool close=false;
	
	// Use this for initialization
	void Start () {

		windowCapture = new CvWindow("capture");
		
		try
		{
			capture = new CvCapture(0);
		}
		catch
		{
			Debug.Log("Error: cant open camera.");
		}

	//	capture.GrabFrame ();
	//	frame = capture.RetrieveFrame ();
	//	Cv.CvtColor(frame,prvs,ColorConversion.BgrToGray);
		
		//capture.QueryFrame ();
		//Cv.ShowImage("Hello",)

	}
	
	// Update is called once per frame
	void Update () {
		//capture = CvCapture.FromCamera (0);
		if (!close) {
			capture.GrabFrame ();
			frame = capture.RetrieveFrame ();
			next = frame.Clone();
			Cv.CvtColor(frame,next,ColorConversion.BgrToGray);

			windowCapture.ShowImage (frame);
		}

		if(Input.GetKey(KeyCode.Escape))
		{
			windowCapture.Close();
			close = true;
		}
		
	}
	
	
}
