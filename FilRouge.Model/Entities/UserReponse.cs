using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace FilRouge.Model.Entities
{
    public partial class UserResponse
    {
        #region Proporties
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Quizz")]
        public int QuizzId { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Response")]
        public int ResponseId { get; set; }
        [MaxLength(1000)]
        public string Valeur { get; set; }
        #endregion
        #region Association
        public Quizz Quizz { get; set; }
        public Response Response{ get; set; }
        #endregion
    }
}
