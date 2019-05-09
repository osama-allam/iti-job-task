using ProductAPICore.Model.Core;
using ProductAPICore.Model.Core.Domains;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPICore.Model.Persistence
{
    public static class UnitOfWorkExtensions
    {
        public static void EnsureSeedDataForContext(this IUnitOfWork unitOfWork)
        {
            if (!unitOfWork.Companies.GetAll().Any())
            {
                var companies = new List<Company>
                {
                    new Company
                    {
                        Name = "Samsung",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "Galaxy Phone",
                                Price = 5000,
                                ImageUrl =
                                    "https://ss7.vzw.com/is/image/VerizonWireless/SAMSUNG_Galaxy_S9_Plus_Purple?$device-lg$"
                            },
                            new Product
                            {
                                Name = "Samsung U Flex Headphones",
                                Price = 1221.15,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2017/08/06/23/46/75/67/item_XL_23467567_34171510.jpg"
                            },
                            new Product
                            {
                                Name = "Samsung 32Inch-HD Smart TV",
                                Price = 1221.15,
                                ImageUrl =
                                    "https://cf5.s3.souqcdn.com/item/2018/07/29/36/68/65/13/item_XL_36686513_144118606.jpg"
                            },
                            new Product
                            {
                                Name = "Gear Fit 2 Pro Large",
                                Price = 2700,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2017/11/07/27/75/60/15/item_XL_27756015_67869854.jpg"
                            }
                        }
                    },
                    new Company
                    {
                        Name = "Unionaire",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "Stainless Table Gas Stove",
                                Price = 800,
                                ImageUrl =
                                    "https://cf4.s3.souqcdn.com/item/2016/06/13/10/91/87/13/item_XL_10918713_14843562.jpg"
                            },
                            new Product
                            {
                                Name = "Unify G+ 012",
                                Price = 6099,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2016/05/12/10/72/16/13/item_XL_10721613_14297749.jpg"
                            },
                            new Product
                            {
                                Name = "Unionair Stand Fan",
                                Price = 599,
                                ImageUrl =
                                    "https://cf1.s3.souqcdn.com/item/2017/08/02/10/21/61/14/item_XL_10216114_34008055.jpg"
                            },
                            new Product
                            {
                                Name = "Electric Water Heater 50 Liter",
                                Price = 1240,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2017/11/07/27/75/60/15/item_XL_27756015_67869854.jpg"
                            }
                        }
                    },
                    new Company
                    {
                        Name = "LEGO",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "LEGO 76058 Spider Man",
                                Price = 801,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2019/04/16/10/88/21/36/item_XL_10882136_f8ac1f058787f.jpg"
                            },
                            new Product
                            {
                                Name = "Creator Expert Ferrari F40",
                                Price = 2599,
                                ImageUrl =
                                    "https://cf5.s3.souqcdn.com/item/2018/12/26/10/26/31/86/item_XL_10263186_9577f0a67b6a0.jpg"
                            },
                            new Product
                            {
                                Name = "Ninjago Master Zane",
                                Price = 349,
                                ImageUrl =
                                    "https://cf5.s3.souqcdn.com/item/2018/01/28/30/43/46/86/item_XL_30434686_100791294.jpg"
                            },
                            new Product
                            {
                                Name = "Technic Telehandler Building Toy",
                                Price = 799,
                                ImageUrl =
                                    "https://cf3.s3.souqcdn.com/item/2017/01/30/21/97/18/49/item_XL_21971849_28369612.jpg"
                            }
                        }
                    },
                    new Company
                    {
                        Name = "Body Sculpture",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "Multi Gym",
                                Price = 7450,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2018/11/14/40/72/78/73/item_XL_40727873_3421e85bc4e16.jpg"
                            },
                            new Product
                            {
                                Name = "Dumbbell Set-Purple",
                                Price = 215,
                                ImageUrl =
                                    "https://cf4.s3.souqcdn.com/item/2017/03/23/22/29/73/12/item_XL_22297312_30099394.jpg"
                            },
                            new Product
                            {
                                Name = "Exercise Wheel",
                                Price = 175,
                                ImageUrl =
                                    "https://cf1.s3.souqcdn.com/item/2016/09/29/11/61/50/53/item_XL_11615053_16634901.jpg"
                            },
                            new Product
                            {
                                Name = "Boxing Man",
                                Price = 6450,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2018/03/01/31/96/74/33/item_XL_31967433_115078999.jpg"
                            }
                        }
                    },
                    new Company
                    {
                        Name = "Bose",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Name = "Bose Soundtouch",
                                Price = 12584,
                                ImageUrl =
                                    "https://cf1.s3.souqcdn.com/item/2015/10/13/92/73/07/0/item_XL_9273070_10014657.jpg"
                            },
                            new Product
                            {
                                Name = "SoundSport In Ear",
                                Price = 3240,
                                ImageUrl =
                                    "https://cf4.s3.souqcdn.com/item/2019/02/20/11/20/12/78/item_XL_11201278_53eb72d3242ff.jpg"
                            },
                            new Product
                            {
                                Name = "Bose Soundlink",
                                Price = 4299,
                                ImageUrl =
                                    "https://cf4.s3.souqcdn.com/item/2016/02/16/99/70/76/3/item_XL_9970763_12357271.jpg"
                            },
                            new Product
                            {
                                Name = "SoundLink Micro",
                                Price = 2505,
                                ImageUrl =
                                    "https://cf2.s3.souqcdn.com/item/2017/11/23/28/29/59/86/item_XL_28295986_73487242.jpg"
                            }
                        }
                    }
                };

                unitOfWork.Companies.AddRange(companies);
                unitOfWork.Complete();
            }
        }
    }
}