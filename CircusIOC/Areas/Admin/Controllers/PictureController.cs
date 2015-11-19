using CircusIOC.Models;
using CircusIOC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CircusIOC.Areas.Admin.Controllers
{
    public class PictureController : Controller
    {
        int pageSize = 10;
        public ActionResult List(string searchStr,int pageIndex = 1)
        {
            if (Session["admin"] != null)
            {
                ViewBag.searchStr = searchStr;
                if (!string.IsNullOrEmpty(searchStr))
                {
                    int iSearch = 0;
                    int.TryParse(searchStr, out iSearch);
                    if (iSearch != 0)
                    {
                        var pic = Service.PictureService.GetById(iSearch);
                        ViewBag.pageX = 1;
                        ViewBag.pageCount = 1;
                        return View(pic);
                    }
                    else
                    {
                        var pic = Service.PictureService.GetByName(searchStr, pageSize, pageIndex);
                        int count = pic.Count();
                        InitPage(pageIndex, count, searchStr);
                        pic = pic.OrderByDescending(item => item.Id).Skip((pageIndex - 1) * pageSize);
                        return View(pic);
                    }
                }
                else
                {
                    var pic = Service.PictureService.GetAll(pageSize, pageIndex);
                    int count = Service.PictureService.GetAllCount();
                    InitPage(pageIndex, count, searchStr);
                    return View(pic);
                }
            }
            else
            {
                return RedirectToAction("Login", "Console");
            }
        }
        public ActionResult Create()
        {
            if (Session["admin"] != null)
            {
                ViewBag.IsCreate = false;
                return View("Edit", new Pictures());
            }
            else
            {
                return RedirectToAction("Login", "Console");
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["admin"] != null)
            {
                ViewBag.isCreate = true;
                var pic = Service.PictureService.GetById(id); 
                return View(pic);
            }
            else
            {
                return RedirectToAction("Login", "Console");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePic(Pictures pic, bool IsCreate)
        {
            if (Session["admin"] != null)
            {
                pic.CreateTime = DateTime.Now.ToString("yyyy-MM-dd");
                string AD = IsBig(pic.PicAD, 2000);

                pic.PicAD = AD;
                pic.PicADS = ToSmall(AD, 320, AD.Insert(20, "small-"));

                if (IsCreate)
                {
                    Service.PictureService.Update(pic);
                }
                else
                {
                    Service.PictureService.Add(pic);
                }
                return Redirect("List");
            }
            else
            {
                return RedirectToAction("Login", "Console");
            }
        }
        public ActionResult Delete(int id, string returnURL)
        {
            var pic = Service.PictureService.GetById(id);
            Service.PictureService.Delete(pic);
            return Redirect(returnURL);
        }
        public string IsBig(string originalImagePath, int intWidth)
        {

            bool isExist = WXSSK.Common.DirectoryAndFile.FileExists(originalImagePath);
            System.Drawing.Image imgOriginal = System.Drawing.Image.FromFile(Server.MapPath(originalImagePath));

            //获取原图片的的宽度与高度
            int originalWidth = imgOriginal.Width;
            int originalHeight = imgOriginal.Height;

            //如果原图片宽度大于原图片的高度              
            if (originalWidth > intWidth)
            {
                originalWidth = intWidth;  //宽度等于缩略图片尺寸
                originalHeight = originalHeight * (intWidth / originalWidth);  //高度做相应比例缩小
            }

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(originalWidth, originalHeight);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            //设置缩略图片质量
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graphics.DrawImage(imgOriginal, 0, 0, originalWidth, originalHeight);

            //System.Drawing.Graphics temp = graphics;
            // 保存缩略图片
            imgOriginal.Dispose();
            WXSSK.Common.DirectoryAndFile.DeleteFile(originalImagePath);
            bitmap.Save(Server.MapPath(originalImagePath), System.Drawing.Imaging.ImageFormat.Jpeg);
            return originalImagePath;
        }

        public string ToSmall(string originalImagePath, int intWidth, string thumbImagePath)
        {
            bool isExist = WXSSK.Common.DirectoryAndFile.FileExists(originalImagePath);
            System.Drawing.Image imgOriginal = System.Drawing.Image.FromFile(Server.MapPath(originalImagePath));

            //获取原图片的的宽度与高度
            int originalWidth = imgOriginal.Width;
            int originalHeight = imgOriginal.Height;

            originalHeight = (int)originalHeight * intWidth / originalWidth;  //高度做相应比例缩小
            originalWidth = intWidth;  //宽度等于缩略图片尺寸

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(originalWidth, originalHeight);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);

            //设置缩略图片质量
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            graphics.DrawImage(imgOriginal, 0, 0, originalWidth, originalHeight);

            // 保存缩略图片
            bitmap.Save(Server.MapPath(thumbImagePath), System.Drawing.Imaging.ImageFormat.Jpeg);
            imgOriginal.Dispose();
            return thumbImagePath;
        }
        private void InitPage(int pageIndex, int count, string searchStr)
        {
            int pageCount = (count % pageSize == 0) ? count / pageSize : count / pageSize + 1;
            if (pageCount == 0)
            {
                pageCount = 1;
            }
            string perPage = Url.Action("List", new { searchStr, pageIndex = (pageIndex < 2) ? 1 : pageIndex - 1 });
            string nextPage = Url.Action("List", new { searchStr, pageIndex = (pageIndex == pageCount) ? pageCount : (pageIndex + 1) });
            string lastPage = Url.Action("List", new { searchStr, pageIndex = pageCount });
            string firstPage = Url.Action("List", new { searchStr, pageIndex = 1 });
            string pageX = Url.Action("List", new { searchStr, pageIndex });
            ViewBag.perPage = perPage;
            ViewBag.nextPage = nextPage;
            ViewBag.pageCount = pageCount;
            ViewBag.pageX = pageX.Substring(pageX.Length - 1, 1);
            ViewBag.lastPage = lastPage;
            ViewBag.firstPage = firstPage;
        }
        
	}
}