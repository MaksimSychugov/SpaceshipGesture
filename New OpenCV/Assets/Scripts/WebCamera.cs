using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using OpenCvSharp;
using System;
using System.IO;
public class WebCamera : MonoBehaviour {
   
    public RawImage rawimage;
    int imWidth = 320;
    int imHeight = 240;
   // IplImage prvs;
    //IplImage next;
    IplImage src;
    IplImage rez;
    //CvMat velx;
   // CvMat vely;
    bool fl = false;
    static public Vector3 moveVec;

    WebCamTexture webcamTexture;
    CvWindow w;
    
	// Use this for initialization
	void Start () {
        webcamTexture = new WebCamTexture(imWidth, imHeight);
        rawimage.texture = webcamTexture;
        rawimage.material.mainTexture = webcamTexture;
        webcamTexture.Play();
       // prvs = new IplImage(imWidth, imHeight, BitDepth.U8, 1);
       
        rez = new IplImage(imWidth, imHeight, BitDepth.U8, 1);
        src = new IplImage(imWidth, imHeight, BitDepth.U8, 3);
     //   velx = Cv.CreateMat(imWidth, imHeight, MatrixType.F32C1);
     //   vely = Cv.CreateMat(imWidth, imHeight, MatrixType.F32C1);
      //     FromTextureToIplImage(prvs);
      //  prvs.CvtColor(prvs, ColorConversion.RgbToGray);
        w = new CvWindow("Web Camera");

	}

    void FromTextureToIplImage(IplImage imageIpl)
    {
        int imH = imHeight;

        for (int v = 0; v < imHeight; ++v)
        {
            for (int u = 0; u < imWidth; ++u)
            {

                CvScalar col = new CvScalar();
                col.Val0 = (double)webcamTexture.GetPixel(u, v).b * 255;
                col.Val1 = (double)webcamTexture.GetPixel(u, v).g * 255;
                col.Val2 = (double)webcamTexture.GetPixel(u, v).r * 255;

                imH = imHeight - v - 1;
                imageIpl.Set2D(imH, u, col);
            
            }
        }
      
    }

    void CalcPoint(CvMat velx, CvMat vely, IplImage rez)
    {
        int sX = 0;
        int sY = 0;
        int coun = 0;
        for (int x = 0; x < imWidth; x += 10)
        {
            for (int y = 0; y < imHeight; y += 10)
            {
                int dx = (int)Cv.GetReal2D(velx, y, x);
                int dy = (int)Cv.GetReal2D(vely, y, x);
                if (dx > 15 || dy > 15)
                {
                    Cv.Line(rez, Cv.Point(x, y), Cv.Point(x + dx, y + dy), Cv.RGB(0, 0, 255), 1, Cv.AA, 0);
                    sX += x;
                    sY += y;
                    coun++;
                }
                if (dx < -15 || dy < -15)
                {
                    Cv.Line(rez, Cv.Point(x, y), Cv.Point(x + dx, y + dy), Cv.RGB(0, 255, 0), 1, Cv.AA, 0);
                    sX += x;
                    sY += y;
                    coun++;
                }
            }
        }
        if (coun > 10)
        {
            moveVec.Set(sX / coun, sY / coun, 0);
        }
    }

	// Update is called once per frame
	void Update () {

        FromTextureToIplImage(src);
        IplImage next = new IplImage(imWidth, imHeight, BitDepth.U8, 1);
        IplImage prvs = new IplImage(imWidth, imHeight, BitDepth.U8, 1);
        CvMat velx = new CvMat(imWidth, imHeight, MatrixType.F32C2);
        CvMat vely = new CvMat(imWidth, imHeight, MatrixType.F32C2);
     
        if (fl)
        {
           
            Cv.CvtColor(src, next, ColorConversion.BgrToGray);
            Cv.Threshold(next, next, 90, 255, ThresholdType.Truncate);
            Cv.Threshold(prvs, prvs, 90, 255, ThresholdType.Truncate);
           //s Cv.AbsDiff
          //  Cv.CalcOpticalFlowFarneback(prvs, next, velx, 0.5, 3, 15, 3, 5, 1.2, 0);
        //    Cv.CalcOpticalFlowLK(prvs, next, Cv.Size(15, 15), velx, vely);
         //   CalcPoint(velx, vely, rez);

            w.Image = rez;
            next.Copy(prvs);
        }
        if (!fl) 
        {
           
            Cv.CvtColor(src, prvs, ColorConversion.BgrToGray);
       //     w.Image = prvs;
            fl = true;
        }


	    
	}

    void OnApplicationQuit()
    {
        w.Close();
    }
}
