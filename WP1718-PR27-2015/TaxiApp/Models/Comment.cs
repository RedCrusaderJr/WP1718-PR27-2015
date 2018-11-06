using System.ComponentModel.DataAnnotations;
using TaxiApp.Common;

namespace TaxiApp.Models
{
    public class Comment
    {
        #region Properties
        [Key]
        public string CommentID { get; private set; }
        public string Description { get; set; }
        public string TaxiDriveRate { get; set; }
        public IUser CommentOwner { get; set; }
        public TaxiDrive CommentedTaxiDrive { get; set; }
        #endregion

        public Comment() { }

        public Comment(string id)
        {
            CommentID = id;
        }

        public Comment(Comment c)
        {
            CommentID = c.CommentID;
            Description = c.Description;
            TaxiDriveRate = c.TaxiDriveRate;
            CommentOwner = c.CommentOwner;
            CommentedTaxiDrive = c.CommentedTaxiDrive;
        }
    }
}