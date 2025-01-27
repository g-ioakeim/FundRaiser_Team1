﻿using FundRaiser_Team1.Models;
using FundRaiser_Team1_API.DTO;
using FundRaiser_Team1_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser_Team1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetAllProjects()
        {
            var dto_list = await _projectService.GetAllProjects();
            if (dto_list == null) return NotFound("No Project Available");
            return Ok(dto_list);
        }

        [HttpGet, Route("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var dto = await _projectService.GetProject(id);
            if (dto == null) return NotFound("Invalid id.");
            return Ok(dto);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<bool>> DeleteProject(int id)
        {
            var deleted = await _projectService.DeleteProject(id);
            if (!deleted) return NotFound("The project you are trying to delete does not exist.") ;
            return Ok(deleted);
        }

        [HttpGet, Route("creator/{id}")]
        public ActionResult<ProjectDto> GetCreator(int id)
        {
            var dto =  _projectService.GetCreator(id);
            if (dto == null) return NotFound("Invalid id.");
            return Ok(dto);
        }

        [HttpGet, Route("funders/{id}")]
        public ActionResult<ProjectDto> GetFunders(int id)
        {
            var dto = _projectService.GetFunders(id);
            if (dto == null) return NotFound("Invalid id.");
            return Ok(dto);
        }
    }
}
