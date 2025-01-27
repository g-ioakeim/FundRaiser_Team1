﻿using FundRaiser_Team1.Interfaces;
using FundRaiser_Team1.Models;
using FundRaiser_Team1.Services;
using FundRaiser_Team1_Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FundRaiser_Team1_MVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IHostEnvironment _hostEnvironment;
     

        public ProjectController(IProjectService projectService, IHostEnvironment hostEnvironment)
        {
            _projectService = projectService;
            _hostEnvironment = hostEnvironment;
            
        }

        // GET: ProjectController
        public ActionResult Index()
        {
            var project = _projectService.GetAllProjects();
            return View(project);
        }

        //GET: ProjectController/GetProject/id
        public ActionResult GetProject(int id)
        {
            var project = _projectService.GetProject(id);
            return View(project);
        }

        public ActionResult CreateProject()
        {
            return View();
        }
        public ActionResult Browse()
        {
            var project = _projectService.GetAllProjects();
            return View(project);
           
        }
        public ActionResult GoFunding()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Buy(int packageId)
        {
            int UserId = int.Parse(Request.Cookies["userId"]);
            try
            {
                using (FundRaiserDbContext db = new FundRaiserDbContext())
                {
                    Package pa = (from pack in db.Package
                                      where pack.Id == packageId
                                      select pack).SingleOrDefault();
                    if (pa != null)
                    {
                        ProjectUser projectUser = new ProjectUser(UserId, pa.ProjectId, Category.BACKER);
                        _projectService.CreateProjectUser(projectUser);
                        PackageUser packageUser = new PackageUser(UserId, packageId , pa.ProjectId);
                        _projectService.CreatePackageUser(packageUser);
                    }
                }
            }
            catch
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GoFunding(int projectId)
        {
            var package = _projectService.GetAllPackages(projectId);
            return View(package);
        }

        [HttpPost]
        public ActionResult CreateProject(ProjectWithImage projectWithImage)
        {
            Project project = projectWithImage.Project;
            var img = projectWithImage.ProjectImage;
            if (img != null)
            {
                var uniqueFileName = GetUniqueFileName(img.FileName);
                var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "\\wwwroot", "images");
                var filePath = Path.Combine(uploads, uniqueFileName);
                img.CopyTo(new FileStream(filePath, FileMode.Create));

                project.Photo = uniqueFileName;
            }

            _projectService.CreateProject(project);
            int UserId = int.Parse(Request.Cookies["userId"]);
            ProjectUser projectUser = new ProjectUser(UserId , project.Id , Category.CREATOR);
           _projectService.CreateProjectUser(projectUser);
             

            return RedirectToAction(nameof(Index));
        }
        public IActionResult FundingPage()
        {
            var list = new List<FundingPage>();

            var project = _projectService.GetAllProjects();
            foreach(var p in project)
            {
                var fundersCount = _projectService.GetFunders(p.Id);
                var money = _projectService.GetMoney(p.Id);
                list.Add(new FundingPage() { 
                    project = p,
                    FundersCount = fundersCount,
                    Money = money
                });
            }

            return View(list);
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult DeleteProject(int id)
        {
            _projectService.DeleteProject(id);
            try
            {
                using (FundRaiserDbContext db = new FundRaiserDbContext())
                {
                    ProjectUser pu = (from projectUser in db.ProjectUser
                                      where projectUser.ProjectId == id
                                      select projectUser).SingleOrDefault();

                    if (pu != null)
                    {
                        _projectService.DeleteProjectUser(pu.ProjectUserId);
                    }
                }
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }



}
