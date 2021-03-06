﻿namespace WebApplication.Service
{
    using System.Linq;
    using WebApplication.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class ApplicationUserDTO
    {
        static  ApplicationDbContext db = new ApplicationDbContext();

        public static string GetUserLogin(string id)
        {
            var res = db.Users.FirstOrDefault(x => x.Id == id);

            return res==null ? "undefined" : res.Name;
        }

        [Display(Name = "Роль пользователя:")]
        public static string GetUserRole(string id)
        {
            var user = db.Users.First(x => x.Id == id);
            var roles = user.Roles;
            var result = "";

            foreach(var r in roles)
            {
                var res=db.Roles.First(x => x.Id == r.RoleId);
                result += res.Name+" ";
            }

            return result;
        }

        [Display(Name = "Пароль пользователя:")]
        public static string GetUserPassword(string id)
        {
            var res = db.Users.First(x => x.Id == id);
            return res.Password;
        }

        [Display(Name = "Аватар пользователя:")]
        public static string GetUserAvatar(string id)
        {
            var res = db.Users.First(x => x.Id == id);
            // Ничего не вернём, если фотки нет
            if(res.Avatar.Count==0)
            {
                return string.Empty;
            }
            else
            {
                var avatar = res.Avatar.Last();
                return avatar.Url;
            }
        }

        [Display(Name = "Карма пользователя:")]
        public static string GetUserKarma(string id)
        {
            var res = db.Users.First(x => x.Id == id);
            return res.Karma.ToString();
        }

        [Display(Name = "Увеличить/уменьшить карму пользователя")]
        public static void ChangeUserKarma(string id, string value)
        {
            var res = db.Users.First(x => x.Id == id);

            res.Karma = res.Karma + int.Parse(value);

            db.Entry(res).State = EntityState.Modified;

            db.SaveChanges();
        }


        [Display(Name = "Раздел о себе пользователя")]
        public static string GetUserInfo(string id)
        {
            var res = db.Users.First(x => x.Id == id).UserInfo;

            return res;

        }
    }
}