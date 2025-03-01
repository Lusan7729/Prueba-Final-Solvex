﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApi.Core.BaseModel
{
    public interface IBaseDto
    {
        int? Id { get; set; }
        bool Deleted { get; set; }
    }
    public class BaseDto : IBaseDto
    {
        public virtual int? Id { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
