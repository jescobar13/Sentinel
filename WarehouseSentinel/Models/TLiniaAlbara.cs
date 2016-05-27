using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSentinel.Controllers.Albara;

namespace WarehouseSentinel.Models
{
    public class TLiniaAlbara
    {
        SentinelDBEntities context;

        public TLiniaAlbara(SentinelDBEntities context)
        {
            this.context = context;
        }

        public void add(liniaalbara la)
        {
            context.liniaalbara.Add(la);
            context.SaveChanges();
        }

        public void modify(liniaalbara la)
        {
            context.SaveChanges();
        }

        public void remove(liniaalbara la)
        {
            context.liniaalbara.Remove(la);
            context.SaveChanges();
        }

        internal int? getNumCaixa()
        {
            return (from tLiniaAlbara in context.liniaalbara
                    select tLiniaAlbara)
                    .OrderByDescending(i => i.caixa)
                    .FirstOrDefault()
                    .caixa;
        }

        internal int? getCodiCaixaBuida(albara albaraActual, producte producteActual)
        {
            var prova = from a in context.liniaalbara
                        where a.Albara_codi == albaraActual.codi && a.idProducte == producteActual.id
                        group a by a.caixa into grp
                        select new { idcaixa = grp.Key, cnt = grp.Count() };

            //cnt és la quantitat de linies d'albara que hi han pertan els productes que hi ha a la caixa.

            return prova.Where(x => x.cnt < producteActual.unitatCaixa).Select(x => x.idcaixa).FirstOrDefault();
        }

        internal int? getQuantitatProductesByCodiCaixa(int codiCaixa)
        {
            var t = from a in context.liniaalbara
                    where a.caixa == codiCaixa
                    group a by a.id into grp
                    select new { cnt = grp.Count() };

            return t.Select(x => x.cnt).FirstOrDefault();
        }

        internal int? getMaxIdCaixa()
        {
            //var t = from a in context.liniaalbara
            //        group a by a.caixa into grp
            //        select new { max = grp.Max() };

            //var prova = t.Select(x => x.max).FirstOrDefault().caixa;
            //return prova;

            return (from a in context.liniaalbara
                    group a by a.caixa into grp
                    select grp.Max(l => l.caixa)).FirstOrDefault();
        }
    }
}
