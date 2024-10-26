//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;
//using Sentry.WebApp.Data.Models;
//using Sentry.WebApp.Data;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using System;

//namespace Sentry.WebApp.ViewModels
//{
//	public class OrganizationBadViewModel : BaseViewModel
//	{
//		public string Name_Output { get; set; }
//		public string Code_Output { get; set; }
//		public string OrganizationName_Output { get; set; }
//		public string OrganizationCode_Output { get; set; }
//		public List<SelectListItem> OrganizationList { get; set; }
//		public List<SelectListItem> DepartmentList { get; set; }
//        public List<DepartmentBad> DepartmentsBadList { get; set; }

//        private AppDbContext _context;

//		public OrganizationBadViewModel()
//		{
//		}

//		public OrganizationBadViewModel(AppDbContext context)
//		{
//			_context = context;
//		}

//		public async Task<OrganizationBadViewModel> LoadDepartmentBadViewModelAsync(long? Id)
//		{
//			var db = await _context.DepartmentsBad.Where(e => e.Id == Id).OrderByDescending(e => e.CreatedOnDT).FirstOrDefaultAsync();
//			OrganizationBadViewModel model = JsonConvert.DeserializeObject<OrganizationBadViewModel>(JsonConvert.SerializeObject(db));
//			model._context = _context;
//			return model;
//		}

//		public async Task UpdateDepartmentAsync(AppDbContext context)
//		{
//			_context = context;
//			DepartmentBadChanged model = JsonConvert.DeserializeObject<DepartmentBadChanged>(JsonConvert.SerializeObject(this));
//			//model.CreatedOnDT = DateTime.Now;
//			_context.Add(model);
//			await _context.SaveChangesAsync();
//		}
//		public async Task LoadOrganizationList()
//		{
//			OrganizationList = (await SelectLists.GetOrganizationListAsync(_context)).ToList();
//		}
//		public async Task LoadDepartmentList()
//		{
//			DepartmentList = (await SelectLists.GetDepartmentListAsync(_context)).ToList();
//		}

//		public bool DepartmentExists(long id)
//		{
//			return _context.DepartmentsBad.Any(e => e.Id == id);
//		}
//	}

//}