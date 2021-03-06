﻿using GZY_CMS.Core.Repository;
using GZY_CMS.IService;
using GZY_CMS.SystemModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GZY_CMS.Service
{
    public class UserService :  IUserService
    {


        public IRepository<GZY_User, SystemContext> SysContext { get; set; }
        public bool Add(GZY_User model)
        {
            return SysContext.Add(model);
        }

        public List<GZY_User> Select(string Loginname, string Name, string ValidYN, int index, int rows, out int total)
        {
            var usercontext = SysContext.GetList();
            if (!string.IsNullOrEmpty(Loginname))
            {
                usercontext = usercontext.Where(a => a.Loginname.Contains(Loginname));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                usercontext = usercontext.Where(a => a.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(ValidYN))
            {
                usercontext = usercontext.Where(a=>a.ValidYN== ValidYN);
            }
            var count = usercontext.Count();
            total = count;
            return usercontext.Skip(rows * (index - 1)).Take(rows).ToList();
        }


        public int DeleteUser(int[] ids)
        {
            List<GZY_User> list = new List<GZY_User>();
            foreach (var item in ids)
            {
                var date = SysContext.Get(a => a.ID == item);
                if (date != null)
                {
                    list.Add(date);
                }

            }
          return  SysContext.RemoveRange(list);
        }

        public GZY_User SelectUserModel(int id)
        {
            return SysContext.Get(a => a.ID == id);
        }


        public bool UpdataUserModel(GZY_User model)
        {
            return SysContext.Update(model);
        }
    }
}
