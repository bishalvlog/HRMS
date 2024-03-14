﻿using HRMS.Core.Comman;
using HRMS.Core.Domain.Users;
using HRMS.Core.Interfaces.Users;
using HRMS.Core.Models.Users;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Repository.Users;

namespace HRMS.Data.DataSeeder
{
    public static class DataSeeder
    {
        private static readonly string _createdNepaliDate = DateConversions.ConvertToNepaliDate(DateTime.Now);

        public static async Task SeedRolesAsync(IRolesRepository rolesRepository)
        {
            var userRoles = new List<AppRole>()
            {
                new AppRole
                {
                    RolesName = UserRoles.Admin,
                    Description = $"Default {UserRoles.Admin} role.",
                    IsActive = true,
                    CreatedBy = "admin"
                },
                new AppRole
                {
                    RolesName = UserRoles.User,
                    Description = $"Default {UserRoles.User} role.",
                    IsActive = true,
                    CreatedBy = "admin"
                }
            };
            var (_, roles) = await rolesRepository.GetRolesAsync();
            var rolesToSeed = userRoles
                .Where(r1 => roles.All(r2 => !r1.RolesName.Equals(r2.RolesName, StringComparison.OrdinalIgnoreCase)));
            if (!rolesToSeed.Any()) return;
            {
                foreach(var role in rolesToSeed)
                {
                    var (spMsgRes, roleCreated) = await rolesRepository.CreateRolesAsync(role);
                    if (roleCreated is null)
                        throw new Exception("Role Creation faild");
                }
            }
        }
        public static async Task SeedUsersAsync(IUserRepository userRepository, IRolesRepository rolesRepository, IUserRolesRepository userRolesRepository)
        {
            var (_,roleAdmin) = await rolesRepository.GetRolesByNameAsync(UserRoles.Admin);
            var (_,userBishal) = await userRepository.GetUserByUserNameAsync("bishal.thapa");
            if(userBishal is null)
            {
                var passSalt = CryptoUtils.GenerateKeySalt();
                var user = new AppUser
                {
                    UserName = "bishal.thapa",
                    Email = "bishal.thapa1200@gmail.com",
                    FullName = "Bishal Thapa",
                    DateOfBirth = new DateTime(2001, 01, 13),
                    PasswordHash = CryptoUtils.HashHmacsha512Base64("Secure#$@12", passSalt),
                    PasswordSalt = passSalt,
                    Gender = 1,
                    IsActive = true,
                    CreatedBy = "admin",
                    IsSuperAdmin = true,
                    CreatedLocalDate = DateTime.Now,
                    CreatedUtcDate = DateTime.UtcNow,
                    CreatedNepaliDate = _createdNepaliDate

                };
                var (_,userCreated) = await userRepository.CreateUserAsync(user);
                if (userCreated is null) throw new Exception("User Create FAil");
                if (roleAdmin is null) throw new Exception($"Role {UserRoles.Admin} not exists!");

                var spMsgRes = await userRolesRepository.AssignUserToRolesAsync(userCreated.Id,roleAdmin.Id);
                if (spMsgRes.StatusCode != 200) throw new Exception("Failed Roles Assign!");
               
            }
            var (_,SuperAdmin) = await userRepository.GetUserByUserNameAsync("Super.Admin");

            if(SuperAdmin is null)
            {
                var passSalt = CryptoUtils.GenerateKeySalt();
                var user = new AppUser
                {
                    UserName = "Super.Admin",
                    Email = "Super.Adminhrms@gmail.com",
                    FullName = "Super.Admin",
                    DateOfBirth = new DateTime(2024, 02, 09),
                    PasswordHash = CryptoUtils.HashHmacsha512Base64("SuperAdmin@123", passSalt),
                    PasswordSalt = passSalt,
                    Gender = 1,
                    IsActive = true,
                    CreatedBy = "Admin",
                    IsSuperAdmin = true,
                    CreatedLocalDate = DateTime.UtcNow,
                    CreatedUtcDate = DateTime.UtcNow,
                    CreatedNepaliDate = _createdNepaliDate
                };
                 var (_, userCreate) = await userRepository.CreateUserAsync(user);
                if (userCreate is null) throw new Exception("User Create Fail");
                if (roleAdmin is null) throw new Exception($"Role {UserRoles.Admin} not exists!");

                var spMsgRes = await userRolesRepository.AssignUserToRolesAsync(userCreate.Id, roleAdmin.Id);
                if (spMsgRes.StatusCode != 200) throw new Exception("failed Roles Assign !");
            }
            var (_, userAdmin) = await userRepository.GetUserByUserNameAsync("admin");
            if(userAdmin is null)
            {
                var passSalt = CryptoUtils.GenerateKeySalt();
                var user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@hrms.com.np",
                    FullName = "Administrator",
                    DateOfBirth = new DateTime(2024, 02, 09),
                    PasswordHash = CryptoUtils.HashHmacsha512Base64("Admin@123", passSalt),
                    PasswordSalt = passSalt,
                    Gender = 1,
                    IsActive = true,
                    CreatedBy = "admin",
                    IsSuperAdmin = true,
                    CreatedLocalDate = DateTime.UtcNow,
                    CreatedUtcDate = DateTime.UtcNow,
                    CreatedNepaliDate = _createdNepaliDate
                };
                var (_, userCreate) = await userRepository.CreateUserAsync(user);
                if (userCreate is null) throw new Exception("user create fail");
                if (roleAdmin is null) throw new Exception($"Role {UserRoles.Admin} not exits!");
                var spMsgRes = await userRolesRepository.AssignUserToRolesAsync(userCreate.Id,roleAdmin.Id);
                if (spMsgRes.StatusCode != 200) throw new Exception("Failedd Roles Assign !");
            }
        }
            
    }
}
            

            
           
        
    

