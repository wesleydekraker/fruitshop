using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FruitShopServiceLibrary
{
    public class FruitShopService : IFruitShopService
    {
        const string insufficientPrivileges = "Insufficient privileges.";
        const string wrongLoginDetails = "Wrong login details.";
        const string usernameExists = "Username already exists.";
        const string accountCreated = "The account is created.";
        const string productNotFound = "Product not found.";
        const string soldOut = "Sorry, product is sold out.";
        const string notEnoughBalance = "Not enough balance.";
        const string productIsPurchased = "The product is purchased.";

        public List<ProductType> GetAvailableProducts()
        {
            Console.WriteLine("GetAvailableProducts is called");
            var productTypes = new List<ProductType>();

            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var products = from p in ctx.ProductSet
                               select p;

                foreach (Product product in products)
                {
                    if (product.Quantity <= 0)
                        continue;

                    var productType = new ProductType
                    {
                        Id = product.Id,

                        ProductDetails = new ProductDetails
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        }
                    };

                    productTypes.Add(productType);
                }
            }

            return productTypes;
        }

        public Message RegisterCustomer(LoginDetails loginDetails)
        {
            Console.WriteLine("RegisterCustomer is called");
            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customerExists = ctx.CustomerSet.Any(
                    c => c.Username == loginDetails.Username);

                if (customerExists)
                    return new Message { Content = usernameExists, IsError = true };

                Customer customer = new Customer
                {
                    Username = loginDetails.Username,
                    Password = loginDetails.Password,
                    Balance = 100
                };

                ctx.CustomerSet.Add(customer);
                ctx.SaveChanges();
            }

            return new Message { Content = accountCreated };
        }


        public bool LoginCustomer(LoginDetails loginDetails)
        {
            Console.WriteLine("LoginCustomer is called");
            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customer = LoginCustomer(ctx, loginDetails);

                return customer != null;
            }
        }

        public int AddProduct(LoginDetails loginDetails, ProductDetails productDetails)
        {
            Console.WriteLine("AddProduct is called");
            if (loginDetails.Username != "admin")
                throw new FaultException(insufficientPrivileges);

            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customer = LoginCustomer(ctx, loginDetails);

                if (customer == null)
                    throw new FaultException(wrongLoginDetails);

                Product product = new Product
                {
                    Name = productDetails.Name,
                    Price = productDetails.Price,
                    Quantity = productDetails.Quantity
                };

                ctx.ProductSet.Add(product);
                ctx.SaveChanges();

                return product.Id;
            }
        }

        public Message BuyProduct(LoginDetails loginDetails, int productId)
        {
            Console.WriteLine("BuyProduct is called");
            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customer = LoginCustomer(ctx, loginDetails);

                if (customer == null)
                    return new Message { Content = wrongLoginDetails, IsError = true };

                var product = ctx.ProductSet.SingleOrDefault(
                    p => p.Id == productId);

                if (product == null)
                    return new Message { Content = productNotFound, IsError = true };

                if (product.Quantity <= 0)
                    return new Message { Content = soldOut, IsError = true };

                if (customer.Balance < product.Price)
                    return new Message { Content = notEnoughBalance, IsError = true };

                product.Quantity -= 1;
                customer.Balance -= product.Price;

                var ownedProduct = ctx.OwnedProductSet.SingleOrDefault(
                    s => s.Customer.Id == customer.Id
                    && s.Product.Id == product.Id);

                if (ownedProduct == null)
                {
                    ownedProduct = new OwnedProduct
                    {
                        Customer = customer,
                        Product = product,
                        Quantity = 1
                    };

                    ctx.OwnedProductSet.Add(ownedProduct);
                }
                else
                {
                    ownedProduct.Quantity += 1;
                }

                ctx.SaveChanges();
            }

            return new Message { Content = productIsPurchased };
        }

        public List<OwnedProductType> GetBoughtProducts(LoginDetails loginDetails)
        {
            Console.WriteLine("GetBoughtProducts is called");
            var ownedProductTypes = new List<OwnedProductType>();

            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customer = ctx.CustomerSet.SingleOrDefault(
                    c => c.Username == loginDetails.Username
                    && c.Password == loginDetails.Password);

                if (customer == null)
                    throw new FaultException(wrongLoginDetails);

                var ownedProducts = from s in ctx.OwnedProductSet
                                    where s.CustomerId == customer.Id
                                    select s;

                foreach (OwnedProduct ownedProduct in ownedProducts)
                {
                    var ownedProductType = new OwnedProductType
                    {
                        Quantity = ownedProduct.Quantity,

                        ProductDetails = new ProductDetails
                        {
                            Name = ownedProduct.Product.Name,
                            Price = ownedProduct.Product.Price,
                            Quantity = ownedProduct.Product.Quantity
                        }
                    };

                    ownedProductTypes.Add(ownedProductType);
                }
            }

            return ownedProductTypes;
        }

        public double GetBalance(LoginDetails loginDetails)
        {
            Console.WriteLine("GetBalance is called");
            using (FruitShopModelContainer ctx = new FruitShopModelContainer())
            {
                var customer = LoginCustomer(ctx, loginDetails);

                if (customer == null)
                    throw new FaultException(wrongLoginDetails);

                return customer.Balance;
            }
        }

        private Customer LoginCustomer(FruitShopModelContainer ctx, LoginDetails loginDetails)
        {
            return ctx.CustomerSet.SingleOrDefault(
                c => c.Username == loginDetails.Username
                && c.Password == loginDetails.Password);
        }
    }
}
