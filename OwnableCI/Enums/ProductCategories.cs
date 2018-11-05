using System;
using System.ComponentModel;
using System.Reflection;

namespace OwnableCI_TestLib.Enums
{
    public enum ProductCategories
    {
        [Description("//div[@id='v-pills-tab']//button[text()=' Top Deals ']")]
        Top_deals,
        [Description("//div[@id='v-pills-tab']/button[9]")]
        TVs,
        [Description("//div[@id='v-pills-tab']//button[text()=' Electronics ']")]
        Electronics,
        [Description("//div[@id='v-pills-tab']//button[text()=' Computers ']")]
        Computers,
        [Description("//div[@id='v-pills-tab']//button[text()=' Appliances ']")]
        Appliances,
        [Description("//div[@id='v-pills-tab']//button[text()=' Cameras ']")]
        Cameras,
        [Description("//div[@id='v-pills-tab']//button[text()=' Game Consoles ']")]
        Game_Consoles,
        [Description("//div[@id='v-pills-tab']//button[text()=' Furniture ']")]
        Furniture,
        [Description("//div[@id='v-pills-tab']//button[text()=' Cell Phones ']")]
        Cell_Phones
    }
    public static class ProductCategoriesEx
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
    
}
