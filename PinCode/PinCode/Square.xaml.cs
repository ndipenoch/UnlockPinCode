using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Timer = System.Timers.Timer;

namespace PinCode
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Square : ContentPage
    {
        int levelNum = 1;
        int codeValue1;
        int codeValue2;
        int codeValue3;
        int codeValue4;
        int rndBtnNum;
        int rdnSquareBtn1;
        int rdnSquareBtn2;
        int rdnSquareBtn3;
        int rdnSquareBtn4;
        int rdnSquareBtn5;
        int rdnSquareBtn6;
        int rdnSquareBtn7;
        int rdnSquareBtn8;
        int rdnSquareBtn9;
        int clickCnt = 1;
        int timerIntervalL1 = 1000;
        int sleepTimeLevel1 = 1000;
        int timerIntervalL2 = 1000;
        int sleepTimeLevel2 = 800;
        int timerIntervalL3 = 1000;
        int sleepTimeLevel3 = 600;
        int timerIntervalL4 = 1000;
        int sleepTimeLevel4 = 400;
        int interval;
        float BestSquareScore=0;
        System.Timers.Timer timer = new System.Timers.Timer();

        public Square()
        {
            InitializeComponent();
            BackgroundImage = "Assets/background.png";
            SquareBoard();
        }

        private void SquareBoard()
        {
            if (App.IsLogin == true)
            {
                S_ScoreLabel.IsVisible = true;
                WecomeMsg.IsVisible = true;
            }
                lblAnswer.Text = " " + levelNum;
                best_score.Text = "" + App.SquareBestScores;
                welcome_Msg.Text = App.CurrentUser + "!";


            Random random = new Random();
            codeValue1 = random.Next(1, 10);
            codeValue2 = random.Next(1, 10);
            codeValue3 = random.Next(1, 10);
            codeValue4 = random.Next(1, 10);

            lblcode1.Text = "   " + codeValue1;
            lblcode2.Text = "   " + codeValue2;
            lblcode3.Text = "   " + codeValue3;
            lblcode4.Text = "   " + codeValue4;
        }


        public void MoveToNextLevle()
        {
            if (clickCnt == 5 && levelNum == 1)
            {
                NextLevelSettings();
            }
            else if (clickCnt == 5 && levelNum == 2)
            {
                NextLevelSettings();
            }
            else if (clickCnt == 5 && levelNum == 3)
            {
                NextLevelSettings();
            }
            else if (clickCnt == 5 && levelNum == 4)
            {
                championship.IsVisible = true;
                timer.Stop();
                ReplayButton.IsVisible = true;

            }

            if (BestSquareScore > App.SquareBestScores && App.IsLogin==true)
            {
                App.SquareBestScores = BestSquareScore;
           
                    FirebaseDAO fb = new FirebaseDAO();
                    fb.UpdateBoardScores();
            }
            best_score.Text = "" + App.SquareBestScores;
        }

        public void SettingsLevelStage_1()
        {
            allIsGood.IsVisible = true;
            good1.IsVisible = true;
            clickCnt++;
            BestSquareScore += 10;
            
        }

        public void SettingsLevelStage_2()
        {
            allIsGood.IsVisible = true;
            good2.IsVisible = true;
            clickCnt++;
            BestSquareScore += 10;

        }

        public void SettingsLevelStage_3()
        {
            allIsGood.IsVisible = true;
            good3.IsVisible = true;
            clickCnt++;
            BestSquareScore += 10;
 
        }

        public void SettingsLevelStage_4()
        {
            allIsGood.IsVisible = true;
            good4.IsVisible = true;
            clickCnt++;
            BestSquareScore += 10;
 
        }

        public void NextLevelSettings()
        {
            levelNum++;
            allIsGood.IsVisible = false;
            bad1.IsVisible = false;
            bad2.IsVisible = false;
            bad3.IsVisible = false;
            bad4.IsVisible = false;
            good1.IsVisible = false;
            good2.IsVisible = false;
            good3.IsVisible = false;
            good4.IsVisible = false;
            nextLevel.IsVisible = true;
            timer.Stop();
            SquareBoard();
            InitTimer();
            clickCnt = 1;
        }


        public void FailAndRestart()
        {
            timer.AutoReset = false;
            timer.Interval = 250;
            timer.Elapsed += FailSleepTime;
            timer.Start();
        }

        public void FailSleepTime(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                timer.Enabled = false;
                Navigation.PushAsync(new Square());
            });

        }

        public void InitTimer()
        {

            if (levelNum == 1)
            {
                interval = timerIntervalL1;
            }
            else if (levelNum == 2)
            {
                interval = timerIntervalL2;
            }
            else if (levelNum == 3)
            {
                interval = timerIntervalL3;
            }
            else if (levelNum == 4)
            {
                interval = timerIntervalL4;
            }

            timer.Interval = interval;
            timer.Elapsed += ButtonsEnable;
            timer.Start();

        }

        private void ButtonsEnable(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {
                allIsGood.IsVisible = false;
                nextLevel.IsVisible = false;
            });

            Random rnd = new Random();
            rndBtnNum = rnd.Next(1, 10);

            switch (rndBtnNum)
            {
                case 1:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn1 = rndom.Next(1, 10);
                        SquareBtn1.Text = rdnSquareBtn1.ToString();
                        SquareBtn1.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn1.IsEnabled = false;
                    });

                    break;
                case 2:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn2 = rndom.Next(1, 10);
                        SquareBtn2.Text = rdnSquareBtn2.ToString();
                        SquareBtn2.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn2.IsEnabled = false;
                    });
                    break;
                case 3:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn3 = rndom.Next(1, 10);
                        SquareBtn3.Text = rdnSquareBtn3.ToString();
                        SquareBtn3.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn3.IsEnabled = false;
                    });
                    break;
                case 4:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn4 = rndom.Next(1, 10);
                        SquareBtn4.Text = rdnSquareBtn4.ToString();
                        SquareBtn4.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn4.IsEnabled = false;
                    });
                    break;
                case 5:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn5 = rndom.Next(1, 10);
                        SquareBtn5.Text = rdnSquareBtn5.ToString();
                        SquareBtn5.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn5.IsEnabled = false;
                    });
                    break;
                case 6:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn6 = rndom.Next(1, 10);
                        SquareBtn6.Text = rdnSquareBtn6.ToString();
                        SquareBtn6.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn6.IsEnabled = false;
                    });
                    break;
                case 7:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn7 = rndom.Next(1, 10);
                        SquareBtn7.Text = rdnSquareBtn7.ToString();
                        SquareBtn7.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn7.IsEnabled = false;
                    });
                    break;
                case 8:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn8 = rndom.Next(1, 10);
                        SquareBtn8.Text = rdnSquareBtn8.ToString();
                        SquareBtn8.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn8.IsEnabled = false;
                    });
                    break;
                case 9:
                    Device.BeginInvokeOnMainThread(() => {
                        Random rndom = new Random();
                        rdnSquareBtn9 = rndom.Next(1, 10);
                        SquareBtn9.Text = rdnSquareBtn9.ToString();
                        SquareBtn9.IsEnabled = true;
                    });

                    if (levelNum == 1)
                    {
                        Thread.Sleep(sleepTimeLevel1);
                    }
                    else if (levelNum == 2)
                    {
                        Thread.Sleep(sleepTimeLevel2);
                    }
                    else if (levelNum == 3)
                    {
                        Thread.Sleep(sleepTimeLevel3);
                    }
                    else if (levelNum == 4)
                    {
                        Thread.Sleep(sleepTimeLevel4);
                    }

                    Device.BeginInvokeOnMainThread(() => {
                        SquareBtn9.IsEnabled = false;
                    });
                    break;

            }
        }

        private void HomeBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void SquareBtn1_Clicked(object sender, EventArgs e)
        {
            //Convert.ToInt32(SquareBtn1.Text)
            if (rdnSquareBtn1 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn1 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn1 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn1 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn2_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn2 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn2 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn2 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn2 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn3_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn3 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn3 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn3 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn3 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn4_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn4 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn4 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn4 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn4 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn5_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn5 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn5 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn5 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn5 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn6_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn6 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn6 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn6 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn6 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn7_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn7 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn7 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn7 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn7 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn8_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn8 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn8 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn8 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn8 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }

        private void SquareBtn9_Clicked(object sender, EventArgs e)
        {
            if (rdnSquareBtn9 == codeValue1 && clickCnt == 1)
            {
                SettingsLevelStage_1();
            }
            else if (rdnSquareBtn9 == codeValue2 && clickCnt == 2)
            {
                SettingsLevelStage_2();
            }
            else if (rdnSquareBtn9 == codeValue3 && clickCnt == 3)
            {
                SettingsLevelStage_3();
            }
            else if (rdnSquareBtn9 == codeValue4 && clickCnt == 4)
            {
                SettingsLevelStage_4();
            }
            else if (clickCnt == 1)
            {
                bad1.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 2)
            {
                bad2.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 3)
            {
                bad3.IsVisible = true;
                FailAndRestart();
            }
            else if (clickCnt == 4)
            {
                bad4.IsVisible = true;
                FailAndRestart();
            }

            MoveToNextLevle();
        }




        public void PlayButton_Clicked(object sender, EventArgs e)
        {


            playButton.IsVisible = false;

            var good = typeof(MainPage);
            string goodGif = "PinCode.Assets.Images.claps.gif";
            allIsGood.Source = ImageSource.FromResource(goodGif, good);

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

            var nextLevel1 = typeof(MainPage);
            string nextLevelImage = "PinCode.Assets.Images.next_level.gif";
            nextLevel.Source = ImageSource.FromResource(nextLevelImage, nextLevel1);

            var winner = typeof(MainPage);
            string winnerImage = "PinCode.Assets.Images.winner.gif";
            championship.Source = ImageSource.FromResource(winnerImage, winner);

            InitTimer();


        }

        private void ReplayButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Square());
        }
    }
}