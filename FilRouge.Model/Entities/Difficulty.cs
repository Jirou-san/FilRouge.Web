using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilRouge.Model.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Difficulty
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DifficultyId { get; set; }
        [Required]
        public string DifficultyName { get; set; }
        #endregion
        #region Associations
        public virtual List<Quizz> Quizzs { get; set; }
        public virtual List<Question> Questions { get; set; }
        #endregion
    }
}
