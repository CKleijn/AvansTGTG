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
        [Description("Brood")]
        Bread,
        [Description("Drank")]
        Drinks,
        [Description("Snack")]
        Snack,
        [Description("Soep")]
        Soup,
        [Description("Warme Maaltijd")]
        WarmDinner
    }
}
