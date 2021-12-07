using DemoProject.Model.Models;
using DemoProject.Repository.Repository;
using DemoProject.Services.Base;

namespace DemoProject.Services.Services
{
    public class UserServices : BaseServices<User>
    {
        private readonly UserRepository _dal;

        public UserServices(UserRepository dal)
        {
            _dal = dal;
            BaseDal = dal;
        }
    }
}