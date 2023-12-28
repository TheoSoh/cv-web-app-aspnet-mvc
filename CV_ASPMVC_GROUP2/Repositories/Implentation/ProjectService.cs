using CV_ASPMVC_GROUP2.Models;
using CV_ASPMVC_GROUP2.Repositories.Abstract;
using Microsoft.CodeAnalysis;

namespace CV_ASPMVC_GROUP2.Repositories.Implentation
{
    public class ProjectService : IProjectService
    {

        private readonly TestDbContext _context;
        public ProjectService(TestDbContext context) 
        {
            this._context = _context;
        }
        public bool Add(Project entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                foreach(string userID in entity.User)
                {
                    var projectUSer = new UserProject
                    {
                        ProjectId = entity.Id,
                        UserId = userID
                    };
                    _context.Add(projectUSer);
                }
                _context.SaveChanges();
                return true;
            } 
            catch (Exception ex) 
            {
                return false;
            }
            //throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                _context.Projects.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            var data = _context.Projects;
            return data;
            //throw new NotImplementedException();
        }

        public Project GetById(int id)
        {
            return _context.Projects.Find(id);
            //throw new NotImplementedException();
        }

        public bool Update(Project entity)
        {
            try
            {
                _context.Projects.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
            //throw new NotImplementedException();
        }
    }
}
