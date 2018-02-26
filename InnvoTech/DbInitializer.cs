using System;
using System.Linq;
using InnvoTech.Models;

namespace InnvoTech
{

    //Subsitute from using a sql database directing the connection to BobTest1 instead of BobTest to use Entity Framework
    internal class DbInitializer
    {
        internal static void Initialize(BobTestContext context)
        {
            //Before "Seeding", make sure the database exists. 
            context.Database.EnsureCreated();
            //Once created, you can start adding records, if none exists.
           if (!context.Products.Any())
            {
                //If no records are present in the Table, add some:
                context.Products.AddRange(new Products
                {
                    Name = "The BiSki",
                    Price = 3499.99m,
                    Description = "The Biski is truly unique; as a single seat(or single plus pillion), twin jet, HSA Motorcycle, it is a world’s first in many ways. At just 2.3m long and under 1m wide, it is the smallest of all Gibbs High speed amphibious platforms, and very probably the most technically advanced.It represents true freedom for the individual; serious fun.",
                    ImageUrl = "/images/biskigify.gif",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products

                {
                    Name = "Google Glass",
                    Price = 2149.99m,
                    Description = "Google Glass is an optical head-mounted display designed in the shape of a pair of eyeglasses. It was developed by X with the mission of producing a ubiquitous computer. Google Glass displayed information in a smartphone-like hands-free format.",
                    ImageUrl = "/images/googleglass.gif",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now

                }, new Products

                {
                    Name = "Portable Language Translation Device",
                    Price = 499.99m,
                    Description = "This little gadget will translate the words you speak in as little as 0.2 seconds and say them right back at you in another language. More specifically, it translates English into Japanese, Chinese, and Spanish.",
                    ImageUrl = "/images/portablelanguagetranslator.jpg",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "Massaging Robot",
                    Price = 399.99m,
                    Description = "A palm-sized robot, designed solely for your relaxation. Using a unique sensor technology, WheeMe moves along the back, creating wave after wave of soothing sensations, without falling down or losing grip. Enjoy your own portable massage therapist anytime, anywhere.",
                    ImageUrl = "/images/massagingrobot.jpg",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "The QuadSki",
                    Price = 8699.99m,
                    Description = "Power comes from a 1.3-liter, four-cylinder BMW engine that pumps out 175 horsepower and 103 pound-feet of torque. Output is limited to 80 hp on land, while a six-speed automatic gearbox powers it through the twisties and inclines.  The top speed is limited to 45 mph on land and water",
                    ImageUrl = "/images/quadski.gif",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "Third Thumb Attatchment",
                    Price = 59.49m,
                    Description = "The Third Thumb Attachment gives you a new functional thumb to use in a variety of ways. The movement of the thumb is ensured by two motors hiding inside of it. But the real culprits that run the show are your toes. Your toes can put pressure onto two sensors that hide in your shoes. The sensors, in return, send the signal back to the thumb via Bluetooth and “tell” it what it’s supposed to do.",
                    ImageUrl = "/images/thirdthumb.gif",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "SlidenJoy Dual Laptop Monitors",
                    Price = 369.49m,
                    Description = "You might have thought that getting yourself a dual or triple monitor for a desktop or laptop would keep you tied to one place. That’s not the case anymore. The use of this portable monitor is easy. As in “click, connect, enjoy” easy. Not only can you attach it to your display like a magnet, the additional slide-out monitors can be rotated to allow you a 360º sharing experience.",
                    ImageUrl = "/images/dualmonitor.jpg",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "Electric Wake Board",
                    Price = 499.99m,
                    Description = "This electric wakeboard has the state of the art electric motor inside the board that is not only water proof, but also long lasting and supercharged. This wakeboard effectively lets you surf without waves at up to 36 mph (58 km/h",
                    ImageUrl = "/images/wakeboard.gif",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }, new Products
                {
                    Name = "Apple Watch",
                    Price = 679.49m,
                    Description = "Space aluminum case, Built-in GPS and GLONASS, Faster dual-core processor, W2 chip, Barometric altimeter, Capacity 8GB, Heart rate sensor Accelerometer and gyroscope, Water resistant 50 meters, Ion-X strengthened glass, Composite back Wi-Fi (802.11b/g/n 2.4GHz), Bluetooth 4.2 Up to 18 hours of battery life3 watchOS 4",
                    ImageUrl = "/images/applewatch.jpg",
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now
                }

                );
                context.SaveChanges();
            }
        }
    }
}