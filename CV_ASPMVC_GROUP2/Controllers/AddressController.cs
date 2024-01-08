using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CV_ASPMVC_GROUP2.Controllers
{
    [Authorize]
    public class AddressController : BaseController
    {
        private readonly TestDbContext _context;
        public AddressController(TestDbContext testDbContext)
        {
            _context = testDbContext;
        }
        [Authorize]
        public IActionResult EditAddress()
        {
            //Hämtar användarens ID från basklassen
            var currentUserId = base.UserId;

            //Hämtar adressen för användaren
            var address = _context.Addresses.Where(a => a.UserId.Equals(currentUserId)).Single();

            //Skapar en ny modell för redigering av adressen baserat på den hämtade adressinformationen
            var model = new EditAddressViewModel
            {
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode.ToString()
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAddress(EditAddressViewModel editAddressViewModel)
        {
            try
            {
                var currentUserId = base.UserId;
                var address = _context.Addresses.Where(a => a.UserId.Equals(currentUserId)).Single();

                //Uppdaterar adressinformationen med den nya informationen från modellen
                address.Street = editAddressViewModel.Street;
                address.City = editAddressViewModel.City;
                address.PostalCode = Int32.Parse(editAddressViewModel.PostalCode);

                //Sparar ändringarna i databasen
                var result = await _context.SaveChangesAsync();

                //Omdirigerar användaren till startsidan om ändringarna går igenom
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) 
            {
                //Om ett fel uppstår under uppdateringen av adressen returneras vyn för att fortsätta redigera
                return View(editAddressViewModel);
            }
            
        }
    }
}
