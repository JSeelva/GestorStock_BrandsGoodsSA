using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorStock_BrandsGoodsSA.Models
{
    /// <summary>
    /// Interface que obriga à utilização do método MainMenu (Menu Principal). Usado nas classes UserAdmin e UserStandard.
    /// </summary>
    interface IUser
    {
        void MainMenu();
    }
}
