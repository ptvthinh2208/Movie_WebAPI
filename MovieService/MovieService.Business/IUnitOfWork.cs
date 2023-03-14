﻿using MovieService.Domain;
using MovieService.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business
{
    public interface IUnitOfWork
    {
        IPropertyRepository<Actor> ActorRepository { get; }
        void Save();
    }
}
