using CV_ASPMVC_GROUP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;

namespace CV_ASPMVC_GROUP2.Controllers
{
    public class SearchController : Controller
    {
        TestDbContext _context;

        public SearchController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            List<UserSearchViewModel> userSearchViewModels = new List<UserSearchViewModel>();
            List<User> allUsers = _context.Users.ToList();
            
            List<Competence> allCompetences = _context.Competences.ToList();
            
            SearchViewModel model = new SearchViewModel { };
            //Kontrollerar om söksträngen är tom
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] words = searchString.Split(' ');
                List<User> usersFiltered = new List<User>();
                List<Competence> competencesFiltered = new List<Competence>();

                //Kontrollerar om användaren är inloggad
                if (User.Identity.IsAuthenticated)
                {
                    //Hämtar användare där antingen förnamn eller användarnamn innehåller ord/tecken från söksträngen
                    foreach (var word in words)
                    {
                        usersFiltered.AddRange(allUsers.Where(u => u.UserName.Contains(word) || u.FirstName.Contains(word)).Where(u => !u.IsDeactivated).ToList());
                    }
                }
                else
                {
                    foreach (var word in words)
                    {
                        usersFiltered.AddRange(allUsers.Where(u => u.UserName.Contains(word) || u.FirstName.Contains(word)).Where(u => !u.PrivateStatus).Where(u => !u.IsDeactivated).ToList());
                    }
                }

                foreach (var user in usersFiltered.Distinct())
                {
                    Cv aCv = null;
                    try
                    {
                        aCv = _context.Cvs.Where(c => c.User_ID.Equals(user.Id)).Single();
                    }
                    catch (Exception ex) { }
                    if (aCv != null)
                    {
                        var cvCompetences = _context.CvCompetences.Where(cc => cc.CvId == aCv.Id).ToList();
                        List<Competence> competences = new List<Competence>();
                        foreach (var cvCompetence in cvCompetences)
                        {
                            foreach(var word in words)
                            {
                                try
                                {
                                    competences.Add(_context.Competences.Where(c => c.Id == cvCompetence.CompetenceId).Where(c => c.Name.Contains(word)).Single());
                                }
                                catch (Exception ex) { }
                            }
                        }

                        userSearchViewModels.Add(new UserSearchViewModel { AUser = user, Competences = competences.Distinct(), Cv = aCv });
                    }
                    else
                    {
                        userSearchViewModels.Add(new UserSearchViewModel { AUser = user, Competences = null, Cv = null });
                    }
                }
                
                model.UserModels = userSearchViewModels;

                foreach (var word in words)
                {
                    competencesFiltered.AddRange(allCompetences.Where(c => c.Name.Contains(word)).ToList());
                }
                competencesFiltered.Distinct();

                model.AllFilteredCompetences = competencesFiltered.Distinct();
                
            }

            // returnerar vyn med model
            return View(model);
        }
    }
}


