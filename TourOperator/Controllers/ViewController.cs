﻿using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using TourOperator.Contexts;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class ViewController : Controller
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;

    public ViewController(TourDbContext tourDbContext, ILogger<ViewController> logger)
    {
        _tourDbContext = tourDbContext;
        _logger = logger;
    }

    public int BasketCount()
    {
        if (! User.Identity.IsAuthenticated)
        {
            return -1;
        }

        return _tourDbContext.BasketItems.Count(i => i.Username == User.Identity.Name);
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Message = new Hashtable{{"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("login")]
    public IActionResult Login()
    {
        ViewBag.Message = new Hashtable{{"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        ViewBag.Message = new Hashtable{{"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("customer")]
    [Authorize]
    public IActionResult Customer()
    {
        ViewBag.Message = new Hashtable{{"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("Basket")]
    public IActionResult Basket()
    {
        ViewBag.Message = new Hashtable{{"tourDb", _tourDbContext}, {"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("Hotels")]
    public IActionResult Hotels()
    {
        ViewBag.Message = new Hashtable{{"tourDb", _tourDbContext}, {"basketCount", BasketCount()}};
        return View();
    }
    
    [HttpGet("Hotel/{hotelId}")]
    public IActionResult Hotel(int hotelId)
    {
        Hotel? hotel = _tourDbContext.Hotels.Find(hotelId);

        if (hotel == null)
        {
            return Problem($"No such hotel: {hotelId}");
        }
        
        ViewBag.Message = new Hashtable{{"hotel", hotel}, {"tourDb", _tourDbContext}, {"basketCount", BasketCount()}};
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
