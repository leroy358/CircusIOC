using CircusIOC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CircusIOC.Repository
{
    public class PictureRepository
    {
        private static MaXiDbContext db = new MaXiDbContext();
        public static int GetAllCount()
        {
            return db.Pictures.Count();
        }
        public static IEnumerable<Pictures> GetAll(int pageSize, int pageIndex)
        {
            return db.Pictures.OrderByDescending(item => item.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        public static Pictures GetById(int id)
        {
            return db.Pictures.Where(item => item.Id == id).FirstOrDefault();
        }
        public static IEnumerable<Pictures> GetByName(string name, int pageSize, int pageIndex)
        {
            return db.Pictures.Where(item => item.PicHead.Contains(name)).OrderByDescending(item => item.Id).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }
        public static void AddPic(Pictures pic)
        {
            db.Pictures.Add(pic);
            db.SaveChanges();
        }
        public static void DeletePic(Pictures pic)
        {
            db.Pictures.Remove(pic);
            db.SaveChanges();
        }
        public static void UpdatePic(Pictures pic)
        {
            db.Entry(pic).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}