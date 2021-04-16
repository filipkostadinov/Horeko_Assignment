using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class DishException : Exception
    {
        public int? DishId { get; set; }
        public string DishName { get; set; }

        public DishException() : base("There has been an issue with a dish")
        { }
        public DishException(int? dishId, string message) : base(message)
        {
            DishId = dishId;
        }
    }
}
