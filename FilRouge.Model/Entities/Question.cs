﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilRouge.Model.Entities
{
    public partial class Question
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        [MaxLength(200)]
        [Required]
        public string Content { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        [Required]
        public bool IsActive { get; set; }
        //Clés étrangères
        [ForeignKey("TypeQuestion")]
        public int QuestionTypeId { get; set; }
        [ForeignKey("Technology")]
        public int TechnologyId { get; set; }
        [ForeignKey("Difficulty")]
        public int DifficultyId { get; set; }
        #endregion
        #region Association

        public virtual ICollection<Quizz> Quizzs { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
        public virtual Technology Technology{ get; set; }
        public virtual Difficulty Difficulty{ get; set; }
        public virtual TypeQuestion TypeQuestion { get; set; }
        #endregion
    }
}
