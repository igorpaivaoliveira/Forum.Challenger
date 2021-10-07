using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Challenger.Web.Models
{
    public class TopicsModel
    {
        [Display(Name = "Code")]
        public int CdTopic { get; set; }
        [Display(Name = "Posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DtTopic { get; set; }
        [Display(Name = "Edition")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DtTopicLastEdition { get; set; }
        [Display(Name = "Title")]
        [MaxLength(50, ErrorMessage = "Max legth 50")]
        public string DsTitle { get; set; }
        [Display(Name = "Text")]
        public string DsText { get; set; }
        public int CdPerson { get; set; }
        [Display(Name = "Active")]
        public bool FlActive { get; set; }
    }
}
