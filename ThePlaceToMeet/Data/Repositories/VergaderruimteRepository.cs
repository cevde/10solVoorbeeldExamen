using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Repositories {
    public class VergaderruimteRepository : IVergaderruimteRepository {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Vergaderruimte> _vergaderruimtes;

        public VergaderruimteRepository(ApplicationDbContext context) {
            _context = context;
            _vergaderruimtes = _context.Vergaderruimtes;
        }

        public IEnumerable<Vergaderruimte> GetAll() {
            return _vergaderruimtes
                .OrderBy(vr => vr.Naam)
                .ThenBy(vr => vr.MaximumAantalPersonen)
                .AsNoTracking()
                .ToList();
        }

        public Vergaderruimte GetById(int id) {
            return _vergaderruimtes
                .Include(vr => vr.Reservaties)
                .SingleOrDefault(vr => vr.Id == id);
        }

        public IEnumerable<Vergaderruimte> GetByMaxAantalPersonen(int maxAantalPersonen) {
            return _vergaderruimtes
                .Where(vr => vr.MaximumAantalPersonen >= maxAantalPersonen)
                .OrderBy(vr => vr.Naam)
                .ThenBy(vr => vr.MaximumAantalPersonen)
                .ToList();
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
