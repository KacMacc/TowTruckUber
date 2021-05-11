using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowTruckUberAPI.Models;

namespace TowTruckUberAPI.Services
{
    public static class Notification
    {
        public static void SendNotificationToCustomer(Trip trip)
        {
            string customerEmail = trip.Customer.Email;

            string subject = $"Trip {trip.Id} on {trip.CreatedAt}";
            string message =
                $"Hello\n" +
                $"We sending you data about your last trip with number{trip.Id} created at {trip.CreatedAt}.\n" +
                $"Your start location was:\n{trip.StartLocation.ToStringForEmail()}\n with end in \n{trip.EndLocation.ToStringForEmail()}\n" +
                $"The distance of trip was{trip.Distance}, with price {trip.Price}.\n" +
                $"Status of your trip is {trip.Status}." +
                $"Thank you for using our services!\n" +
                $"Best Regards" +
                $"Tow Truck Uber devs";

            EmailSender.SendEmailAsync(customerEmail, subject, message);
        }
    }
}
