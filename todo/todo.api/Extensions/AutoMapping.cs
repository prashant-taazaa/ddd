using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo.api.Contracts.Response;

namespace todo.api.Extensions
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<domain.Models.Task, TaskResponse>();
        }
    }
}
