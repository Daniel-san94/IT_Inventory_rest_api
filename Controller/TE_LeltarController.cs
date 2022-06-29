using IT_Inventory_rest_api.Data;
using IT_Inventory_rest_api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IT_Inventory_rest_api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TE_LeltarController : ControllerBase

    {
        /// <summary>
        /// Létrehozok egy Selfdev_reportingContext tipusú _context változót, ami provát és csak olvasható.
        /// </summary>

        private readonly Selfdev_reportingContext _context;

        /// <summary>
        /// A TE_LeltarController metódusnak átadok egy Selfdev_reportingContext tipusú context változót.
        /// </summary>
        /// <param name="context"></param>

        public TE_LeltarController(Selfdev_reportingContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Ez egy GET kérés ami egy felsorolásba lekéri az összes leltározott itemet.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TeLeltar> GetAll()
        {
            return _context.TeLeltars;
        }

        /// <summary>
        /// Ez a metódus id szerint tud lekérni adatot az adatbázisból. Ha nincs találat akkor NotFound-dal tér vissza,
        /// ha van találat akkor visszatér egy Ok státuszkóddal és az adott id-jú itemmel.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _context.TeLeltars.FirstOrDefault(c => c.Nid == id);
            if (item == null)
            {
                return NotFound();

            }
            return Ok(item);

        }

        /// <summary>
        /// Ez a metódus leltári szám szerint tud lekérni adatot az adatbázisból. Ha nincs találat akkor NotFound-dal tér vissza,
        /// ha van találat akkor visszatér egy Ok státuszkóddal és az adott leltár számú itemmel.
        /// </summary>
        /// <param name="Leltari_Szam"></param>
        /// <returns></returns>

        [HttpGet("leltariszam/{Leltari_Szam}")]

        public IActionResult GetTeLeltarbyLeltariSzam(string Leltari_Szam)
        {
            var item = _context.TeLeltars.FirstOrDefault(c => c.LeltariSzam == Leltari_Szam);
            if (item == null)
            {
                return NotFound();

            }
            return Ok(item);
        }

        /// <summary>
        /// Ez a metódus sorozatszám szerint tud lekérni adatot az adatbázisból. Ha nincs találat akkor NotFound-dal tér vissza,
        /// ha van találat akkor visszatér egy Ok státuszkóddal és az adott sorozatszámú itemmel.
        /// </summary>
        /// <param name="SorozatSzam"></param>
        /// <returns></returns>

        [HttpGet("sorozatszam/{SorozatSzam}")]

        public IActionResult GetTeLeltarbySorozatSzam(string SorozatSzam)
        {
            var item = _context.TeLeltars.FirstOrDefault(c => c.Sorozatszam == SorozatSzam);
            if (item == null)
            {
                return NotFound();

            }
            return Ok(item);
        }


        /// <summary>
        /// Ez a lekérdezés nem működött jól. pl "https://localhost:44391/api/TE_Leltar/hely/Olaj-szósz / Oil sauce". Tehát a helység nevekben található karakterek miatt volt gond, HTML hibát kaptam.
        /// Ha egyben van a helységnév és nincs benne speciális karakter akkor minden gond nélkül ment a lekérdezés.
        /// Ez a metódus hely szerint tud lekérni adatot az adatbázisból. Ha nincs találat akkor NotFound-dal tér vissza,
        /// ha van találat akkor visszatér egy Ok státuszkóddal és az adott helyű itemmel. A 'where s.Hely == Hely' átírtam az alább látható
        /// lekérésre a Contains segítségével. Így most már jól működik a lekérdezés, mivel elég a '/' jel előtti részt lekérni.
        /// </summary>
        /// <param name="Hely"></param>
        /// <returns></returns>

        [HttpGet("hely/{Hely}")]

        public ActionResult<IEnumerable<TeLeltar>> GetTeLeltarbyHely(string Hely)
        {

            var items = (from s in _context.TeLeltars where s.Hely.Contains(Hely) select s).ToList();

            foreach (var item in _context.TeLeltars)
            {
                items = items.Where(s => s.Hely.Contains(Hely)).ToList();
                _context.TeLeltars.Add(item);

            }
            if (items.Count == 0)
            {
                return NotFound();

            }
            return Ok(items);
        }

        /// <summary>
        /// Ez egy Post kérés ha a ModelState nem valid akkor BadRequest stásuszkóddal térünk vissza. Különben létrehozok egy teLeltarEntry listát,
        /// amibe feltöltöm az adatokat majd elmentem és Ok státuszkóddal térünk vissza meg az új itemmel.
        /// </summary>
        /// <param name="teLeltar"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post([FromBody] TeLeltarDto teLeltar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Hiba");
            }
            var teLeltarEntry = new TeLeltar
            {
                Nev = teLeltar.Nev,
                Hely = teLeltar.Hely,
                Felhasznalo = teLeltar.Felhasznalo,
                Csoport = teLeltar.Csoport,
                Statusz = teLeltar.Statusz,
                Tipusok = teLeltar.Tipusok,
                Gyarto = teLeltar.Gyarto,
                Modell = teLeltar.Modell,
                Sorozatszam = teLeltar.Sorozatszam,
                LeltariSzam = teLeltar.LeltariSzam
            };
            if (teLeltarEntry.Nev == null || teLeltarEntry.Hely == null || teLeltarEntry.Felhasznalo == null || teLeltarEntry.Csoport == null
                || teLeltarEntry.LeltariSzam == null || teLeltarEntry.Modell == null || teLeltarEntry.Sorozatszam == null || teLeltarEntry.Tipusok == null
                || teLeltarEntry.Statusz == null)
            {
                return BadRequest("Nem rögzíthető az adat");
            }
            var result = _context.TeLeltars.Add(teLeltarEntry);
            _context.SaveChanges();
            return Ok(result.Entity);
        }

        /// <summary>
        /// Ez egy Put kérés. Id szerint tudunk szerkeszteni itemet. Ha a ModelState nem valid akkor BadRequest státuszkóddal térünk vissza.
        /// Az entry változóba betöltöm az id szerint kiválasztott itemet, Ha ez null akkor NotFound-dal térünk vissza. Különben
        /// az eredeti változóba is betöltöm id szerint az itemet. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teLeltar"></param>
        /// <returns></returns>


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TeLeltarDto teLeltar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Hiba");
            }

            var entry = _context.TeLeltars.FirstOrDefault(c => c.Nid == id);

            if (entry == null)
            {
                NotFound();
            }

            var eredeti = _context.TeLeltars.FirstOrDefault(c => c.Nid == id);

            if (teLeltar.Nev == null)
            {
                entry.Nev = eredeti.Nev;

            }
            else
            {
                entry.Nev = teLeltar.Nev;
            }

            if (teLeltar.Felhasznalo == null)
            {
                entry.Felhasznalo = eredeti.Felhasznalo;
            }
            else
            {
                entry.Felhasznalo = teLeltar.Felhasznalo;
            }

            if (teLeltar.Hely == null)
            {
                entry.Hely = eredeti.Hely;
            }
            else
            {
                entry.Hely = teLeltar.Hely;
            }

            if (teLeltar.Csoport == null)
            {
                entry.Csoport = eredeti.Csoport;
            }
            else
            {
                entry.Csoport = teLeltar.Csoport;
            }

            if (teLeltar.Statusz == null)
            {
                entry.Statusz = eredeti.Statusz;
            }
            else
            {
                entry.Statusz = teLeltar.Statusz;
            }

            if (teLeltar.Tipusok == null)
            {
                entry.Tipusok = eredeti.Tipusok;
            }
            else
            {
                entry.Tipusok = teLeltar.Tipusok;
            }

            if (teLeltar.Gyarto == null)
            {
                entry.Gyarto = eredeti.Gyarto;
            }
            else
            {
                entry.Gyarto = teLeltar.Gyarto;
            }

            if (teLeltar.Modell == null)
            {
                entry.Modell = eredeti.Modell;
            }
            else
            {
                entry.Modell = teLeltar.Modell;
            }

            if (teLeltar.Sorozatszam == null)
            {
                entry.Sorozatszam = eredeti.Sorozatszam;
            }
            else
            {
                entry.Sorozatszam = teLeltar.Sorozatszam;
            }

            if (teLeltar.LeltariSzam == null)
            {
                entry.LeltariSzam = eredeti.LeltariSzam;
            }
            else
            {
                entry.LeltariSzam = teLeltar.LeltariSzam;
            }

            _context.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Ez egy Delete kérés. Id szerint tudunk törölni. A torles változóba betöltöm id szerint az itemet. Ha nincs ilyen id,
        /// akkor NotFound-dal térünk vissza, különben töröljük az adott itemet és Ok státuszkóddal térünk vissza.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var torles = _context.TeLeltars.FirstOrDefault(c => c.Nid == id);

            if (torles == null)
            {
                return NotFound();
            }

            _context.TeLeltars.Remove(torles);
            _context.SaveChanges();

            return Ok();
        }
    }
}
