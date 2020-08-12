using System.Collections.Generic;
using X.PagedList;

namespace ozelgunmesajlaricom.Models
{
    public class Listele
    {
        public List<Menu> menus { get; set; }
        public string cBaslik { get; set; }
        public IPagedList<Mesaj> mesajs { get; set; }
    }
}