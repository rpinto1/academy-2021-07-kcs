using KCSit.SalesforceAcademy.Kappify.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KCSit.SalesforceAcademy.Kappify.Logic
{
    public class GenericReturn<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }



    }
}
