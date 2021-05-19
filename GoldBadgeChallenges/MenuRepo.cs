using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe //Methods are in here
{
    public class MenuRepo
    {//private list of menuitems
        private List<MenuItem> _menu = new List<MenuItem>();
        //create 2 reads, update, delete
        public bool AddMenuItem(MenuItem item)
        {
            int startingCount = _menu.Count;

            _menu.Add(item);
            bool wasAdded = (_menu.Count > startingCount) ? true : false;
            return wasAdded;
        }

        public List<MenuItem> GetMenu()
        {
            return _menu;
        }
        public MenuItem GetByMealNumber(int mealNumber)
        {
            foreach (MenuItem item in _menu)
            {
                if (item.MealNumber == mealNumber)
                {
                    return item;
                }
            }
            return null;
        }
        public bool UpdateExistingMenuItem(int oldMealNumber, MenuItem newMenuItem)
        {
            MenuItem oldMenuItem = GetByMealNumber(oldMealNumber);
            if (oldMenuItem != null)
            {
                oldMenuItem.MealNumber = newMenuItem.MealNumber;
                oldMenuItem.MealName = newMenuItem.MealName;
                oldMenuItem.MealDescription = newMenuItem.MealDescription;
                oldMenuItem.MealIngredients = newMenuItem.MealIngredients;
                oldMenuItem.MealPrice = newMenuItem.MealPrice;
                return true;
            }
            else { return false; }
        }
        public bool DeleteExistingMenuItem(MenuItem item)
        {
           return _menu.Remove(item);
        }
    }
}
