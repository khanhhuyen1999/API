using DoAn5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace DoAn5.Services
{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        Users Create(Users user, string password);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        privateDoAn5Context _context;

        public UserService(DoAn5Context context)
        {
            _context = context;
        }

        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!BC.Verify(password, user.Password))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }

        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public Users Create(Users user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Vui lòng nhập mật khẩu");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Tài khoản \"" + user.Username + "\" đã được đăng ký bởi tài khoản khác");

            string passwordHash = BC.HashPassword(password);

            user.Password = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        //public void Update(Users userParam, string password = null)
        //{
        //    var user = _context.Users.Find(userParam.Id);

        //    if (user == null)
        //        throw new Exception("User not found");

        //    // update username if it has changed
        //    if (!string.IsNullOrWhiteSpace(userParam.Email) && userParam.Email != user.Email)
        //    {
        //        // throw error if the new username is already taken
        //        if (_context.Users.Any(x => x.Email == userParam.Email))
        //            throw new Exception("Email " + userParam.Email + " is already taken");

        //        user.Email = userParam.Email;
        //    }

        //    // update password if provided
        //    if (!string.IsNullOrWhiteSpace(password))
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //        user.PasswordHash = passwordHash;
        //        user.PasswordSalt = passwordSalt;
        //    }

        //    _context.Users.Update(user);
        //    _context.SaveChanges();
        //}

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
