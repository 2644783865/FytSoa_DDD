﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Model.Entity
{
    public class Entity<TKey> : IEntity
    {
        //public Entity();

        /// <summary>
        /// 唯一编号
        /// <summary>
        [SugarColumn(IsPrimaryKey = true)]
        public virtual TKey Id { get; set; }
    }
}
