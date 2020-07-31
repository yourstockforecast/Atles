﻿using System;
using System.Collections.Generic;

namespace Atlas.Models.Public
{
    public class IndexPageModel
    {
        public IList<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

        public class CategoryModel
        {
            public string Name { get; set; }

            public IList<ForumModel> Forums { get; set; } = new List<ForumModel>();
        }

        public class ForumModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int TotalTopics { get; set; }
            public int TotalReplies { get; set; }
            public bool CanRead { get; set; }
        }
    }
}
