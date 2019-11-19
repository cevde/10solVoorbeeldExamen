using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThePlaceToMeet.Filters;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Controllers
{
    public class ReservatieController : Controller
    {
        private readonly IVergaderruimteRepository _vergaderruimteRepository;
        private readonly ICateringRepository _cateringRepository;
        private readonly IKortingRepository _kortingRepository;

        public ReservatieController(IVergaderruimteRepository vergaderruimteRepository, ICateringRepository cateringRepository, IKortingRepository kortingRepository)
        {
            _vergaderruimteRepository = vergaderruimteRepository;
            _cateringRepository = cateringRepository;
            _kortingRepository = kortingRepository;
        }

        public IActionResult Index(int? aantalPersonen)
        {
            // implementeer
            IEnumerable<Vergaderruimte> ruimtes;
            if (aantalPersonen.HasValue)
                ruimtes = _vergaderruimteRepository.GetByMaxAantalPersonen(aantalPersonen.Value);
            else
                ruimtes = _vergaderruimteRepository.GetAll();
            ViewData["aantalPersonen"] = aantalPersonen;
            return View(ruimtes.OrderBy(v => v.VergaderruimteType).ThenBy(v => v.MaximumAantalPersonen));
        }

        public IActionResult Reserveer(int id)
        {
            Vergaderruimte ruimte = _vergaderruimteRepository.GetById(id);
            if (ruimte == null)
                return NotFound();
            ViewData["catering"] = new SelectList(_cateringRepository.GetAll().OrderBy(c => c.Titel), nameof(Catering.Id), nameof(Catering.Titel));
            return View(new ReservatieViewModel(ruimte));
        }

        [HttpPost]
        [ServiceFilter(typeof(KlantFilter))]
        public IActionResult Reserveer(int id, ReservatieViewModel viewmodel, Klant klant)
        {
            throw new NotImplementedException();
        }
    }
}