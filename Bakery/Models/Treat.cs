using System.Collections.Generic;
namespace Bakery.Models
{
    public class Treat
    {
        public Treat()
        {
            this.Flavors = new HashSet<TreatFlavor>();
        }
        public int TreatId { get; set; }
        public string Description { get; set; }
        
        public ICollection<TreatFlavor> Flavors { get; }
        public virtual ApplicationUser User { get; set; }
    }
}