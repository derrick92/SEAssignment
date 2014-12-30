using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Repos;

namespace BusinessLayer
{
    public class CartBL
    {
        /// <summary>
        /// Adds an item to the shopping cart
        /// </summary>
        /// <param name="cart"> the shopping cart item</param>
        public void AddToCart(Common.ShoppingCart cart)
        {
            new CartRepo().AddToCart(cart);
        }
        /// <summary>
        /// Gets the shopping cart for the userid
        /// </summary>
        /// <param name="userid">the id of the user</param>
        /// <param name="productid">the id of the product</param>
        /// <returns>returns the shopping cart</returns>
        public Common.ShoppingCart GetShoppingCart(int userid, int productid)
        {
            return new CartRepo().GetShoppingCart(userid, productid);
        }
        /// <summary>
        /// Updates the cart item qty value
        /// </summary>
        /// <param name="userid">the id of the user</param>
        /// <param name="productId">the id of the product</param>
        /// <param name="newQty">the new quantity of the product</param>
        public void UpdateCart(int userid, int productId, int newQty)
        {
            new CartRepo().UpdateCart(userid, productId, newQty);
        }
        /// <summary>
        /// Updates the cart item qty value
        /// </summary>
        /// <param name="userid">the id of the user</param>
        /// <param name="productId">the id of the product</param>
        /// <param name="newQty">the new quantity of the product</param>
        public void UpdateCartActualValue(int userid, int productId, int newQty)
        {
            new CartRepo().UpdateCartActualValue(userid, productId, newQty);
        }

        /// <summary>
        /// The number of items in the cart
        /// </summary>
        /// <param name="userid">the id of the user</param>
        /// <returns></returns>
        public int GetNoOfItemsInCart(int userid)
        {
            return new CartRepo().GetNoOfItemsInCart(userid);
        }

        /// <summary>
        /// Removes an item from the cart
        /// </summary>
        /// <param name="id">the id of the item</param>
        public void DeleteCartItem(int id)
        {
            new CartRepo().DeleteCartItem(id);
        }

        /// <summary>
        /// Gets a list of shopping cart items by user id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<Common.ShoppingCart> GetShoppingCartOfUser(int userID)
        {
            return new CartRepo().GetShoppingCartOfUser(userID);
        }

        /// <summary>
        /// Gets the shopping cart by cart id
        /// </summary>
        /// <param name="cartID">The cart id</param>
        /// <returns>returns the shopping cart</returns>
        public Common.ShoppingCart GetShoppingCartbyID(int cartID)
        {
            return new CartRepo().GetShoppingCartbyID(cartID);
        }

        /// <summary>
        /// Updates the shopping Cart 
        /// </summary>
        /// <param name="gb">The updated shopping cart item</param>
        public void Update(Common.ShoppingCart gb)
        {
            new CartRepo().Update(gb);
        }
    }
}
