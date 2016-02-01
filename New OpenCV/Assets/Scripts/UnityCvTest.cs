using UnityEngine;
using System.Collections;
using OpenCvSharp;




public class UnityCvTest : MonoBehaviour {

	CvCapture cap;
	CvWindow w;
	bool close = false;
	IplImage prvs;
	static public int rows;
	static public int cols;
    public GameObject planeRight;
    public GameObject planeLeft;
    Texture2D myTexture2D;
	static public Vector3 moveVec;
	

	void Start () {
		moveVec = new Vector3(0,0,0);
		
        cap = CvCapture.FromCamera (0);
		w = new CvWindow("Original");

		IplImage frame = cap.QueryFrame ();
        
        cols = frame.Width;
		rows = frame.Height;
   
        myTexture2D = new Texture2D(cols/2, rows/2);
        prvs = new IplImage(cols, rows, BitDepth.U8, 1);
		frame.CvtColor (prvs, ColorConversion.BgrToGray);
	}

	void CalcPoint(CvMat velx, CvMat vely, IplImage rez)
	{
		int sX = 0;
		int sY = 0;
		int coun = 0;
		for (int x = 0; x < cols; x += 10) {
			for (int y = 0; y < rows; y += 10) {
				int dx = (int)Cv.GetReal2D (velx, y, x);
				int dy = (int)Cv.GetReal2D (vely, y, x);
				if(Mathf.Abs(dx)>8 && Mathf.Abs(dy)>8 && Mathf.Abs(dx)<75 && Mathf.Abs(dy)<75)
				{
					Cv.Line (rez, Cv.Point (x, y), Cv.Point (x + dx, y + dy), Cv.RGB (0, 0, 255), 1, Cv.AA, 0);
					sX += x;
					sY += y;
					coun++;
				}	


                if(y == 0 && x == cols/8*3 || y == 0 && x == cols/8*5 )
                {
                    Debug.Log("wewe");
                    Cv.Line(rez, Cv.Point(x, 0), Cv.Point(x, rows), Cv.RGB(255, 0, 0), 3, Cv.AA, 0);
                }

                if (x == 0 && y == rows / 3 || x == 0 && y == rows / 3 * 2)
                {
                    Cv.Line(rez, Cv.Point(0, y), Cv.Point(cols, y), Cv.RGB(255, 0, 0), 3, Cv.AA, 0);
                }
			}
		}
		if (coun >15) {
            Cv.Circle(rez, Cv.Point(sX / coun, sY / coun),30, Cv.RGB(255, 255, 0),5);
			moveVec.Set (sX / coun, sY / coun, 0);
		}
	}

	void DrawOpticalFlow(CvMat velx, CvMat vely, IplImage rez)
	{
	
		for (int x = 0; x < cols; x += 10) {
			for (int y = 0; y < rows; y += 10) {
				int dx = (int)Cv.GetReal2D (velx, y, x);
				int dy = (int)Cv.GetReal2D (vely, y, x);
				if(Mathf.Abs(dx)>8 && Mathf.Abs(dy)>8 && Mathf.Abs(dx)<75 && Mathf.Abs(dy)<75)
				{
					Cv.Line (rez, Cv.Point (x, y), Cv.Point (x + dx, y + dy), Cv.RGB (0, 0, 255), 1, Cv.AA, 0);
				}	
			}
		}

	}

    void FromIplImageToTexture(IplImage matrix, GameObject gameObject)
    {
        int jBackwards = rows;

        for (int i = 0; i < rows/2; i++)
        {
            for (int j = 0; j < cols/2; j++)
            {
                float b = (float)matrix[i*2, j*2].Val0;
                float g = (float)matrix[i*2, j*2].Val1;
                float r = (float)matrix[i*2, j*2].Val2;
                Color color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);


                jBackwards = rows/2 - i - 1; // notice it is jBackward and i
                myTexture2D.SetPixel(j, jBackwards, color);
            }
        }
        myTexture2D.Apply();
        gameObject.GetComponent<Renderer>().material.mainTexture = myTexture2D;

    }
	
	// Update is called once per frame
	void Update () {
	
		if (!close)
		{
			IplImage src = cap.QueryFrame();

            IplImage next = new IplImage(cols, rows, BitDepth.U8, 1);

            IplImage rez = new IplImage(cols, rows, BitDepth.U8, 3);
            src.Copy(rez);

			CvMat velx = Cv.CreateMat(rows, cols, MatrixType.F32C1);
			CvMat vely = Cv.CreateMat(rows, cols, MatrixType.F32C1);

			src.CvtColor(next, ColorConversion.BgrToGray);
			//uu = new CvMat(src.GetRow, src.GetCol, BitDepth.U8,
			//src.CvtColor(uu,ColorConversion.BgrToGray);
			//Cv.AbsDiff(prvs,next, rez);
			//Cv.CalcOpticalFlowFarneback(prvs,next,rez,0.5, 3, 15, 3, 5, 1.2, 0);
			//Cv.Canny(next, dstCanny, 50, 50, ApertureSize.Size3);
			//Cv.Erode(
			//Cv.Threshold(prvs,prvs,100,255,ThresholdType.Mask);
			//Cv.Threshold(next,next,100,255,ThresholdType.Mask);

		//	next.InRangeS(CvScalar(160,120,120), CvScalar(179,255,255),next);
        //    Cv.Erode(prvs, prvs, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
        //    Cv.Dilate(prvs, prvs, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
         //   Cv.Erode(next, next, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
        //    Cv.Dilate(next, next, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));

       //     Cv.Dilate(prvs, prvs, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
       //     Cv.Erode(prvs, prvs, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
	
        //    Cv.Dilate(next, next, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));
        //    Cv.Erode(next, next, Cv.CreateStructuringElementEx(15, 15, 7, 7, ElementShape.Ellipse));


            Cv.Threshold(next, next, 120, 255, ThresholdType.Truncate);
            Cv.Threshold(prvs, prvs, 120, 255, ThresholdType.Truncate);
            
           
			Cv.CalcOpticalFlowLK(prvs,next,Cv.Size(15,15),velx,vely);
	    	CalcPoint(velx,vely,rez);
		//	DrawOpticalFlow(velx,vely,rez);
            FromIplImageToTexture(rez, planeRight);
           // FromIplImageToTexture(src, planeLeft);

			if (Input.GetKey (KeyCode.Space)) {
				src.SaveImage("Assets/1.jpg");
				Debug.Log("Save");
			
			}


			w.Image = rez;
			next.Copy(prvs);

			
		
		}
		//Debug.Log (move.x);
		if (Input.GetKey (KeyCode.Escape)) {
			close = true;
			w.Close();
		}

	}
	void OnApplicationQuit()
	{
		Cv.DestroyAllWindows ();
	}

	void Exit(){
		w.Close();
	}
}
