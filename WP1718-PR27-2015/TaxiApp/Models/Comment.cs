using TaxiApp.Common;

namespace TaxiApp.Models
{
    public class Comment
    {
        public string CommentID { get; set; }
        public string Description { get; set; }
        public string TaxiDriveRate { get; set; }
        public IUser CommentOwner { get; set; }
        public TaxiDrive CommentedTaxiDrive { get; set; }
    }
}