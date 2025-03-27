using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Domain.Enuns
{
    public struct IResponseController
    {
        public enum ResponseJsonTypes
        {
            Success = 0,
            Error = 1,
            Warning = 2,
            Information = 3,
            Notification = 4
        }
    }
}
