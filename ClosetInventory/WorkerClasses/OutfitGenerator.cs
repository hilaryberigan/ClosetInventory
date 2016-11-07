using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosetInventory.Models;
using System.Threading.Tasks;

namespace ClosetInventory.WorkerClasses
{
    public class Collections
    {
        public List<Cover> Covers { get; set; }
        public List<Pants> PantsList { get; set; }
        public List<Dress> Dresses { get; set; }
        public List<Shirt> Shirts { get; set; }
        public List<Skirt> Skirts { get; set; }
        public List<Shoe> Shoes { get; set; }

    }
    public class OutfitGenerator
    {
        public async Task<TotalViewModel> AddOutfitToViewModel(ApplicationDbContext db, TotalViewModel model)
        {

            string userId = model.User.Id;
            WeatherSearch weather = new WeatherSearch();
            Weather weatherModel = new Weather();
            DateTime date = DateTime.Today;
            var temp = 70.0;

            weatherModel = db.Weathers.Where(m => m.UserId == userId && m.Date == date).FirstOrDefault();

            if (weatherModel != null)
            {
                temp = weatherModel.Temperature;
            }
            else {

                weatherModel = new Weather();


                model.Covers = db.Covers.Where(m => m.UserId == userId).ToList();
                model.Pants = db.Pants.Where(m => m.UserId == userId).ToList();
                model.Shoes = db.Shoes.Where(m => m.UserId == userId).ToList();
                model.Skirts = db.Skirts.Where(m => m.UserId == userId).ToList();
                model.Shirts = db.Shirts.Where(m => m.UserId == userId).ToList();
                model.Dresses = db.Dresses.Where(n => n.UserId == userId).ToList();

                //finds outfits from today with currentoutfitid


                var temperature = weather.GetTemperature(weatherModel);
                temp = await temperature;
                weatherModel.Temperature = temp;
                weatherModel.Date = date;
                db.Weathers.Add(weatherModel);
                db.SaveChanges();
            }

            model.Outfit = MakeOutfit(model.User, db, temp, model.Dressiness);
            
      
            
            return model;


            }


            string[] badWithRed =
        {
            "green",
            "orange",
            "light orange",
            "dark orange"
        };

        string[] badWithOrange =
        {
            "purple",
            "light purple",
            "dark purple",
            "green",
            "light green",
            "dark green",
            "red",
            "light red",
            "dark red"
        };

        public Collections GetDressyClothes(Collections collections)
        {

            collections.Shirts = collections.Shirts.Where(m => m.DressinessRating > 7).ToList();
            collections.Shoes = collections.Shoes.Where(m => m.DressinessRating > 7).ToList();
            collections.Dresses = collections.Dresses.Where(m => m.DressinessRating > 7).ToList();
            collections.PantsList = collections.PantsList.Where(m => m.DressinessRating > 7).ToList();
            collections.Covers = collections.Covers.Where(m => m.DressinessRating > 7).ToList();

            return collections;
        }

        public Collections GetBusinessCasualClothes(Collections collections)
        {
            collections.Shirts = collections.Shirts.Where(m => m.DressinessRating > 4).ToList();
            collections.Shoes = collections.Shoes.Where(m => m.DressinessRating > 5 && m.DressinessRating < 9).ToList();
            collections.Dresses = collections.Dresses.Where(m => m.DressinessRating > 4 && m.DressinessRating < 8).ToList();
            collections.PantsList = collections.PantsList.Where(m => m.DressinessRating > 5).ToList();
            collections.Covers = collections.Covers.Where(m => m.DressinessRating > 4).ToList();

            return collections;
        }
        public Collections GetCasualClothes(Collections collections)
        {
            collections.Shirts = collections.Shirts.Where(m => m.DressinessRating <= 7).ToList();
            collections.Shoes = collections.Shoes.Where(m => m.DressinessRating <= 5 ).ToList();
            collections.Dresses = collections.Dresses.Where(m => m.DressinessRating <= 5).ToList();
            collections.PantsList = collections.PantsList.Where(m => m.DressinessRating <= 5).ToList();
            collections.Covers = collections.Covers.Where(m => m.DressinessRating <= 6).ToList();

            return collections;
        }
        public Collections GetWarmClothes(Collections collections)
        {
            
            collections.Shirts = collections.Shirts.Where(m => m.WarmthRating > 5).ToList();
            collections.Shoes = collections.Shoes.Where(m => m.WarmthRating > 6).ToList();
            collections.Dresses = collections.Dresses.Where(m => m.WarmthRating > 5).ToList();
            collections.PantsList = collections.PantsList.Where(m => m.WarmthRating > 3 && m.isCapri != true && m.isShorts != true).ToList();
            collections.Covers = collections.Covers.Where(m => m.WarmthRating > 3).ToList();

            return collections;
        }
        public Collections GetCoolClothes(Collections collections)
        {
            collections.Shirts = collections.Shirts.Where(m => m.WarmthRating <= 7).ToList();
            collections.Shoes = collections.Shoes.Where(m => m.WarmthRating <= 6).ToList();
            collections.Dresses = collections.Dresses.Where(m => m.WarmthRating <= 8).ToList();
            collections.PantsList = collections.PantsList.Where(m => m.WarmthRating <= 8 && m.isShorts != true).ToList();
            collections.Covers = collections.Covers.Where(m => m.WarmthRating <= 7).ToList();
            return collections;
        }


        public Cover GetCover(Outfit outfit, List<Cover> covers)
        {
            Random random = new Random();
            Cover cover = new Cover();
            try {
                if (covers.Count > 0)
                {
                    if (outfit.Shirt != null)
                    {
                        var oppositeCovers = covers.Where(m => m.ColorType != outfit.Shirt.ColorType && m.Color != outfit.Shirt.Color).ToList();
                        int a = random.Next(0, oppositeCovers.Count);
                        cover = oppositeCovers[a];
                    }
                    else if (outfit.Dress != null)
                    {
                        var oppositeCovers = covers.Where(m => m.ColorType != outfit.Dress.ColorType && m.Color != outfit.Dress.Color).ToList();
                        int a = random.Next(0, oppositeCovers.Count);
                        cover = oppositeCovers[a];
                    }
                    return cover;
                }
                return null;
            }
            catch { }
            return null;
        }

        public Pants GetPants(Outfit outfit, List<Pants> pantsList)
        {
            Random random = new Random();
            Pants pants = new Pants();
            try
            {
                if (pantsList.Count > 0)
                {
                    var oppositePants = pantsList.Where(m => m.ColorType != outfit.Shirt.ColorType && m.Color != outfit.Shirt.Color).ToList();
                    int a = random.Next(0, oppositePants.Count);
                    pants = oppositePants[a];

                    return pants;
                }
                return null;
            }
            catch { }
            return null;
          }
        public Shoe GetShoes(Outfit outfit, List<Shoe> shoes)
        {
            Random random = new Random();
            Shoe shoe = new Shoe();
            try
            {
                if (shoes.Count > 0)
                {
                    if (outfit.Cover.Color == "black" || outfit.Pants.Color == "black")
                    {
                        shoes = shoes.Where(m => m.Color != "brown" || m.Color != "lightbrown" || m.Color != "darkbrown").ToList();
                    }
                    shoes = shoes.Where(m => m.ColorType != outfit.Shirt.ColorType).ToList();

                    int a = random.Next(0, shoes.Count);
                    shoe = shoes[a];

                    return shoe;
                }
                return null;
            }
            catch { };
            return null;
        }


        public Outfit MakeOutfit(ApplicationUser user, ApplicationDbContext db, double temperature, string outfitType)
        {
            Random random = new Random();
            Outfit outfit = new Outfit();
            Collections collections = new Collections();

            collections.Covers = (from x in db.Covers where x.UserId == user.Id select x).ToList();
            collections.PantsList = (from y in db.Pants where y.UserId == user.Id select y).ToList();
            collections.Shoes = (from z in db.Shoes where z.UserId == user.Id select z).ToList();
            collections.Skirts = (from j in db.Skirts where j.UserId == user.Id select j).ToList();
            collections.Shirts = (from k in db.Shirts where k.UserId == user.Id select k).ToList();
            collections.Dresses = (from l in db.Dresses where l.UserId == user.Id select l).ToList();


            if (temperature < 40)
            {
                collections = GetWarmClothes(collections);
            }
            else
            {
                collections = GetCoolClothes(collections);
            }


            if (outfitType == "dressy")
            {
                collections = GetDressyClothes(collections);
            }
            else if (outfitType == "business casual")
            {
                collections = GetBusinessCasualClothes(collections);
            }

            else if (outfitType == "casual")
            {
                collections = GetCasualClothes(collections);
            }


            
                if (collections.Shirts.Count > 0)
                {
                    int i = random.Next(0, 10);

                    if (i != 1)
                    {
                        int e = random.Next(0, collections.Shirts.Count);
                        outfit.Shirt = collections.Shirts[e];

                        outfit.Pants = GetPants(outfit, collections.PantsList);
                    }
                    else
                    {
                        if (collections.Dresses.Count > 0)
                        {
                            int e = random.Next(0, collections.Dresses.Count);
                            outfit.Dress = collections.Dresses[e];
                        }
                        else
                        {
                            int e = random.Next(0, collections.Shirts.Count);
                            outfit.Shirt = collections.Shirts[e];
                        }
                    }


                }
                else if (collections.Dresses.Count > 0)
                {
                    int e = random.Next(0, collections.Dresses.Count);
                    outfit.Dress = collections.Dresses[e];
                }


                if (outfit.Shirt != null || outfit.Dress != null)
                {
                    outfit.Cover = GetCover(outfit, collections.Covers);
                    outfit.Shoe = GetShoes(outfit, collections.Shoes);
                    outfit.Date = DateTime.Today.Date;
                    outfit.UserId = user.Id;
                    db.Outfits.Add(outfit);
                    db.SaveChanges();
                }

            return outfit;
        }


            
        }
    }
