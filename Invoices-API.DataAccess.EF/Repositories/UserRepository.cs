using Invoices_API.DataAccess.EF.Context;
using Invoices_API.DataAccess.EF.DTO;
using Invoices_API.DataAccess.EF.Models;
using Invoices_API.DataAccess.EF.Repositories.Interfaces;
using Invoices_API.DataAccess.EF.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices_API.DataAccess.EF.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly InvoicesDbContext _context;
        private readonly IPasswordService _passwordService;

        public UserRepository(InvoicesDbContext context, IPasswordService passwordService) {
            _context = context; 
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            User? existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null) 
            {
                throw new Exception("user not found");
            }

            return existingUser;

        }

        public async Task<User> GetUserByName(string email)
        {
            var userToFind = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (userToFind == null)
            {
                throw new Exception("user not found");
            }

            return userToFind;
        }

        public async Task<User> CreateUser(UserDTO user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            string hashed = _passwordService.HashPassword(user.Password);

            var userEntity = new User
            {
                Email = user.Email,
                Password = hashed,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                RevokedAt = DateTime.UtcNow.AddMinutes(15),
                IsActive = true,
                Role = "user",
                RefreshToken = ""
            };


            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return userEntity;

        }

        public async Task<User> UpdateUser(int id, string email, string password)
        {
            User? existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.Email = email;

            existingUser.Password = password;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteUser(int id)
        {
            User? existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
