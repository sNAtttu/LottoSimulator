using LottoSimulator.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSimulator.Model
{
    public class LottoModel
    {

        private int oneBall;

        public int OneBall
        {
            get { return oneBall; }
            set { oneBall = value; }
        }
        private int twoBall;

        public int TwoBall
        {
            get { return twoBall; }
            set { twoBall = value; }
        }
        private int threeBall;

        public int ThreeBall
        {
            get { return threeBall; }
            set { threeBall = value; }
        }
        private int fourBall;

        public int FourBall
        {
            get { return fourBall; }
            set { fourBall = value; }
        }
        private int fiveBall;

        public int FiveBall
        {
            get { return fiveBall; }
            set { fiveBall = value; }
        }
        private int sixBall;

        public int SixBall
        {
            get { return sixBall; }
            set { sixBall = value; }
        }
        private int sevenBall;

        public int SevenBall
        {
            get { return sevenBall; }
            set { sevenBall = value; }
        }

        private int extraOne;

        public int ExtraOne
        {
            get { return extraOne; }
            set { extraOne = value; }
        }
        private int extraTwo;

        public int ExtraTwo
        {
            get { return extraTwo; }
            set { extraTwo = value; }
        }

        public LottoModel()
        {
            OneBall = 1;
            TwoBall = 2;
            ThreeBall = 3;
            FourBall = 4;
            FiveBall = 5;
            SixBall = 6;
            SevenBall = 7;
            ExtraOne = 8;
            ExtraTwo = 9;
        }

        public override string ToString()
        {
            return OneBall.ToString() + " "
                + TwoBall.ToString() + " "
                + ThreeBall.ToString() + " "
                + FourBall.ToString() + " "
                + FiveBall.ToString() + " "
                + SixBall.ToString() + " "
                + SevenBall.ToString();
        }
    }
}
