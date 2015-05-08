using Microsoft.WindowsAzure.Mobile.Service;

namespace LottoSimulatorService.DataObjects
{
    public class Lotto : EntityData
    {
        public string WinningRow { get; set; }

        public long Cost { get; set; }

        public string playerName { get; set; }

       public double longitude { get; set; }

        public double latitude { get; set; }
    }
}