using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Enums
{
    public enum Cities
    {
        [Display(Name = "Breda")]
        Breda,
        [Display(Name = "Den Bosch")]
        DenBosch,
        [Display(Name = "Tilburg")]
        Tilburg
    }
}
