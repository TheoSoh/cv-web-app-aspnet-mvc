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
        public IActionResult EditAddress()
        {
            var currentUserId = base.UserId;
            var address = _context.Addresses.Where(a => a.UserId.Equals(currentUserId)).Single();
            var model = new EditAddressViewModel
            {
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode.ToString()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAddress(EditAddressViewModel editAddressViewModel)
        {
            try
            {
                var currentUserId = base.UserId;
                var address = _context.Addresses.Where(a => a.UserId.Equals(currentUserId)).Single();

                address.Street = editAddressViewModel.Street;
                address.City = editAddressViewModel.City;
                address.PostalCode = Int32.Parse(editAddressViewModel.PostalCode);
                
                var result = await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) 
            { 
                return View(editAddressViewModel);
            }
            
        }
    }
}
