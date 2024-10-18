using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendGastos.Service.DTOs.CategoriaGasto
{
    public class CategoriaGastoWithAmountDto : IEquatable<CategoriaGastoWithAmountDto>
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public decimal ImporteTotal { get; set; }


        public bool Equals(CategoriaGastoWithAmountDto other)
        {
            if (other == null) return false;
            return Id == other.Id &&
                   Descripcion == other.Descripcion &&
                   ImporteTotal == other.ImporteTotal;

        }

        public override bool Equals(object obj) => Equals(obj as CategoriaGastoWithAmountDto);

        public override int GetHashCode() =>
            HashCode.Combine(Id, Descripcion, ImporteTotal);

    }
}
