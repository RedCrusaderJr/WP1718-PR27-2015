using TaxiApp.Common;

namespace TaxiApp.Models.NonDbModels.UsersControllerModels
{
    public class TaxiDriveModel
    {
        public string TaxiDriveID { get; set; }
        public EVehicleType VehicleType { get; set; }
        public EDriveStatus DriveStatus { get; set; }
        public int Amount { get; set; }
        public string TaxiDriveDriverID { get; set; }
        public string TaxiDriveCustomerID { get; set; }
        public string TaxiDriveDispatcherID { get; set; }
        public string TaxiDriveCommentID { get; set; }
        public string TaxiDriveStartingLocationID { get; set; }
        public string TaxiDriveDestinationID { get; set; }
    }
}