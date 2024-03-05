using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LoginDC6.Server.Entities
{
    [Table("TblPageNames", Schema = "Inf")]
    public class TblPageNames
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageID { get; set; }
        [MaxLength(1500), Required]
        public string MnuTitle { get; set; }
        [MaxLength(1500)]
        public string? MnuLinkTitle { get; set; }
        public bool IsVisible { get; set; }
        public int MnuOrderNumber { get; set; }
        public int PageParentID { get; set; }
    }

    [Table("TblUsersPermissions", Schema = "Usr")]
    public class TblUsersPermissions
    {
        public decimal PageAccessPermId { get; set; }
        public int PageId { get; set; }
        [Required, MaxLength(450)]
        public string PUserID { get; set; }
        public string PageCrcPerm { get; set; }
        public virtual TblPageNames? PageNames { get; set; }
        public virtual ApplicationUser AppUsers { get; set; }
    }

    [Table("TblPagesTexts", Schema = "Inf")]
    public class TblPagesTexts
    {
        [Key]
        public int TextId { get; set; }
        [Required]
        [MaxLength(250)]
        public string ShortText { get; set; }
        [Required, MaxLength(3000)]
        public string LongText { get; set; }
        [Required]
        [MaxLength(150)]
        public string PageName { get; set; }
        [Required]
        public DateTime InsertDate { get; set; }
        [Required, MaxLength(450)]
        public string InsertedById { get; set; }
        [ForeignKey("InsertedById")]
        public virtual ApplicationUser InsertedBy { get; set; }
        public virtual ICollection<TblPagesTextsEditHistory> EditHistory { get; set; }
    }

    [Table("TblPagesTextsEditHistory", Schema = "Inf")]
    public class TblPagesTextsEditHistory
    {
        [Key]
        public int TextHistId { get; set; }
        [Required]
        public int TextId { get; set; }
        [Required]
        public DateTime EditDate { get; set; }
        [Required, MaxLength(450)]
        public String EditedById { get; set; }
        [Required, MaxLength(250)]
        public string ShortTextPre { get; set; }
        [Required, MaxLength(3000)]
        public string LongTextPre { get; set; }
        [Required, MaxLength(250)]
        public string ShortTextNew { get; set; }
        [Required, MaxLength(3000)]
        public string LongTextNew { get; set; }
        [ForeignKey("TextId")]
        public virtual TblPagesTexts PagesTexts { get; set; }
    }
}