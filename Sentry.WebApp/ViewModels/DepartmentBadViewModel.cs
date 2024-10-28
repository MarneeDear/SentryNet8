//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;
//using Sentry.WebApp.Data;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Integrations.Core.Models;

//namespace Sentry.WebApp.ViewModels
//{
//	public class DepartmentBadViewModel : BaseViewModel
//	{
//        [Display(Name = "Department:")]
//        [Required(ErrorMessage = "Department Name is required.")]
//        //[StringLength(60, MinimumLength = 3)]
//        public string Name_Output { get; set; }

//        [Display(Name = "Dept Code:")]
//        [Required(ErrorMessage = "Dept Code is required.")]
//        //[StringLength(60, MinimumLength = 3)]
//        public string Code_Output { get; set; }

//        [Display(Name = "Organization:")]
//        [Required(ErrorMessage = "Organization is required.")]
//        //[StringLength(60, MinimumLength = 3)]
//        public string OrganizationName_Output { get; set; }

//        [Display(Name = "Org Code:")]
//        [Required(ErrorMessage = "Org Code is required.")]
//        //[StringLength(60, MinimumLength = 3)]
//        public string OrganizationCode_Output { get; set; }

//		public List<SelectListItem> OrganizationList { get; set; }

//		public List<SelectListItem> DepartmentList { get; set; }

//		public List<DepartmentBad> DepartmentsBadList { get; set; }

//		private AppDbContext _context;

//		public DepartmentBadViewModel()
//		{
//		}

//		public DepartmentBadViewModel(AppDbContext context)
//		{
//			_context = context;
//		}

//		public async Task<DepartmentBadViewModel> LoadDepartmentBadViewModelAsync(long? Id)
//		{
//			var db = await _context.DepartmentsBad.Where(e => e.Id == Id).OrderByDescending(e => e.CreatedOnDT).FirstOrDefaultAsync();
//			DepartmentBadViewModel model = JsonConvert.DeserializeObject<DepartmentBadViewModel>(JsonConvert.SerializeObject(db));
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