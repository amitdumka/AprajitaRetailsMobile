using System; 
using AprajitaRetails.Mobile.DataModels.Base;
using AprajitaRetails.Mobile.Operations.Prefernces;
using AprajitaRetails.Shared.AutoMapper.DTO;
using AprajitaRetails.Shared.Models.Payroll;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Mobile.DataModels.Payroll
{
    public class AttendanceDataModel : BaseDM<AttendanceDTO>// BaseDataModel<Attendance, Employee, MonthlyAttendance>
    {
        private bool _localSynced;
        private DateTime _syncTime;

        public AttendanceDataModel() : base()
        {
           
            apiurl = "Attendances";
            apiDtoURL = $"Attendances/bystoredto?storeid={CurrentSession.StoreCode}";
        }
        public AttendanceDataModel(ConType conType) : base(conType)
        {
        }

        public AttendanceDataModel(ConType conType, RolePermission role) : base(conType, role)
        {
        }

        public override Task<string> GenrateID()
        {
            throw new NotImplementedException();
        }

        

        public override async Task<List<AttendanceDTO>> GetItemsAsync(string storeid)
        {
            throw new NotImplementedException();
            //if (!_localSynced)
            //{
            //    var db = GetContextAzure();
            //  var atts= await db.Attendances.Where(c => c.StoreId == storeid && c.OnDate.Date == DateTime.Today.Date).ToListAsync();
            //    SaveAllAsync(atts);
            //    _syncTime = DateTime.Now;
            //    return atts;
            //}else if (_syncTime.AddHours(1)>DateTime.Now)
            //{
            //    var db = GetContextAzure();
            //    var atts = await db.Attendances.Where(c => c.StoreId == storeid && c.OnDate.Date == DateTime.Today.Date).ToListAsync();
            //    AddOrUpdateOrDiscardAsync(atts);
            //    return atts;
            //}
            //else
            //{
            //    var db = GetContextLocal();
            //   return await db.Attendances.Where(c => c.StoreId == storeid && c.OnDate.Date == DateTime.Today.Date).ToListAsync();
            //}
        }
        public async Task<List<Attendance>> AddOrUpdateOrDiscardAsync(List<Attendance> atts)
        {
            var db = GetContextLocal();
            
            foreach (var item in atts)
            {
                if (db.Attendances.Any(c => c.AttendanceId == item.AttendanceId))
                {
                    if (item.EntryStatus == EntryStatus.Updated)
                    {
                        db.Attendances.Update(item);
                    }
                    else
                    {
                        atts.Remove(item);
                    }
                }
                else
                {
                    db.Attendances.AddAsync(item);
                }
            }
            int save = await db.SaveChangesAsync();
            if (save > 0)
            {
                return atts;
            }
            else return null;
        }
        public override  List<int> GetYearList(string storeid)
        {
           return  GetContext().Attendances.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToList();
        }

        public override  List<int> GetYearList()
        {
            return  GetContext().Attendances.Select(c => c.OnDate.Year).Distinct().ToList();
        }

        //public override Task<List<int>> GetYearListY(string storeid)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<List<int>> GetYearListY()
        //{
        //    throw new NotImplementedException();
        //}

        //public override async Task<List<int>> GetYearListZ(string storeid)
        //{
        //    return await GetContext().MonthlyAttendances.Where(c => c.StoreId == storeid).Select(c => c.OnDate.Year).Distinct().ToListAsync();
        //}

        //public override async Task<List<int>> GetYearListZ()
        //{
        //    return await GetContext().MonthlyAttendances.Select(c => c.OnDate.Year).Distinct().ToListAsync();
        //}

        //public override Task<List<Employee>> GetYFiltered(QueryParam query)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<List<Employee>> GetYItems(string storeid)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<List<MonthlyAttendance>> GetZFiltered(QueryParam query)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<List<MonthlyAttendance>> GetZItems(string storeid)
        //{
        //    return GetContext().MonthlyAttendances.Where(c => c.StoreId == storeid && c.OnDate.Year == DateTime.Today.Year).OrderByDescending(c => c.OnDate).ToListAsync();
        //}

        public override async Task<bool> InitContext()
        {
            return Connect();
        }


        public void SyncUp(Attendance att, bool isNew=true, bool delete=false)
        {
            var db = GetContextAzure();
            if (delete)
                db.Attendances.Remove(att);
            else
            {
                if (isNew)
                    db.Attendances.Add(att);
                else
                    db.Attendances.Update(att);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }
        public void SyncUp(MonthlyAttendance att, bool isNew = true, bool delete = false)
        {
            var db = GetContextAzure();
            if (delete)
                db.MonthlyAttendances.Remove(att);
            else
            {
                if (isNew)
                    db.MonthlyAttendances.Add(att);
                else
                    db.MonthlyAttendances.Update(att);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }
        public void SyncUp(Employee emp, bool isNew = true, bool delete = false)
        {
            var db = GetContextAzure();
            if (delete)
                db.Employees.Remove(emp);
            else
            {
                if (isNew)
                    db.Employees.Add(emp);
                else
                    db.Employees.Update(emp);
            }
            var x = db.SaveChanges();
            if (x > 0)
            {
                Console.WriteLine("c");
            }
            else
                Console.WriteLine("e");
        }

        public override List<AttendanceDTO> GetFiltered(QueryParam query)
        {
            throw new NotImplementedException();
        }
    }
}

