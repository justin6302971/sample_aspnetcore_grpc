using System;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GrpcGreeter.Models
{
    public partial class Car
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid? PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
