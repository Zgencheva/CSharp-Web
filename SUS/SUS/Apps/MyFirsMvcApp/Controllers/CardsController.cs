﻿using BattleCards.Data;
using BattleCards.ViewModels;
using MyFirsMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace MyFirsMvcApp.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse Add()
        {
            return this.View();
        }
        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd() 
        {
           
            var dbContext = new ApplicationDbContext();
            if (this.Request.FormData["name"].Length <5)
            {
                return this.Error("Name should be at least five characters long");
            }
            dbContext.Cards.Add(new Card 
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"],
            });
            dbContext.SaveChanges();

            return this.Redirect("/Cards/All");
        }
        public HttpResponse All()
        {
            var db = new ApplicationDbContext();
            var cardsViewModel = db.Cards.Select(x => new CardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.Keyword,
            })
            .ToList();
            return this.View(cardsViewModel);
        }
        public HttpResponse Collection()
        {
            return this.View();
        }
    }
}
