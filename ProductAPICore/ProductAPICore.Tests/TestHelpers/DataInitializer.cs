using ProductAPICore.API.ViewModels;
using System.Collections.Generic;

namespace ProductAPICore.Tests.TestHelpers
{
    public class DataInitializer
    {
        public static List<GetProductViewModel> GetAllProducts()
        {
            var products = new List<GetProductViewModel>()
            {
                new GetProductViewModel
                {
                    Id = 1,
                    Name = "Galaxy Phone",
                    ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                    Price = 5000,
                    CompanyId = 1,
                    CompanyName = "Samsung"
                },
                new GetProductViewModel
                {
                    Id = 2,
                    Name = "SoundSport In Ear",
                    ImageUrl = "https://cf4.s3.souqcdn.com/item/2019/02/20/11/20/12/78/item_XL_11201278_53eb72d3242ff.jpg",
                    Price = 3240,
                    CompanyId = 5,
                    CompanyName = "Bose"
                },
                new GetProductViewModel
                {
                    Id = 3,
                    Name = "Bose Soundtouch",
                    ImageUrl = "https://cf1.s3.souqcdn.com/item/2015/10/13/92/73/07/0/item_XL_9273070_10014657.jpg",
                    Price = 12584,
                    CompanyId = 5,
                    CompanyName = "Bose"
                },
                new GetProductViewModel
                {
                    Id = 4,
                    Name = "Boxing Man",
                    ImageUrl = "https://cf2.s3.souqcdn.com/item/2018/03/01/31/96/74/33/item_XL_31967433_115078999.jpg",
                    Price = 6450,
                    CompanyId = 4,
                    CompanyName = "Body Sculpture"
                },
                new GetProductViewModel
                {
                    Id = 1,
                    Name = "Galaxy Phone",
                    ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                    Price = 5000,
                    CompanyId = 1,
                    CompanyName = "Samsung"
                },
                new GetProductViewModel
                {
                    Id = 5,
                    Name = "Exercise Wheel",
                    ImageUrl = "https://cf1.s3.souqcdn.com/item/2016/09/29/11/61/50/53/item_XL_11615053_16634901.jpg",
                    Price = 175,
                    CompanyId = 4,
                    CompanyName = "Body Sculpture"
                },
                new GetProductViewModel
                {
                    Id = 6,
                    Name = "Dumbbell Set-Purple",
                    ImageUrl = "https://cf4.s3.souqcdn.com/item/2017/03/23/22/29/73/12/item_XL_22297312_30099394.jpg",
                    Price = 215,
                    CompanyId = 4,
                    CompanyName = "Body Sculpture"
                },
                new GetProductViewModel
                {
                    Id = 7,
                    Name = "Multi Gym",
                    ImageUrl = "https://cf2.s3.souqcdn.com/item/2018/11/14/40/72/78/73/item_XL_40727873_3421e85bc4e16.jpg",
                    Price = 7450,
                    CompanyId = 4,
                    CompanyName = "Body Sculpture"
                },
                new GetProductViewModel
                {
                    Id = 1,
                    Name = "Galaxy Phone",
                    ImageUrl = "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$",
                    Price = 5000,
                    CompanyId = 1,
                    CompanyName = "Samsung"
                },
                new GetProductViewModel
                {
                    Id = 8,
                    Name = "Technic Telehandler Building Toy",
                    ImageUrl = "https://cf3.s3.souqcdn.com/item/2017/01/30/21/97/18/49/item_XL_21971849_28369612.jpg",
                    Price = 799,
                    CompanyId = 3,
                    CompanyName = "LEGO"
                },
                new GetProductViewModel
                {
                    Id = 9,
                    Name = "Ninjago Master Zane",
                    ImageUrl = "https://cf5.s3.souqcdn.com/item/2018/01/28/30/43/46/86/item_XL_30434686_100791294.jpg",
                    Price = 349,
                    CompanyId = 3,
                    CompanyName = "LEGO"
                },
                new GetProductViewModel
                {
                    Id = 10,
                    Name = "Creator Expert Ferrari F40",
                    ImageUrl = "https://cf5.s3.souqcdn.com/item/2018/12/26/10/26/31/86/item_XL_10263186_9577f0a67b6a0.jpg",
                    Price = 2599,
                    CompanyId = 3,
                    CompanyName = "LEGO"
                }
            };
            return products;
        }

    }
}