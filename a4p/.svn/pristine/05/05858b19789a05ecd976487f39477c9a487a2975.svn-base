using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Calender;
using Model;
using System.Collections.Generic;
using ADOPets.Web.Common.Helpers;
using System.Globalization;


namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class CalenderController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var UserId = HttpContext.User.GetUserId();
            var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == UserId).OrderBy(c => c.Date).Select(c => new IndexViewModel(c));
            return View(calendar);
        }

        [HttpGet]
        public ActionResult IndexStatus(int id)
        {
            var reminder = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == id);
            reminder.IsRead = true;
            UnitOfWork.CalendarRepository.Update(reminder);
            UnitOfWork.Save();

            var UserId = HttpContext.User.GetUserId();
            var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == UserId).OrderBy(c => c.Date).Select(c => new IndexViewModel(c));
            return View("Index", calendar);
        }

        [HttpGet]
        public ActionResult Details()
        {
            var userId = HttpContext.User.GetUserId();
            // var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == userId && c.Date > DateTime.Now).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));

            var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == userId).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            return View(calendar);
        }

        [HttpGet]
        public ActionResult GetUpcomming()
        {
            var userId = HttpContext.User.GetUserId();
            var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == userId && c.Date > DateTime.Now).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            return View(calendar);
        }

        [HttpGet]
        public ActionResult List(bool? showAll)
        {
            var UserId = HttpContext.User.GetUserId();
            var calender = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == UserId);
            IEnumerable<DetailsViewModel> lst = null;
            lst = calender.Select(c => new DetailsViewModel(c)).OrderBy(c => c.Date);
            TempData["showAll"] = true;
            return PartialView("_List", lst);
        }

        [HttpGet]
        public ActionResult Calendar()
        {
            var UserId = HttpContext.User.GetUserId();
            var calendar = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == UserId).Select(c => new IndexViewModel(c));
            return PartialView("_Calendar", calendar);
        }

        [HttpGet]
        public ActionResult PopupOver(int Id)
        {
            var popup = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == Id);
            return PartialView("_PopupOver", new DetailsViewModel(popup));
        }

        [HttpGet]
        public ActionResult Add(string redirectToUrl, DateTime? clickDate)
        {
            var UserId = HttpContext.User.GetUserId();
            return PartialView("_Add", new AddViewModel(UserId, redirectToUrl, clickDate));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                DomainTypeEnum domainCulture = DomainHelper.GetDomain();
                Model.Calendar reminder = null;
                if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
                {
                    reminder = model.Map();
                }
                else if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-IN")
                {
                    reminder = model.MapIn();
                }
                else
                {
                    reminder = model.MapFr();
                }

                if (DateTime.Compare(model.Date.Value.Date, DateTime.Now.Date) == -1)
                {
                    reminder.IsRead = true;
                }
                UnitOfWork.CalendarRepository.Insert(reminder);
                UnitOfWork.Save();
                Success(Resources.Wording.Calendar_Add_AddSuccessMessage);
                return RedirectToAction(model.RedirectToActionName);
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int reminderId, string redirectToUrl)
        {
            var reminder = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == reminderId);
            return PartialView("_Edit", new EditViewModel(reminder, redirectToUrl));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reminder = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == model.Id);
                if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-US")
                {
                    model.Map(reminder);
                }
                else if (System.Globalization.CultureInfo.CurrentCulture.Name == "en-IN")
                {
                    model.MapIn(reminder);
                }
                else
                {
                    model.MapFr(reminder);
                }

                if (DateTime.Compare(reminder.Date.Value.Date, DateTime.Now.Date) == -1)
                {
                    reminder.IsRead = true;

                }
                UnitOfWork.CalendarRepository.Update(reminder);
                UnitOfWork.Save();
                Success(Resources.Wording.Calendar_Edit_EditSuccessMessage);
                if (model.RedirectToActionName == "List")
                {
                    return RedirectToAction(model.RedirectToActionName, new { showAll = TempData["showAll"] });
                }
                else { return RedirectToAction(model.RedirectToActionName); }
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult SearchReminder(string StartDate, string EndDate)
        {
            var UserId = HttpContext.User.GetUserId();
            var calender = UnitOfWork.CalendarRepository.GetAll(c => c.UserId == UserId);
            IEnumerable<DetailsViewModel> lst = null;

            DateTime? StartDate1 = (!string.IsNullOrEmpty(StartDate)) ? (DateTime?)Convert.ToDateTime(StartDate, CultureInfo.CurrentCulture) : null;
            DateTime? EndDate1 = (!string.IsNullOrEmpty(EndDate)) ? (DateTime?)Convert.ToDateTime(EndDate, CultureInfo.CurrentCulture) : null;


            if (StartDate1 != null && EndDate1 != null)
            {
                lst = calender.Where(c => c.Date >= StartDate1 && Convert.ToDateTime(c.Date.Value.ToShortDateString()) <= EndDate1).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            }
            else if (StartDate1 != null && EndDate1 == null)
            {
                var maxDate = calender.Select(c => c.Date).Max();
                lst = calender.Where(c => c.Date >= StartDate1 && c.Date <= maxDate).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            }
            else if (StartDate1 == null && EndDate1 != null)
            {
                var minDate = calender.Select(c => c.Date).Min();
                lst = calender.Where(c => c.Date >= minDate && Convert.ToDateTime(c.Date.Value.ToShortDateString()) <= EndDate1).OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            }
            else
            {
                lst = calender.OrderBy(c => c.Date).Select(c => new DetailsViewModel(c));
            }

            return PartialView("_List", lst);

        }

        [HttpGet]
        public ActionResult DeleteConfirm(int reminderId)
        {
            var calender = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == reminderId);
            return PartialView("_Delete", new DeleteViewModel(calender));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            var calender = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == Id);
            UnitOfWork.CalendarRepository.Delete(calender);
            UnitOfWork.Save();
            Success(Resources.Wording.Calendar_Delete_DeleteSuccessMessage);
            return RedirectToAction("List");
        }


        public ActionResult CalenderNotification()
        {
            var userId = HttpContext.User.GetUserId();

            DateTime dt = TimeZoneHelper.GetCurrentUserLocalTime();
            var newreminder = UnitOfWork.CalendarRepository.GetAll(r => r.UserId == userId, reminder => reminder.OrderBy(i => i.Date)).Where(i => i.Date.Value >= dt)
                                .Select(r => new CalenderNotificationViewModel(r.Id.ToString(), r.Reason, r.UserId, r.Date));

            return PartialView("_CalenderNotification", newreminder.Take(2));
        }
    }
}
