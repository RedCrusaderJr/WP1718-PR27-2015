namespace TaxiApp.Models.NonDbModels.UsersControllerModels
{
    public class CommentModel
    {
        public string CommentID { get; set; }
        public string Description { get; set; }
        public string TaxiDriveRate { get; set; }
        public string CommentOwnerAdminID { get; set; }
        public string CommentOwnerDriverID { get; set; }
        public string CommentOwnerCustomerID { get; set; }
        public string CommentedTaxiDriveID { get; set; }
    }
}