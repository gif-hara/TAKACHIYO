using UnityEngine.Localization;

namespace TAKACHIYO
{
    /// <summary>
    /// 
    /// </summary>
    public static class BoolExtensions
    {
        /// <summary>
        /// <c>true</c>の場合は「する」を返す
        /// <c>false</c>の場合は「しない」を返す
        /// </summary>
        public static string LocalizedStringDoNotDo(this bool self)
        {
            var tableEntryName = self ? "Do" : "NotDo";
            return new LocalizedString("Common", tableEntryName).GetLocalizedString();
        }
    }
}
