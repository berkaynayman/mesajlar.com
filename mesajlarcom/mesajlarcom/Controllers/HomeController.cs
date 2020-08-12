using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using X.PagedList;

namespace ozelgunmesajlaricom.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Listele(string id, int? sayfa, string arama)
        {
            Models.Listele listele = new Models.Listele();
            ViewBag.cSeo = id;
            int iSayfaNo = sayfa ?? 1;
            using (Data.DataClassesDataContext dc = new Data.DataClassesDataContext())
            {
                ViewBag.cArama = arama;
                listele.menus =
                    (from table in dc.MesajTipis
                     where
                        table.iAktifMi == 1
                     select new Models.Menu
                     {
                         iKodMesajTipi = table.iKodMesajTipi,
                         cAdi = table.cAdi,
                         cSeo = table.cSeo
                     }).OrderBy(x => x.cAdi).ToList();

                List<Models.Mesaj> result = null;
                if (string.IsNullOrEmpty(id))
                {
                    listele.cBaslik = "Tüm Mesajlar";

                    result =
                        (from table in dc.Mesajs
                         where
                            table.iAktifMi == 1
                         select new Models.Mesaj
                         {
                             iKodMesaj = table.iKodMesaj,
                             cMesaj = table.cMesaj
                         }).OrderBy(x => x.iKodMesaj).ToList();
                }
                else
                {
                    var sonucMesajTipi =
                        (from table in dc.MesajTipis
                         where
                            table.cSeo == id &&
                            table.iAktifMi == 1
                         select table).FirstOrDefault();
                    if (sonucMesajTipi != null && !string.IsNullOrEmpty(sonucMesajTipi.cAdi) && sonucMesajTipi.iKodMesajTipi > 0)
                    {
                        listele.cBaslik = sonucMesajTipi.cAdi;

                        result =
                            (from table in dc.Mesajs
                             where
                                table.iKodMesajTipi == sonucMesajTipi.iKodMesajTipi &&
                                table.iAktifMi == 1
                             select new Models.Mesaj
                             {
                                 iKodMesaj = table.iKodMesaj,
                                 cMesaj = table.cMesaj
                             }).OrderBy(x => x.iKodMesaj).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(arama))
                {
                    listele.mesajs = result.Where(x => x.cMesaj.ToLower().Contains(arama.ToLower())).OrderBy(x => x.iKodMesaj).ToPagedList(iSayfaNo, 25);
                }
                else
                {
                    listele.mesajs = result.OrderBy(x => x.iKodMesaj).ToPagedList(iSayfaNo, 25);
                }
            }
            return View(listele);
        }
    }
}