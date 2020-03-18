﻿using _4oito6.Infra.Data.Repositories.Core.Implementation;
using _4oito6.Template.Infra.Data.Context;
using _4oito6.Template.Infra.Data.Model.Entities;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;

namespace _4oito6.Template.Infra.Data.Repositories.Implementation.Entity
{
    public class UserRepository : EntityRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(TemplateContext context) : base(context)
        {
        }
    }
}