﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        SuperMarket214338766Context _superMarketContext;

        public OrderRepository(SuperMarket214338766Context superMarket214338766Context)
        {
            _superMarketContext = superMarket214338766Context;
        }

        public async Task<Order> createNewOrder(Order order)
        {
            await _superMarketContext.Orders.AddAsync(order);
            await _superMarketContext.SaveChangesAsync();
            return order;
        }

    }
}
