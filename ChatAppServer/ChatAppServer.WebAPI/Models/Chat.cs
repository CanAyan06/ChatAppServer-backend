﻿using System.Data;

namespace ChatAppServer.WebAPI.Models
{
    public sealed class Chat
    {
        public Chat()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DataSetDateTime Date { get; set; }

        public static implicit operator Chat(Chat v)
        {
            throw new NotImplementedException();
        }
    }
}