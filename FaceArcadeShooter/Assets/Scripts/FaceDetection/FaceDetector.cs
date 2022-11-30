using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class FaceDetector : MonoBehaviour
{

    WebCamTexture _webCamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect MyFace;

    [SerializeField] private RectTransform canvasRectTransform;



    //UI stuff
    //Cursor UI Sprite Transform
    [SerializeField] RectTransform testCursorTrans;
    Vector2 faceCord;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name); //We use 1 to get my plugged in webcam
        //_webCamTexture.requestedFPS = 30;
        _webCamTexture.Play();

        cascade = new CascadeClassifier(System.IO.Directory.GetCurrentDirectory() + @"/haarcascade_frontalface_default.xml");
    }

    // Update is called once per frame
    void Update()
    {
        //Use for material
        //GetComponent<Renderer>().material.mainTexture = _webCamTexture;

        //Use in UI
        GetComponent<RawImage>().texture = _webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

        findNewFace(frame);
        display(frame);
        TrailMove();
    }

    void findNewFace(Mat frame)
    {
        var faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

        if(faces.Length >= 1)
        {
            //Debug.Log(faces[0].Location);
            MyFace = faces[0];

            faceCord = new Vector2(faces[0].X, faces[0].Y);
        }
    }

    void display(Mat frame)
    {
        if(MyFace != null)
        {
            frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<RawImage>().texture = newtexture;
        //GetComponent<Renderer>().material.mainTexture = newtexture;

    }


    //This is a trial method to see if we can correctly move the cursor
    //Based on the players facial position
    void TrailMove()
    {
        //We are subtracting our face position from the width so that it mirros the player
        //And we are subtracting on the height because our coordinate system is inverted
        Vector2 cursorMove = 
            new Vector2(canvasRectTransform.rect.width - faceCord.x, 
            canvasRectTransform.rect.height - faceCord.y);

        //testCursorTrans.anchoredPosition = faceCord;
        testCursorTrans.anchoredPosition = cursorMove;
    }
}
