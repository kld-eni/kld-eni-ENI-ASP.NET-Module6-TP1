using Module6.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Module6.ViewModels
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }

            
        public int? SelectedWeapon { get; set; }

        [Display(Name = "Arts martiaux maîtrisés")]
        public List<int> SelectedMartialArts { get; set; } = new List<int>();

        public List<SelectListItem> WeaponsListItems { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> MartialArtsListItems { get; set; } = new List<SelectListItem>();
    }
}