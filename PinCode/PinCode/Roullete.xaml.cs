using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Threading;
using Android.Graphics;

namespace PinCode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Roullete : ContentPage
	{
        int levelNum = 1;
        int codeValue1;
        int codeValue2;
        int codeValue3;
        int codeValue4;
        int cnt=0;
        int pressCnt = 0;
        int faceValue1;   
        int faceValue2;
        int faceValue3;
        int faceValue4;
        int faceValue5;
        int faceValue6;
        int faceValue7;
        int faceValue8;
        int faceValue9;
        int clickCnt = 0;
        int levelSpeed;
     

        System.Timers.Timer timer = new System.Timers.Timer();

        private void RoulleteBoard()
        {

            lblAnswer.Text = " " + levelNum;

            Random random = new Random();
            codeValue1 = random.Next(1, 10);
            codeValue2 = random.Next(1, 10);
            codeValue3 = random.Next(1, 10);
            codeValue4 = random.Next(1, 10);

            lblcode1.Text = "   " + codeValue1;
            lblcode2.Text = "   " + codeValue2;
            lblcode3.Text = "   " + codeValue3;
            lblcode4.Text = "   " + codeValue4;

            if (levelNum == 1)
            {
                levelSpeed = 1;
            }else if (levelNum == 2)
            {
                levelSpeed = 3;
            }else if (levelNum == 3)
            {
                levelSpeed = 4;
            }else if (levelNum == 4)
            {
                levelSpeed = 6;
            }else if (levelNum == 5)
            {
                pressButton.IsVisible = false;
                ReplayButton.IsVisible = true;
                championship.IsVisible = true;
            }
        }

        public void SleepTime()
        {
            timer.AutoReset = false;
            timer.Interval = 500;
            timer.Elapsed += FailSleepTime;
            timer.Start();
        }

        public void FailSleepTime(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                timer.Enabled = false;
            });

        }

        private void DefaultSettings()
        {
            var hi5 = typeof(MainPage);
            string hiFive = "PinCode.Assets.Images.hifive.png";
            hi.Source = ImageSource.FromResource(hiFive, hi5);

            var bad_1 = typeof(MainPage);
            string badImage_1 = "PinCode.Assets.Images.bad2.png";
            bad1.Source = ImageSource.FromResource(badImage_1, bad_1);

            var bad_2 = typeof(MainPage);
            string badImage_2 = "PinCode.Assets.Images.bad2.png";
            bad2.Source = ImageSource.FromResource(badImage_2, bad_2);

            var bad_3 = typeof(MainPage);
            string badImage_3 = "PinCode.Assets.Images.bad2.png";
            bad3.Source = ImageSource.FromResource(badImage_3, bad_3);

            var bad_4 = typeof(MainPage);
            string badImage_4 = "PinCode.Assets.Images.bad2.png";
            bad4.Source = ImageSource.FromResource(badImage_4, bad_4);

            var good_1 = typeof(MainPage);
            string goodImage_1 = "PinCode.Assets.Images.swoosh.png";
            good1.Source = ImageSource.FromResource(goodImage_1, good_1);

            var good_2 = typeof(MainPage);
            string goodImage_2 = "PinCode.Assets.Images.swoosh.png";
            good2.Source = ImageSource.FromResource(goodImage_2, good_2);

            var good_3 = typeof(MainPage);
            string goodImage_3 = "PinCode.Assets.Images.swoosh.png";
            good3.Source = ImageSource.FromResource(goodImage_3, good_3);

            var good_4 = typeof(MainPage);
            string goodImage_4 = "PinCode.Assets.Images.swoosh.png";
            good4.Source = ImageSource.FromResource(goodImage_4, good_4);

           
            var winner = typeof(MainPage);
            string winnerImage = "PinCode.Assets.Images.winner.gif";
            championship.Source = ImageSource.FromResource(winnerImage, winner);
        }

        SKPaint blackfillpain = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
            
        };

        SKPaint GreenStroke = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,

        };

        SKPaint RedStroke = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
           TextSize= 35.0f,
            Color = SKColors.Red,
            StrokeWidth = 2,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true,

        };

        public Roullete()
        {
            
            InitializeComponent();
            DefaultSettings();
            RoulleteBoard();

            Device.StartTimer(TimeSpan.FromSeconds(1f/60), () =>
             {
                 canvasView.InvalidateSurface();
                 return true;
             });

        }

        public void NextLevel()
        {
            clickCnt++;
            if (clickCnt == 4)
            {
                levelNum++;
                clickCnt = 0;
                SleepTime();
                RoulleteBoard();
                bad1.IsVisible = false;
                bad2.IsVisible = false;
                bad3.IsVisible = false;
                bad4.IsVisible = false;
                good1.IsVisible = false;
                good2.IsVisible = false;
                good3.IsVisible = false;
                good4.IsVisible = false;
                pressCnt=0;
            }
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.Silver);

            int width = e.Info.Width;
            int height = e.Info.Height;

            //set transform
            canvas.Translate(width / 2, height / 2);
            canvas.Scale(width /214f);

            //current Time
            DateTime dateTime = DateTime.Now;

            //draw Circle
           // canvas.DrawCircle(0, 0, 100, blackfillpain);

            canvas.DrawText("1", 0, -90, RedStroke);
            canvas.DrawText("2", 60, -60, RedStroke);
            canvas.DrawText("6", -60, 65, RedStroke);
            canvas.DrawText("5", 0,  92, RedStroke);
            canvas.DrawText("3", 90, 0, RedStroke);
            canvas.DrawText("4", 60, 60, RedStroke);
            canvas.DrawText("9", -60, -60, RedStroke);
            canvas.DrawText("7", -90, 30, RedStroke);
            canvas.DrawText("8", -90, 0, RedStroke);

         

            //moving arm
            canvas.Save();
            cnt= cnt+levelSpeed;
           // seconds = dateTime.AddMilliseconds(500); /*+ dateTime.Millisecond / 1000f*/;
            canvas.RotateDegrees(cnt);
            if (cnt >= 360)
            {
                cnt = 0;
            }
            faceValue1 = 0;
            faceValue2 = 0;
            faceValue3 = 0;
            faceValue4 = 0;
            faceValue5 = 0;
            faceValue6 = 0;
            faceValue7 = 0;
            faceValue8 = 0;
            faceValue9 = 0;


            //Get the facevalue of the numbers
            if (cnt >= 0 && cnt <= 20)
            {
                faceValue1 = 1;
                
            }
            else if (cnt >= 30 && cnt <= 60)
            {
                faceValue2 = 2;
            }
            else if(cnt >= 80 && cnt <= 98)
            {
                faceValue3 = 3;
            }
            else if (cnt >= 115 && cnt <= 140)
            {
                faceValue4 = 4;
            }
            else if (cnt >= 170 && cnt <= 185)
            {
                faceValue5 = 5;
               // System.Diagnostics.Debug.WriteLine("faceValue5 : " + faceValue5);
            }
            else if (cnt >= 215 && cnt <= 239)
            {
                faceValue6 = 6;
            }
            else if (cnt >= 250 && cnt <= 270)
            {
                faceValue7 = 7;
            }
            else if (cnt >= 272 && cnt <= 295)
            {
                faceValue8 = 8;
            }
            else if (cnt >= 318 && cnt <= 345)
            {
                faceValue9 = 9;
            }

           // System.Diagnostics.Debug.WriteLine("Face value eight : " + faceValue8);
            GreenStroke.StrokeWidth = 2;
            canvas.DrawLine(0, 10, 0, -80, GreenStroke);
            canvas.Restore();

        }

        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void PressButton_Clicked(object sender, EventArgs e)
        {
            //increase presscont
            pressCnt++;
            switch (pressCnt)
            {
                case 1:
                    if (codeValue1== faceValue1)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue2)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue3)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue4)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue5)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue6)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue7)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue8)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue1 == faceValue9)
                    {
                        good1.IsVisible = true;
                        NextLevel();
                    }
                    else
                    {
                        bad1.IsVisible = true;
                        SleepTime();
                        Navigation.PushAsync(new Roullete());
                    }
                    break;
                case 2:
                    if (codeValue2 == faceValue1)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue2)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue3)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue4)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue5)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue6)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue7)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue8)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue2 == faceValue9)
                    {
                        good2.IsVisible = true;
                        NextLevel();
                    }
                    else
                    {
                        bad2.IsVisible = true;
                        SleepTime();
                        Navigation.PushAsync(new Roullete());
                    }
                    break;
                case 3:
                    if (codeValue3 == faceValue1)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue2)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue3)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue4)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue5)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue6)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue7)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue8)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue3 == faceValue9)
                    {
                        good3.IsVisible = true;
                        NextLevel();
                    }
                    else
                    {
                        bad3.IsVisible = true;
                        SleepTime();
                        Navigation.PushAsync(new Roullete());
                    }
                    break;
                case 4:
                    if (codeValue4 == faceValue1)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue2)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue3)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue4)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue5)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue6)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue7)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue8)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else if (codeValue4 == faceValue9)
                    {
                        good4.IsVisible = true;
                        NextLevel();
                    }
                    else
                    {
                        bad4.IsVisible = true;
                        SleepTime();
                        Navigation.PushAsync(new Roullete());
                    }
                    break;
                default:
                    break;
            }
 
        }

        private void ReplayButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Roullete());
        }
    }
}