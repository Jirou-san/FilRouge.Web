using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilRouge.Model.Entities
{
    public partial class DifficultyMaster
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiffMasterId { get; set; }
        [MaxLength(15)]
        [Required]
        public string DiffMasterName { get; set; }
        [Required]
        public decimal Rate { get; set; }
        //Clé étrangère
        [ForeignKey("RefDifficultyMaster")]
        public int DifficultyId { get; set; }
        #endregion
        #region Associations
        public virtual List<Quizz> Quizzs { get; set; }
        public virtual  List<Question> Questions { get; set; }
        public virtual DifficultyMaster RefDifficultyMaster { get; set; }
        #endregion
    }
}