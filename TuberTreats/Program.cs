



using TuberTreats.Models;
using TuberTreats.Models.DTO;
using TuberTreats.Models.DTOs;

List<Customer> customers = new List<Customer> { };
List<Topping> toppings = new List<Topping> { };
List<TuberDriver> tuberDrivers = new List<TuberDriver> { };
List<TuberOrder> tuberOrders = new List<TuberOrder> { };
List<TuberTopping> tuberToppings = new List<TuberTopping> { };

// Lists


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
List<Topping> topping = new List<Topping>()
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


app.MapGet("/api/tuberorders/{id}", (int id) =>
{
    TuberOrder order = tuberOrder.FirstOrDefault(d => d.Id == id);

    if (order == null)
    {
        return Results.NotFound();
    }

    var correctCustomer = customer.FirstOrDefault(c => c.Id == order.CustomerId);
    var driver = tuberDriver.FirstOrDefault(d => d.Id == order.TuberDriverId);
    var toppings = tuberTopping.Where(t => t.TuberOrderId == order.Id).ToList();

    return Results.Ok(new TuberOrderDTO
    {
        Id = order.Id,
        OrderPlacedOnDate = order.OrderPlacedOnDate,
        CustomerId = order.CustomerId,
        TuberDriverId = order.TuberDriverId,
        DeliveredOnDate = order.DeliveredOnDate,
        Customer = new CustomerDTO
        {
            Id = correctCustomer.Id,
            Name = correctCustomer.Name,
            Address = correctCustomer.Address
        },
        TuberDriver = new TuberDriverDTO
        {
            Id = driver.Id,
            Name = driver.Name
        },
        Toppings = toppings.Select(t => new TuberToppingDTO
        {
            Id = t.Id,
            TuberOrderId = t.TuberOrderId,
            ToppingId = t.ToppingId
        }).ToList()
    });
});




app.MapGet("/api/toppings", () =>
{
    return topping.Select(t => new ToppingDTO
    {
        Id = t.Id,
        Name = t.Name
    });
});



app.MapGet("/api/toppings/{id}", (int id) =>
{
    Topping byId = topping.FirstOrDefault(e => e.Id == id);

    if (byId == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new ToppingDTO
    {
        Id = byId.Id,
        Name = byId.Name
    });
});

app.MapPost("/api/toppings", (Topping newTopping) =>
{
    newTopping.Id = topping.Max(c => c.Id) + 1;

    topping.Add(newTopping);

    return Results.Created($"/api/toppings/{newTopping.Id}", new ToppingDTO
    {
        Id = newTopping.Id,
        Name = newTopping.Name
    });
});

app.MapDelete("/api/toppings/{id}", (int id) =>
{
    Topping byId = topping.FirstOrDefault(e => e.Id == id);

    if (byId == null)
    {
        return Results.NotFound();
    }

    topping.Remove(byId);

    return Results.Ok();
});





app.MapGet("/api/tubertoppings", () =>
{
    return tuberTopping.Select(t => new TuberToppingDTO
    {
        Id = t.Id,
        TuberOrderId = t.TuberOrderId,
        ToppingId = t.ToppingId
    });
});




app.MapGet("/api/customers", () =>
{
    return customer.Select(t => new CustomerDTO
    {
        Id = t.Id,
        Name = t.Name,
        Address = t.Address
    });
});

app.MapGet("/api/customers/{id}", (int id) =>
{
    Customer customerById = customer.FirstOrDefault(c => c.Id == id);

    if (customerById == null)
    {
        return Results.NotFound();
    }

    var customerOrders = tuberOrder.Where(o => o.CustomerId == id).ToList();

    return Results.Ok(new CustomerDTO
    {
        Id = customerById.Id,
        Name = customerById.Name,
        Address = customerById.Address,
        TuberOrders = customerOrders.Select(o => new TuberOrderDTO
        {
            Id = o.Id,
            OrderPlacedOnDate = o.OrderPlacedOnDate,
            CustomerId = o.CustomerId,
            TuberDriverId = o.TuberDriverId,
            DeliveredOnDate = o.DeliveredOnDate
        }).ToList()
    });
});


app.MapPost("/api/customers", (Customer newCustomer) =>
{
    newCustomer.Id = customer.Max(c => c.Id) + 1;

    customer.Add(newCustomer);

    return Results.Created($"/api/customers/{newCustomer.Id}", new CustomerDTO
    {
        Id = newCustomer.Id,
        Name = newCustomer.Name,
        Address = newCustomer.Address
    });
});

app.MapDelete("/api/customers/{id}", (int id) =>
{
    Customer customerById = customer.FirstOrDefault(c => c.Id == id);

    if (customerById == null)
    {
        return Results.NotFound();
    }

    customer.Remove(customerById);

    return Results.Ok();
});



app.MapGet("/api/tuberdrivers", () =>
{
    return tuberDriver.Select(t => new TuberDriverDTO
    {
        Id = t.Id,
        Name = t.Name
    });
});


app.MapGet("/api/tuberdrivers/{id}", (int id) =>
{
    TuberDriver driver = tuberDriver.FirstOrDefault(d => d.Id == id);

    if (driver == null)
    {
        return Results.NotFound();
    }


    var driverOrders = tuberOrder.Where(o => o.TuberDriverId == id).ToList();

    return Results.Ok(new TuberDriverDTO
    {
        Id = driver.Id,
        Name = driver.Name,
        TuberDeliveries = driverOrders.Select(o => new TuberOrderDTO
        {
            Id = o.Id,
            OrderPlacedOnDate = o.OrderPlacedOnDate,
            CustomerId = o.CustomerId,
            TuberDriverId = o.TuberDriverId,
            DeliveredOnDate = o.DeliveredOnDate
        }).ToList()
    });
});






app.Run();
//don't touch or move this!
public partial class Program { }