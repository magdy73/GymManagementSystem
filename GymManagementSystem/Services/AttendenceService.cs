using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class AttendenceService : IAttendanceService
    {
        private readonly AppDbContext context;

        public AttendenceService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<AttendanceDTO> CreateAttendanceAsync(int memberId)
        {
            var attendance = new AttendanceModel();
            attendance.MemberId = memberId;
            attendance.CheckInTime =DateTime.Now;
            context.attendances.Add(attendance);
            await context.SaveChangesAsync();
            var member = await context.members.FindAsync(memberId);
            AttendanceDTO attendanceDTO = new AttendanceDTO
            {
                AttendanceId = attendance.Id,
                MemberId = attendance.MemberId,
                MemberName = member?.Name,
                CheckInTime = attendance.CheckInTime.ToString("yyyy-MM-dd  hh:mm tt")
            };
            return attendanceDTO;
        }

     

        public async Task<IEnumerable<AttendanceDTO>> GetAllAttendancesAsync()
        {
            var attendences = await context.attendances.Include(a => a.Member).OrderByDescending(a=>a.CheckInTime)
                .Select(a=>new AttendanceDTO
                {
                    AttendanceId = a.Id,
                    MemberId=a.MemberId,
                    MemberName=a.Member!=null ?a.Member.Name:"unKnown",
                    CheckInTime = a.CheckInTime.ToString("yyyy-MM-dd  hh:mm tt")
                } ).ToListAsync();
            
            return attendences;
        }

        public async Task<IEnumerable<AttendanceDTO>> GetAttendancebyIDAsync(int memberId)
        {
            var attendence= await context.attendances.Where(x=>x.MemberId==memberId).OrderByDescending(a => a.CheckInTime).Select(a => new AttendanceDTO
            {
                AttendanceId = a.Id,
                MemberId = a.MemberId,
                MemberName = a.Member != null ? a.Member.Name : "unKnown",
                CheckInTime = a.CheckInTime.ToString("yyyy-MM-dd  hh:mm tt")
            }).ToListAsync();
            return attendence;
        } 
        public async Task<IEnumerable<AttendanceDTO>> FilterAsync(AttendenceFilterDTO filter)
        {
            var query = context.attendances.Include(a => a.Member).AsQueryable();
            if(filter.StartDate!=null)
                query=query.Where(a=>a.CheckInTime >= filter.StartDate.Value);
            if (filter.EndDate!= null)
                query = query.Where(a => a.CheckInTime <= filter.EndDate.Value);
            
            if(filter.SortBy=="MemberName")
                query= filter.SortDescending ? query.OrderByDescending(a => a.Member.Name) :query.OrderBy(a=>a.Member.Name);
            else if(filter.SortBy== "CheckInTime")
                query=filter.SortDescending? query.OrderByDescending(a=>a.CheckInTime):query.OrderBy(a=>a.CheckInTime);
            
            int skip=(filter.PageNumber-1)*filter.PageSize;
            query=query.Skip(skip).Take(filter.PageSize);

            var result = await query.Select(a => new AttendanceDTO
            {
                AttendanceId = a.Id,
                MemberId = a.MemberId,
                MemberName = a.Member.Name,
                CheckInTime = a.CheckInTime.ToString("yyyy-MM-dd  hh:mm tt"),
            }).ToListAsync();

            return result;
        }
    }
}
