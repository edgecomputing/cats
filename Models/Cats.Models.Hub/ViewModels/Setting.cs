using System.ComponentModel.DataAnnotations;
using System.Linq;
using Cats.Models.Hubs.MetaModels;

namespace Cats.Models.Hubs
{
    
    partial class Setting
    {        
      //TODO:refactor  HubContext context = new HubContext();

        public Setting GetSetting(string key)
        {            
            ////Setting set = context.Settings.FirstOrDefault(m => m.Key == key);

            //if (set != null)
            //{
            //    // successs
            //    s.ID = set.ID;
            //    s.Value = set.Value;
            //    s.Key = key;
            //    s.Option = set.Option;
            //    s.Type = set.Type;
            //    return 1;
            //}
            ////return set;
            return null;
            //return 0;
        }

        public void EditSetting(Setting s)
        {
            ////Setting set = context.Settings.FirstOrDefault(m => m.Key == s.Key);

            ////if (set != null)
            ////{
            ////    set.Category = s.Category;
            ////    set.Value = s.Value;
            ////    set.Option = s.Option;
            ////    set.Type = s.Type;
            ////    context.SaveChanges();
            ////}
            ////else
            ////{
            ////    context.Settings.Add(s);

            ////    context.SaveChanges();
            ////}
            //else
            //{
            //    set = new Setting();

            //    set.Category = s.Category;
            //    set.Key  = s.Key;
            //    set.Value = s.Value;
            //    set.Option = s.Option;
            //    set.Type = s.Type;

            //    context.Settings.AddObject(set);
            //}
     
        }

        public void AddSetting(Setting s)
        {
            ////context.Settings.Add(s);
            
            ////context.SaveChanges();
        }
    }


}