using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginDC6.Server.Entities
{
    [Table("TblBaseInformation", Schema = "Bse")]
    public abstract class TblBaseInformation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long BsInfoID { get; set; }
        [MaxLength(250), Required]
        public string BsInfoValue { get; set; }
        [MaxLength(250)]
        public string BsInfoDescription { get; set; }
        [MaxLength(3500)]
        public string BsInfoFurtherInfo { get; set; }
        [Required]
        public bool BlnIsActive { get; set; }
        [Required]
        public DateTime DateOfInsert { get; set; }
        [Required]
        public int BsInfoTypeID { get; set; }
    }

    /// <summary>
    /// سالهای مالی
    /// </summary>
    public class TblFiscalPeriodYear : TblBaseInformation
    {
    }
}
