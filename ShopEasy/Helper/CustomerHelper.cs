using Microsoft.EntityFrameworkCore;
using ShopEasy.Data;
using ShopEasy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopEasy.Helper
{
    public static class CustomerHelper
    {
        private static readonly AppDbContext context = new AppDbContext();
        public static void AddCustomerAndProfile()
        {
            

            var Customer = new Customer
            {
                FullName = "nada emad",
                Email = "nada@gmail.com",
                PhoneNumber = "01019999999",
                Profile = new CustomerProfile
                {
                    Address = "hehia",
                    City = "Sharkia",
                    PostalCode = "14554",
                    NationalId = "30555555555555",
                }
            };
            context.Customers.Add(Customer);
            context.SaveChanges();
        }

        public static Customer GetCustomerDetails(int customerId)
        {
            var PersonalDetails = context.Customers.Include(c => c.Profile)
                                                   .Include(c => c.Orders)
                                                   .SingleOrDefault(c => c.CustomerId == customerId);

            if (PersonalDetails == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found.");
            }
            return PersonalDetails;
        }

        public static void UpdateAdressProfile(int customerId, string NewAddress)
        {
            var result = context.Customers.SingleOrDefault(c => c.CustomerId == customerId);

            if (result == null)
            {
                Console.WriteLine($"Customer with ID: {customerId} not found.");
                return;
            }
            context.Entry(result).Reference(c => c.Profile).Load();
            result.Profile.Address = NewAddress;

            Console.WriteLine("Address Updated");
        }
    }
}
