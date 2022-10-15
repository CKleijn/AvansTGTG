using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Enums
{
    public enum MealTypes
    {
        [Display(Name = "Brood")]
        Bread,
        [Display(Name = "Drank")]
        Drinks,
        [Display(Name = "Snack")]
        Snack,
        [Display(Name = "Soep")]
        Soup,
        [Display(Name = "Warme Maaltijd")]
        WarmDinner
    }
}
