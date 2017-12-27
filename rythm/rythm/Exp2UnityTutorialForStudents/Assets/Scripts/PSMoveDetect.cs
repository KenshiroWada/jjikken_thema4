// Sugimoto Lab: Interactive Media Lab
// Experiment #2
// Last Update 2017/08/11 edited by kuno
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;


public class PSMoveDetect : MonoBehaviour
{
	// カメラのWidth, Height
	const int CAPTURE_WIDTH = 640;
	const int CAPTURE_HEIGHT = 480;
	// カメラの内部パラメータ
	const double fx = 660.5071807875812;
	const double fy = 662.0103568287129;
	const double ux = 399.1575466095347;
	const double uy = 298.9527473279853;
	// PSMoveの半径
	const double SPHERE_R = 2.25;
	// HSVのレンジの設定
	const double RANGE_H = 5.0;
	const double RANGE_S = 5.0;
	const double RANGE_V = 5.0;
	// Webカメラを扱うための変数
	CvCapture _Capture;
	CvWindow _Window;
	CvScalar _PointColour;
	CvScalar pointhsv;
	IplImage _Frame;
	// Webカメラ画像中のPSMoveの半径と中心および
	// Webカメラ画像から求めたPSMoveの3次元位置
	float _Radius;
	CvPoint2D32f _Center;
	public static double Sx = 0;
	public static double Sy = 0;
	public static double Sz = 0;
	const double CTR_PARAM = 0.02;

	const double MAX_R = 255;
	const double MAX_G = 255;
	const double MAX_B = 255;

	// Use this for initialization
	void Start()
	{
		//CvCapture capture;
		// Webカメラを使うという宣言
		_Capture = Cv.CreateCameraCapture(0);
		// 横幅と縦幅の設定
		Cv.SetCaptureProperty(_Capture, CaptureProperty.FrameWidth, CAPTURE_WIDTH);
		Cv.SetCaptureProperty(_Capture, CaptureProperty.FrameHeight, CAPTURE_HEIGHT);
		// Webカメラからフレーム取得
		//IplImage frame;
		_Frame = Cv.QueryFrame(_Capture);
		// Unity上に縦幅と横幅のフレームサイズをコンソール出力
		Debug.Log("width:" + _Frame.Width + " height:" + _Frame.Height);
		// 動画内のRGBを取るための変数を初期化
		_Window = new CvWindow("GetRGBWindow", _Frame);
		_Window.OnMouseCallback += new CvMouseCallback(GetClickedPixelRgb);
		// 領域分割に使う色をHSVで指定
		pointhsv = new CvScalar(0.0, 0.0, 0.0);
		// ウィンドウの名前設定
		Cv.NamedWindow("Original");
		Cv.NamedWindow("STEP1:Smoothing");
		Cv.NamedWindow("STEP2:HSV");
		Cv.NamedWindow("STEP3:Segmentation");
		Cv.NamedWindow("STEP4:Morphology");
		Cv.NamedWindow("STEP5:Detected");
		Cv.NamedWindow ("window");

	}

	// Update is called once per frame
	void Update()
	{
		// Webカメラから1フレーム分の画像を取得
		_Frame = Cv.QueryFrame(_Capture);
		// Webカメラ画像をコピーしてSTEP1の入力画像に使用
		IplImage img = _Frame.Clone();
		// STEP1用変数
		IplImage smoothed = new IplImage(img.Size, BitDepth.U8, 3);
		// STEP2用変数
		IplImage hsv = new IplImage(img.Size, BitDepth.U8, 3);
		// STEP3用変数
		IplImage segmented = new IplImage(img.Size, BitDepth.U8, 1);
		CvMemStorage storage = new CvMemStorage();
		// STEP4用変数
		IplImage imgTmp = new IplImage(img.Size, BitDepth.U8, 1);
		IplImage morphology = new IplImage(img.Size, BitDepth.U8, 1);
		// STEP5用変数
		CvSeq<CvPoint> contours;
		IplImage detected = _Frame.Clone();

		// STEP1: ノイズ除去
		Cv.Smooth(img, smoothed, SmoothType.Blur,1);
		//Cv.ShowImage("window",smoothed);
		//Cv.Smooth(smoothed, smoothed, SmoothType.Gaussian,1);

		// STEP2: 色をRGBからHSVに変換
		Cv.CvtColor(smoothed, hsv ,ColorConversion.BgrToHsv);
		//Cv.ShowImage("window",hsv);
		// STEP3: 領域分割
		storage.Clear();
		Cv.InRangeS (hsv,
			new CvScalar ((pointhsv.Val0) - RANGE_H,
				(pointhsv.Val1) - RANGE_S,
				(pointhsv.Val2) - RANGE_V),
			new CvScalar ((pointhsv.Val0) + RANGE_H,
				(pointhsv.Val1) + RANGE_S,
				(pointhsv.Val2) + RANGE_V),
			segmented);
		//Cv.ShowImage("window",segmented);
		// STEP4: ノイズ除去

		Cv.Dilate (segmented, imgTmp);
		Cv.Erode (imgTmp,imgTmp);

		Cv.Erode (imgTmp,imgTmp);
		Cv.Dilate (imgTmp, morphology);

		//Cv.ShowImage("window",morphology);

		// STEP5: 円の検出
		Cv.FindContours (morphology, storage, out contours,
			CvContour.SizeOf, ContourRetrieval.Tree,
			ContourChain.ApproxNone);

		if (contours == null) {
			Debug.Log ("PSMove is not detected");
		}
		else {
			contours = Cv.ApproxPoly (contours, CvContour.SizeOf,
				storage, ApproxPolyMethod.DP,
				Cv.ContourPerimeter (contours) * CTR_PARAM, true);

			Cv.DrawContours (morphology, contours,
				new CvScalar (MAX_G, 0, 0), new CvScalar (0, MAX_B, 0), 3, -1);

			Cv.MinEnclosingCircle (contours, out _Center, out _Radius);
			Cv.DrawCircle(morphology, _Center, 2, new CvScalar(0, MAX_B, 0));

			// STEP6: 画像をウィンドウに出力
			Sz = fx * SPHERE_R / _Radius;

			Sx = -((_Center.X - ux) * Sz) / fx;
			Sy = -((_Center.Y - uy) * Sz) / fy;
		}

		_Window.ShowImage(_Frame);
		Cv.ShowImage("Original", img);
		Cv.ShowImage("STEP1:Smoothing", smoothed);
		Cv.ShowImage("STEP2:HSV", hsv);
		Cv.ShowImage("STEP3:Segmentation", segmented);
		Cv.ShowImage("STEP4:Morphology", morphology);
		Cv.ShowImage("STEP5:Detected", detected);
	}

	void GetClickedPixelRgb(MouseEvent me, int x, int y, MouseEvent me2)
	{
		if (me == MouseEvent.LButtonDown) {
			using (IplImage frameColor = Cv.RetrieveFrame(_Capture)) {
				if (frameColor != null) {
					_PointColour = Cv.Get2D(frameColor, y, x);
					Cv.CvtColor(frameColor, frameColor, ColorConversion.BgrToHsv);
					pointhsv = Cv.Get2D(frameColor, y, x);

					//Debug.Log("R : " + _PointColour.Val2 + " G : " + _PointColour.Val1 + " B : " + _PointColour.Val0);
					//Debug.Log("H : " + (_PointHsv.Val0 * 2) + " S : " + _PointHsv.Val1 + " V : " + _PointHsv.Val2);
				}

			}
		}
	}

	// 終了処理
	void OnDestroy()
	{
		Cv.DestroyAllWindows();
		Cv.ReleaseCapture(_Capture);
	}
}