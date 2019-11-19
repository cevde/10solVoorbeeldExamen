using System;
using System.Collections.Generic;
using System.Linq;

namespace ThePlaceToMeet.Models.Domain
{
    public class Vergaderruimte
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        public VergaderruimteType VergaderruimteType { get; set; }
        public int MaximumAantalPersonen { get; set; }
        public decimal PrijsPerUur { get; set; } //prijs voor huur vergaderruimte per uur
        public decimal PrijsPerPersoonStandaardCatering { get; set; } //prijs voor de standaardcatering (koffie, thee, water) per persoon
        public ICollection<Reservatie> Reservaties { get; private set; }
        #endregion

        #region Constructors and methods
        public Vergaderruimte()
        {
            Reservaties = new List<Reservatie>();
        }

        private IEnumerable<Reservatie> GetReservatiesVoorDag(DateTime dag)
        {
            //implementeer
            return Reservaties.Where(r => r.Dag.Date == dag.Date).OrderBy(r => r.BeginUur);
        }

        public Reservatie Reserveer(Klant klant, IEnumerable<Korting> kortingen, DateTime dag, int beginUur, int aantalUren, int aantalPersonen, Catering catering, bool standaardCatering)
        {
            //implementeer
            IEnumerable<Reservatie> reservaties = GetReservatiesVoorDag(dag.Date);
            int tot = beginUur + aantalUren;
            if (dag.Date < DateTime.Today.AddDays(1))
                throw new ArgumentException("Een vergaderruimte dient minstens 1 dag op voorhand te worden gereserveerd");
            if (aantalUren < 2)
                throw new ArgumentException("Een vergaderruimte dient minstens voor 2u te worden gereserveerd");
            if (beginUur < 8 || tot > 22)
                throw new ArgumentException("Ruimte kan gereserveerd worden van 8u tot 22u");
            if (reservaties.Any(r => beginUur < r.Tot && tot > r.BeginUur))
                throw new ArgumentException("De vergaderruimte is niet meer beschikbaar op opgegeven tijdstip");
            if (aantalPersonen > MaximumAantalPersonen)
                throw new ArgumentException("De vergaderruimte is te klein voor opgegeven aantal personen");
            if (catering != null && dag < DateTime.Today.AddDays(7))
                throw new ArgumentException("Catering dient minstens 1 week op voorhand te worden gereserveerd");
            Reservatie reservatie = new Reservatie() { Dag = dag, BeginUur = beginUur, DuurInUren = aantalUren, AantalPersonen = aantalPersonen, Catering = catering, PrijsPerUur = PrijsPerUur };
            if (standaardCatering) reservatie.PrijsPerPersoonStandaardCatering = PrijsPerPersoonStandaardCatering;
            if (catering != null) reservatie.PrijsPerPersoonCatering = catering.PrijsPerPersoon;
            Reservaties.Add(reservatie);
            klant.VoegReservatieToe(reservatie);
            int aantalReservaties = klant.GetAantalReservaties(dag.Date.Year);
            reservatie.Korting = GetKorting(kortingen, aantalReservaties);
            return reservatie;
        }

        private Korting GetKorting(IEnumerable<Korting> kortingen, int aantalReservaties)
        {
            //implementeer
            return kortingen.OrderByDescending(t => t.MinimumAantalReservatiesInJaar).FirstOrDefault(k => k.MinimumAantalReservatiesInJaar <= aantalReservaties);
        }
        #endregion
    }
}
