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

        internal int? getCodiCaixaBuida(albara albaraActual, producte producteActual)
        {
            var prova = from a in context.liniaalbara
                        where a.Albara_codi == albaraActual.codi && a.idProducte == producteActual.id
                        group a by a.caixa into grp
                        select new { idcaixa = grp.Key, cnt = grp.Count() };

            //cnt és la quantitat de linies d'albara que hi han pertan els productes que hi ha a la caixa.

            return prova.Where(x => x.cnt < producteActual.unitatCaixa).Select(x => x.idcaixa).FirstOrDefault();
        }

        internal int? getQuantitatProductesByAlbara(int idProducte)
        {
            return (from a in context.liniaalbara
                    where a.idProducte == idProducte
                    select a).ToList().Count();
        }

        internal int? getQuantitatProductesByCodiCaixa(int codiCaixa, int producteSeleccionat)
        {
            return (from a in context.liniaalbara
                     where a.caixa == codiCaixa && a.idProducte == producteSeleccionat
                     select a).ToList().Count;
        }

        internal int? getMaxIdCaixa()
        {
            //var t = from a in context.liniaalbara
            //        group a by a.caixa into grp
            //        select new { max = grp.Max(a.caixa) };

            //var prova = t.Select(x => x.max).FirstOrDefault().caixa;
            //return prova;

            return (from a in context.liniaalbara
                    group a by a.caixa into grp
                    select grp.Max(l => l.caixa)).FirstOrDefault();
        }

        internal float? sumaTotsPesos(int codiCaixa)
        {
            return (from a in context.liniaalbara
                    where a.caixa == codiCaixa
                    select a).ToList()
                    .Sum(x => x.pes);

            //float pes = 0;

            //List<liniaalbara> llista = (from l in context.liniaalbara
            //                            where l.caixa == codiCaixa
            //                            select l).ToList();
            //foreach (liniaalbara l in llista)
            //{
            //    pes += l.pes.GetValueOrDefault() ;
            //}

            //return pes;
        }

        internal int getQuantitatProductesByComanda(int codi)
        {
            return (from a in context.liniaalbara
                    where a.Albara_Comanda_codi == codi
                    select a).ToList().Count();
        }
    }
}
