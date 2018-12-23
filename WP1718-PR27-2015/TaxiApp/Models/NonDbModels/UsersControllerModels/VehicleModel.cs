using TaxiApp.Common;

namespace TaxiApp.Models.NonDbModels.UsersControllerModels
{
    public class VehicleModel
    {
        public string VehicleID { get; set; }
        public string LicencePlateNo { get; set; }
        public string ProductionYear { get; set; }
        public EVehicleType VehicleType { get; set; }
        public string VehicleDriverID { get; set; }
    }
}