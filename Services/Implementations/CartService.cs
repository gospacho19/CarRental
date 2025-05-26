using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using LuxuryCarRental.Data;
using LuxuryCarRental.Models;
using LuxuryCarRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuxuryCarRental.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _ctx;
        private readonly IPricingService _pricing;

        public CartService(AppDbContext ctx, IPricingService pricing)
        {
            _ctx = ctx;
            _pricing = pricing;
        }

        public void AddToCart(int customerId, Car car, DateRange period, IEnumerable<string> options)
        {
            // a) Load customer for the Basket ctor
            var customer = _ctx.Customers.Find(customerId)
                         ?? throw new InvalidOperationException("Customer not found.");

            // b) find or create Basket
            var basket = _ctx.Baskets
                .Include(b => b.Items)
                .FirstOrDefault(b => b.CustomerId == customerId)
              ?? new Basket(customerId, customer);

            // c) create the CartItem
            var item = new CartItem(
                basket,
                car,
                period,
                options ?? Array.Empty<string>()
            );

            basket.Items.Add(item);
            _ctx.SaveChanges();
        }


        public IEnumerable<CartItem> GetCartItems(int customerId)
            => _ctx.CartItems.Where(ci => ci.Basket.CustomerId == customerId).ToList();

        public Money GetCartTotal(int customerId)
        {
            var total = GetCartItems(customerId).Sum(ci => ci.Subtotal);
            return new Money(total, "USD");
        }

        public void RemoveFromCart(int customerId, int cartItemId)
        {
            var item = _ctx.CartItems.Find(cartItemId);
            if (item != null)
            {
                _ctx.CartItems.Remove(item);
                _ctx.SaveChanges();
            }
        }

        public void ClearCart(int customerId)
        {
            var items = _ctx.CartItems.Where(ci => ci.Basket.CustomerId == customerId);
            _ctx.CartItems.RemoveRange(items);
            _ctx.SaveChanges();
        }
    }
}
