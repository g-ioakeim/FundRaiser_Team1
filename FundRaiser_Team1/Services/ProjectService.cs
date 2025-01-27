﻿using FundRaiser_Team1.Interfaces;
using FundRaiser_Team1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiser_Team1.Services
{
    public class ProjectService : IProjectService 
    {
        private readonly FundRaiserDbContext _dbContext;

        public ProjectService(FundRaiserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Project CreateProject(Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
            return project;
        }

        public bool DeleteProject(int projectId)
        {
            var projectDb = _dbContext.Projects.Find(projectId);
            if (projectDb is null) return false;
            _dbContext.Projects.Remove(projectDb);

            return _dbContext.SaveChanges() == 1;
        }

        public List<User> GetBackers(int projectId)
        {
            var projectDb = _dbContext.Projects.Find(projectId);
            return projectDb.Users;
        }

        public User GetCreator(int projectId)
        {
            var projectDb = _dbContext.Projects.Find(projectId);
            return (User)projectDb.Users.Where(user => user.Category == Category.CREATOR);
        }

        public List<Package> GetPackages(int projectId)
        {
            var projectDb = _dbContext.Projects.Find(projectId);
            return projectDb.AwardPackages;
        }

        public List<Project> GetAllProjects()
        {
            return _dbContext.Projects.ToList();
        }

        public Project GetProject(int projectId)
        {
            var projectDb = _dbContext.Projects.Include(project => project.Users)
                                               .FirstOrDefault(project => project.Id == projectId);
            return projectDb;
        }

        public Project UpdateProject(int projectId, Project project)
        {
            var projectDb = _dbContext.Projects.Find(projectId);
            if (projectDb == null) throw new KeyNotFoundException();
            projectDb.Title = project.Title;
            projectDb.Description = project.Description;
            projectDb.StatusPost = project.StatusPost;
            //projectDb.Photos = project.Photos;
            //projectDb.Videos = project.Videos;
            projectDb.AwardPackages = project.AwardPackages;

            _dbContext.SaveChanges();
            return projectDb;
        }

        public void CreateProjectUser(ProjectUser projectUser)
        {
            _dbContext.ProjectUser.Add(projectUser);
            try { _dbContext.SaveChanges(); }
            catch { }

        }

        public ProjectUser ReadProjectUser(int id)
        {
            ProjectUser projectUser = _dbContext.ProjectUser.Find(id);
            return projectUser;
        }

        public List<ProjectUser> ReadProjectUser()
        {
            return _dbContext.ProjectUser.ToList();
        }

        public bool DeleteProjectUser(int projectUserId)
        {
            var projectDb = _dbContext.ProjectUser.Find(projectUserId);
            if (projectDb is null) return false;
            _dbContext.ProjectUser.Remove(projectDb);

            return _dbContext.SaveChanges() == 1;
        }

        public List<Package> GetAllPackages(int projectId)
        {
            List<Package> packages = (from package in _dbContext.Package
                                   where package.ProjectId == projectId
                                   select package).ToList();

            return packages;
        }

        public void CreatePackageUser(PackageUser packageUser)
        {
            _dbContext.PackageUser.Add(packageUser);
            try { _dbContext.SaveChanges(); }
            catch { }
        }
        public int GetFunders(int projectId)
        {
            var usrList = new List<User>();
            var user = _dbContext.Set<ProjectUser>()
                .Where(p => p.ProjectId == projectId && p.CategoryProject == Category.BACKER)
                .ToList();

            foreach (var usr in user)
            {
                var my_user = _dbContext.User.Find(usr.UserId);

                usrList.Add(my_user);
            }

            return usrList.Count;
        }

        public decimal GetMoney(int projectId)
        {
            decimal sum = 0;

            List <PackageUser> pa = (from pack in _dbContext.PackageUser
                          where pack.ProjectId == projectId
                          select pack).ToList();




            if (pa != null)
            {
                List<int> pi = new List<int>();
                foreach (PackageUser p in pa)
                {
                    pi.Add(p.PackageId);
                }
                foreach(var id in pi)
                {
                    Package myPackage = (from pck in _dbContext.Package
                                         where pck.Id == id
                                         select pck).SingleOrDefault();
                    sum = sum + myPackage.PackagePrice;
                }
            }

            return sum;
        }
    }
}
