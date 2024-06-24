using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ERP6.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using ERP6.ViewModels.Home;
using NETCore.Encrypt;

namespace ERP6.Controllers
{
    public class LoginController : Controller
    {
        private readonly EEPEF01Context _context;

        public LoginController(EEPEF01Context context)
        {
            _context = context;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Lg(string userAC, string userPW)
        {
            //step 1 先檢查是否有帳號
            var checkdata = await _context.Users.Where(x => x.Userid == userAC).FirstOrDefaultAsync();

            if (checkdata == null)
            {
                HttpContext.Session.SetString("msg", "帳密錯誤");
                return RedirectToAction(nameof(Index));
            }

            var test = EncryptPwd("12345");

            //如果有再檢查新密碼欄位是否有資料
            if (!string.IsNullOrEmpty(checkdata.NewPwd))
            {
                //如果新密碼欄位有資料
                //密碼檢查
                var password = string.Empty;

                if (!string.IsNullOrEmpty(userPW))
                {
                    password = EncryptPwd(userPW);
                }
                
                var result = await _context.Users.Where(x => x.Userid == userAC && x.NewPwd == password).FirstOrDefaultAsync();
                var permission = result != null ? (result.PERMISSIONS).Split('_') : new string[1] { "0" };
                //新增權限
                if (result == null || Convert.ToInt32(permission[0]) < 99)
                {
                    HttpContext.Session.SetString("msg", "帳密錯誤");
                    return RedirectToAction(nameof(Index));
                }

                HttpContext.Session.SetString("msg", "登入成功");

                HttpContext.Session.SetString("UserAc", result.Userid);
                HttpContext.Session.SetString("UserName", result.Username);

                return RedirectToAction(nameof(Index), "Home", new HomeIndexViewModel { UserAC = userAC });

            }
            else
            {
                //如果新密碼欄位沒有資料
                HttpContext.Session.SetString("msg", "登入成功");

                HttpContext.Session.SetString("UserAc", checkdata.Userid);
                HttpContext.Session.SetString("UserName", checkdata.Username);

                return RedirectToAction(nameof(Index), "Home", new HomeIndexViewModel { UserAC = userAC });
            }

            //var Result = await _context.Users.Where(x => x.Userid == userAC).FirstOrDefaultAsync();

            //if (Result == null)
            //{
            //    HttpContext.Session.SetString("msg", "帳密錯誤");
            //    return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    HttpContext.Session.SetString("msg", "");

            //    HttpContext.Session.SetString("UserAc", Result.Userid);
            //    HttpContext.Session.SetString("UserName", Result.Username);

            //}

            //return RedirectToAction(nameof(Index), "Home", new HomeIndexViewModel { UserAC = userAC, UserPW = userPW });
        }

        public async Task<IActionResult> Out()
        {

            //var Result = _context.Users.Where(x => x.Userid == userAC && x.Pwd == userPW).FirstOrDefault();
            //if (Result == null)
            //{
            //    session["msg"]="登出成功";
            //    return RedirectToAction(nameof(Index));
            //}


            HttpContext.Session.SetString("msg", "登出成功");

            HttpContext.Session.SetString("UserAc", "");
            HttpContext.Session.SetString("UserName", "");


            return RedirectToAction(nameof(Index), "Login");
        }

        public void msgClear()
        {

            HttpContext.Session.SetString("msg", "");

        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Agent,Pwd,Createdate,Creater,Modifydate,Modifier,Description,Sysflag")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Userid,Username,Agent,Pwd,Createdate,Creater,Modifydate,Modifier,Description,Sysflag")] Users users)
        {
            if (id != users.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Userid == id);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string EncryptPwd(string pwd)
        {
            //SHA256
            var hashed = EncryptProvider.Sha256(pwd);

            return hashed;
        }
    }
}
