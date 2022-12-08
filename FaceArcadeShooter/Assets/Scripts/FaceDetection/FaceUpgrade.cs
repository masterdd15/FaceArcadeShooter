using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;


//Face Detection Script is Heavily copied over from
//Sam Sallivan NYU TISCH Student
//We were working off his prototype, and I was unable to implement a better
//Face Detection in time for our final versio
public class FaceUpgrade : MonoBehaviour
{
    WebCamTexture camTex;

    CascadeClassifier faceEngine;
    CascadeClassifier smileEngine;
    OpenCvSharp.Rect faceRect;
    OpenCvSharp.Rect smileRect;

    public Vector2 faceCord;
    public Vector2 centerCord; 
    public Vector2 lastCord;
    public bool reset;

    //UI stuff
    //Cursor UI Sprite Transform
    [SerializeField] RectTransform testCursorTrans;
    [SerializeField] private RectTransform canvasRectTransform;
    //Vector2 faceCord;

    //Shooting Script
    [SerializeField] CursorLogic cursLog;

    private void Awake()
    {
        cursLog = GameObject.FindGameObjectWithTag("Player").GetComponent<CursorLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        camTex = new WebCamTexture(devices[0].name);
        camTex.Play();

        // faceEngine = new CascadeClassifier(System.IO.Path.Combine(Application.dataPath + @"/haarcascade_frontalface_default.xml"));
        // smileEngine = new CascadeClassifier(System.IO.Path.Combine(Application.dataPath + @"/haarcascade_smile.xml"));
        faceEngine = new CascadeClassifier(System.IO.Directory.GetCurrentDirectory() + @"/haarcascade_frontalface_default.xml");
        smileEngine = new CascadeClassifier(System.IO.Directory.GetCurrentDirectory() + @"/haarcascade_smile.xml");

        //rotation = cam.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Use in UI
        GetComponent<RawImage>().texture = camTex;

        Mat tex = OpenCvSharp.Unity.TextureToMat(camTex);
        faceDetection(tex);
        //Debug.Log(tex.Size());
        //Debug.Log(tex.Width + " and " + tex.Height);
        TrailMove(tex.Width, tex.Height);
    }

    void faceDetection(Mat frame)
    {
        int[] faceRejectLevels;
        double[] faceLevelWeights;
        var faces = faceEngine.DetectMultiScale(frame, out faceRejectLevels, out faceLevelWeights, 1.1, 2, HaarDetectionType.ScaleImage, new Size(25, 25), new Size(1000, 1000), true);

        if (faces.Length >= 1)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                if (faceLevelWeights[i] >= 2f)
                {

                    faceRect = faces[i];
                    frame.Rectangle(faceRect, new Scalar(255, 0, 0, 0), 3);
                    faceCord = new Vector2(faces[i].X, faces[i].Y);
                    if (!reset)
                    {
                        reset = true;
                        lastCord = faceCord;
                        centerCord = faceCord;
                    }
                    Mat faceArea = frame[faceRect];

                    int[] smileRejectLevels;
                    double[] smileLevelWeights;
                    var smiles = smileEngine.DetectMultiScale(faceArea, out smileRejectLevels, out smileLevelWeights, 1.16, 65, HaarDetectionType.ScaleImage, new Size(25, 25), new Size(1000, 1000), true);

                    if (smiles.Length >= 1)
                    {
                        for (int j = 0; j < smiles.Length; j++)
                        {
                            smileRect = smiles[j];
                            if (smileLevelWeights[j] >= 2f)
                            {
                                faceArea.Rectangle(smileRect, new Scalar(0, 0, 255, 0), 3);
                                cursLog.isShooting = true;
                            }
                            else
                            {
                                cursLog.isShooting = false;
                            }
                        }
                    }

                }
                else
                {
                    faceCord = centerCord;
                }

            }
        }


        Texture drawRect = OpenCvSharp.Unity.MatToTexture(frame);
        //GetComponent<Renderer>().material.mainTexture = drawRect;
        GetComponent<RawImage>().texture = drawRect;
    }

    //This is a trial method to see if we can correctly move the cursor
    //Based on the players facial position
    void TrailMove(float camWidth, float camHeight)
    {

        Debug.Log(faceCord);
        //We are subtracting our face position from the width so that it mirros the player
        //And we are subtracting on the height because our coordinate system is inverted
        //float canvasRemapY = 

        float remapX = (faceCord.x * canvasRectTransform.rect.width) / camWidth;
        float remapY = (faceCord.y * canvasRectTransform.rect.height) / camHeight;
        //canvasRectTransform.rect.width - faceCord.x

        Vector2 cursorMove =
            new Vector2(canvasRectTransform.rect.width - remapX,
            canvasRectTransform.rect.height - remapY);

        Debug.Log(cursorMove);

        //testCursorTrans.anchoredPosition = faceCord;
        testCursorTrans.anchoredPosition = cursorMove;
    }
}
