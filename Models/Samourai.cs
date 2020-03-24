using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module6.Models
{
    public class Samourai : ObjectWithId
    {
        public int Force { get; set; }
        public string Nom { get; set; }
        public virtual Arme Arme { get; set; }
        [Display(Name = "Arts martiaux maîtrisés")]
        public virtual List<ArtMartial> ArtsMartiaux { get; set; } = new List<ArtMartial>();
    }
}
