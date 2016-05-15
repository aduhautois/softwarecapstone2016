using System.ComponentModel;
namespace com.GreenThumb.BusinessObjects
{
    public enum Active
    {
        active,
        inactive,
        all
    }

    public enum RecipeCategories
    {
        Baked,
        Beverage,
        Canning,
        Dessert,
        Grilled,
        [Description("Main Dish")]
        MainDish,
        Salad,
        [Description("Side Dish")]
        SideDish,
        Soup
    }
}
