using System.ComponentModel;

namespace SinyaCrawler.Utility
{
    public class Util
    {
        /// <summary>
        /// 取得欄位設定DescriptionAttribute的值
        /// </summary>
        /// <param name="type">類別</param>
        /// <param name="name">欄位名稱</param>
        /// <returns></returns>
        public static string GetDescription(Type type, string name)
        {
            //// 利用反射找出相對應的欄位
            var field = type.GetProperty(name);
            //// 取得欄位設定DescriptionAttribute的值
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            //// 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return name;
            }

            //// 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }
    }
}
