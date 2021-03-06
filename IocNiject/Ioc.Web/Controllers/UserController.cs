﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ioc.Service;
using Ioc.Web.Models;
using Ioc.Core.Data;

namespace Ioc.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<UserModel> users = _userService.GetUsers().Select(u => new UserModel
                {
                    FirstName = u.UserProfile.FirstName,
                    LastName = u.UserProfile.LastName,
                    Email = u.Email,
                    Address = u.UserProfile.Address,
                    ID = u.ID

                });
            return View(users);
        }

        [HttpGet]
        public ActionResult CreateEditUser(int? id)
        {
            UserModel model = new UserModel();
            if (id.HasValue && id != 0)
            {
                User userEntity = _userService.GetUser(id.Value);
                model.FirstName = userEntity.UserProfile.FirstName;
                model.LastName = userEntity.UserProfile.LastName;
                model.Address = userEntity.Email;
                model.UserName = userEntity.UserName;
                model.Password = userEntity.Password;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditUser(UserModel model)
        {

            if (model.ID == 0)
            {
                User userEntity = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    AddedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    IP = Request.UserHostAddress,

                    UserProfile = new UserProfile()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        AddedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        IP = Request.UserHostAddress
                    }
                };
                _userService.InsertUser(userEntity);

                if (userEntity.ID > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            else
            {
                User userEntity = _userService.GetUser(model.ID);
                userEntity.UserName = model.UserName;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;
                userEntity.ModifiedDate = DateTime.UtcNow;
                userEntity.IP = Request.UserHostAddress;
                userEntity.UserProfile.FirstName = model.FirstName;
                userEntity.UserProfile.LastName = model.LastName;
                userEntity.UserProfile.Address = model.Address;
                userEntity.UserProfile.IP = Request.UserHostAddress;

                _userService.UpdateUser(userEntity);

                if (userEntity.ID > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        public ActionResult DetailUser(int ?id)
        {
            UserModel model = new UserModel();
            if (id.HasValue && id != 0)
            {
              User userEntity=  _userService.GetUser(id.Value);
              //model.ID = userEntity.ID;
              model.FirstName = userEntity.UserProfile.FirstName;
              model.LastName = userEntity.UserProfile.LastName;
              model.Address = userEntity.UserProfile.Address;
              model.Email = userEntity.Email;
              model.AddedDate = userEntity.AddedDate;
              model.UserName = userEntity.UserName;


            }
            return View(model);
        }


        public ActionResult DeleteUser(int id)
        {
            UserModel model = new UserModel();
            if (id != 0)
            {
                User userEntity = _userService.GetUser(id);
                model.FirstName = userEntity.UserProfile.FirstName;
                model.LastName = userEntity.UserProfile.LastName;
                model.Address = userEntity.UserProfile.Address;
                model.Email = userEntity.Email;
                model.AddedDate = userEntity.AddedDate;
                model.UserName = userEntity.UserName;
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection fc)
        {
            try
            {
                if (id != 0)
                {
                    User userEntity = _userService.GetUser(id);
                    _userService.DeleteUser(userEntity);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}