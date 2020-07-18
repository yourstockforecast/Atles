﻿using System.Linq;
using System.Threading.Tasks;
using Atlas.Server.Caching;
using Atlas.Server.Data;
using Atlas.Server.Domain;
using Atlas.Server.Services;
using Atlas.Shared.Models.Admin.ForumGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Server.Controllers.Admin
{
    [Authorize(Policy = "Admin")]
    [Route("api/admin/forum-groups")]
    [ApiController]
    public class ForumGroupsController : ControllerBase
    {
        private readonly AtlasDbContext _dbContext;
        private readonly IContextService _contextService;
        private readonly ICacheManager _cacheManager;

        public ForumGroupsController(AtlasDbContext dbContext, IContextService contextService, ICacheManager cacheManager)
        {
            _dbContext = dbContext;
            _contextService = contextService;
            _cacheManager = cacheManager;
        }

        [HttpGet("index-model")]
        public async Task<IndexModel> Index()
        {
            var result = new IndexModel();

            var site = await _contextService.CurrentSiteAsync();

            var forumGroups = await _dbContext.ForumGroups
                .Include(x => x.PermissionSet)
                .Where(x => x.SiteId == site.Id && x.Status != StatusType.Deleted)
                .OrderBy(x => x.SortOrder)
                .ToListAsync();

            foreach (var forumGroup in forumGroups)
            {
                result.ForumGroups.Add(new IndexModel.ForumGroupModel
                {
                    Id = forumGroup.Id,
                    Name = forumGroup.Name,
                    SortOrder = forumGroup.SortOrder,
                    TotalTopics = forumGroup.TopicsCount,
                    TotalReplies = forumGroup.RepliesCount
                });
            }

            return result;
        }
    }
}
