
using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAsync();

        Task<Attendance?> GetByIdAsync(int id);

        Task<Attendance?> GetByEmployeeAndDateAsync(
            int employeeId,
            DateTime attendanceDate);

        Task<Attendance> AddAsync(
            Attendance attendance);

        Task UpdateAsync(
            Attendance attendance);

        Task DeleteAsync(
            Attendance attendance);
    }
}
