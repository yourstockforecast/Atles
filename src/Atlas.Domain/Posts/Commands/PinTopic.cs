﻿using System;
using Docs.Attributes;

namespace Atlas.Domain.Posts.Commands
{
    /// <summary>
    /// Request to pin/unpin a topic.
    /// </summary>
    [DocRequest(typeof(Post))]
    public class PinTopic : CommandBase
    {
        /// <summary>
        /// The unique identifier of the topic to pin/unpin.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The unique identifier of the forum.
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// Value indicating whether the topic needs to be pinned (true) or unpinned (false).
        /// </summary>
        public bool Pinned { get; set; }
    }
}