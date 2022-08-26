using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suls.Data
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(800)]
        public string Code { get; set; }
        [Required]
        public int AchievedResult  { get; set; }
        [Required]
        public DateTime CreatedOn   { get; set; }
        public  virtual Problem Problem { get; set; }
        public string ProblemId { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
