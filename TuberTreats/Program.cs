



using TuberTreats.Models;
using TuberTreats.Models.DTOs;

List<Customer> customers = new List<Customer> { };
List<Topping> toppings = new List<Topping> { };
List<TuberDriver> tuberDrivers = new List<TuberDriver> { };
List<TuberOrder> tuberOrders = new List<TuberOrder> { };
List<TuberTopping> tuberToppings = new List<TuberTopping> { };

// Lists

// 3 drivers
List<TuberDriver> tuberDriver = new List<TuberDriver>()
{
    new TuberDriver()
    {
        Id = 1,
        Name = "Matt",
    },
    new TuberDriver()
    {
        Id = 2,
        Name = "Liz",
    },
    new TuberDriver()
    {
        Id = 3,
        Name = "Rachel",
    }
};


// 5 customers
List<Customer> customer = new List<Customer>()
{
    new Customer()
    {
        Id = 1,
        Name = "Emma",
        Address = "123 book dr."
    },
    new Customer()
    {
        Id = 2,
        Name = "Gus",
        Address = "123 book dr."
    },
    new Customer()
    {
        Id = 3,
        Name = "Sydney",
        Address = "225 Ingowlhood dr."
    },
    new Customer()
    {
        Id = 4,
        Name = "Jadyn",
        Address = "354 Matterhorn dr."
    },
    new Customer()
    {
        Id = 5,
        Name = "Tandy",
        Address = "203 Punk rd."
    }
};


// 5 toppings
List<Topping> Topping = new List<Topping>()
{
    new Topping()
    {
        Id = 1,
        Name = "Bacon"
    },
    new Topping()
    {
        Id = 2,
        Name = "Sour Cream"
    },
    new Topping()
    {
        Id = 3,
        Name = "Chives"
    },
    new Topping()
    {
        Id = 4,
        Name = "Pork"
    },
    new Topping()
    {
        Id = 5,
        Name = "Butter"
    },


};


// 3 orders
List<TuberOrder> tuberOrder = new List<TuberOrder>()
{
    new TuberOrder()
    {
        Id = 1,
        OrderPlacedOnDate = new DateTime(2023, 12, 19),
        CustomerId = 2,
        TuberDriverId = 1,
        DeliveredOnDate = new DateTime(2023, 12, 19)
    },
    new TuberOrder()
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2023, 12, 31),
        CustomerId = 5,
        TuberDriverId = 3,
        DeliveredOnDate = new DateTime(2024, 1, 1)
    },
    new TuberOrder()
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2023, 12, 26),
        CustomerId = 3,
        TuberDriverId = 2,
        DeliveredOnDate = new DateTime(2023, 12, 26)
    }
};   


// tubertoppings
List<TuberTopping> tuberTopping = new List<TuberTopping>()
{
    new TuberTopping()
    {
        Id = 1,
        TuberOrderId = 2,
        ToppingId = 5
    },
    new TuberTopping()
    {
        Id = 2,
        TuberOrderId = 2,
        ToppingId = 1
    },
    new TuberTopping()
    {
        Id = 3,
        TuberOrderId = 1,
        ToppingId = 3
    }
};





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//add endpoints here

app.MapGet("/api/tuberorders", () =>
{
    return tuberOrder.Select(t => new TuberOrderDTO
    {
        Id = t.Id,
        OrderPlacedOnDate = t.OrderPlacedOnDate,
        CustomerId = t.CustomerId,
        TuberDriverId = t.TuberDriverId,
        DeliveredOnDate = t.DeliveredOnDate
    });
});








app.Run();
//don't touch or move this!
public partial class Program { }