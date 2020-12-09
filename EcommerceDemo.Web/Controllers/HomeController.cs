using EcommerceDemo.Models.MappingConfiguration;
using EcommerceDemo.Models.Model;
using EcommerceDemo.Services.Interfaces;
using EcommerceDemo.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EcommerceDemo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductAttributeLookupService productAttributeLookupService;

        public HomeController(IProductService _productService
            , IProductCategoryService _productCategoryService
            , IProductAttributeLookupService _productAttributeLookupService)
        {
            productService = _productService;
            productCategoryService = _productCategoryService;
            productAttributeLookupService = _productAttributeLookupService;
        }

        [HttpGet]

        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> GetProductList(string name = null)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var sortColumn = "";
            var sortColumnDir = "ASC";

            var pageList = new PageList()
            {
                PageSize = 5,
                SortColumn = sortColumn,
                SortOrder = sortColumnDir.ToUpper(),
            };

            pageList.RecordStart = start != null ? Convert.ToInt32(start) : 0;

            ProductSearchModel searchModel = new ProductSearchModel();
            searchModel.pageList = pageList;
            searchModel.Name = name;

            var result = await productService.GetProducts(searchModel);
            long recordsTotal = 0;
            if (result.Any()) recordsTotal = result.FirstOrDefault().TotalCount;
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Add(int id = 0)
        {
            ProductModel model;

            if (id > 0)
            {
                var entity = await productService.GetProductById(id);
                model = entity.FirstOrDefault();
                model.ProductAttributes = await productService.GetProductAttributesById(id);
            }
            else
            {
                model = new ProductModel();
                model.ProductId = 0;
            }

            await BindCategories();
            await BindAttributes(model.ProdCatId);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await productService.SaveProduct(model);
                    if (result.Status)
                    {
                        SetNotification(result.Message, NotificationTypes.Success, "Product");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SetNotification(result.Message, NotificationTypes.Error, "Product Error!");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    SetNotification(ex.Message, NotificationTypes.Error, "Product Error!");
                }
            }

            await BindCategories();
            await BindAttributes(model.ProdCatId);

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> GetProductAttributes(int categoryId)
        {
            List<ProductAttributeLookupModel> attributeLookupList = (await productAttributeLookupService.GetProductAttributeLookups(categoryId)).ToList();

            return PartialView("_ProductAttribute", attributeLookupList);
        }

        [HttpPost]
        public async Task<ActionResult> GetAttribute(int categoryId)
        {
            await BindAttributes(categoryId);
            return PartialView("_Attribute", new ProductAttributeModel());
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(ProductModel model)
        {
            var result = await productService.DeleteProduct(model.ProductId);
            if (result.Status)
            {
                SetNotification(result.Message, NotificationTypes.Success, "Product Deleted");
                return RedirectToAction("Index");
            }
            else
            {
                SetNotification(result.Message, NotificationTypes.Error, "Product Delete Error!");
            }
            return View(result);
        }

        #region Private Method

        private async Task<bool> BindCategories()
        {
            var categoryList = new List<SelectListItem>() { new SelectListItem() { Text = "Select Category", Value = "", } };
            categoryList.AddRange(ConvertModelToSelectListItem(await productCategoryService.GetCategories()));
            ViewBag.CategoryList = categoryList;
            return true;
        }

        private async Task<bool> BindAttributes(int categoryId)
        {
            var attributes = new List<SelectListItem>() { new SelectListItem() { Text = "Select Attribute", Value = "", } };
            foreach (var item in await productAttributeLookupService.GetProductAttributeLookups(categoryId))
            {
                attributes.Add(new SelectListItem() { Text = item.AttributeName, Value = item.AttributeId.ToString() });
            }
            ViewBag.Attributes = attributes;
            return true;
        }

        private static List<SelectListItem> ConvertModelToSelectListItem(IEnumerable<ComboBoxModel> items)
        {
            var list = new List<SelectListItem>();

            foreach (var item in items)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                });
            }

            return list;
        }

        #endregion
    }
}