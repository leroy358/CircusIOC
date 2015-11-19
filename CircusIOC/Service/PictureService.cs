using CircusIOC.Models;
using CircusIOC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CircusIOC.Service
{
    public class PictureService
    {
        public static int GetAllCount()
        {
            return Repository.PictureRepository.GetAllCount();
        }
        public static IEnumerable<Pictures> GetAll(int pageSize, int pageIndex)
        {
            return Repository.PictureRepository.GetAll(pageSize, pageIndex);
        }
        public static Pictures GetById(int id)
        {
            return Repository.PictureRepository.GetById(id);
        }
        public static IEnumerable<Pictures> GetByName(string name, int pageSize, int pageIndex)
        {
            return Repository.PictureRepository.GetByName(name, pageSize, pageIndex);
        }
        public static void Add(Pictures pic)
        {
            Repository.PictureRepository.AddPic(pic);
        }
        public static void Delete(Pictures pic)
        {
            Repository.PictureRepository.DeletePic(pic);
        }
        public static void Update(Pictures pic)
        {
            Repository.PictureRepository.UpdatePic(pic);
        }

    }
}