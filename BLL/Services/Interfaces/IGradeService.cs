﻿using CORE.DAL;
using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IGradeService
    {
        ServiceResponse Add(GradeInput input);
        ServiceResponse Update(GradeInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
    }
}
