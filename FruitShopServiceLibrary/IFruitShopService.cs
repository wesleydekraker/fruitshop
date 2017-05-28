using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FruitShopServiceLibrary
{
    [ServiceContract]
    public interface IFruitShopService
    {
        [OperationContract]
        List<ProductType> GetAvailableProducts();

        [OperationContract]
        Message RegisterCustomer(LoginDetails loginDetails);

        [OperationContract]
        bool LoginCustomer(LoginDetails loginDetails);

        [OperationContract]
        int AddProduct(LoginDetails loginDetails, ProductDetails productDetails);

        [OperationContract]
        Message BuyProduct(LoginDetails loginDetails, int productId);

        [OperationContract]
        List<OwnedProductType> GetBoughtProducts(LoginDetails loginDetails);

        [OperationContract]
        double GetBalance(LoginDetails loginDetails);
    }


    [DataContract]
    public class ProductType
    {
        int id;
        ProductDetails productDetails;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public ProductDetails ProductDetails
        {
            get { return productDetails; }
            set { productDetails = value; }
        }
    }

    [DataContract]
    public class ProductDetails
    {
        string name;
        double price;
        int quantity;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }

    [DataContract]
    public class CustomerType
    {
        LoginDetails loginDetails;
        double balance;

        [DataMember]
        public LoginDetails LoginDetails
        {
            get { return loginDetails; }
            set { loginDetails = value; }
        }

        [DataMember]
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }
    }

    [DataContract]
    public class OwnedProductType
    {
        int quantity;
        ProductDetails productDetails;

        [DataMember]
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [DataMember]
        public ProductDetails ProductDetails
        {
            get { return productDetails; }
            set { productDetails = value; }
        }
    }

    [DataContract]
    public class LoginDetails
    {
        string username;
        string password;

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

    [DataContract]
    public class Message
    {
        string content;
        bool isError = false;

        [DataMember]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        [DataMember]
        public bool IsError
        {
            get { return isError; }
            set { isError = value; }
        }
    }
}
