using System.Collections.Generic;
namespace Bakery.Models
{
    public class Flavor
    {
        public Flavor()
        {
            this.Treats = new HashSet<TreatFlavor>();
        }
        public int FlavorId { get; set; }
        public string Description { get; set; }
        
        public ICollection<TreatFlavor> Treats { get; }
        public virtual ApplicationUser User { get; set; }
    }
}