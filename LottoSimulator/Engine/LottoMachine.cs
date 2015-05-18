using LottoSimulator.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottoSimulator.Engine
{
    class LottoMachine
    {

        public LottoModel GenerateNumbers(LottoModel lotto)
        {
            Random rng = new Random();
            int numbersLeft = 39;
            List<int> usedNumbers = new List<int>();
            List<int> winningNumbers = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                int temp = rng.Next(1,numbersLeft);
                if(!usedNumbers.Contains(temp))
                {
                    winningNumbers.Add(temp);
                    usedNumbers.Add(temp);
                    numbersLeft--;     
                }
                else
                {
                    i--;
                }
            }

            winningNumbers.Sort();
            lotto.OneBall = winningNumbers[0];
            lotto.TwoBall = winningNumbers[1];
            lotto.ThreeBall = winningNumbers[2];
            lotto.FourBall = winningNumbers[3];
            lotto.FiveBall = winningNumbers[4];
            lotto.SixBall = winningNumbers[5];
            lotto.SevenBall = winningNumbers[6];
            lotto.ExtraOne = winningNumbers[7];
            lotto.ExtraTwo = winningNumbers[8];

            return lotto;
        }

        public int[] checkWinnings(LottoModel playerLotto, LottoModel generatedLotto)
        {
            List<int> playerNumbers = new List<int>();
            List<int> cpuNumbers = new List<int>();
            List<int> extraNumbers = new List<int>();

            int[] winHits = new int[2];
            int countHits = 0;
            int extraHits = 0;

            playerNumbers.Add(playerLotto.OneBall);
            playerNumbers.Add(playerLotto.TwoBall);
            playerNumbers.Add(playerLotto.ThreeBall);
            playerNumbers.Add(playerLotto.FourBall);
            playerNumbers.Add(playerLotto.FiveBall);
            playerNumbers.Add(playerLotto.SixBall);
            playerNumbers.Add(playerLotto.SevenBall);

            cpuNumbers.Add(generatedLotto.OneBall);
            cpuNumbers.Add(generatedLotto.TwoBall);
            cpuNumbers.Add(generatedLotto.ThreeBall);
            cpuNumbers.Add(generatedLotto.FourBall);
            cpuNumbers.Add(generatedLotto.FiveBall);
            cpuNumbers.Add(generatedLotto.SixBall);
            cpuNumbers.Add(generatedLotto.SevenBall);

            extraNumbers.Add(generatedLotto.ExtraOne);
            extraNumbers.Add(generatedLotto.ExtraTwo);

            foreach (int playerNum in playerNumbers)
            {
                if (cpuNumbers.Contains(playerNum))
                {
                    countHits++;
                }
            }

            foreach (int playerNum in playerNumbers)
            {
                if (extraNumbers.Contains(playerNum))
                    extraHits++;
            }

            winHits[0] = countHits;
            winHits[1] = extraHits;

            return winHits;
        }

        public int GetWinnings(int hits, int extraHits)
        {
            int sum = 0;

            switch (hits)
            {
                case 3:
                    if (extraHits == 1)
                        sum = 2;
                    if (extraHits == 2)
                        sum = 5;
                    break;
                case 4:
                    sum = 10;
                    if (extraHits == 1)
                        sum = 15;
                    if (extraHits == 2)
                        sum = 20;
                    break;
                case 5:
                    sum = 50;
                    if (extraHits == 1)
                        sum = 100;
                    if (extraHits == 2)
                        sum = 150;
                    break;
                case 6:
                    sum = 2000;
                    if (extraHits == 1)
                        sum = 5000;
                    if (extraHits == 2)
                        sum = 50000;
                    break;
                case 7:
                    sum = 5000000;
                    break;

            }

            return sum;
        }

    }
}
