using System;
using System.ComponentModel.DataAnnotations;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Controllers
{
    public class ReservatieViewModel
    {
        public string Naam { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Dag { get; set; }

        [Required]
        [Range(8, 20)]
        [Display(Name = "Begin uur")]
        public int BeginUur { get; set; }

        [Required]
        [Display(Name = "Hoeveel uur wil je de vergaderzaal reserveren?")]
        public int Duur { get; set; }

        [Required]
        [Display(Name = "Met hoeveel personen wil je vergaderen?")]
        public int AantalPersonen { get; set; }

        [Display(Name = "Standaard catering (koffie, thee, water) ")]
        public bool StandaardCatering { get; set; }

        [Display(Name = "Indien je ook catering wenst, selecteer wat nodig is")]
        public int CateringId { get; set; }

        public ReservatieViewModel()
        {
            Dag = DateTime.Today.Date.AddDays(7);
            BeginUur = 8;
            Duur = 2;
            AantalPersonen = 1;
        }

        public ReservatieViewModel(Vergaderruimte ruimte) : this()
        {
            Naam = ruimte.Naam;
        }
    }
}