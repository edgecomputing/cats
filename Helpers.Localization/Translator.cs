using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Localization
{
   
       /* private static BekaMvcTestsContext db = new BekaMvcTestsContext();
        
        //private List<ResourceManager> _resourceManagers = new List<ResourceManager>();
        private LocalizationTextService()
        { db = new BekaMvcTestsContext(); }
        */
    public class Translator
    {
         private static Translator _instance = new Translator();
         public static string defaultLanguage = "en";
         public static string CurrentLanguage = "en";
         private Services.LocalizedTextService _dataservice; 
         private Translator()
            { 
             _dataservice = new Services.LocalizedTextService(new Data.UnitWork.UnitOfWork());
             //db = new BekaMvcTestsContext(); 
            }
         public static Translator Instance { get { return _instance; } }
         public static void LoadTexts()
         {
             _instance = new Translator();
         }
         public string _Translate(string TextKey, string LanguageCode)
         {
             return _dataservice.Translate(TextKey, LanguageCode);
         }
         
         public static string Translate(string TextKey)
         {
             return _instance._Translate(TextKey, CurrentLanguage);
         }
         public string Translate(string TextKey, string LanguageCode)
         {
             return _instance._Translate(TextKey, LanguageCode);
         }
    }
}
