using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Auction.Data;
using E_Auction.Models;

namespace E_Auction.Services.AuthService
{
    public class AuthService 
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        // public AuthRepository(DataContext context, IConfiguration configuration)
        // {
        //     _configuration = configuration;
        //     _context = context;
        // }
        // public async Task<ServiceResponse<string>> Login(string username, string password)
        // {
        //     throw new NotImplementedException();
        // }

        // public async Task<ServiceResponse<int>> Register(User user, string password)
        // {
        //     throw new NotImplementedException();
        // }

        // public async Task<bool> UserExists(string username)
        // {
        //     throw new NotImplementedException();
        // }
    }
}