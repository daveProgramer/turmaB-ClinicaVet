using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaVeterinaria.Models
{
    internal class DataBaseGeneratedAttribute : Attribute
    {
        private DatabaseGeneratedOption none;

        public DataBaseGeneratedAttribute(DatabaseGeneratedOption none)
        {
            this.none = none;
        }
    }
}